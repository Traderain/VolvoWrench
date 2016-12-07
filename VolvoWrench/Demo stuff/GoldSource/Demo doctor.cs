using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using VolvoWrench.Properties;

namespace VolvoWrench.Demo_stuff.GoldSource
{
    /// <summary>
    /// A form for repairing goldsource demos
    /// </summary>
    public partial class DemoDoctor : Form
    {
        /// <summary>
        /// Path to the file being repaired
        /// </summary>
        public string DemoFile = string.Empty;

        /// <summary>
        /// We call this constructor when finding a broken demo
        /// </summary>
        /// <param name="filename"></param>
        public DemoDoctor(string filename)
        {
            InitializeComponent();
            DemoFile = filename;
            label1.Text = Path.GetFileName(filename);
        }

        /// <summary>
        /// Normal constructor
        /// </summary>
        public DemoDoctor() { InitializeComponent(); }

        private void button1_Click(object sender, EventArgs e) //SELECT
        {
            richTextBox1.Text = "";
            var a = new OpenFileDialog
            {
                Multiselect = false,
                Filter = @"Demo files .dem | *.dem"
            };
            if (a.ShowDialog() == DialogResult.OK)
            {
                if (CrossDemoParser.CheckDemoType(a.FileName) != Parseresult.GoldSource)
                {
                    richTextBox1.Text = (@"Only goldsource, understund?!");
                    return;
                }
                label1.Text = Path.GetFileName(a.FileName);
                DemoFile = a.FileName;
                richTextBox1.AppendText("Selected: " + a.FileName);
            }
            else
            {
                richTextBox1.AppendText("Failed to select file. Please reselect!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DemoFile != null)
            {
                var sf = new SaveFileDialog { Filter = @"Demo files .dem | *.dem" };
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    Random r = new Random();
                    var ran = r.Next(1, 2873432);
                    var path = Path.Combine(Path.GetTempPath(), "demo-repair" + ran +".exe");
                    File.WriteAllBytes(path, Resources.demo_repair);
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo(path)
                        {
                            Arguments = "\""+ DemoFile + "\"" + " " + "\"" + sf.FileName + "\"",
                            WorkingDirectory = Path.GetTempPath(),
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    p.Start();
                    
                    p.WaitForExit();
                    File.Delete(path);
                    if(File.Exists(sf.FileName))
                    richTextBox1.AppendText("\n Repaired demo file and saved as: " + sf.FileName);
                    else
                    richTextBox1.AppendText("\nDemo repair lost but we can because we trust!");
                }
            }
        }
    }
}
