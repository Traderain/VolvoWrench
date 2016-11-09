using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using VolvoWrench.Demo_stuff;
using VolvoWrench.Demo_stuff.GoldSource;
using VolvoWrench.Hotkey;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Brush = SharpDX.Direct2D1.Brush;
using Factory = SharpDX.Direct2D1.Factory;
using FactoryType = SharpDX.Direct2D1.FactoryType;
using Font = System.Drawing.Font;
using FontFactory = SharpDX.DirectWrite.Factory;
using FontStyle = SharpDX.DirectWrite.FontStyle;
using TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode;
using Timer = System.Windows.Forms.Timer;

namespace External_Overlay
{
    public partial class OverlayForm : Form
    {
        public static string FilePath = "";
        public static int RescanKey;
        public static int ExitKey;
        public static Font TextFont;
        public static Color TextColor;
        public static string Currentwindow = "";
        public static string Demodata = "";

        public static string[] GameTitles = new[]
        {
            "HALF-LIFE 2",
            "PORTAL",
            "PORTAL 2"
        };

        private WindowRenderTarget _device;
        private HwndRenderTargetProperties _renderProperties;
        private SolidColorBrush _solidColorBrush;
        private Factory _factory;

        private TextFormat _font, _fontSmall;
        private FontFactory _fontFactory;
        private const string FontFamily = "Arial";
        private const float FontSize = 12.0f;
        private const float FontSizeSmall = 10.0f;


        private IntPtr _handle;
        private Timer _timer1;
        private Thread _sDx = null;

        #region  DllImports
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMargins);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        #endregion 

        public const uint SwpNosize = 0x0001;
        public const uint SwpNomove = 0x0002;
        public const uint TopmostFlags = SwpNomove | SwpNosize;
        public static IntPtr HwndTopmost = new IntPtr(-1);

        public OverlayForm(string file,Color color,Font font,int resetkey,int exitkey)
        {
            _handle = Handle;
            var initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(Handle, -20, initialStyle | 0x80000 | 0x20);
            SetWindowPos(Handle, HwndTopmost, 0, 0, 0, 0, TopmostFlags);
            OnResize(null);
            TopMost = true;
            InitializeComponent();
            _timer1.Interval = 300;
            _timer1.Enabled = true;
            _timer1.Start();
            FilePath = file;
            ExitKey = exitkey;
            RescanKey = resetkey;
            TextColor = color;
            TextFont = font;
            var cpr = CrossDemoParser.Parse(FilePath);
            PrintOverlayData(cpr);
        }

        protected override sealed void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            Width = 1920;
            Height = 1080;
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

            _solidColorBrush = new SolidColorBrush(_device, new RawColor4(Color.Orange.R, Color.Orange.G, Color.Orange.B, Color.Orange.A));
            _font = new TextFormat(_fontFactory, FontFamily, FontSize);
            _fontSmall = new TextFormat(_fontFactory, FontFamily, FontSizeSmall);

            _sDx = new Thread(new ParameterizedThreadStart(SDxThread))
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

