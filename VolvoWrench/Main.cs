using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using VolvoWrench.Demo_stuff;
using static System.Convert;

namespace VolvoWrench
{
    public sealed partial class Main : Form
    {
        public enum DemoType
        {
            None,
            GoldSource,
            Source
        }

        public static readonly string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                                "\\" + "VolvoWrench" + "\\" +
                                                "VWLog.log";

        public static readonly string SettingsPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "VolvoWrench" + "\\" +
            "VWSettings.config";

        public static int Demo_Popup_Key = 0x70; //F1
        public CrossParseResult CurrentDemoFile;
        public DemoType CurrentDemoType;
        public string CurrentFile;

        public Main()
        {
            InitializeComponent();
            SettingsManager(false);
            AllowDrop = true;
            HotkeyTimer.Start();
            goldSourceToolsToolStripMenuItem.Enabled = false;
            toolsToolStripMenuItem.Enabled = false;
            if (File.Exists(LogPath))
            {
                File.Delete(LogPath);
            }

            #region OpenedWithFile check

            var dropFile = (Environment.GetCommandLineArgs().Any(x => Path.GetExtension(x) == ".dem"))
                ? Environment.GetCommandLineArgs().First(x => Path.GetExtension(x) == ".dem")
                : null;
            if (dropFile == null)
            {
                toolsToolStripMenuItem.Enabled = false;
                richTextBox1.Text = "^ Use File->Open to open a correct \".dem\" file!" + "\n" +
                                    "No file dropped!";
            }
            else
            {
                if (dropFile == null || (!File.Exists(dropFile) || Path.GetExtension(dropFile) != ".dem"))
                {
                    CurrentFile = dropFile;
                    CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                    PrintSetails(CurrentDemoFile);
                    toolsToolStripMenuItem.Enabled = true;
                    Log(Path.GetFileName(CurrentFile + " opened"));
                }
                else
                {
                    toolsToolStripMenuItem.Enabled = false;
                    richTextBox1.Text = "^ Use File->Open to open a correct \".dem\" file!" + "\n" +
                                        "No file dropped!";
                }
            }

            #endregion
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var of = new OpenFileDialog())
            {
                of.Filter = "Demo files | *.dem";
                switch (of.ShowDialog())
                {
                    case DialogResult.OK:
                        CurrentFile = of.FileName;
                        if (CurrentFile != null &&
                            (File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem"))
                        {
                            CurrentDemoFile = CrossDemoParser.Parse(of.FileName);
                            //TODO: Add print
                            Log(Path.GetFileName(CurrentFile) + " opened!");
                        }
                        break;
                }
            }
        }

        public static void Log(string s)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                      "VolvoWrench");
            File.AppendAllLines(LogPath, new[]
            {
                (DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") + " " +
                 $"[{WindowsIdentity.GetCurrent()?.Name}]" + ": " + s)
            });
        }

