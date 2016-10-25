using System;
using System.Collections.Generic;
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

        public static readonly string LogPath = string.Format("{0}\\" + "VolvoWrench" + "\\" + "VWLog.log", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        public static readonly string SettingsPath = string.Format("{0}\\" + "VolvoWrench" + "\\" + "VWSettings.config", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        public static int DemoPopupKey = 0x70; //F1
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
                File.Delete(LogPath);
            #region OpenedWithFile check

            var dropFile = (Environment.GetCommandLineArgs().Any(x => Path.GetExtension(x) == ".dem"))
                ? Environment.GetCommandLineArgs().First(x => Path.GetExtension(x) == ".dem")
                : null;
            if (dropFile == null)
            {
                toolsToolStripMenuItem.Enabled = false;
                goldSourceToolsToolStripMenuItem.Enabled = false;
                richTextBox1.Text = @"^ Use File->Open to open a correct .dem file or drop the file here!";
            }
            else
            {
                if ((!File.Exists(dropFile) || Path.GetExtension(dropFile) != ".dem"))
                {
                    CurrentFile = dropFile;
                    richTextBox1.Text = @"Analyzing file...";
                    CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                    PrintDemoDetails(CurrentDemoFile);
                    Log(Path.GetFileName(CurrentFile + " opened"));
                }
                else
                {
                    toolsToolStripMenuItem.Enabled = false;
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    richTextBox1.Text = @"^ Use File->Open to open a correct .dem file or drop the file here!";
                }
            }
            #endregion
        }


        //Open with main constructor
        public Main(string s)
        {
            InitializeComponent();
            CurrentFile = s;
            SettingsManager(false);
            AllowDrop = true;
            HotkeyTimer.Start();
            goldSourceToolsToolStripMenuItem.Enabled = false;
            toolsToolStripMenuItem.Enabled = false;
            if (File.Exists(LogPath))
                File.Delete(LogPath);
            if (CurrentFile != null || (File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) == ".dem"))
            {
                richTextBox1.Text = @"Analyzing file...";
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                PrintDemoDetails(CurrentDemoFile);
                Log(CurrentFile + " opened!");
            }
            else
            {
                toolsToolStripMenuItem.Enabled = false;
                goldSourceToolsToolStripMenuItem.Enabled = false;
                richTextBox1.Text = @"^ Use File->Open to open a correct .dem file or drop the file here!";
            }
        }

        #region File ToolStrip stuff
        private void openAsavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sa = new saveanalyzerform())
            {
                sa.ShowDialog();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var of = new OpenFileDialog())
            {
                of.Filter = @"Demo files | *.dem";
                if (of.ShowDialog() != DialogResult.OK) return;
                if ((!File.Exists(of.FileName) || Path.GetExtension(of.FileName) != ".dem")) return;
                CurrentFile = of.FileName;
                RescanFile();
                PrintDemoDetails(CurrentDemoFile);
            }
        }
        #endregion

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
            a.Filter = "XML Files | *.xml";
            if (a.ShowDialog() == DialogResult.OK && CurrentDemoFile != null)
            {
                //TODO: Add save
            }
        }

        private void renameDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null || !File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem") return;
            {
                richTextBox1.Text = @"Analyzing file...";
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
            }
            switch (CurrentDemoFile.Type)
            {
                case Parseresult.UnsupportedFile:
                    break;
                case Parseresult.GoldSource:
                    File.Move(CurrentFile,
                        Path.GetDirectoryName(CurrentFile) + "\\" +
                        CurrentDemoFile.GsDemoInfo.Header.MapName + "-" +
                        $"{CurrentDemoFile.GsDemoInfo.DirectoryEntries.Last().TrackTime.ToString("#,0.000")}" + "-" + Environment.UserName + ".dem");
                    break;
                case Parseresult.Hlsooe:
                    File.Move(CurrentFile,
                        Path.GetDirectoryName(CurrentFile) + "\\" +
                        CurrentDemoFile.HlsooeDemoInfo.Header.MapName + "-" +
                        $"{CurrentDemoFile.HlsooeDemoInfo.DirectoryEntries.Last().PlaybackTime.ToString("#,0.000")}" + "-" + Environment.UserName + ".dem");
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

        #region Settings and hotkeys stuff

        private void hotkeysToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var a = new Hotkey())
                a.ShowDialog();
        }

        private void HotkeyTimer_Tick(object sender, EventArgs e)
        {
            if (CurrentDemoFile == null) return;
            var popupkey = KeyInputApi.GetKeyState(DemoPopupKey);
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
                        #region Mbox
                        MessageBox.Show(@"
Demo protocol: " + CurrentDemoFile.Sdi.DemoProtocol + @"
Net protocol: " + CurrentDemoFile.Sdi.NetProtocol + @"
Server name: " + CurrentDemoFile.Sdi.ServerName + @"
Client name: " + CurrentDemoFile.Sdi.ClientName + @"
Map name: " + CurrentDemoFile.Sdi.MapName + @"
Game directory: " + CurrentDemoFile.Sdi.GameDirectory + @"
Length in seconds: " + CurrentDemoFile.Sdi.Seconds + @"
Tick count: " + CurrentDemoFile.Sdi.TickCount + @"
Frame count: " + CurrentDemoFile.Sdi.FrameCount);
                        #endregion
                        break;
                }
            }
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
                richTextBox1.Text = @"Analyzing file...";
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                PrintDemoDetails(CurrentDemoFile);
                Log(Path.GetFileName(CurrentFile + " rescanned for font change.")); //Terribble hack for recolor.
                Log("Font changed");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        public static void SettingsManager(bool reset)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                      "VolvoWrench");
            if (!File.Exists(SettingsPath) || reset)
            {
                #region Config text
                File.WriteAllLines(SettingsPath,
                    new[]
                    {
                        @";[VolvoWrench config file]
; Here are your hotkeys for the program.
; Every line which starts with a semicolon(';') is ignored.
; Please keep that in mind.
[HOTKEYS]
;You can modify these keys. Google VKEY
demo_popup = 0x70;
[SETTINGS]
Language = EN;"
                    });
                #endregion
                DemoPopupKey = 0x70; //F1
            }
            else
            {
                var filteredArray = File.ReadAllLines(SettingsPath).Where(x => !x.StartsWith(";")).ToArray();
                DemoPopupKey = ToInt32(filteredArray
                    .First(x => x
                        .Contains("demo_popup"))
                    .Replace(" ", string.Empty)
                    .Replace(";", string.Empty)
                    .Trim()
                    .Split('=')[1], 16);
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
            richTextBox1.Text = @"Analyzing file...";
            CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
            PrintDemoDetails(CurrentDemoFile);
            Log(Path.GetFileName(CurrentFile) + " opened!");
        }
        #endregion

        public void PrintDemoDetails(CrossParseResult demo)
        {
            if (demo == null) return;
            StripEnabler(demo);
            switch (demo.Type)
            {
                case Parseresult.UnsupportedFile:
                    richTextBox1.Text = @"Unsupported file!";
                    break;
                case Parseresult.GoldSource:
                    richTextBox1.Text = $@"Analyzed GoldSource engine demo file ({demo.GsDemoInfo.Header.GameDir}):
----------------------------------------------------------
Demo protocol:              {demo.GsDemoInfo.Header.DemoProtocol}
Net protocol:               {demo.GsDemoInfo.Header.NetProtocol}
Directory Offset:           {demo.GsDemoInfo.Header.DirectoryOffset}
Map name:                   {demo.GsDemoInfo.Header.MapName}
Game directory:             {demo.GsDemoInfo.Header.GameDir}
Length in seconds:          {demo.GsDemoInfo.DirectoryEntries.Sum(x=> x.TrackTime).ToString("n3")}s
Frame count:                {demo.GsDemoInfo.DirectoryEntries.Sum(x=> x.FrameCount)}
----------------------------------------------------------";
                    break;
                case Parseresult.Hlsooe:
                    richTextBox1.Text = $@"Analyzed HLS:OOE engine demo file ({demo.HlsooeDemoInfo.Header.GameDirectory}):
----------------------------------------------------------
Demo protocol:              {demo.HlsooeDemoInfo.Header.DemoProtocol}
Net protocol:               {demo.HlsooeDemoInfo.Header.Netprotocol}
Directory offset:           {demo.HlsooeDemoInfo.Header.DirectoryOffset}
Map name:                   {demo.HlsooeDemoInfo.Header.MapName}
Game directory:             {demo.HlsooeDemoInfo.Header.GameDirectory}
Length in seconds:          {demo.HlsooeDemoInfo.DirectoryEntries.Skip(1).Sum(x=> x.Frames.Last().Key.Time).ToString("n3")}s
Frame count:                {demo.HlsooeDemoInfo.DirectoryEntries.Sum(x => x.FrameCount)}
----------------------------------------------------------";
                    //TODO: Bug in time print
                    break;
                case Parseresult.Source:
                    richTextBox1.Text = $@"Analyzed source engine demo file ({demo.Sdi.GameDirectory}):
----------------------------------------------------------
Demo protocol:              {demo.Sdi.DemoProtocol}
Net protocol:               {demo.Sdi.NetProtocol}
Server name:                {demo.Sdi.ServerName}
Client name:                {demo.Sdi.ClientName}
Map name:                   {demo.Sdi.MapName}
Game directory:             {demo.Sdi.GameDirectory}
Length in seconds:          {demo.Sdi.Seconds.ToString("#,0.000")}s
Tick count:                 {demo.Sdi.TickCount}
Frame count:                {demo.Sdi.FrameCount}
----------------------------------------------------------";
                    foreach (var f in demo.Sdi.Flags)
                        switch (f.Name)
                        {
                            case "#SAVE#":
                                richTextBox1.Text += $"\n#SAVE# flag at Tick: {f.Tick} -> {f.Time}s";
                                HighlightLastLine(richTextBox1, Color.Yellow);
                                break;
                            case "autosave":
                                richTextBox1.Text += $"\nAutosave at Tick: {f.Tick} -> {f.Time}s";
                                HighlightLastLine(richTextBox1, Color.DarkOrange);
                                break;
                        }
                    break;
            }
        }

        public void RescanFile()
        {
            if (CurrentFile == null || (!File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem")) return;
            {
                richTextBox1.Text = @"Analyzing file...";
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
            }
            PrintDemoDetails(CurrentDemoFile);
            Log(Path.GetFileName(CurrentFile + " rescanned."));
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

        public static void Log(string s)
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                      "VolvoWrench");
            File.AppendAllLines(LogPath, new[]
            {
                (DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") + " " +
                 $"[{WindowsIdentity.GetCurrent().Name}]" + ": " + s)
            });
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // if (MessageBox.Show("Are you sure you would like to close the program?","Confirm!",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                //e.Cancel//TODO: Uncomment this when releasing but its annoying so I won't leave this in while testing. :p
            }
        }

        public void StripEnabler(CrossParseResult cpr)
        {
            switch (cpr.Type)
            {
                case Parseresult.UnsupportedFile:
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    toolsToolStripMenuItem.Enabled = false;
                    break;
                case Parseresult.Hlsooe:
                case Parseresult.GoldSource:
                    goldSourceToolsToolStripMenuItem.Enabled = true;
                    toolsToolStripMenuItem.Enabled = false;
                    break;
                case Parseresult.Source:
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    toolsToolStripMenuItem.Enabled = true;
                    break;
            }
        }

        private void heatmapGeneratorToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            var CoordList = new List<Point>();
            CoordList.Add(new Point(10,1));
            CoordList.Add(new Point(8,1));
            CoordList.Add(new Point(3,1));
            CoordList.Add(new Point(10,17));
            CoordList.Add(new Point(120,1));
            CoordList.Add(new Point(100,16));
            using (var hmw = new Heatmap.Heatmap(CoordList, new Bitmap(560, 560)))
            {
                hmw.ShowDialog();
            }
        }

        private void demoDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dd = new Demo_doctor(CurrentFile))
            {
                dd.ShowDialog();
            }
           
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sf = new Statisctics())
            {
                sf.ShowDialog();
            }
        }
    }
}