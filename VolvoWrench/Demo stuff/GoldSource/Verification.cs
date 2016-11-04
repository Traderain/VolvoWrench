using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VolvoWrench.Demo_stuff
{
    public partial class Verification : Form
    {
        public Verification()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog
            {
                Filter = @"Demo files (.dem) | *.dem",
                Multiselect = true
            };
            var df = new Dictionary<string, CrossParseResult>();
            if (of.ShowDialog() == DialogResult.OK)
            {
                mrtb.Text = @"Please wait. Parsing demos...";
                mrtb.Invalidate();
                mrtb.Update();
                mrtb.Refresh();
                Application.DoEvents();
                foreach (var file in of.FileNames.Where(file => File.Exists(file) && Path.GetExtension(file) == ".dem"))
                    df.Add(file, CrossDemoParser.Parse(file));
                if (df.Any(x => x.Value.Type != Parseresult.GoldSource))
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
                    foreach (var d in df)
                    {
                        float ftm = 0f, ftmx = 0f;
                        var fts = 0.0;
                        var count = 0;
                        int mm = 0, mmx = 0;
                        long msecSum = 0;
                        var first = true;
                        foreach (var f in from entry in d.Value.GsDemoInfo.DirectoryEntries
                            from frame in entry.Frames
                            where (int) frame.Key.Type < 2 || (int) frame.Key.Type > 9
                            select (GoldSource.NetMsgFrame) frame.Value)
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
                        frametimeMax.Add(1/ftmx);
                        frametimeMin.Add(1/ftm);
                        frametimeSum.Add(count/fts);
                        msecMin.Add(1000.0/mm);
                        msecMax.Add(1000.0/mmx);
                        avgmsec.Add(1000.0/(msecSum/(double) count));
                    }
                    mrtb.AppendText(
                        $@"
Highest FPS:                {(frametimeMin.Max()).ToString("N2")}
Lowest FPS:                 {(frametimeMin.Min()).ToString("N2")}
Average FPS:                {frametimeSum.Average().ToString("N2")}
Lowest msec:                {(msecMax.Min()).ToString("N2")} FPS
Highest msec:               {(msecMin.Max()).ToString("N2")} FPS
Average msec:               {avgmsec.Average().ToString("N2")} FPS

Total time of the demos:    {df.Sum(x => x.Value.GsDemoInfo.DirectoryEntries.Sum(y => y.TrackTime))}s" + "\n\n");
                    mrtb.AppendText("Demo cheat check:" + "\n");
                    foreach (var dem in df)
                    {
                        mrtb.AppendText(Path.GetFileName(dem.Key) + " -> " + dem.Value.GsDemoInfo.Header.MapName + "\n");
                        foreach (var f in dem.Value.GsDemoInfo.DirectoryEntries.SelectMany(entry =>
                                        (from frame in entry.Frames.Where(
                                                x => x.Key.Type == GoldSource.DemoFrameType.ConsoleCommand)
                                            select (GoldSource.ConsoleCommandFrame) frame.Value into f
                                            let cheats = new List<string>
                                            {
                                                "+lookup",
                                                "+lookdown",
                                                "+left",
                                                "+right"
                                            } where cheats.Contains(f.Command) select f))) {mrtb.AppendText(f.Command + "\n");}
                    }
                }
            }
        }
    }
}