using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolvoWrench.Properties;

namespace VolvoWrench.MapStuff
{
    public partial class Leveloverview : Form
    {
        public Leveloverview()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            using (var of = new OpenFileDialog())
            {
                if (of.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = of.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Not implemented yet properly!");
            return; //TODO: finish implementing this
            if (File.Exists(textBox1.Text) && Path.GetExtension(textBox1.Text) == ".bsp")
            {
                var sf = new SaveFileDialog { Filter = @"Demo files .dem | *.dem" };
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    Random r = new Random();
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
                    else
                        pictureBox1.Image = Image.FromFile("");
                }
            }
            else
            {
                MessageBox.Show(@"Please make sure you enteret correct values and a valid map is used!");
            }
        }
    }
}
