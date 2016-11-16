using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace VolvoWrench.Demo_stuff.GoldSource
{
    public partial class Demo_doctor : Form
    {
        public string DemoFile = string.Empty;
        public Demo_doctor(string filename)
        {
            InitializeComponent();
            DemoFile = filename;
            label1.Text = Path.GetFileName(filename);
        }

        public Demo_doctor() { InitializeComponent(); }

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
                    var path = Path.Combine(Path.GetTempPath(), "demo-repair.exe");
                    File.WriteAllBytes(path, Properties.Resources.demo_repair);
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo(path + " " + DemoFile + " " + sf.FileName)
                        {
                            WorkingDirectory = Path.GetTempPath(),
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    p.Start();
                    p.WaitForInputIdle();
                    richTextBox1.AppendText("\n Repaired demo file and saved as: " + sf.FileName);
                }
            }
        }
    }
}