        public void PrintSetails(CrossParseResult demo)
        {
            if (demo == null) return;
            switch (demo.Type)
            {
                case Parseresult.UnsupportedFile:
                    break;
                case Parseresult.GoldSource:
                    break;
                case Parseresult.Hlsooe:
                    break;
                case Parseresult.Source:
                    richTextBox1.Text =
                        "Analyzed source engine demo file:" + 
                        $"----------------------------------------------------------" +
                        $"{$"\n{$"Demo protocol: {demo.Sdi.DemoProtocol}\n"}{$"Net protocol: {demo.Sdi.NetProtocol}\n"}{$"Server name: {demo.Sdi.ServerName}\n"}{$"Client name: {demo.Sdi.ClientName}\n"}{$"Map name: {demo.Sdi.MapName}\n"}{$"Game directory: {demo.Sdi.GameDirectory}\n"}{$"Length in seconds: {demo.Sdi.Seconds}\n"}{$"Tick count: {demo.Sdi.TickCount}\n"}{$"Frame count: {demo.Sdi.FrameCount}\n"}----------------------------------------------------------"}\n";
                        foreach (var f in demo.Sdi.Flags)
                            switch (f.Name)
                            {
                                case "#SAVE#":
                                    richTextBox1.Text += $"#SAVE# flag at Tick: {f.Tick} -> {f.Time}s" + "\n";
                                    HighlightLastLine(richTextBox1, Color.Yellow);
                                    break;
                                case "autosave":
                                    richTextBox1.Text += $"Autosave at Tick: {f.Tick} -> {f.Time}s" + "\n";
                                    HighlightLastLine(richTextBox1, Color.DarkOrange);
                                    break;
                            }
                    break;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if (MessageBox.Show("Are you sure you would like to close the program?","Confirm!",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                //TODO: Uncomment this when releasing but its annoying so I won't leave this in while testing. :p
            }
        }

        public void HighlightLastLine(RichTextBox textControl, Color highlightColor)
        {
            textControl.Text = textControl.Text.Trim();
            textControl.SelectionStart = 0;
            textControl.SelectionLength = 0;
            textControl.SelectionColor = Color.Black;
            var lastLineText = textControl.Lines[richTextBox1.Lines.Count() - 1];
            var lastLineStartIndex = richTextBox1.Text.LastIndexOf(lastLineText, StringComparison.Ordinal);
            textControl.SelectionStart = lastLineStartIndex;
            textControl.SelectionLength = textControl.Text.Length - 1;
            textControl.SelectionColor = highlightColor;
            textControl.DeselectAll();
            textControl.Select(textControl.Text.Length, 0);
        }

        private void HotkeyTimer_Tick(object sender, EventArgs e)
        {
            if (CurrentDemoFile == null) return;
            var popupkey = KeyInputApi.GetKeyState(Demo_Popup_Key);
            if ((popupkey & 0x8000) != 0)
            {
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
                else
                {
                    TopMost = true;
                    Focus();
                    BringToFront();
                    TopMost = false;
                }
                RescanFile();
                switch (CurrentDemoFile.Type)
                {
                    case Parseresult.UnsupportedFile:
                        break;
                    case Parseresult.GoldSource:
                        break;
                    case Parseresult.Hlsooe:
                        break;
                    case Parseresult.Source:
                        MessageBox.Show("Demo protocol: " + CurrentDemoFile.Sdi.DemoProtocol + "\n"
                        + "Net protocol: " + CurrentDemoFile.Sdi.NetProtocol + "\n"
                        + "Server name: " + CurrentDemoFile.Sdi.ServerName + "\n"
                        + "Client name: " + CurrentDemoFile.Sdi.ClientName + "\n"
                        + "Map name: " + CurrentDemoFile.Sdi.MapName + "\n"
                        + "Game directory: " + CurrentDemoFile.Sdi.GameDirectory + "\n"
                        + "Length in seconds: " + CurrentDemoFile.Sdi.Seconds + "\n"
                        + "Tick count: " + CurrentDemoFile.Sdi.TickCount + "\n"
                        + "Frame count: " + CurrentDemoFile.Sdi.FrameCount);
                        break;
                }
            }
        }

        public static void SettingsManager(bool reset)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                      "VolvoWrench");
            if (!File.Exists(SettingsPath) || reset)
            {
                File.WriteAllLines(SettingsPath,
                    new[]
                    {
                        ";[VolvoWrench config file]\r\n; Here are your hotkeys for the program.\r\n; Every line which starts with a semicolon(\';\') is ignored.\r\n; Please keep that in mind.\r\n[HOTKEYS]\r\n;You can modify these keys. Google VKEY\r\ndemo_popup = 0x70;\r\n[SETTINGS]\r\nLanguage = EN;"
                    });
                Demo_Popup_Key = 0x70; //F1
            }
            else
            {
                var filteredArray = File.ReadAllLines(SettingsPath).Where(x => !x.StartsWith(";")).ToArray();
                Demo_Popup_Key = ToInt32(filteredArray
                    .First(x => x
                        .Contains("demo_popup"))
                    .Replace(" ", string.Empty)
                    .Replace(";", string.Empty)
                    .Trim()
                    .Split('=')[1], 16);
            }
        }

