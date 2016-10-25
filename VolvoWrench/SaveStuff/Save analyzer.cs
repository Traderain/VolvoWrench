using System;
using System.IO;
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
                        var ParsedSave = Listsave.ParseFile(of.FileName);
                        richTextBox1.Text =
                            $@"Save parsed
Filename:               {Path.GetFileName(of.FileName)}
Header:                 {ParsedSave.Header}
SaveVersion:            {ParsedSave.SaveVersion}      
Size:                   {ParsedSave.TokenTableFileTableOffset}
TokenCount:             {ParsedSave.TokenCount}
Tokensize:              {ParsedSave.TokenTableSize}";
                        richTextBox1.Text += "\n\n";
                        richTextBox1.Text += @"Savestate files in save:";
                        foreach (var valvFile in ParsedSave.Files)
                        {
                            richTextBox1.Text += "\n";
                            richTextBox1.Text += $@"Name:                   {valvFile.FileName}
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
            using (var a = new SaveFileExplorer())
            {
                a.ShowDialog();
            }
        }
    }
}