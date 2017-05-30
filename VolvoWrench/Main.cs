using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using Ookii.Dialogs;
using VolvoWrench.Demo_stuff.GoldSource;
using VolvoWrench.Demo_Stuff;
using VolvoWrench.Demo_Stuff.GoldSource;
using VolvoWrench.Demo_Stuff.L4D2Branch.PortalStuff;
using VolvoWrench.Demo_Stuff.Source;
using VolvoWrench.Hotkey;
using VolvoWrench.MapStuff;
using VolvoWrench.Overlay;
using VolvoWrench.SaveStuff;
using static System.Convert;

namespace VolvoWrench
{
    /// <summary>
    ///     The main form
    /// </summary>
    public sealed partial class Main : Form
    {
        /// <summary>
        ///     The path of the log file
        /// </summary>
        public static readonly string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                                "\\" + "VolvoWrench" + "\\" + "VWLog.log";

        /// <summary>
        ///     The path of the settings file
        /// </summary>
        public static readonly string SettingsPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + "VolvoWrench" +
            "\\" + "VWSettings.ini";

        /// <summary>
        /// This is a bool whether the os is running in high perf mode
        /// </summary>
        public bool HighPerf = !SystemInformation.UIEffectsEnabled;

        /// <summary>
        ///     The button we use to open the demo details popup
        /// </summary>
        public static int DemoPopupKey;

        /// <summary>
        ///     The button we use to exit the overlay
        /// </summary>
        public static int OverLayExitKey;

        /// <summary>
        ///     The button we use in the overlay to rescan the demo
        /// </summary>
        public static int OverLayRescanKey;

        /// <summary>
        ///     The font we use in the overlay
        /// </summary>
        public static Font OverlayFont;

        /// <summary>
        ///     The color of the overlay
        /// </summary>
        public static Color OverLayColor;

        /// <summary>
        ///     The main font's richtextbox's font
        /// </summary>
        public static Font MainFont;

        /// <summary>
        ///     The details of the current demo file
        /// </summary>
        public CrossParseResult CurrentDemoFile;

        /// <summary>
        ///     The path to the current demo
        /// </summary>
        public string CurrentFile;

        /// <summary>
        ///     The main constructor with a string param for openwith file thingy
        /// </summary>
        /// <param name="file"></param>
        public Main(string file)
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
            Log("Application loaded!");
            CurrentFile = file;
            if ((File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem"))
            {
                richTextBox1.Text = @"Analyzing file...";
                UpdateForm();
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
                PrintDemoDetails(CurrentDemoFile);
                Log(Path.GetFileName(CurrentFile + " opened"));
            }
            else
            {
                SourceToolsToolStripMenuItem.Enabled = false;
                goldSourceToolsToolStripMenuItem.Enabled = false;
                richTextBox1.Text = @"^ Use demo_file->Open to open a correct .dem file or drop the file here!";
                UpdateForm();
            }
        }

        /// <summary>
        ///     Normal constructor
        /// </summary>
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
                if ((File.Exists(dropFile) && Path.GetExtension(dropFile) == ".dem"))
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
                    SourceToolsToolStripMenuItem.Enabled = false;
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    richTextBox1.Text = @"^ Use demo_file->Open to open a correct .dem file or drop the file here!";
                    UpdateForm();
                }
            }

            #endregion