        public void RescanFile()
        {
            if (CurrentFile == null || (!File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem")) return;
            CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
            PrintSetails(CurrentDemoFile);
            toolsToolStripMenuItem.Enabled = true;
            Log(Path.GetFileName(CurrentFile + " rescanned."));
        }

        private void openAsavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sa = new saveanalyzerform())
            {
                sa.ShowDialog();
            }
        }

        #region Help Toolstrip Stuff

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var a = new About())
            {
                Log("About");
                a.ShowDialog();
            }
        }

        private void sourcerunsWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://wiki.sourceruns.org");
        }

        private void sourcerunsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://sourceruns.org");
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Github check
            using (var a = new Update())
            {
                a.ShowDialog();
                Log("Updatecheck");
            }
        }

        #endregion

        #region Context menu stuff Showlog,Export etc.

        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Copyall");
            Clipboard.SetText(richTextBox1.Text);
        }

        private void rescanFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RescanFile();
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Log opened");
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "VWLog.log");
        }

        private void exportDemoDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = new SaveFileDialog();
            if (a.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(a.FileName, richTextBox1.Lines);
                //TODO: EXPORT AS XML
            }
        }

        private void renameDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null || !File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem") return;
            CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
            switch (CurrentDemoFile.Type)
            {
                //TODO: Renaming for everything
                case Parseresult.UnsupportedFile:
                    break;
                case Parseresult.GoldSource:
                    break;
                case Parseresult.Hlsooe:
                    break;
                case Parseresult.Source:
                    var stime = (CurrentDemoFile.Sdi.Flags.Count(x => x.Name == "#SAVE#") == 0)
                    ? CurrentDemoFile.Sdi.Seconds.ToString("#,0.000")
                    : CurrentDemoFile.Sdi.Flags.Last(x => x.Name == "#SAVE#").Time.ToString("#,0.000");
                    File.Move(CurrentFile,
                        Path.GetDirectoryName(CurrentFile) + "\\" +
                        CurrentDemoFile.Sdi.MapName.Substring(3, CurrentDemoFile.Sdi.MapName.Length - 3) + "-" +
                        $"{stime}" + "-" + CurrentDemoFile.Sdi.ClientName + ".dem");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Source Tools

        private void netdecodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null || (!File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem")) return;
            using (Stream cfs = File.Open(CurrentFile, FileMode.Open))
            using (var nd = new DemoDecoder(cfs))
            {
                Log("Netdecode opened");
                nd.ShowDialog();
            }
        }

        private void heatmapGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Finish this.
        }

        #endregion

        #region Settings

        private void hotkeysToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var a = new Hotkey())
                a.ShowDialog();
        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var fd = new FontDialog())
                {
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Font = fd.Font;
                    }
                }
                if (CurrentFile == null || (!File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem"))
                    return;
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                PrintSetails(CurrentDemoFile);
                toolsToolStripMenuItem.Enabled = true;
                Log(Path.GetFileName(CurrentFile + " rescanned for font change.")); //Terribble hack for recolor.
                Log("Font changed");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        #endregion

        #region DragDrop file

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            var dropfiles = (string[]) e.Data.GetData(DataFormats.FileDrop);
            var dropfile = (dropfiles.Any(x => Path.GetExtension(x) == ".dem"))
                ? dropfiles.First(x => Path.GetExtension(x) == ".dem")
                : null;
            if (dropfile != null) CurrentFile = dropfile;
            if (CurrentFile == null || (!File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem")) return;
            CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
            PrintSetails(CurrentDemoFile);
            toolsToolStripMenuItem.Enabled = true;
            Log(Path.GetFileName(CurrentFile) + " opened!");
        }
        #endregion
    }
}