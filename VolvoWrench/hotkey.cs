using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace VolvoWrench
{
    public partial class Hotkey : Form
    {
        private static readonly byte[] DistinctVirtualKeys = Enumerable
            .Range(0, 256)
            .Select(KeyInterop.KeyFromVirtualKey)
            .Where(item => item != Key.None)
            .Distinct()
            .Select(item => (byte) KeyInterop.VirtualKeyFromKey(item))
            .ToArray();

        public int InfoPopupHotkey;

        public Hotkey()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetKeyboardState(byte[] lpKeyState);

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button3.Enabled = false;
            label1.Text = "Press any key!";
            var keypickerThread = new Thread(new ThreadStart(PickInfoHotkey)) {IsBackground = true};
            if (!keypickerThread.IsAlive)
            {
                keypickerThread.Start();
                keypickerThread.Join();
                button1.Enabled = true;
                button3.Enabled = true;
                label1.Text = "Info popup:" + KeyInterop.KeyFromVirtualKey(InfoPopupHotkey);
            }
        }

        public void PickInfoHotkey()
        {
            var i = GetDownKeys().ToList().Count;
            var currentkeys = GetDownKeys().ToList();
            if (i >= 1)
            {
                InfoPopupHotkey = KeyInterop.VirtualKeyFromKey(currentkeys.First());
            }
            else
            {
                while (currentkeys.Count == 0)
                {
                    currentkeys = GetDownKeys().ToList();
                    Thread.Sleep(10); //So we don't rape the pc. :q
                }
                InfoPopupHotkey = KeyInterop.VirtualKeyFromKey(currentkeys.First());
            }
            
        }

        public static IEnumerable<Key> GetDownKeys()
        {
            var keyboardState = new byte[256];
            GetKeyboardState(keyboardState);
            return (from virtualKey in DistinctVirtualKeys
                where (keyboardState[virtualKey] & 0x80) != 0
                select KeyInterop.KeyFromVirtualKey(virtualKey)).ToList();
        }
    }
}