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
using VolvoWrench.SaveStuff;

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
                of.Filter = @"Save files (*.sav) | *.sav";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    var a = Listsave.ParseFile(of.FileName);
                    richTextBox1.Text = $@"
File name:		{Path.GetFileName(of.FileName)}
Chapter:		{a.Chapter}
Map:		    {a.Map}
Version:		{a.SaveVersion}
Tokencount:		{a.TokenCount}
Tokensize:		{a.Tokensize}
";
                    //TODO: Make the parser myself because that guy is autistic.
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
