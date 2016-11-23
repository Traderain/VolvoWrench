using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using VolvoWrench.Demo_stuff;
using VolvoWrench.Demo_stuff.GoldSource;
using VolvoWrench.Demo_stuff.L4D2Branch.PortalStuff;
using VolvoWrench.Demo_stuff.Source;
using VolvoWrench.Hotkey;
using VolvoWrench.Overlay;
using VolvoWrench.SaveStuff;
using static System.Convert;

namespace VolvoWrench
{
    public sealed partial class Main : Form
    {
        public static readonly string LogPath = string.Format("{0}\\" + "VolvoWrench" + "\\" + "VWLog.log", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        public static readonly string SettingsPath = string.Format("{0}\\" + "VolvoWrench" + "\\" + "VWSettings.ini", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        public static int DemoPopupKey;
        public static int OverLayExitKey;
        public static int OverLayRescanKey;
        public static Font OverlayFont;
        public static Color OverLayColor;
        public static Font MainFont;

        public CrossParseResult CurrentDemoFile;
        public string CurrentFile;

        public Main()
        {
            InitializeComponent();
            SettingsManager(false);
            richTextBox1.Font = MainFont;
            AllowDrop = true;
            netdecodeToolStripMenuItem.Enabled = false;
            heatmapGeneratorToolStripMenuItem1.Enabled = false;
            statisticsToolStripMenuItem.Enabled = false;
            HotkeyTimer.Start();
            if (File.Exists(LogPath))
                File.Delete(LogPath);
            #region OpenedWithFile check

            var dropFile = (Environment.GetCommandLineArgs().Any(x => Path.GetExtension(x) == ".dem"))
                ? Environment.GetCommandLineArgs().First(x => Path.GetExtension(x) == ".dem")
                : null;
            if (dropFile == null)
            {
                richTextBox1.Text = @"^ Use demo_file->Open to open a correct .dem file or drop the file here!";
                UpdateForm();
            }
            else
            {
                if ((!File.Exists(dropFile) || Path.GetExtension(dropFile) != ".dem"))
                {
                    CurrentFile = dropFile;
                    richTextBox1.Text = @"Analyzing file...";
                    UpdateForm();
                    CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                    PrintDemoDetails(CurrentDemoFile);
                    Log(Path.GetFileName(CurrentFile + " opened"));
                }
                else
                {
                    toolsToolStripMenuItem.Enabled = false;
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    richTextBox1.Text = @"^ Use demo_file->Open to open a correct .dem file or drop the file here!";
                    UpdateForm();
                }
            }
            #endregion
        }

        public Main(string s)
        {
            InitializeComponent();
            CurrentFile = s;
            SettingsManager(false);
            richTextBox1.Font = MainFont;
            AllowDrop = true;
            netdecodeToolStripMenuItem.Enabled = false;
            statisticsToolStripMenuItem.Enabled = false;
            heatmapGeneratorToolStripMenuItem1.Enabled = false;
            HotkeyTimer.Start();
            if (File.Exists(LogPath))
                File.Delete(LogPath);
            if (CurrentFile != null || (File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) == ".dem"))
            {
                richTextBox1.Text = @"Analyzing file...";
                UpdateForm();
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                PrintDemoDetails(CurrentDemoFile);
                Log(CurrentFile + " opened!");
            }
            else
            {
                richTextBox1.Text = @"^ Use demo_file->Open to open a correct .dem file or drop the file here!";
                UpdateForm();
            }
        }

        private void UpdateForm()
        {
            richTextBox1.Invalidate();
            richTextBox1.Update();
            richTextBox1.Refresh();
            Application.DoEvents();

        }

        #region demo_file ToolStrip stuff
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
            if (CurrentFile != null && File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem")
            {
                if (CurrentDemoFile == null)
                {
                    richTextBox1.Text = @"Analyzing file...";
                    UpdateForm();
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
                            CurrentDemoFile.Sdi.MapName + "-" +
                            $"{stime}" + "-" + CurrentDemoFile.Sdi.ClientName + ".dem");
                        break;
                }
            }
            else
            {
                richTextBox1.Text = @"Please select a file first!";
            }
            
        }

