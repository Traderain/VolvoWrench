using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VolvoWrench.Properties;

namespace VolvoWrench.MapStuff
{
    public partial class Leveloverview : Form
    {
        /// <summary>
        /// This generates cl_leveloverview from bsp files
        /// </summary>
        public Leveloverview()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var of = new FolderBrowserDialog())
            {
                if (of.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = of.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Not implemented yet properly!");
            return; //TODO: finish implementing this
            if (File.Exists(textBox1.Text) && Path.GetExtension(textBox1.Text) == ".bsp")
            {
                var sf = new SaveFileDialog {Filter = @"Demo files .dem | *.dem"};
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    var r = new Random();
                    var ran = r.Next(1, 2873432);
                    var path = Path.Combine(Path.GetTempPath(), "demo-repair" + ran + ".exe");
                    File.WriteAllBytes(path, Resources.demo_repair);
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo(path)
                        {
                            Arguments = "\"" + textBox1.Text + "\"" + " " + "\"" + sf.FileName + "\"",
                            WorkingDirectory = Path.GetTempPath(),
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    p.Start();

                    p.WaitForExit();
                    File.Delete(path);
                    if (File.Exists(sf.FileName))
                        MessageBox.Show(@"Please make sure you enteret correct values and a valid map is used!");
                    pictureBox1.Image = Image.FromFile("");
                }
            }
            MessageBox.Show(@"Please make sure you enteret correct values and a valid map is used!");
        }
    }
}