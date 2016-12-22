using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VolvoWrench.Properties;

namespace VolvoWrench.MapStuff
{
    /// <summary>
    /// A form to generate leveloverview
    /// </summary>
    public partial class Leveloverview : Form
    {
        /// <summary>
        /// This generates cl_leveloverview from bsp files
        /// </summary>
        public Leveloverview()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            richTextBox1.Text = @"Please select a file and press the button to generate the leveloverview.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var of = new OpenFileDialog())
            {
                of.Filter = @"Valve map files (.bsp) | *.bsp";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = of.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text) && Path.GetExtension(textBox1.Text) == ".bsp")
            {
                    var path = Path.Combine(Path.GetTempPath(), "BSP_Slicer.exe");
                    string args = "-c";
                    if (numericUpDown1.Value != 0 && numericUpDown2.Value != 0)
                        args += " -bN" + numericUpDown1.Value + " -eN" + numericUpDown2.Value;
                    if (File.Exists(textBox1.Text) && Path.GetExtension(textBox1.Text) == ".bsp")
                        args += " " + "\"" + textBox1.Text + "\"";
                    else
                    {
                        MessageBox.Show(@"Sorry the map you selected is invalid!");
                        return;
                    }
                    File.WriteAllBytes(path, Resources.BSP_slicer);
                    var p = new Process
                    {
                        StartInfo = new ProcessStartInfo(path)
                        {
                            Arguments = args,
                            WorkingDirectory = Path.GetTempPath(),
                            RedirectStandardOutput = true,
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    p.Start();

                    p.WaitForExit();
                    richTextBox1.Text = p.StandardOutput.ReadToEnd();               
                    pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetTempPath(),"result.bmp"));
                    Main.Alert("Leveloverview created!");
                }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                using (var sf = new SaveFileDialog())
                {
                    sf.Filter = @"Bmp files | *.bmp";
                    if (sf.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.Image.Save(sf.FileName);
                        Main.Alert("Saved as " + sf.FileName);
                    }

                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Clipboard.SetImage(pictureBox1.Image);
                Main.Alert("Copied!");
            }
        }
    }
}