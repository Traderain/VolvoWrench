using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using VolvoWrench.SaveStuff;

namespace VolvoWrench
{
    public partial class saveanalyzerform : Form
    {
        public saveanalyzerform()
        {
            InitializeComponent();
        }

        public Listsave.SaveFile CurrentSaveFile;

        public saveanalyzerform(string file)
        {
            InitializeComponent();
            if ((File.Exists(file) && Path.GetExtension(file) == ".sav"))
            {
                label1.Text = Path.GetFileName(file);
                var parsedSave = Listsave.ParseFile(file);
                richTextBox1.Text =
                    $@" - Save parsed -
Filename:               {Path.GetFileName(file)}
Header:                 {parsedSave.Header}
SaveVersion:            {parsedSave.SaveVersion}      
Size:                   {parsedSave.TokenTableFileTableOffset}
TokenCount:             {parsedSave.TokenCount}
Tokensize:              {parsedSave.TokenTableSize}
";
                richTextBox1.Text += @"Savestate files in save:";
                foreach (var valvFile in parsedSave.Files)
                {
                    richTextBox1.Text += $@"
Name:                   {valvFile.FileName}
Magic Word:             {valvFile.MagicWord}
Size:                   {valvFile.Data.Length} bytes
--------------------------------------";
                }
            }
            else
            {
                label1.Text = @"Bad file!";
                richTextBox1.Text = @"Select a correct file please.";
                Main.Log("Save parse open failed.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            using (var of = new OpenFileDialog())
            {
                of.Multiselect = false;
                of.Filter = @"Demo files (*.sav) | *.sav";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    if ((File.Exists(of.FileName) && Path.GetExtension(of.FileName) == ".sav"))
                    {
                        var parsedSave = Listsave.ParseFile(of.FileName);
                        CurrentSaveFile = parsedSave;
                        richTextBox1.Text =
                            $@"Save parsed
Filename:               {Path.GetFileName(of.FileName)}
Header:                 {parsedSave.Header}
SaveVersion:            {parsedSave.SaveVersion}      
Size:                   {parsedSave.TokenTableFileTableOffset}
TokenCount:             {parsedSave.TokenCount}
Tokensize:              {parsedSave.TokenTableSize}

Savestate files in save:
";
                        foreach (var file in parsedSave.Files)
                        {
                            richTextBox1.Text += $@"
{file.FileName}";
                        }
                    }
                    else
                    {
                        label1.Text = @"Bad file!";
                        richTextBox1.Text = @"Select a correct file please.";
                        Main.Log("Save parse open failed.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (CurrentSaveFile != null && CurrentSaveFile?.Files.Count > 1)
            {
                using (var a = new SaveFileExplorer(CurrentSaveFile.Files))
                {
                    a.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Bad file!","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}