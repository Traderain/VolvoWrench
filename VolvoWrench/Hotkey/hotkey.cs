using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace VolvoWrench
{
    public partial class Hotkey : Form
    {
        public Hotkey()
        {
            this.KeyPreview = true;
            InitializeComponent();
            label1.Text = @"Demo info popup: " + KeyInterop.KeyFromVirtualKey(Main.Demo_Popup_Key);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main.SettingsManager(false);
            label1.Text = @"Demo info popup: " + KeyInterop.KeyFromVirtualKey(Main.Demo_Popup_Key);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.SettingsManager(true);
            label1.Text = @"Demo info popup: " + KeyInterop.KeyFromVirtualKey(Main.Demo_Popup_Key);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Main.SettingsPath);
        }
    }
}