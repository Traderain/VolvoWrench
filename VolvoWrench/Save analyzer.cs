using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolvoWrench
{
    public partial class saveanalyzerform : Form
    {
        public saveanalyzerform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog of = new OpenFileDialog())
            {
                of.Filter = "Save files (*.sav) | *.sav";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    label1.Text = Path.GetFileName(of.FileName);
                    //TODO: Add Freelancer.com parser
                }
                else
                {
                    Main.Log("Save parse open failed.");
                }
            }
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
