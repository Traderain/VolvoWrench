using System;
using System.IO;
using System.Windows.Forms;
using VolvoWrench.SaveStuff;

namespace VolvoWrench
{
    public partial class saveanalyzerform : Form
    {
        public Listsave.SaveFile CurrentSaveFile;

        public saveanalyzerform()
        {
            InitializeComponent();
            splitContainer1.FixedPanel = FixedPanel.Panel1;
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
                MessageBox.Show("Bad file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PrintandAnalyze(string s)
        {
            richTextBox1.Text = "";
            if ((File.Exists(s) && Path.GetExtension(s) == ".sav"))
            {
                label1.Text = Path.GetFileName(s);
                var parsedSave = Listsave.ParseFile(s);
                richTextBox1.AppendText($@"- Save parsed -
Filename:               {Path.GetFileName(s)}
Header:                 {
                        parsedSave.Header}
SaveVersion:            {parsedSave.SaveVersion
                        }      
Size:                   {parsedSave.TokenTableFileTableOffset
                        }
TokenCount:             {
                        parsedSave.TokenCount}
Tokensize:              {parsedSave.TokenTableSize}
");
                richTextBox1.AppendText(@"
-----------------------
Savestate files in save
-----------------------");
                foreach (var valvFile in parsedSave.Files)
                {
                    richTextBox1.AppendText($@"
Name:                   {valvFile.FileName}
Magic Word:             {valvFile.MagicWord}
Size:                   {valvFile.Data.Length} bytes
--------------------------------------");
                    richTextBox1.Invalidate();
                    richTextBox1.Update();
                    richTextBox1.Refresh();
                    Application.DoEvents();

                }
            }

            else
            {
                label1.Text = @"Bad file!";
                richTextBox1.Text = @"Select a correct file please.";
                Main.Log("Save parse open failed.");
            }
        }
    }
}