            Log("Application loaded!");
        }

        /// <summary>
        ///     Method to update the frozen com
        /// </summary>
        private void UpdateForm()
        {
            richTextBox1.Invalidate();
            richTextBox1.Update();
            richTextBox1.Refresh();
            Application.DoEvents();
        }

        #region Map toolstrip item
        private void leveloverviewGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var lf = new Leveloverview())
            {
                Log("Leveloverview form opened.");
                lf.ShowDialog();
            }
        }

        private void warpPlannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Feature not implemented yet!");
        }
        #endregion

        /// <summary>
        ///     Print the details of the demo to the Main richtextbox
        /// </summary>
        /// <param name="demo"></param>
        public void PrintDemoDetails(CrossParseResult demo)
        {
            Log("Demo details printed!");
            if (demo != null)
            {
                Text = @"VolvoWrench - " + Path.GetFileName(CurrentFile);
                PrintDetails(richTextBox1, demo.DisplayData);
                StripEnabler(demo);
            }
            else
            {
                richTextBox1.Text = @"Not a demo! ";
            }
        }

        /// <summary>
        ///     Rescan the demo file.
        /// </summary>
        public void RescanFile()
        {
            if (CurrentFile != null && (File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem"))
            {
                richTextBox1.Text = @"Analyzing file...";
                UpdateForm();
                CurrentDemoFile = CrossDemoParser.Parse(CurrentFile);
            }
            PrintDemoDetails(CurrentDemoFile);
            Log(Path.GetFileName(CurrentFile) + " rescanned.");
        }

        /// <summary>
        ///     This method highlight the last line of the richtextbox
        /// </summary>
        /// <param name="textControl"></param>
        /// <param name="highlightColor"></param>
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
            Log("Last line highlighted!");
        }

        /// <summary>
        ///     This method send a little popup/alert to windows.
        /// </summary>
        /// <param name="msg">This is the message which is sent to the notification system</param>
        public static void Alert(string msg)
        {
            using (var ni = new NotifyIcon())
            {
                ni.Icon = SystemIcons.Exclamation;
                ni.Visible = true;
                ni.ShowBalloonTip(3000, "VolvoWrench", msg, ToolTipIcon.Info);
            }
            Log("Alert shown with message " + msg);
        }

        /// <summary>
        ///     This logs data to our logpath.
        /// </summary>
        /// <param name="s"> The logmessage </param>
        /// <returns></returns>
        public static Task Log(string s)
        {
            try
            {
                if (
                    !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                      "VolvoWrench"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                              "\\" + "VolvoWrench");
                return WriteTextAsync(LogPath,
                    ("\n" + DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") + " " +
                     $"[{WindowsIdentity.GetCurrent().Name}]" + ": " + s));
            }
            catch (Exception)
            {
                return Task.FromResult(0);
            }
        }

        /// <summary>
        /// Log the exception that happened
        /// </summary>
        /// <param name="e">The exception that happened</param>
        /// <returns></returns>
        public static Task Log(Exception e)
        {
            var s = "Error happened! Message: " + e.Message + " Source: " + e.Source + " Stacktrace: " + e.StackTrace +
                       " Innerexception: " + e.InnerException;
            try
            {
                if (
                    !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                      "VolvoWrench"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                              "\\" + "VolvoWrench");
                return WriteTextAsync(LogPath,
                    ("\n" + DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz") + " " +
                     $"[{WindowsIdentity.GetCurrent().Name}]" + ": " + s));
            }
            catch (Exception)
            {
                return Task.FromResult(0);
            }
        }

        /// <summary>
        ///     Async logging
        /// </summary>
        /// <param name="filePath"> The path </param>
        /// <param name="text"> The text to log </param>
        /// <returns></returns>
        private static async Task WriteTextAsync(string filePath, string text)
        {
            var encodedText = Encoding.Unicode.GetBytes(text);
            if (!File.Exists(filePath))
                File.Create(filePath);
            using (
                var sourceStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, 4096,
                    true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
        }

        /// <summary>
        ///     This is the event that happens when out form is closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Log("Application exit!");
                var parser = new FileIniDataParser();
                var iniD = parser.ReadFile(SettingsPath);
                var showexitdialog = ToBoolean(int.Parse(iniD["DIALOG_PREFERENCES"]["exit_dialog"]));
                if (showexitdialog)
                {
                    #region Check for the dont show again

                    var td = new TaskDialog
                    {
                        WindowTitle = @"Warning",
                        Content = @"Are you sure you would like to exit?"
                    };
                    td.Buttons.Add(new TaskDialogButton
                    {
                        ButtonType = ButtonType.No
                    });
                    td.Buttons.Add(new TaskDialogButton
                    {
                        ButtonType = ButtonType.Yes,
                        Default = true
                    });
                    var checkbox = new TaskDialogRadioButton
                    {
                        Text = @"Never show this again!",
                        Checked = false
                    };
                    td.RadioButtons.Add(checkbox);
                    if (td.ShowDialog().ButtonType == ButtonType.No)
                    {
                        e.Cancel = true;
                        Log("Exit cancelled!");
                    }
                    if (td.RadioButtons[0].Checked)
                        iniD["DIALOG_PREFERENCES"]["exit_dialog"] = "0";

                    #endregion
                }
                parser.WriteFile(SettingsPath, iniD);
            }
            catch (Exception ex)
            {
                Log("Terrible error happened at Close!");
                Log(ex.Message + " => " + ex.Source);
            }
        }

        /// <summary>
        /// Prints the demo tuple details to given richtextbox
        /// </summary>
        /// <param name="rtb">Box to print to</param>
        /// <param name="details">Details of the demo</param>
        public static void PrintDetails(RichTextBox rtb, List<Tuple<string, string>> details)
        {
            rtb.Text = "";
            foreach (var detail in details)
            {
                rtb.AppendText(detail.Item1 + detail.Item2 + "\n");
            }
            Log("Printed demo details to richtextbox");
        }

        /// <summary>
        ///     This method is responsible for enabling the correct MenuStrips after demoparsing
        /// </summary>
        /// <param name="cpr"></param>
        public void StripEnabler(CrossParseResult cpr)
        {
            Log("Strips changed!");
            switch (cpr.Type)
            {
                case Parseresult.UnsupportedFile:
                    SourceToolsToolStripMenuItem.Enabled = false;
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    netdecodeToolStripMenuItem.Enabled = false;
                    statisticsToolStripMenuItem.Enabled = false;
                    heatmapGeneratorToolStripMenuItem1.Enabled = false;
                    break;
                case Parseresult.Hlsooe:
                case Parseresult.GoldSource:
                    SourceToolsToolStripMenuItem.Enabled = false;
                    goldSourceToolsToolStripMenuItem.Enabled = true;
                    netdecodeToolStripMenuItem.Enabled = false;
                    statisticsToolStripMenuItem.Enabled = false;
                    heatmapGeneratorToolStripMenuItem1.Enabled = false;
                    break;
                case Parseresult.Portal:
                case Parseresult.Source:
                    SourceToolsToolStripMenuItem.Enabled = true;
                    netdecodeToolStripMenuItem.Enabled = true;
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    statisticsToolStripMenuItem.Enabled = true;
                    heatmapGeneratorToolStripMenuItem1.Enabled = true;
                    break;
                case Parseresult.L4D2Branch:
                    SourceToolsToolStripMenuItem.Enabled = false;
                    netdecodeToolStripMenuItem.Enabled = true;
                    goldSourceToolsToolStripMenuItem.Enabled = false;
                    statisticsToolStripMenuItem.Enabled = false;
                    heatmapGeneratorToolStripMenuItem1.Enabled = false;
                    break;
            }
        }

        #region demo_file ToolStrip stuff

        /// <summary>
        ///     Open save file dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Save analyzer opened!");
            using (var sa = new Saveanalyzerform())
            {
                sa.ShowDialog();
            }
        }

        /// <summary>
        ///     Openfile dialog for selecting a demo file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        ///     Opens the details of the assembly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("About opened!");
            using (var a = new About())
            {
                Log("About");
                a.ShowDialog();
            }
        }

        /// <summary>
        ///     Opens the sourceruns wiki
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sourcerunsWikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Sourceruns wiki opened!");
            Process.Start("https://wiki.sourceruns.org");
        }

        /// <summary>
        ///     Opens sourceruns.org
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sourcerunsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Sourceruns opened!");
            Process.Start("https://sourceruns.org");
        }

        #endregion

        #region Context menu stuff Showlog,Export etc.

        /// <summary>
        ///     Copy all demo data to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Copied demo data to clipboard!");
            Clipboard.SetText(richTextBox1.Text);
            Alert("Demo data copied to clipboard!");
        }

        /// <summary>
        ///     Rescan the demo file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rescanFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RescanFile();
        }

        /// <summary>
        ///     Open logpath's log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log("Log opened");
            if (!File.Exists(LogPath))
            {
                File.Create(LogPath);
                Log("Log file created because it was missing o.O");
            }
            Process.Start(LogPath);
        }

        /// <summary>
        ///     Save demo data as an XML file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportDemoDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var a = new SaveFileDialog {Filter = @"XML Files | *.xml"};
            if (a.ShowDialog() == DialogResult.OK && CurrentDemoFile != null)
            {
                //TODO: This is probably the last thing I will do in the project. :p
                Log("Saved dema data as " + a.FileName);
            }
        }

        /// <summary>
        ///     Rename the demo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
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
                            MessageBox.Show(@"Couldn't rename! There is no file loaded/non-supported file!");
                            Log("Tried to rename file but failed " + CurrentFile);
                            break;
                        case Parseresult.GoldSource:
                            File.Move(CurrentFile,
                                Path.GetDirectoryName(CurrentFile) + "\\" +
                                CurrentDemoFile.GsDemoInfo.Header.MapName + "-" +
                                $"{CurrentDemoFile.GsDemoInfo.DirectoryEntries.Last().TrackTime.ToString("#,0.000")}" + "-" +
                                Environment.UserName + ".dem");
                            break;
                        case Parseresult.Hlsooe:
                            File.Move(CurrentFile,
                                Path.GetDirectoryName(CurrentFile) + "\\" +
                                CurrentDemoFile.HlsooeDemoInfo.Header.MapName + "-" +
                                $"{CurrentDemoFile.HlsooeDemoInfo.DirectoryEntries.Last().PlaybackTime.ToString("#,0.000")}" +
                                "-" + Environment.UserName + ".dem");
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
                        case Parseresult.Portal:
                            File.Move(CurrentFile,
                            Path.GetDirectoryName(CurrentFile) + "\\" +
                            CurrentDemoFile.L4D2BranchInfo.PortalDemoInfo.MapName + "-" +
                            (CurrentDemoFile.L4D2BranchInfo.PortalDemoInfo?.AdjustedTicks * (1f / (CurrentDemoFile.L4D2BranchInfo.Header.PlaybackTicks / CurrentDemoFile.L4D2BranchInfo.Header.PlaybackTime))) + "-" + CurrentDemoFile.L4D2BranchInfo.PortalDemoInfo.PlayerName + ".dem");
                            break;
                        case Parseresult.L4D2Branch:
                            File.Move(CurrentFile,
                            Path.GetDirectoryName(CurrentFile) + "\\" +
                            CurrentDemoFile.L4D2BranchInfo.Header.MapName + "-" +
                            (CurrentDemoFile.L4D2BranchInfo.Header.PlaybackTime) + "-" + CurrentDemoFile.L4D2BranchInfo.Header.ClientName + ".dem");
                            break;
                    }
                    Log("Renamed demo");
                }
                else
                {
                    richTextBox1.Text = @"Please select a file first!";
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion

        #region Toolstrip Demo Tools

        /// <summary>
        ///     Show netdecode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Opens a treeview form to view frames
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frameViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentDemoFile.Type == Parseresult.GoldSource)
                using (var fw = new frmFrameView(CurrentDemoFile.GsDemoInfo))
                    fw.ShowDialog();
        }

        /// <summary>
        ///     Show the demo timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void demoTimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dtf = new DemoTimer())
            {
                dtf.ShowDialog(this);
            }
        }

        /// <summary>
        ///     Show the demo doctor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void demoDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dd = new DemoDoctor(CurrentFile))
                dd.ShowDialog();
        }

        /// <summary>
        ///     Show the statistics tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sf = new Statisctics(CurrentDemoFile.Sdi))
                sf.ShowDialog();
        }

        /// <summary>
        ///     Show the demo verifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void demoVerificationToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllowDrop = false;
            using (var a = new Verification())
                a.ShowDialog();
            AllowDrop = true;
        }

        #endregion

        #region Overlay

        /// <summary>
        ///     Show the overlay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(CurrentFile) && Path.GetExtension(CurrentFile) == ".dem" && CurrentFile != null)
                using (var a = new OverlayForm(CurrentFile))
                {
                    var str = richTextBox1.Text;
                    richTextBox1.Text = @"Overlay launched please switch to your game!";
                    a.ShowDialog();
                    richTextBox1.Text = str;
                }
            else
                MessageBox.Show(@"No file selected please select one to use the overlay!",
                    @"Couldn't open overlay!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        /// <summary>
        ///     Open the OverLaySettings form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ols = new OverlaySettings())
            {
                ols.ShowDialog(this);
            }
            SettingsManager(false);
        }

        #endregion

        #region Settings and hotkeys stuff

        /// <summary>
        ///     Open the hotkey settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hotkeysToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var a = new Hotkey.Hotkey())
                a.ShowDialog();
            SettingsManager(false);
        }

        /// <summary>
        ///     This is the timer we use for hotkey detection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                //MessageBox.Show(GetDemoDetails(CurrentDemoFile)); //TODO: Once the l4d2 parser is done resolve this
            }
        }

        /// <summary>
        ///     Font setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        var iniD = parser.ReadFile(SettingsPath);
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

        /// <summary>
        ///     This method manages our settings
        /// </summary>
        /// <param name="reset">This determines if we want to reset the settings</param>
        public static void SettingsManager(bool reset)
        {
            try
            {
                if (
                    !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" +
                                      "VolvoWrench"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                              "\\" +
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
overlay_color={
                                Color.Orange.A}:{Color.Orange.R}:{Color.Orange.B}:{Color.Orange.G
                                }
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
average_msec=0
[DIALOG_PREFERENCES]
exit_dialog=1"
                        });

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
                    DemoPopupKey = ToInt32(data["HOTKEYS"]["demo_popup"], 16);
                    OverLayExitKey = ToInt32(data["HOTKEYS"]["overlay_exit"], 16);
                    OverLayRescanKey = ToInt32(data["HOTKEYS"]["overlay_rescan"], 16);
                    OverlayFont = cvt.ConvertFromString(data["SETTINGS"]["overlay_font"]) as Font;
                    MainFont = cvt.ConvertFromString(data["SETTINGS"]["main_font"]) as Font;
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

        /// <summary>
        ///     This happens when we enter the from so we set the correct effect and text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            richTextBox1.Text = @"
╔══════════════════════════════╗
║     Drop the file here       ║
╚══════════════════════════════╝";
            UpdateForm();
        }

        /// <summary>
        ///     This happens when we leave the form and we clear the "Drop the file here" text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragLeave(object sender, EventArgs e)
        {
            richTextBox1.Text = @"^ Use demo_file->Open to open a correct .dem file or drop the file here!";
            UpdateForm();
            PrintDemoDetails(CurrentDemoFile);
            UpdateForm();
        }

        /// <summary>
        ///     This happens when we drop the file on the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}