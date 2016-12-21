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
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
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
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    p.Start();

                    p.WaitForExit();
                    //File.Delete(path);                    
                    pictureBox1.Image = Image.FromFile(Path.GetTempPath() + "\\" + ".bmp");
                    //File.Delete(Path.GetTempPath() + "\\" + ".bmp");
                }
        }
    }
}