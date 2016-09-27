using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolvoWrench
{
    public partial class hotkey : Form
    {
        bool PickingPopupKey = false;
        public hotkey()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PickingPopupKey = true;
        }

        private void hotkey_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void hotkey_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void hotkey_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
