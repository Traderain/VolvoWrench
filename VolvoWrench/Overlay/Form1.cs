using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IniParser;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using VolvoWrench.Demo_Stuff;
using VolvoWrench.Demo_Stuff.GoldSource;
using VolvoWrench.Hotkey;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.Direct2D1.Factory;
using Font = System.Drawing.Font;
using FontFactory = SharpDX.DirectWrite.Factory;
using FontStyle = SharpDX.DirectWrite.FontStyle;
using TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode;
using Timer = System.Windows.Forms.Timer;

namespace VolvoWrench.Overlay
{
    /// <summary>
    /// This is a Directx9 Invisible form which is used for drawing an overlay
    /// </summary>
    public partial class OverlayForm : Form
    {
        /// <summary>
        /// Path of the demo file
        /// </summary>
        public static string FilePath = "";

        /// <summary>
        /// VKEY of the key used to force update the overlay
        /// </summary>
        public static int RescanKey;

        /// <summary>
        /// Key to exit the overlay
        /// </summary>
        public static int ExitKey;

        /// <summary>
        /// Font of the overlay
        /// </summary>
        public static Font TextFont;

        /// <summary>
        /// Color of the overlay text
        /// </summary>
        public static Color TextColor;

        /// <summary>
        /// Name of the current window, we use this to check if hl2 etc. is focused.
        /// </summary>
        public static string Currentwindow = "";

        /// <summary>
        /// This is what is printed.
        /// </summary>
        public static string Demodata = "";


        /// <summary>
        /// This is what checks the demofolder periodically for change
        /// </summary>
        public BackgroundWorker DemoParserSlave;

        /// <summary>
        /// Refresh rate of the backgroundworker
        /// </summary>
        private const int DirectoryScannerRefreshRate = 500;

        /// <summary>
        /// Window names to check for focus
        /// </summary>
        public static string[] GameTitles = new[]
        {
            "HALF-LIFE 2",
            "PORTAL",
            "PORTAL 2"
        };

        /// <summary>
        /// The Source demo overlay settings
        /// </summary>
        public static Sourceoverlaysettings Sos;
        /// <summary>
        /// The HLS:OOE overlay settings
        /// </summary>
        public static Hlsooeoverlaysettings Hos;
        /// <summary>
        /// The L4D2 Branch overlay settings
        /// </summary>
        public static L4D2Branchoverlaysettings Los;
        /// <summary>
        /// The GoldSource overlay settings
        /// </summary>
        public static Goldsourceoverlaysettings Gos;

        /// <summary>
        /// Determines if the overlay form should be running
        /// </summary>
        public static bool Shouldrun = true;

        private WindowRenderTarget _device;
        private HwndRenderTargetProperties _renderProperties;
        private SolidColorBrush _solidColorBrush;
        private Factory _factory;

        private FontFactory _fontFactory;
        readonly FontConverter _cvt = new FontConverter();
        readonly FileIniDataParser _parser = new FileIniDataParser();

        private Timer _timer1;
        private Thread _sDx;

        #region  DllImports
        /// <summary>
        /// Set window size Pinvoke
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Gets the size of a window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Sets the position of a window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        /// <summary>
        /// Maximizes a window
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="pMargins"></param>
        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        #endregion 

        /// <summary>
        /// Noresize flag
        /// </summary>
        public const uint SwpNosize = 0x0001;
        /// <summary>
        /// Nomove flag
        /// </summary>
        public const uint SwpNomove = 0x0002;
        /// <summary>
        /// Topmost flag
        /// </summary>
        public const uint TopmostFlags = SwpNomove | SwpNosize;
        /// <summary>
        /// Flag for being the topmost window
        /// </summary>
        public static IntPtr HwndTopmost = new IntPtr(-1);

