using System;
using System.Linq;
using System.Windows.Forms;
using VolvoWrench.Demo_stuff;

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
            label1.Text = "";
            using (var of = new OpenFileDialog())
            {
                of.Multiselect = true;
                of.Filter = @"Demo files (*.sav) | *.sav";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Text = @"Sorry fam not yet :c";
                    //TODO: Make the parser myself because that guy is autistic.
                }
                else
                {
                    label1.Text = @"Bad file!";
                    richTextBox1.Text = @"Select a correct file please.";
                    Main.Log("Save parse open failed.");
                }
            }
        }
    }
}