        #endregion

        #region Toolstrip Demo Tools

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

        private void demoTimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dtf = new DemoTimer())
            {
                dtf.ShowDialog(this);
            }
        }

        private void demoDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dd = new Demo_doctor(CurrentFile))
                dd.ShowDialog();
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sf = new Statisctics(CurrentDemoFile.Sdi))
                sf.ShowDialog();
        }

        private void demoVerificationToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var a = new Verification())
                a.ShowDialog();
        }
        #endregion

        #region Overlay
        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem" && CurrentFile != null)
                using (var a = new OverlayForm(CurrentFile))
                    a.ShowDialog();
            else
                MessageBox.Show(@"No file selected please select one to use the overlay!",
                    @"Couldn't open overlay!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ols = new OverlaySettings())
            {
                ols.ShowDialog(this);
            }
            SettingsManager(false); //BUG: Saving
        }

        public void UpdateOverLaySettings(Font f, Color c)
        {
            OverlayFont = f;
            OverLayColor = c;
        }

        #endregion

        #region Settings and hotkeys stuff

        private void hotkeysToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var a = new Hotkey.Hotkey())
                a.ShowDialog();
            SettingsManager(false);
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
                    //TODO: Add other mboxs
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
                        UpdateForm();
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
                        var parser = new FileIniDataParser();
                        var cvt = new FontConverter();
                        IniData iniD = parser.ReadFile(SettingsPath);
                        iniD["SETTINGS"]["main_font"] = cvt.ConvertToString(fd.Font);
                        parser.WriteFile(SettingsPath, iniD);
                    }
                }
                if (CurrentFile == null || (!File.Exists(CurrentFile) || Path.GetExtension(CurrentFile) != ".dem"))
                    return;
                richTextBox1.Text = @"Analyzing file...";
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                PrintDemoDetails(CurrentDemoFile);
                Log(Path.GetFileName(CurrentFile + " rescanned for font change."));
                Log("Font changed");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        public static void SettingsManager(bool reset)
        {
            try
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +"VolvoWrench"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                 "VolvoWrench");
                }
                if (!File.Exists(SettingsPath) || reset)
                {
                    #region Config text
                File.WriteAllLines(SettingsPath,
                    new[]
                    {
$@"[VolvoWrench config file]
; Here are your hotkeys for the program.
; Every line which starts with a semicolon(';') is ignored.
; Please keep that in mind.
[HOTKEYS]
;You can modify these keys. Google VKEY
demo_popup=0x70
overlay_exit=0x71
overlay_rescan=0x72
[SETTINGS]
Language=EN
main_font=Microsoft Sans Serif; 8pt
overlay_font=Microsoft Sans Serif; 8pt
overlay_color={Color.Orange.A}:{Color.Orange.R}:{Color.Orange.B}:{Color.Orange.G}
[OVERLAY_SOURCE]
demo_protocol=0
net_protocol=0
server_name=0
client_name=0
map_name=0
game_directory=0
measured_time=1
measured_ticks=1
save_flag=1
autosave_flag=0
[OVERLAY_HLSOOE]
demo_protocol=0
net_protocol=0
server_name=0
client_name=0
map_name=0
game_directory=0
measured_time=1
measured_ticks=1
save_flag=1
autosave_flag=0
[OVERLAY_L4D2BRANCH]
demo_protocol=0
net_protocol=0
server_name=0
client_name=0
map_name=1
game_directory=0
measured_time=1
measured_ticks=1
save_flag=1
autosave_flag=0
adjusted_time=1
adjusted_ticks=1
[OVERLAY_GOLDSOURCE]
demo_protocol=0
net_protocol=0
server_name=0
client_name=0
map_name=0
game_directory=0
measured_time=1
measured_ticks=1
highest_fps=0
lowest_fps=0
average_fps=0
lowest_msec=0
highest_msec=0
average_msec=0"});
                #endregion
                    DemoPopupKey = 0x70; //F1
                    OverLayExitKey = 0x71;
                    OverLayRescanKey = 0x72;

                }
                else
                {
                    var cvt = new FontConverter();
                    var parser = new FileIniDataParser();
                    var data = parser.ReadFile(SettingsPath);
                    DemoPopupKey = ToInt32(data["HOTKEYS"]["demo_popup"],16);
                    OverLayExitKey = ToInt32(data["HOTKEYS"]["overlay_exit"],16);
                    OverLayRescanKey = ToInt32(data["HOTKEYS"]["overlay_rescan"],16);
                    OverLayExitKey = ToInt32(data["HOTKEYS"]["overlay_rescan"],16);
                    OverlayFont =  cvt.ConvertFromString(data["SETTINGS"]["overlay_font"]) as Font;
                    MainFont =  cvt.ConvertFromString(data["SETTINGS"]["main_font"]) as Font;
                    var colorstring = data["SETTINGS"]["overlay_color"].Split(':');
                    OverLayColor = Color.FromArgb(
                        ToInt32(colorstring[0]),
                        ToInt32(colorstring[1]),
                        ToInt32(colorstring[2]),
                        ToInt32(colorstring[3]));
                }
            }
            catch (Exception e)
            {
                File.Delete(SettingsPath);
                Log("Error when parsing settings: " + e.Message);
            }
        }
        #endregion

        #region DragDrop file

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            richTextBox1.Text = @"╔══════════════════════════════╗
║     Drop the file here       ║
╚══════════════════════════════╝";
            UpdateForm();
        }

        private void Main_DragLeave(object sender, EventArgs e)
        {
            richTextBox1.Text = @"^ Use demo_file->Open to open a correct .dem file or drop the file here!";
            UpdateForm();
            PrintDemoDetails(CurrentDemoFile);
            UpdateForm();
        }

        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            var dropfiles = (string[]) e.Data.GetData(DataFormats.FileDrop);
            var dropfile = (dropfiles.Any(x => Path.GetExtension(x) == ".dem"))
                ? dropfiles.First(x => Path.GetExtension(x) == ".dem")
                : null;
            if (dropfile != null) CurrentFile = dropfile;
            if (CurrentFile != null && (File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem"))
            {
                richTextBox1.Text = @"Analyzing file...";
                UpdateForm();
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                PrintDemoDetails(CurrentDemoFile);
                Log(Path.GetFileName(CurrentFile) + " opened!");
            }
            else
            {
                richTextBox1.Text = @"Bad file!";
            }
        }
        #endregion

        public void PrintDemoDetails(CrossParseResult demo)
        {
            if (demo != null)
            {
                Text = @"VolvoWrench - " + Path.GetFileName(CurrentFile);
                richTextBox1.Text = @"Demo parsed!";
                StripEnabler(demo);
                #region Print
                switch (demo.Type)
                {
                    case Parseresult.UnsupportedFile:
                        richTextBox1.Text = @"Unsupported file!";
                        UpdateForm();
                        break;
                    case Parseresult.GoldSource:
                        if (demo.GsDemoInfo.ParsingErrors.ToArray().Length > 0)
                        {
                            richTextBox1.Text = @"Error while parsing goldsource demo: 
";
                            UpdateForm();
                            foreach (var err in demo.GsDemoInfo.ParsingErrors)
                            {
                                richTextBox1.AppendText("\n" + err);
                                UpdateForm();
                            }
                            if (MessageBox.Show(@"Would you like to open the demo doctor?", @"Demo errors detected!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                using (var dd = new Demo_doctor(CurrentFile))
                                    dd.ShowDialog();    
                        }
                        else
                        {
                            float frametimeMin = 0f, frametimeMax = 0f;
                            var frametimeSum = 0.0;
                            var count = 0;
                            int msecMin = 0, msecMax = 0;
                            long msecSum = 0;
                            var first = true;
                            foreach (var f in from entry in demo.GsDemoInfo.DirectoryEntries from frame in entry.Frames where (int) frame.Key.Type < 2 || (int) frame.Key.Type > 9 select (GoldSource.NetMsgFrame) frame.Value)
                            {
                                frametimeSum += f.RParms.Frametime;
                                msecSum += f.UCmd.Msec;
                                count++;

                                if (first)
                                {
                                    first = false;
                                    frametimeMin = f.RParms.Frametime;
                                    frametimeMax = f.RParms.Frametime;
                                    msecMin = f.UCmd.Msec;
                                    msecMax = f.UCmd.Msec;
                                }
                                else
                                {
                                    frametimeMin = Math.Min(frametimeMin, f.RParms.Frametime);
                                    frametimeMax = Math.Max(frametimeMax, f.RParms.Frametime);
                                    msecMin = Math.Min(msecMin, f.UCmd.Msec);
                                    msecMax = Math.Max(msecMax, f.UCmd.Msec);
                                }
                            }
                            richTextBox1.Text =
                                $@"Analyzed GoldSource engine demo file ({demo.GsDemoInfo.Header.GameDir}):
----------------------------------------------------------
Demo protocol:              {demo.GsDemoInfo.Header.DemoProtocol}
Net protocol:               {demo.GsDemoInfo.Header.NetProtocol}
Directory Offset:           {demo.GsDemoInfo.Header.DirectoryOffset}
Map name:                   {demo.GsDemoInfo.Header.MapName}
Game directory:             {demo.GsDemoInfo.Header.GameDir}
Length in seconds:          {demo.GsDemoInfo.DirectoryEntries.Sum(x => x.TrackTime).ToString("n3")}s
Frame count:                {demo.GsDemoInfo.DirectoryEntries.Sum(x => x.FrameCount)}

Higest FPS:                 {(1/frametimeMin).ToString("N2")}
Lowest FPS:                 {(1/ frametimeMax).ToString("N2")}
Average FPS:                {(count/frametimeSum).ToString("N2")}
Lowest msec:                {(1000.0 / msecMax).ToString("N2")} FPS
Highest msec:               {(1000.0 / msecMin).ToString("N2")} FPS
Average msec:               {(1000.0 / (msecSum / (double)count)).ToString("N2")} FPS
----------------------------------------------------------";
                        }
                        UpdateForm();
                        break;
                    case Parseresult.Hlsooe:
                        if (demo.HlsooeDemoInfo.ParsingErrors.ToArray().Length > 0)
                        {
                            richTextBox1.Text = @"Error while parsing HLSOOE demo: 
";
                            UpdateForm();
                            foreach (var err in demo.HlsooeDemoInfo.ParsingErrors)
                            {
                                richTextBox1.AppendText(err);
                                UpdateForm();
                            }
                        }
                        else
                        {
                            richTextBox1.Text = $@"Analyzed HLS:OOE engine demo file ({demo.HlsooeDemoInfo.Header.GameDirectory}):
----------------------------------------------------------
Demo protocol:              {demo.HlsooeDemoInfo.Header.DemoProtocol}
Net protocol:               {demo.HlsooeDemoInfo.Header.Netprotocol}
Directory offset:           {demo.HlsooeDemoInfo.Header.DirectoryOffset}
Map name:                   {demo.HlsooeDemoInfo.Header.MapName}
Game directory:             {demo.HlsooeDemoInfo.Header.GameDirectory}
Length in seconds:          {(demo.HlsooeDemoInfo.DirectoryEntries.Last().Frames.LastOrDefault().Key.Tick) *0.015}s
Tick count:                 {(demo.HlsooeDemoInfo.DirectoryEntries.SkipWhile(x => x.FrameCount < 1).Max(x=>x.Frames.Max(y => y.Key.Tick)))}
----------------------------------------------------------";
                            UpdateForm();
                            foreach (
                                var flag in
                                    demo.HlsooeDemoInfo.DirectoryEntries.SelectMany(
                                        demoDirectoryEntry => demoDirectoryEntry.Flags))
                                richTextBox1.AppendText(flag.Value.Command + " at " + flag.Key.Frame + " -> " +
                                                        (flag.Key.Frame*0.015).ToString("n3") + "s");
                        }
                        break;
                    case Parseresult.Source:
                        if (demo.Sdi.ParsingErrors.ToArray().Length > 0)
                        {
                            richTextBox1.Text = @"Error while parsing Source engine demo: ";
                            UpdateForm();
                            foreach (var err in demo.Sdi.ParsingErrors)
                            {
                                richTextBox1.AppendText("\n"+err);
                                UpdateForm();
                            }
                        }
                        else
                        {
                            richTextBox1.Text =
                                $@"Analyzed source engine demo file ({demo.Sdi.GameDirectory}):
----------------------------------------------------------
Demo protocol:              {demo.Sdi.DemoProtocol}
Net protocol:               {demo.Sdi.NetProtocol}
Server name:                {demo.Sdi.ServerName}
Client name:                {demo.Sdi.ClientName}
Map name:                   {demo.Sdi.MapName}
Game directory:             {demo.Sdi.GameDirectory}
Playback seconds:           {demo.Sdi.Seconds.ToString("n3")}s
Playback tick:              {demo.Sdi.TickCount}
Frame count:                {demo.Sdi.FrameCount}

Measured time:              {(demo.Sdi.Messages.SkipWhile(x=> x.Type != SourceParser.MessageType.SyncTick).Max(x => x.Tick)*0.015).ToString("n3")}s
Measured ticks:             {demo.Sdi.Messages.SkipWhile(x => x.Type != SourceParser.MessageType.SyncTick).Max(x => x.Tick)}
----------------------------------------------------------";
                            UpdateForm();
                            foreach (var f in demo.Sdi.Flags)
                                switch (f.Name)
                                {
                                    case "#SAVE#":
                                        richTextBox1.AppendText($"\n#SAVE# flag at Tick: {f.Tick} -> {f.Time}s");
                                        UpdateForm();
                                        HighlightLastLine(richTextBox1, Color.Yellow);
                                        UpdateForm();
                                        break;
                                    case "autosave":
                                        richTextBox1.AppendText($"\nAutosave at Tick: {f.Tick} -> {f.Time}s");
                                        UpdateForm();
                                        HighlightLastLine(richTextBox1, Color.DarkOrange);
                                        UpdateForm();
                                        break;
                                }
                        }
                        break;
                    case Parseresult.Portal:
                    case Parseresult.L4D2Branch:
                        if (demo.L4D2BranchInfo.Parsingerrors.ToArray().Length > 0)
                        {
                            richTextBox1.Text = @"Error while parsing L4D2Branch demo:";
                            UpdateForm();
                            foreach (var err in demo.L4D2BranchInfo.Parsingerrors)
                            {
                                richTextBox1.AppendText("\n" + err);
                                UpdateForm();
                            }
                        }
                        else
                        {
                            richTextBox1.Text = $@"Analyzed L4D2Branch demo file ({demo.L4D2BranchInfo.Header.GameDirectory}):
----------------------------------------------------------
Protocol:           {demo.L4D2BranchInfo.Header.Protocol}
Network protocol:   {demo.L4D2BranchInfo.Header.NetworkProtocol}
Server name:        {demo.L4D2BranchInfo.Header.ServerName}
Client name:        {demo.L4D2BranchInfo.Header.ClientName}
Mapname:            {demo.L4D2BranchInfo.Header.MapName}
GameDir:            {demo.L4D2BranchInfo.Header.GameDirectory}
Playbacktime:       {(demo.L4D2BranchInfo.Header.PlaybackTime).ToString("n3")}s
Playbackticks:      {demo.L4D2BranchInfo.Header.PlaybackTicks}
Playbackframes:     {demo.L4D2BranchInfo.Header.PlaybackFrames}
Signonlength:       {demo.L4D2BranchInfo.Header.SignonLength}

Tickrate:           {demo.L4D2BranchInfo.Header.PlaybackTicks / demo.L4D2BranchInfo.Header.PlaybackTime}
Start tick:         {demo.L4D2BranchInfo.PortalDemoInfo?.StartAdjustmentTick}
Type:               {demo.L4D2BranchInfo.PortalDemoInfo?.StartAdjustmentType}
End tick:           {demo.L4D2BranchInfo.PortalDemoInfo?.EndAdjustmentTick}
Type:               {demo.L4D2BranchInfo.PortalDemoInfo?.EndAdjustmentType}

Adjusted time:      {demo.L4D2BranchInfo.PortalDemoInfo?.AdjustedTicks* (1f / (demo.L4D2BranchInfo.Header.PlaybackTicks / demo.L4D2BranchInfo.Header.PlaybackTime)) + "s"}
Adjusted ticks:     {demo.L4D2BranchInfo.PortalDemoInfo?.AdjustedTicks}
----------------------------------------------------------
";                       
                        }
                        break;
                }
                #endregion
            }
            else
            {
                richTextBox1.Text = @"Not a demo! ";
            }
        }

        public void RescanFile()
        {
            if (CurrentFile != null && (File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem"))
                {
                    richTextBox1.Text = @"Analyzing file...";
                    UpdateForm();
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
            textControl.SelectionColor = Color.White;
            var lastLineText = textControl.Lines[richTextBox1.Lines.Count() - 1];
            var lastLineStartIndex = richTextBox1.Text.LastIndexOf(lastLineText, StringComparison.Ordinal);
            textControl.SelectionStart = lastLineStartIndex;
            textControl.SelectionLength = textControl.Text.Length - 1;
            textControl.SelectionColor = highlightColor;
            textControl.DeselectAll();
            textControl.Select(textControl.Text.Length, 0);
        }

        public static Task Log(string s)
        {
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "VolvoWrench"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +"VolvoWrench");
            return WriteTextAsync(LogPath, ("\n"+DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") + " " +
                 $"[{WindowsIdentity.GetCurrent().Name}]" + ": " + s));
        }

        static async Task WriteTextAsync(string filePath, string text)
        {
            var encodedText = Encoding.Unicode.GetBytes(text);
            using (var sourceStream = new FileStream(filePath,FileMode.Append, FileAccess.Write, FileShare.None,bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsManager(false);
            if (MessageBox.Show(@"Are you sure you would like to close the program?",@"Confirm!",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
              e.Cancel = true;
            }
        }

        public void StripEnabler(CrossParseResult cpr)
        {
            switch (cpr.Type)
            {
                case Parseresult.UnsupportedFile:
                    netdecodeToolStripMenuItem.Enabled = false;
                    statisticsToolStripMenuItem.Enabled = false;
                    heatmapGeneratorToolStripMenuItem1.Enabled = false;
                    break;
                case Parseresult.Hlsooe:
                case Parseresult.GoldSource:
                    netdecodeToolStripMenuItem.Enabled = false;
                    statisticsToolStripMenuItem.Enabled = false;
                    heatmapGeneratorToolStripMenuItem1.Enabled = false;
                    break;
                case Parseresult.Portal:
                case Parseresult.Source:
                    netdecodeToolStripMenuItem.Enabled = true;
                    statisticsToolStripMenuItem.Enabled = true;
                    heatmapGeneratorToolStripMenuItem1.Enabled = true;
                    break;
                case Parseresult.L4D2Branch:
                    netdecodeToolStripMenuItem.Enabled = false;
                    statisticsToolStripMenuItem.Enabled = false;
                    heatmapGeneratorToolStripMenuItem1.Enabled = false;
                    break;
            }
        }
    }
}