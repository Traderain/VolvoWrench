using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolvoWrench.Overlay
{
    public partial class OverlaySettings : Form
    {
        public OverlaySettings(Font fo,Color co)
        {
            InitializeComponent();
            F = fo;
            C = co;
            label1.Font = F;
            button2.BackColor = co;
        }

        public Font F;
        public Color C;

        private void OverlaySettings_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //Color pick
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = cd.Color;
                C = cd.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                label1.Font = fd.Font;
                F = fd.Font;
            }
        }

        private void button3_Click(object sender, EventArgs e) //OK
        {
            var parentT = (Main)Owner;
            parentT.UpdateOverLaySettings(F,C);
            Close();
        }

        private void button4_Click(object sender, EventArgs e) //CANCEL
        {
            Close();
        }
    }
}