        /// <summary>
        /// The constructor of the overlay form
        /// </summary>
        /// <param name="file"> Path to the demo file</param>
        public OverlayForm(string file)
        {
            InitializeComponent();
            var initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(Handle, -20, initialStyle | 0x80000 | 0x20);
            SetWindowPos(Handle, HwndTopmost, 0, 0, 0, 0, TopmostFlags);
            OnResize(null);
            TopMost = true;
            #region Settings Load
            var iniD = _parser.ReadFile(Main.SettingsPath);
            TextFont = _cvt.ConvertFromString(iniD["SETTINGS"]["overlay_font"]) as Font;
            var colorstring = iniD["SETTINGS"]["overlay_color"].Split(':');
            TextColor = Color.FromArgb(
                Convert.ToInt32(colorstring[0]),
                Convert.ToInt32(colorstring[1]),
                Convert.ToInt32(colorstring[2]),
                Convert.ToInt32(colorstring[3]));
            RescanKey = Convert.ToInt32(iniD["HOTKEYS"]["overlay_rescan"], 16);
            ExitKey = Convert.ToInt32(iniD["HOTKEYS"]["overlay_exit"], 16);
            //SOURCE OVERLAY_SOURCE
            /* object[] settings = { Sos = new OVERLAY_SOURCE(),Hos = new OVERLAY_HLSOOE(),Los = new OVERLAY_L4D2BRANCH(),Gos = new OVERLAY_GOLDSOURCE()};
            foreach (var obj in settings)
            {
                var objType = obj.GetType();
                var fields = objType.GetFields();
                foreach (var field in fields)
                {
                    var piInstance = objType.GetProperty(field.Name);
                    piInstance.SetValue(obj, Convert.ToBoolean(int.Parse(iniD[objType.Name][field.Name])));
                }

            }*/
            Sos.DemoProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["demo_protocol"]));
            Sos.NetProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["net_protocol"]));
            Sos.ServerName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["server_name"]));
            Sos.ClientName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["client_name"]));
            Sos.MapName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["map_name"]));
            Sos.GameDirectory = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["game_directory"]));
            Sos.MeasuredTime = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["measured_time"]));
            Sos.MeasuredTicks = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["measured_ticks"]));
            Sos.SaveFlag = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["save_flag"]));
            Sos.AutosaveFlag = Convert.ToBoolean(int.Parse(iniD["OVERLAY_SOURCE"]["autosave_flag"]));
            //HLSOOE OVERLAY_HLSOOE
            Hos.DemoProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["demo_protocol"]));
            Hos.NetProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["net_protocol"]));
            Hos.ServerName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["server_name"]));
            Hos.ClientName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["client_name"]));
            Hos.MapName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["map_name"]));
            Hos.GameDirectory = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["game_directory"]));
            Hos.MeasuredTime = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["measured_time"]));
            Hos.MeasuredTime = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["measured_ticks"]));
            Hos.SaveFlag = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["save_flag"]));
            Hos.AutosaveFlag = Convert.ToBoolean(int.Parse(iniD["OVERLAY_HLSOOE"]["autosave_flag"]));
            //L4D2 Branch OVERLAY_L4D2BRANCH
            Los.DemoProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["demo_protocol"]));
            Los.NetProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["net_protocol"]));
            Los.ServerName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["server_name"]));
            Los.ClientName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["client_name"]));
            Los.MapName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["map_name"]));
            Los.GameDirectory = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["game_directory"]));
            Los.MeasuredTime = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["measured_time"]));
            Los.MeasuredTicks = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["measured_ticks"]));
            Los.SaveFlag = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["save_flag"]));
            Los.AutosaveFlag = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["autosave_flag"]));
            Los.AdjustedTime = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["adjusted_time"]));
            Los.AdjustedTicks = Convert.ToBoolean(int.Parse(iniD["OVERLAY_L4D2BRANCH"]["adjusted_ticks"]));
            //GOLDSOURCE OVERLAY_GOLDSOURCE
            Gos.DemoProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["demo_protocol"]));
            Gos.NetProtocol = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["net_protocol"]));
            Gos.ServerName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["server_name"]));
            Gos.ClientName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["client_name"]));
            Gos.MapName = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["map_name"]));
            Gos.GameDirectory = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["game_directory"]));
            Gos.MeasuredTime = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["measured_time"]));
            Gos.MeasuredTicks = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["measured_ticks"]));
            Gos.HighestFps = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["highest_fps"]));
            Gos.LowestFps = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["lowest_fps"]));
            Gos.AverageFps = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["average_fps"]));
            Gos.LowestFps = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["lowest_msec"]));
            Gos.HighestMsec = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["highest_msec"]));
            Gos.AverageMsec = Convert.ToBoolean(int.Parse(iniD["OVERLAY_GOLDSOURCE"]["average_msec"]));
            #endregion
            _timer1.Interval = 300;
            _timer1.Enabled = true;
            _timer1.Start();
            FilePath = file;

            if (new FileInfo(FilePath).Length > 540)
            {
                var cpr = CrossDemoParser.Parse(FilePath);
                PrintOverlayData(cpr);
            }
            else
            {
                Demodata = "";
            }

            DemoParserSlave.RunWorkerAsync(FilePath);
        }


        #region DX init stuff
        protected override sealed void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |// this reduce the flicker
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.Opaque |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            TopMost = true;
            Visible = true;

            _factory = new Factory();
            _fontFactory = new FontFactory();
            _renderProperties = new HwndRenderTargetProperties()
            {
                Hwnd = Handle,
                PixelSize = new Size2(1920, 1080),
                PresentOptions = PresentOptions.None
            };
            _device = new WindowRenderTarget(_factory, new RenderTargetProperties(new PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)), _renderProperties);

            _solidColorBrush = new SolidColorBrush(_device, new RawColor4(TextColor.R, TextColor.G, TextColor.B, TextColor.A));

            _sDx = new Thread(SDxThread)
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };

            _sDx.Start();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var marg = new[] { 0, 0, Width, Height };
            DwmExtendFrameIntoClientArea(Handle, ref marg);
        }
        #endregion

        private void hotkeytimer_Tick(object sender, EventArgs e)
        {
            Currentwindow = GetActiveWindowTitle() ?? "";
            var exitstate = KeyInputApi.GetKeyState(ExitKey);
            var rstate = KeyInputApi.GetKeyState(RescanKey);
            if ((exitstate & 0x8000) != 0)
            {
                Close();
            }
            if ((rstate & 0x8000) != 0)
            {
                if (File.Exists(FilePath) && Path.GetExtension(FilePath) == ".dem" && FilePath != null)
                {
                    var cpr = CrossDemoParser.Parse(FilePath);
                    PrintOverlayData(cpr);
                }
            }
        }

        /// <summary>
        /// This is the thread where we draw
        /// </summary>
        /// <param name="sender"></param>
        private void SDxThread(object sender)
        {
            Main.Alert("Overlay launched!");
            while (Shouldrun)
            {
                _device.BeginDraw();
                _device.Clear(new RawColor4(Color.Transparent.R, Color.Transparent.G, Color.Transparent.B, Color.Transparent.A));
                _device.TextAntialiasMode = TextAntialiasMode.Default;
                using (var g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    if (!string.IsNullOrEmpty(Currentwindow))
                    {
                        if (GameTitles.Any(x=> Currentwindow.ToUpper().Contains(x)))
                        {
                            _device.DrawText(Demodata,
                                new TextFormat(_fontFactory,
                                    TextFont.FontFamily.Name,
                                    FontWeight.Normal,
                                    FontStyle.Normal,
                                    TextFont.Size),
                                new RawRectangleF(0, 0,
                                    (int)g.MeasureString(Demodata, TextFont).Width + 5,
                                    (int)g.MeasureString(Demodata, TextFont).Height + 5),
                                    _solidColorBrush);
                        }
                    }
                }
                _device.EndDraw();
            }
        }

        /// <summary>
        /// This is where we check the settings and add the data to the string that will be printed
        /// </summary>
        /// <param name="demo"></param>
        public static void PrintOverlayData(CrossParseResult demo)
        {
            Demodata = "Parsed file!";
            #region Print
            switch (demo.Type)
            {
                case Parseresult.UnsupportedFile:
                    Demodata = @"Unsupported file!";
                    break;
                case Parseresult.GoldSource:
                    if (demo.GsDemoInfo.ParsingErrors.ToArray().Length > 0)
                    {
                        Demodata = "Error while parsing goldsource demo: \n";
                        foreach (var err in demo.GsDemoInfo.ParsingErrors)
                        {
                            Demodata += ("\n" + err);
                        }

                    }
                    else
                    {
                        float frametimeMin = 0f, frametimeMax = 0f;
                        var frametimeSum = 0.0;
                        var count = 0;
                        int msecMin = 0, msecMax = 0;
                        long msecSum = 0;
                        var first = true;
                        foreach (var f in from entry in demo.GsDemoInfo.DirectoryEntries from frame in entry.Frames where (int)frame.Key.Type < 2 || (int)frame.Key.Type > 9 select (GoldSource.NetMsgFrame)frame.Value)
                        {
                            frametimeSum += f.RParms.Frametime;
                            msecSum += f.UCmd.Msec;
                            count++;

                            if (first)
                            {
                                first = false;
                                frametimeMin = f.RParms.Frametime;
                                frametimeMax = f.RParms.Frametime;
                                msecMin = f.UCmd.Msec;
                                msecMax = f.UCmd.Msec;
                            }
                            else
                            {
                                frametimeMin = Math.Min(frametimeMin, f.RParms.Frametime);
                                frametimeMax = Math.Max(frametimeMax, f.RParms.Frametime);
                                msecMin = Math.Min(msecMin, f.UCmd.Msec);
                                msecMax = Math.Max(msecMax, f.UCmd.Msec);
                            }
                        }
                        if (Gos.DemoProtocol)
                            Demodata += $"\nDemo protocol:              {demo.GsDemoInfo.Header.DemoProtocol}";
                        if (Gos.NetProtocol)
                            Demodata += $"\nNet protocol:               {demo.GsDemoInfo.Header.NetProtocol}";
                        if (Gos.MapName)
                            Demodata += $"\nMap name:                   {demo.GsDemoInfo.Header.MapName}";
                        if (Gos.GameDirectory)
                            Demodata += $"\nGame directory:             {demo.GsDemoInfo.Header.GameDir}";
                        if (Gos.MeasuredTime)
                            Demodata += $"\nLength in seconds:          {demo.GsDemoInfo.DirectoryEntries.Sum(x => x.TrackTime).ToString("n3")}s";
                        if (Gos.MeasuredTicks)
                            Demodata += $"\nFrame count:                {demo.GsDemoInfo.DirectoryEntries.Sum(x => x.FrameCount)}";
                        if (Gos.HighestFps)
                            Demodata += $"\nHigest FPS:                 {(1 / frametimeMin).ToString("N2")}";
                        if (Gos.LowestFps)
                            Demodata += $"\nLowest FPS:                 {(1 / frametimeMax).ToString("N2")}";
                        if (Gos.AverageFps)
                            Demodata += $"\nAverage FPS:                {(count / frametimeSum).ToString("N2")}";
                        if (Gos.LowestMsec)
                            Demodata += $"\nLowest msec:                {(1000.0 / msecMax).ToString("N2")} FPS";
                        if (Gos.HighestMsec)
                            Demodata += $"\nHighest msec:               {(1000.0 / msecMin).ToString("N2")} FPS";
                        if (Gos.AverageMsec)
                            Demodata += $"\nAverage msec:               {(1000.0 / (msecSum / (double)count)).ToString("N2")} FPS";
                    }
                    break;
                case Parseresult.Hlsooe:
                    if (demo.HlsooeDemoInfo.ParsingErrors.ToArray().Length > 0)
                    {
                        Demodata = @"Error while parsing HLSOOE demo: 
";
                        foreach (var err in demo.HlsooeDemoInfo.ParsingErrors)
                        {
                            Demodata += ("\n" + err);
                        }
                    }
                    else
                    {
                        if (Hos.DemoProtocol)
                            Demodata += $"\nDemo protocol:              {demo.HlsooeDemoInfo.Header.DemoProtocol}";
                        if (Hos.NetProtocol)
                            Demodata += $"\nNet protocol:               {demo.HlsooeDemoInfo.Header.NetProtocol}";
                        if (Hos.MapName)
                            Demodata += $"\nMap name:                   {demo.HlsooeDemoInfo.Header.MapName}";
                        if (Hos.GameDirectory)
                            Demodata += $"\nGame directory:             {demo.HlsooeDemoInfo.Header.GameDir}";
                        if (Hos.MeasuredTime)
                            Demodata += $"\nLength in seconds:          {(demo.HlsooeDemoInfo.DirectoryEntries.Last().Frames.LastOrDefault().Key.Frame) * 0.015}s";
                        if (Hos.MeasuredTicks)
                            Demodata += $"\nTick count:                 {(demo.HlsooeDemoInfo.DirectoryEntries.Last().Frames.LastOrDefault().Key.Frame)}";
                        foreach (var flag in demo.HlsooeDemoInfo.DirectoryEntries.SelectMany(demoDirectoryEntry => demoDirectoryEntry.Flags))
                            Demodata += (flag.Value.Command + " at " + flag.Key.Frame + " -> " + (flag.Key.Frame * 0.015).ToString("n3") + "s");
                    }
                    break;
                case Parseresult.Source:
                    if (demo.Sdi.ParsingErrors.ToArray().Length > 0)
                    {
                        Demodata = @"Error while parsing Source engine demo: ";
                        foreach (var err in demo.Sdi.ParsingErrors)
                        {
                            Demodata += ("\n" + err);
                        }
                    }
                    else
                    {
                        if (Sos.DemoProtocol)
                            Demodata += $"\nDemo protocol:              {demo.Sdi.DemoProtocol}";
                        if (Sos.ServerName)
                            Demodata += $"\nServer name:                {demo.Sdi.ServerName}";
                        if (Sos.ClientName)
                            Demodata += $"\nClient name:                {demo.Sdi.ClientName}";
                        if (Sos.MapName)
                            Demodata += $"\nMap name:                   {demo.Sdi.MapName}";
                        if (Sos.MeasuredTime)
                            Demodata += $"\nMeasured time:              {(demo.Sdi.Messages.Max(x => x.Tick) * 0.015).ToString("n3")}s";
                        if (Sos.MeasuredTicks)
                            Demodata += $"\nMeasured ticks:             {demo.Sdi.Messages.Max(x => x.Tick)}";
                        foreach (var f in demo.Sdi.Flags)
                            switch (f.Name)
                            {
                                case "#SAVE#":
                                    if(Sos.SaveFlag)
                                    Demodata += ($"\n#SAVE# flag at Tick: {f.Tick} -> {f.Time}s");
                                    break;
                                case "autosave":
                                    if(Sos.AutosaveFlag)
                                    Demodata += ($"\nAutosave at Tick: {f.Tick} -> {f.Time}s");
                                    break;
                            }
                    }
                    break;
                case Parseresult.Portal:
                case Parseresult.L4D2Branch:
                    if (demo.L4D2BranchInfo.Parsingerrors.ToArray().Length > 0)
                    {
                        Demodata = @"Error while parsing L4D2Branch demo: 
";
                        foreach (var err in demo.L4D2BranchInfo.Parsingerrors)
                        {
                            Demodata += ("\n" + err);
                        }
                    }
                    else
                    {
                        if (Los.DemoProtocol)
                            Demodata += $"\nProtocol:           {demo.L4D2BranchInfo.Header.Protocol}";
                        if (Los.NetProtocol)
                            Demodata += $"\nNetwork protocol:   {demo.L4D2BranchInfo.Header.NetworkProtocol}";
                        if (Los.ServerName)
                            Demodata += $"\nServer name:        {demo.L4D2BranchInfo.Header.ServerName}";
                        if (Los.ClientName)
                            Demodata += $"\nClient name:        {demo.L4D2BranchInfo.Header.ClientName}";
                        if (Los.MapName)
                            Demodata += $"\nMapname:            {demo.L4D2BranchInfo.Header.MapName}";
                        if (Los.GameDirectory)
                            Demodata += $"\nGameDir:            {demo.L4D2BranchInfo.Header.GameDirectory}";
                        if (Los.MeasuredTime)
                            Demodata += $"\nPlaybacktime:       {(demo.L4D2BranchInfo.Header.PlaybackTicks * 0.015).ToString("n3")}s";
                        if (Los.MeasuredTicks)
                            Demodata += $"\nPlaybackticks:      {demo.L4D2BranchInfo.Header.PlaybackTicks}";
                        if (Los.AdjustedTicks)
                            Demodata += $"\nAdjusted ticks:     {demo.L4D2BranchInfo.PortalDemoInfo?.AdjustedTicks}";
                        if (Los.AdjustedTime)
                            Demodata += $"\nAdjusted time:      {demo.L4D2BranchInfo.PortalDemoInfo?.AdjustedTicks * 0.015 + "s"}";
                        if (Los.DemoProtocol)
                            Demodata += $"\nProtocol:           {demo.L4D2BranchInfo.Header.Protocol}";
                        if (Los.DemoProtocol)
                            Demodata += $"\nProtocol:           {demo.L4D2BranchInfo.Header.Protocol}";
                    }
                    break;
            }
            #endregion
        }

        private static string GetActiveWindowTitle()
        {
            
            const int nChars = 256;
            var buff = new StringBuilder(nChars);
            var handle = GetForegroundWindow();
            if (GetWindowText(handle, buff, nChars) > 0)
            {
                return buff.ToString();
            }
            return "";
        }

        #region Background worker methods
        private static void MonitorDemo(BackgroundWorker worker, string demo)
        {
            Thread.Sleep(DirectoryScannerRefreshRate);
            while (!worker.CancellationPending)
            {
                try
                {
                        var demoParseResult = CrossDemoParser.Parse(demo);                        
                            worker.ReportProgress(0, demoParseResult);
                            return;
                }
                catch
                {
                    //demo still being written to
                }
                Thread.Sleep(DirectoryScannerRefreshRate);
            }
        }

        private void DirectoryScannerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DemoParserSlave.RunWorkerAsync(FilePath);
        }

        private void DirectoryScannerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var startTime = DateTime.Now;

            while (worker != null && !worker.CancellationPending)
            {
                    var writeTime = File.GetLastWriteTime(FilePath);
                    if (writeTime.CompareTo(startTime) > 0)
                    {
                        Thread.Sleep(10);
                        startTime = DateTime.Now;
                        if (new FileInfo(FilePath).Length > 540 &&
                            new FileInfo(FilePath).Length != 237568)
                        {
                             worker.ReportProgress(0, Path.GetFileName(FilePath));
                             MonitorDemo(worker, FilePath);
                             worker.ReportProgress(0, null);
                             break;
                        }
                    }
                    Thread.Sleep(1);
            }
        }

        private void DirectoryScannerWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var crossParseResult = e.UserState as CrossParseResult;
            if (crossParseResult != null)
                PrintOverlayData(crossParseResult);
        }

        /// <summary>
        /// Checks if we can use the file eg.: if the game is still writing to it
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        protected virtual bool IsFileLocked(string f)
        {
            var file = new FileInfo(f);
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }
            return false;
        }
        #endregion
    }

    public struct Sourceoverlaysettings
    {
        public bool DemoProtocol;
        public bool NetProtocol;
        public bool ServerName;
        public bool ClientName;
        public bool MapName;
        public bool GameDirectory;
        public bool MeasuredTime;
        public bool MeasuredTicks;
        public bool SaveFlag;
        public bool AutosaveFlag;
    }

    public struct Hlsooeoverlaysettings
    {
        public bool DemoProtocol;
        public bool NetProtocol;
        public bool ServerName;
        public bool ClientName;
        public bool MapName;
        public bool GameDirectory;
        public bool MeasuredTime;
        public bool MeasuredTicks;
        public bool SaveFlag;
        public bool AutosaveFlag;
    }

    public struct L4D2Branchoverlaysettings
    {
        public bool DemoProtocol;
        public bool NetProtocol;
        public bool ServerName;
        public bool ClientName;
        public bool MapName;
        public bool GameDirectory;
        public bool MeasuredTime;
        public bool MeasuredTicks;
        public bool SaveFlag;
        public bool AutosaveFlag;
        public bool AdjustedTime;
        public bool AdjustedTicks;
    }

    public struct Goldsourceoverlaysettings
    {
        public bool DemoProtocol;
        public bool NetProtocol;
        public bool ServerName;
        public bool ClientName;
        public bool MapName;
        public bool GameDirectory;
        public bool MeasuredTime;
        public bool MeasuredTicks;
        public bool HighestFps;
        public bool LowestFps;
        public bool AverageFps;
        public bool LowestMsec;
        public bool HighestMsec;
        public bool AverageMsec;
    }

}