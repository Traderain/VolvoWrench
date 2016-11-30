using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using IniParser;
using IniParser.Parser;

namespace VolvoWrench.Hotkey
{
    public partial class Hotkey : Form
    {
        public int DemoPopupKey;
        public int OverLayExitKey;
        public int OverLayRescanKey;

        public Hotkey()
        {
            KeyPreview = true;
            InitializeComponent();
            var _parser = new FileIniDataParser();
            var iniD = _parser.ReadFile(Main.SettingsPath);
            DemoPopupKey = Convert.ToInt32(iniD["HOTKEYS"]["demo_popup"], 16);
            OverLayExitKey = Convert.ToInt32(iniD["HOTKEYS"]["overlay_exit"], 16);
            OverLayRescanKey = Convert.ToInt32(iniD["HOTKEYS"]["overlay_rescan"],16);
            label1.Text = @"Demo info popup: " + KeyInterop.KeyFromVirtualKey(DemoPopupKey);
            label2.Text = @"Overlay exit: " + KeyInterop.KeyFromVirtualKey(OverLayExitKey);
            label3.Text = @"Overlay rescan: " + KeyInterop.KeyFromVirtualKey(OverLayRescanKey);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main.SettingsManager(false);
            label1.Text = @"Demo info popup: " + KeyInterop.KeyFromVirtualKey(DemoPopupKey);
            label2.Text = @"Overlay exit: " + KeyInterop.KeyFromVirtualKey(OverLayExitKey);
            label3.Text = @"Overlay rescan: " + KeyInterop.KeyFromVirtualKey(OverLayRescanKey);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.SettingsManager(true);
            label1.Text = @"Demo info popup: " + KeyInterop.KeyFromVirtualKey(DemoPopupKey);
            label2.Text = @"Overlay exit: " + KeyInterop.KeyFromVirtualKey(OverLayExitKey);
            label3.Text = @"Overlay rescan: " + KeyInterop.KeyFromVirtualKey(OverLayRescanKey);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(Main.SettingsPath);
        }
    }
}