using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoreLinq;

namespace VolvoWrench.Demo_stuff.GoldSource
{
    public sealed partial class Verification : Form
    {
        public List<string> DemopathList; 
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

        public void Verify(string[] files)
        {
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
            if (Df.Any(x => x.Value.Type != Parseresult.GoldSource))
                MessageBox.Show(@"Only goldsource supported");
            else
            {
                mrtb.Text = "";
                mrtb.AppendText("" + "\n");
                mrtb.AppendText("Parsed demos. Results:" + "\n");
                mrtb.AppendText("General stats:" + "\n");
                var frametimeMax = new List<float>();
                var frametimeMin = new List<float>();
                var frametimeSum = new List<double>();
                var msecMin = new List<double>();
                var msecMax = new List<double>();
                var avgmsec = new List<double>();
                foreach (var d in Df)
                {
                    float ftm = 0f, ftmx = 0f;
                    var fts = 0.0;
                    var count = 0;
                    int mm = 0, mmx = 0;
                    long msecSum = 0;
                    var first = true;
                    foreach (var f in from entry in d.Value.GsDemoInfo.DirectoryEntries
                                      from frame in entry.Frames
                                      where (int)frame.Key.Type < 2 || (int)frame.Key.Type > 9
                                      select (GoldSource.NetMsgFrame)frame.Value)
                    {
                        fts += f.RParms.Frametime;
                        msecSum += f.UCmd.Msec;
                        count++;

                        if (first)
                        {
                            first = false;
                            ftm = f.RParms.Frametime;
                            ftmx = f.RParms.Frametime;
                            mm = f.UCmd.Msec;
                            mmx = f.UCmd.Msec;
                        }
                        else
                        {
                            ftm = Math.Min(ftm, f.RParms.Frametime);
                            ftmx = Math.Max(ftmx, f.RParms.Frametime);
                            mm = Math.Min(mm, f.UCmd.Msec);
                            mmx = Math.Max(mmx, f.UCmd.Msec);
                        }
                    }
                    frametimeMax.Add(1 / ftmx);
                    frametimeMin.Add(1 / ftm);
                    frametimeSum.Add(count / fts);
                    msecMin.Add(1000.0 / mm);
                    msecMax.Add(1000.0 / mmx);
                    avgmsec.Add(1000.0 / (msecSum / (double)count));
                }
                mrtb.AppendText($@"
Highest FPS:                {(frametimeMin.Max()).ToString("N2")}
Lowest FPS:                 {(frametimeMin.Min()).ToString("N2")}
Average FPS:                {frametimeSum.Average().ToString("N2")}
Lowest msec:                {(msecMax.Min()).ToString("N2")} FPS
Highest msec:               {(msecMin.Max()).ToString("N2")} FPS
Average msec:               {avgmsec.Average().ToString("N2")} FPS

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