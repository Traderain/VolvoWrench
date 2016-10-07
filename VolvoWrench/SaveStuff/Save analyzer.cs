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
            using (OpenFileDialog of = new OpenFileDialog())
            {
                of.Multiselect = true;
                // of.Filter = "Save files (*.sav) | *.sav";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    label1.Text = Path.GetFileName(of.FileName);
                    foreach (var VARIABLE in of.FileNames)
                    {
                        GoldSourceParser.ParseDemoHlsooe(VARIABLE);
                        richTextBox1.Text += CrossDemoParser.CheckDemoType(VARIABLE) +" - " + Path.GetFileName(VARIABLE) + "\n";
                    }
                    
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
