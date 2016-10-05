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
        public class KeyboardKey
        {
            public int vkeycode;
            public bool state;

            public KeyboardKey(int vk,bool state)
            {
                this.vkeycode = vk;
                this.state = state;
            }
        }
        public int InfoPopupHotkey;
        public List<KeyboardKey> KeyStates = new List<KeyboardKey>();
        public Hotkey()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e) => Close();

        private void button3_Click(object sender, EventArgs e) => Close();

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            label1.Text = @"Press any key!";
            
        }

        public void PickInfoHotkey()
        {

        }

        private void Hotkey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyStates.Any(x => x.vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)))
            {
                KeyStates.First(x => x.vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)).state = true;
            }
            else
            {
                KeyStates.Add(new KeyboardKey(KeyInterop.VirtualKeyFromKey((Key) e.KeyCode),true));
            }
        }

        private void Hotkey_KeyPress(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) return;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            label1.Text = @"Infopopup: None";
        }

        private void Hotkey_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyStates.Any(x => x.vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)))
            {
                KeyStates.First(x => x.vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)).state = false;
            }
        }

        //TODO:Make a Dictionary<VKEYCODE,BOOL> and handle keys in keydown.
    }
}