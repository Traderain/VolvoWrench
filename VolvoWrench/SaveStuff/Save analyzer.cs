using System;
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
                of.Filter = @"Demo files (*.dem) | *.dem";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    foreach (var f in of.FileNames)
                    {
                        var mp = CrossDemoParser.Parse(f);
                        switch (mp.Type)
                        {
                            case Parseresult.UnsupportedFile:
                                richTextBox1.Text += "\nSorry but this file is not supported";
                                break;
                            case Parseresult.GoldSource:
                                richTextBox1.Text += $"\nGoldsource engine demo file, map:" +
                                                     $"{mp.GsDemoInfo.Header.MapName}" +
                                                     $"";
                                break;
                            case Parseresult.Hlsooe:
                                richTextBox1.Text += $"\nHLSOOE engine demo file, map:" +
                                                     $"{mp.HlsooeDemoInfo.Header.MapName}" +
                                                     $"";
                                break;
                            case Parseresult.Source:
                                richTextBox1.Text += $"\nSource engine demo file, time:" +
                                                     $"{mp.Sdi.Seconds}" +
                                                     $"";
                                break;
                            default:
                                Main.Log("Error when multiparsing");
                                break;
                        }
                    }
                    //TODO: Make the parser myself because that guy is autistic.
                }
                else
                {
                    label1.Text = "Bad file!";
                    richTextBox1.Text = "Select a correct file please.";
                    Main.Log("Save parse open failed.");
                }
            }
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}