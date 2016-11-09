using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Brush = SharpDX.Direct2D1.Brush;
using Factory = SharpDX.Direct2D1.Factory;
using FactoryType = SharpDX.Direct2D1.FactoryType;
using Font = System.Drawing.Font;
using FontFactory = SharpDX.DirectWrite.Factory;
using FontStyle = SharpDX.DirectWrite.FontStyle;
using TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode;

namespace External_Overlay
{
    public partial class OverlayForm : Form
    {
        public static string FilePath = "";

        public static string Demodata = @"Analyzed L4D2Branch demo file (swarm):
----------------------------------------------------------
Protocol:           4
Network protocol:   7109
Server name:        localhost:27015
Client name:        @FollowOnin
Mapname:            asi-jac1-landingbay_01
GameDir:            swarm
Playbacktime:       61,395s
Playbackticks:      4093
Playbackframes:     28518
Signonlength:       172104

Adjusted time:      0s
Adjusted ticks:     0

----------------------------------------------------------
";

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

        private void SDxThread(object sender)
        {
            
            while (true)
            {
                _device.BeginDraw();
                _device.Clear(new RawColor4(Color.Transparent.R, Color.Transparent.G, Color.Transparent.B, Color.Transparent.A));
                _device.TextAntialiasMode = TextAntialiasMode.Aliased;// you can set another text mode
                using (var g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    _device.DrawText(Demodata,
                        new TextFormat(_fontFactory, FontFamily, FontWeight.Normal, FontStyle.Normal, FontSize),
                        new RawRectangleF(0, 0,(int)g.MeasureString(Demodata, new System.Drawing.Font(FontFamily, FontSize)).Width+5,(int) g.MeasureString(Demodata, new System.Drawing.Font(FontFamily, FontSize)).Height+5), _solidColorBrush);
                }
                _device.EndDraw();
            }
        }
    }
}
