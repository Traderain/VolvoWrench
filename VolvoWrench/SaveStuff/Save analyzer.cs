using System;
using System.Windows.Forms;

namespace VolvoWrench.SaveStuff
{
    public partial class saveanalyzerform : Form
    {
        public Listsave.SaveFile CurrentSaveFile;

        public saveanalyzerform()
        {
            InitializeComponent();
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            propertyGrid1.AutoScaleMode = AutoScaleMode.None; 
        }

        public saveanalyzerform(string file)
        {
            InitializeComponent();
            PrintandAnalyze(file);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            using (var of = new OpenFileDialog())
            {
                of.Multiselect = false;
                of.Filter = @"Demo files (*.sav) | *.sav";
                if (of.ShowDialog() == DialogResult.OK)
                    PrintandAnalyze(of.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CurrentSaveFile != null && CurrentSaveFile?.Files.Count > 1)
                using (var a = new SaveFileExplorer(CurrentSaveFile.Files))
                    a.ShowDialog();
            else
                MessageBox.Show(@"Bad file!", @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PrintandAnalyze(string s)
        {
            CurrentSaveFile = Listsave.ParseSaveFile(s);
            propertyGrid1.SelectedObject = CurrentSaveFile;
        }
    }
}