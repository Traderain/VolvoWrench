using System;
using System.Windows.Forms;

namespace VolvoWrench
{
    public partial class hotkey : Form
    {
        private bool PickingPopupKey;

        public hotkey()
        {
            InitializeComponent();
        }

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
            PickingPopupKey = true;
        }

        private void hotkey_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void hotkey_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void hotkey_KeyDown(object sender, KeyEventArgs e)
        {
            if (PickingPopupKey)
            {
                MessageBox.Show(e.KeyCode.ToString());
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
        }
    }
}