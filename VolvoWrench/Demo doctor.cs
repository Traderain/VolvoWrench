using System;
using System.IO;
using System.Windows.Forms;

namespace VolvoWrench
{
    public partial class Demo_doctor : Form
    {
        public string File = string.Empty;
        public Demo_doctor(string filename)
        {
            InitializeComponent();
            File = filename;
            label1.Text = Path.GetFileName(filename);
        }

        public Demo_doctor() { InitializeComponent(); }

        private void button1_Click(object sender, EventArgs e) //SELECT
        {
            var a = new OpenFileDialog
            {
                Multiselect = false,
                Filter = @"Demo files .dem | *.dem"
            };
            if (a.ShowDialog() == DialogResult.OK)
                label1.Text = Path.GetFileName(a.FileName);
        }

        private void button2_Click(object sender, EventArgs e) //FIX
        {
            MessageBox.Show(@"Not yet");
        }
    }
}
