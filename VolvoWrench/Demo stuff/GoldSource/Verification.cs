using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MoreLinq;

namespace VolvoWrench.Demo_stuff.GoldSource
{
    /// <summary>
    /// This is a form to aid goldsource run verification
    /// </summary>
    public sealed partial class Verification : Form
    {
        /// <summary>
        /// This list contains the paths to the demos
        /// </summary>
        public List<string> DemopathList;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Verification()
        {
            InitializeComponent();
            DemopathList = new List<string>();
            AllowDrop = true;
        }

        public static Dictionary<string,CrossParseResult> Df = new Dictionary<string, CrossParseResult>();

        private void button1_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog
            {
                Filter = @"Demo files (.dem) | *.dem",
                Multiselect = true
            };
            
            if (of.ShowDialog() == DialogResult.OK && of.FileNames.Length > 1)
            {
                //CrossDemoParser.MultiDemoParse(of.FileNames);
                Verify(of.FileNames);
            }
            else
            {
                mrtb.Text = @"No file selected/bad file/only 1 file selected!";
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void demostartCommandToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DemopathList.ToArray().Length <= 32)
                Clipboard.SetText("startdemos " + DemopathList
                    .Select(Path.GetFileNameWithoutExtension)
                    .OrderBy(x => int.Parse(Regex.Match(x+"0", @"\d+").Value))
                    .ToList()
                    .Aggregate((c, n) => c + " " + n));
            else
            {
                Clipboard.SetText(DemopathList
                    .Select(Path.GetFileNameWithoutExtension)
                    .OrderBy(x => int.Parse(Regex.Match(x+"0", @"\d+").Value))
                    .Batch(32)
                    .Aggregate(String.Empty,(x,y) => x + ";startdemos " + (y.Aggregate((c,n) => c + " " + n))).Substring(1));

            }
            using (var ni = new NotifyIcon())
            {
                ni.Icon = SystemIcons.Exclamation;
                ni.Visible = true;
                ni.ShowBalloonTip(5000, "VolvoWrench", "Demo names copied to clipboard", ToolTipIcon.Info);
            }
        }

        /// <summary>
        /// This is the actuall verification method     
        /// </summary>
        /// <param name="files">The paths of the files</param>
        public void Verify(string[] files)
        {
            Df.Clear();
            mrtb.Text = $@"Please wait. Parsing demos... 0/{files.Length}";
            mrtb.Invalidate();
            mrtb.Update();
            mrtb.Refresh();
            Application.DoEvents();
            var curr = 0;
            foreach (var dt in files.Where(file => File.Exists(file) && Path.GetExtension(file) == ".dem"))
            {
                DemopathList.Add(dt);
                Df.Add(dt, CrossDemoParser.Parse(dt)); //If someone bothers me that its slow make it async.
                mrtb.Text = $@"Please wait. Parsing demos... {curr++}/{files.Length}";
                mrtb.Invalidate();
                mrtb.Update();
                mrtb.Refresh();
                Application.DoEvents();
            }
            if (Df.Any(x => x.Value.GsDemoInfo.ParsingErrors.Count > 0))
            {
                var brokendemos = Df.Where(x => x.Value.GsDemoInfo.ParsingErrors.Count > 0)
                    .ToList()
                    .Aggregate("", (c, n) => c += "\n" + n.Key);
                MessageBox.Show(@"Broken demos found:
" + brokendemos,@"Error!",MessageBoxButtons.OK);
                Main.Log("Broken demos when verification: " + brokendemos);
                mrtb.Text = @"Please fix the demos then reselect the files!";
                return;
            }
            if (Df.Any(x => x.Value.Type != Parseresult.GoldSource))
                MessageBox.Show(@"Only goldsource supported");
            else
            {
                mrtb.Text = "";
                mrtb.AppendText("" + "\n");
                mrtb.AppendText("Parsed demos. Results:" + "\n");
                mrtb.AppendText("General stats:" + "\n");
                mrtb.AppendText($@"
Highest FPS:                {(1/Df.Select(x=> x.Value).ToList().Min(y => y.GsDemoInfo.AditionalStats.FrametimeMin)).ToString("N2")}
Lowest FPS:                 {(1/Df.Select(x=> x.Value).ToList().Max(y => y.GsDemoInfo.AditionalStats.FrametimeMax)).ToString("N2")}
Average FPS:                {(Df.Select(z => z.Value).ToList().Average(k => k.GsDemoInfo.AditionalStats.Count/k.GsDemoInfo.AditionalStats.FrametimeSum)).ToString("N2")}
Lowest msec:                {(1000.0/Df.Select(x => x.Value).ToList().Min(y => y.GsDemoInfo.AditionalStats.MsecMin)).ToString("N2")} FPS
Highest msec:               {(1000.0/Df.Select(x => x.Value).ToList().Max(y => y.GsDemoInfo.AditionalStats.MsecMax)).ToString("N2")} FPS
Average msec:               {(Df.Select(x => x.Value).ToList().Average(y => y.GsDemoInfo.AditionalStats.MsecSum / (double)y.GsDemoInfo.AditionalStats.Count)).ToString("N2")} FPS

Total time of the demos:    {Df.Sum(x => x.Value.GsDemoInfo.DirectoryEntries.Sum(y => y.TrackTime))}s" + "\n\n");
                mrtb.AppendText("Demo cheat check:" + "\n");
                foreach (var dem in Df)
                {
                    mrtb.AppendText(Path.GetFileName(dem.Key) + " -> " + dem.Value.GsDemoInfo.Header.MapName + "\n");
                    foreach (var f in dem.Value.GsDemoInfo.DirectoryEntries.SelectMany(entry =>
                                    (from frame in entry.Frames.Where(
                                            x => x.Key.Type == GoldSource.DemoFrameType.ConsoleCommand)
                                     select (GoldSource.ConsoleCommandFrame)frame.Value into f
                                     let cheats = new List<string>
                                        {
                                                "+lookup",
                                                "+lookdown",
                                                "+left",
                                                "+right" //TODO: When yalter is not lazy and adds the anticheat frames add them here.
                                     }
                                     where cheats.Contains(f.Command)
                                     select f))) { mrtb.AppendText(f.Command + "\n"); }
                }
            }
        }

        private void Verification_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Verification_DragDrop(object sender, DragEventArgs e)
        {
            var dropfiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            Verify(dropfiles);
        }

        private void Verification_DragLeave(object sender, EventArgs e)
        {
            
        }
    }
}