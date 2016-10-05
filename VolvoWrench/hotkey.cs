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
            public int Vkeycode;
            public bool State;

            public KeyboardKey(int vk,bool state)
            {
                this.Vkeycode = vk;
                this.State = state;
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
            this.Text = @"Press any key!";
            label1.Text = "";

        }

        public void PickInfoHotkey()
        {

        }

        private void Hotkey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyStates.Any(x => x.Vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)))
            {
                KeyStates.First(x => x.Vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)).State = true;
            }
            else
            {
                KeyStates.Add(new KeyboardKey(KeyInterop.VirtualKeyFromKey((Key) e.KeyCode),true));
            }

        }

        private void Hotkey_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (KeyStates.Any(x => x.Vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)))
            {
                KeyStates.First(x => x.Vkeycode == KeyInterop.VirtualKeyFromKey((Key)e.KeyCode)).State = false;
            }
            label1.Text = KeyInterop.KeyFromVirtualKey(e.KeyValue).ToString();
        }

        //TODO:Make a Dictionary<VKEYCODE,BOOL> and handle keys in keydown.
    }
}