        private void hotkeytimer_Tick(object sender, EventArgs e)
        {
            Currentwindow = GetActiveWindowTitle();
            if (Currentwindow == null)
                Currentwindow = "";
            if (FilePath == null) return;
            var exitstate = KeyInputApi.GetKeyState(ExitKey);
            var rstate = KeyInputApi.GetKeyState(RescanKey);
            if ((exitstate & 0x8000) != 0)
            {
                this.Close();
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

        private void SDxThread(object sender)
        {
            
            while (true)
            {
                _device.BeginDraw();
                _device.Clear(new RawColor4(Color.Transparent.R, Color.Transparent.G, Color.Transparent.B, Color.Transparent.A));
                _device.TextAntialiasMode = TextAntialiasMode.Aliased;// you can set another text mode
                using (var g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    if (!string.IsNullOrEmpty(Currentwindow))
                    {
                        if (GameTitles.Any(x=> x.Contains(Currentwindow)))
                        {
                            _device.DrawText(Demodata,
                                new TextFormat(_fontFactory,
                                    FontFamily,
                                    FontWeight.Normal,
                                    FontStyle.Normal,
                                    FontSize),
                                new RawRectangleF(0, 0,
                                    (int)g.MeasureString(Demodata, new System.Drawing.Font(FontFamily, FontSize)).Width + 5,
                                    (int)g.MeasureString(Demodata, new System.Drawing.Font(FontFamily, FontSize)).Height + 5),
                                    _solidColorBrush);
                        }
                    }
                }
                _device.EndDraw();
            }
        }

        public static void PrintOverlayData(CrossParseResult demo)
        {
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
                        Demodata =
                            $@"Analyzed GoldSource engine demo file ({demo.GsDemoInfo.Header.GameDir}):
----------------------------------------------------------
Demo protocol:              {demo.GsDemoInfo.Header.DemoProtocol}
Net protocol:               {demo.GsDemoInfo.Header.NetProtocol}
Directory Offset:           {demo.GsDemoInfo.Header.DirectoryOffset}
Map name:                   {demo.GsDemoInfo.Header.MapName}
Game directory:             {demo.GsDemoInfo.Header.GameDir}
Length in seconds:          {demo.GsDemoInfo.DirectoryEntries.Sum(x => x.TrackTime).ToString("n3")}s
Frame count:                {demo.GsDemoInfo.DirectoryEntries.Sum(x => x.FrameCount)}

Higest FPS:                 {(1 / frametimeMin).ToString("N2")}
Lowest FPS:                 {(1 / frametimeMax).ToString("N2")}
Average FPS:                {(count / frametimeSum).ToString("N2")}
Lowest msec:                {(1000.0 / msecMax).ToString("N2")} FPS
Highest msec:               {(1000.0 / msecMin).ToString("N2")} FPS
Average msec:               {(1000.0 / (msecSum / (double)count)).ToString("N2")} FPS
----------------------------------------------------------";
                    }
                    break;
                case Parseresult.Hlsooe:
                    if (demo.HlsooeDemoInfo.ParsingErrors.ToArray().Length > 0)
                    {
                        Demodata = @"Error while parsing HLSOOE demo: 
";
                        foreach (var err in demo.HlsooeDemoInfo.ParsingErrors)
                        {
                            Demodata += (err);
                        }
                    }
                    else
                    {
                        Demodata = $@"Analyzed HLS:OOE engine demo file ({demo.HlsooeDemoInfo.Header.GameDirectory}):
----------------------------------------------------------
Demo protocol:              {demo.HlsooeDemoInfo.Header.DemoProtocol}
Net protocol:               {demo.HlsooeDemoInfo.Header.Netprotocol}
Directory offset:           {demo.HlsooeDemoInfo.Header.DirectoryOffset}
Map name:                   {demo.HlsooeDemoInfo.Header.MapName}
Game directory:             {demo.HlsooeDemoInfo.Header.GameDirectory}
Length in seconds:          {(demo.HlsooeDemoInfo.DirectoryEntries.Last().Frames.LastOrDefault().Key.Frame) * 0.015}s
Tick count:                 {(demo.HlsooeDemoInfo.DirectoryEntries.Last().Frames.LastOrDefault().Key.Frame)}
----------------------------------------------------------";
                        foreach (
                            var flag in
                                demo.HlsooeDemoInfo.DirectoryEntries.SelectMany(
                                    demoDirectoryEntry => demoDirectoryEntry.Flags))
                            Demodata += (flag.Value.Command + " at " + flag.Key.Frame + " -> " +
                                                    (flag.Key.Frame * 0.015).ToString("n3") + "s");
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
                        Demodata =
                            $@"Analyzed source engine demo file ({demo.Sdi.GameDirectory}):
----------------------------------------------------------
Demo protocol:              {demo.Sdi.DemoProtocol}
Net protocol:               {demo.Sdi.NetProtocol}
Server name:                {demo.Sdi.ServerName}
Client name:                {demo.Sdi.ClientName}
Map name:                   {demo.Sdi.MapName}
Game directory:             {demo.Sdi.GameDirectory}
Playback seconds:           {demo.Sdi.Seconds.ToString("n3")}s
Playback tick:              {demo.Sdi.TickCount}
Frame count:                {demo.Sdi.FrameCount}

Measured time:              {(demo.Sdi.Messages.Max(x => x.Tick) * 0.015).ToString("n3")}s
Measured ticks:             {demo.Sdi.Messages.Max(x => x.Tick)}
----------------------------------------------------------";
                        foreach (var f in demo.Sdi.Flags)
                            switch (f.Name)
                            {
                                case "#SAVE#":
                                    Demodata += ($"\n#SAVE# flag at Tick: {f.Tick} -> {f.Time}s");
                                    break;
                                case "autosave":
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
                        Demodata = $@"Analyzed L4D2Branch demo file ({demo.L4D2BranchInfo.Header.GameDirectory}):
----------------------------------------------------------
Protocol:           {demo.L4D2BranchInfo.Header.Protocol}
Network protocol:   {demo.L4D2BranchInfo.Header.NetworkProtocol}
Server name:        {demo.L4D2BranchInfo.Header.ServerName}
Client name:        {demo.L4D2BranchInfo.Header.ClientName}
Mapname:            {demo.L4D2BranchInfo.Header.MapName}
GameDir:            {demo.L4D2BranchInfo.Header.GameDirectory}
Playbacktime:       {(demo.L4D2BranchInfo.Header.PlaybackTicks * 0.015).ToString("n3")}s
Playbackticks:      {demo.L4D2BranchInfo.Header.PlaybackTicks}
Playbackframes:     {demo.L4D2BranchInfo.Header.PlaybackFrames}
Signonlength:       {demo.L4D2BranchInfo.Header.SignonLength}

Adjusted time:      {demo.L4D2BranchInfo.PortalDemoInfo?.AdjustedTicks * 0.015 + "s"}
Adjusted ticks:     {demo.L4D2BranchInfo.PortalDemoInfo?.AdjustedTicks}

----------------------------------------------------------
";
                    }
                    break;
            }
        }

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            var Buff = new StringBuilder(nChars);
            var handle = GetForegroundWindow();
            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return "";
        }
    }
}
