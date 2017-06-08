using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoreLinq;
using VolvoWrench.Demo_stuff.GoldSource;

namespace VolvoWrench.Demo_Stuff.GoldSource
{
    /// <summary>
    ///     This is a form to aid goldsource run verification
    /// </summary>
    public sealed partial class Verification : Form
    {
        public static Dictionary<string, CrossParseResult> Df = new Dictionary<string, CrossParseResult>();

        /// <summary>
        ///     This list contains the paths to the demos
        /// </summary>
        public List<string> DemopathList;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Verification()
        {
            InitializeComponent();
            DemopathList = new List<string>();
            this.mrtb.DragDrop += Verification_DragDrop;
            this.mrtb.DragEnter += Verification_DragEnter;
            this.mrtb.AllowDrop = true;
            AllowDrop = true;
        }

        private void openDemosToolStripMenuItem_Click(object sender, EventArgs e)
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
            Close();
        }

        private void demostartCommandToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DemopathList.ToArray().Length <= 32)
                Clipboard.SetText("startdemos " + DemopathList
                    .Select(Path.GetFileNameWithoutExtension)
                    .OrderBy(x => int.Parse(Regex.Match(x + "0", @"\d+").Value))
                    .ToList()
                    .Aggregate((c, n) => c + " " + n));
            else
            {
                Clipboard.SetText(DemopathList
                    .Select(Path.GetFileNameWithoutExtension)
                    .OrderBy(x => int.Parse(Regex.Match(x + "0", @"\d+").Value))
                    .Batch(32)
                    .Aggregate(string.Empty, (x, y) => x + ";startdemos " + (y.Aggregate((c, n) => c + " " + n)))
                    .Substring(1));
            }
            using (var ni = new NotifyIcon())
            {
                ni.Icon = SystemIcons.Exclamation;
                ni.Visible = true;
                ni.ShowBalloonTip(5000, "VolvoWrench", "Demo names copied to clipboard", ToolTipIcon.Info);
            }
        }

        /// <summary>
        ///     This is the actuall verification method
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
" + brokendemos, @"Error!", MessageBoxButtons.OK);
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
Highest FPS:                {(1/Df.Select(x => x.Value).ToList().Min(y => y.GsDemoInfo.AditionalStats.FrametimeMin)).ToString("N2")}
Lowest FPS:                 {(1/Df.Select(x => x.Value).ToList().Max(y => y.GsDemoInfo.AditionalStats.FrametimeMax)).ToString("N2")}
Average FPS:                {(Df.Select(z => z.Value).ToList().Average(k => k.GsDemoInfo.AditionalStats.Count/k.GsDemoInfo.AditionalStats.FrametimeSum)).ToString("N2")}
Lowest msec:                {(1000.0/Df.Select(x => x.Value).ToList().Min(y => y.GsDemoInfo.AditionalStats.MsecMin)).ToString("N2")} FPS
Highest msec:               {(1000.0/Df.Select(x => x.Value).ToList().Max(y => y.GsDemoInfo.AditionalStats.MsecMax)).ToString("N2")} FPS
Average msec:               {(Df.Select(x => x.Value).ToList().Average(y => y.GsDemoInfo.AditionalStats.MsecSum/(double) y.GsDemoInfo.AditionalStats.Count)).ToString("N2")} FPS

Total time of the demos:    {Df.Sum(x => x.Value.GsDemoInfo.DirectoryEntries.Sum(y => y.TrackTime))}s
Human readable time:        {TimeSpan.FromSeconds(Df.Sum(x => x.Value.GsDemoInfo.DirectoryEntries.Sum(y => y.TrackTime))).ToString("g")}" + "\n\n");

                mrtb.AppendText("Demo cheat check:" + "\n");
                foreach (var dem in Df)
                {
                    if (dem.Value.GsDemoInfo.Cheats.Count > 0)
                    {
                        mrtb.AppendText("Possible cheats:\n");
                        foreach (var cheat in dem.Value.GsDemoInfo.Cheats.Distinct())
                        {
                            mrtb.AppendText("\t" + cheat + "\n");
                        }
                    }
                    mrtb.AppendText(Path.GetFileName(dem.Key) + " -> " + dem.Value.GsDemoInfo.Header.MapName);
                    mrtb.AppendText("\nBXTData:");
                    mrtb.AppendText("\n" + ParseBxtData(dem));
                }
            }
        }

        /// <summary>
        /// Parses the bxt data into treenodes
        /// </summary>
        /// <param name="Infos"></param>
        public string ParseBxtData(KeyValuePair<string, CrossParseResult> info)
        {
            string ret = "\n";
            const string bxtVersion = "7e9089359bf7bee6e460d5652298bc2f8788749b-CLEAN based on may-31-2017";
            const string bxxVersion = "";
            var cvarRules = new Dictionary<string, string>()
            {
                {"BXT_AUTOJUMP", "0"},
                {"BXT_BHOPCAP", "0"},
                {"BXT_FADE_REMOVE", "0"},
                {"BXT_HUD_DISTANCE", "0"},
                {"BXT_HUD_ENTITY_HP", "0"},
                {"BXT_HUD_ORIGIN", "0"},
                {"BXT_HUD_SELFGAUSS","0"},
                {"BXT_HUD_USEABLES", "0"},
                {"BXT_HUD_VELOCITY", "0"},
                {"BXT_HUD_VISIBLE_LANDMARKS", "0"},
                {"BXT_SHOW_HIDDEN_ENTITIES", "0"},
                {"BXT_SHOW_TRIGGERS", "0"},
                {"CHASE_ACTIVE", "0"},
                {"CL_ANGLESPEEDKEY", "0.67"},
                {"CL_BACKSPEED", "400"},
                {"CL_FORWARDSPEED", "400"},
                {"CL_PITCHDOWN", "89"},
                {"CL_PITCHSPEED", "225"},
                {"CL_PITCHUP", "89"},
                {"CL_SIDESPEED", "400"},
                {"CL_UPSPEED", "320"},
                {"CL_YAWSPEED", "210"},
                {"GL_MONOLIGHTS", "0"},
                {"HOST_FRAMERATE", "0"},
                {"HOST_SPEEDS", "0"},
                {"R_DRAWENTITIES", "1"},
                {"R_FULLBRIGHT", "0"},
                {"SND_SHOW", "0"},
                {"SV_AIRACCELERATE", "10"},
                {"SV_CHEATS", "0"},
                {"SV_FRICTION", "4"},
                {"SV_GRAVITY", "800"},
                {"SV_WATERACCELERATE", "10"},
                {"SV_WATERFRICTION", "1"},
                {"S_SHOW", "0"}
            };
            var demonode = new TreeNode(Path.GetFileName(info.Key)) { ForeColor = Color.White };
            for (int i = 0; i < info.Value.GsDemoInfo.IncludedBXtData.Count; i++)
            {
                bool jp = false, jm = false, dp = false, dm = false;
                var datanode = new TreeNode("\nBXT Data Frame [" + i + "]") { ForeColor = Color.White };
                for (int index = 0; index < info.Value.GsDemoInfo.IncludedBXtData[i].Objects.Count; index++)
                {
                    KeyValuePair<Bxt.RuntimeDataType, Bxt.BXTData> t = info.Value.GsDemoInfo.IncludedBXtData[i].Objects[index];
                    switch (t.Key)
                    {
                        case Bxt.RuntimeDataType.VERSION_INFO:
                            {
                                ret +=("\t" + "BXT Version: " + ((((Bxt.VersionInfo)t.Value).bxt_version == bxtVersion) ? "Good" : ("INVALID=" + ((Bxt.VersionInfo)t.Value).bxt_version)) + "\n");
                                ret +=("\t" + "Game Version: " + ((Bxt.VersionInfo)t.Value).build_number + "\n");
                                datanode.Nodes.Add(new TreeNode("Version info")
                                {
                                    ForeColor = Color.White,
                                    Nodes =
                                    {
                                        new TreeNode("Game version: " + ((Bxt.VersionInfo) t.Value).build_number),
                                        new TreeNode("BXT Version: " + ((Bxt.VersionInfo) t.Value).bxt_version)
                                    }
                                });
                                break;
                            }
                        case Bxt.RuntimeDataType.CVAR_VALUES:
                            {
                                foreach (var cvar in ((Bxt.CVarValues)t.Value).CVars.Where(cvar => cvarRules.ContainsKey(cvar.Key.ToUpper())).Where(cvar => cvarRules[cvar.Key.ToUpper()] != cvar.Value.ToUpper()))
                                {
                                    ret +=("\t" + "Illegal Cvar: " + cvar.Key + " " + cvar.Value + "\n");
                                }
                                var cvarnode = new TreeNode("Cvars [" + ((Bxt.CVarValues)t.Value).CVars.Count + "]")
                                {
                                    ForeColor = Color.White
                                };
                                cvarnode.Nodes.AddRange(
                                    ((Bxt.CVarValues)t.Value).CVars.Select(
                                        x => new TreeNode(x.Key + " " + x.Value) { ForeColor = Color.White }).ToArray());
                                datanode.Nodes.Add(cvarnode);
                                break;
                            }
                        case Bxt.RuntimeDataType.TIME:
                            {
                                if (i+1 == info.Value.GsDemoInfo.IncludedBXtData.Count)
                                {
                                    ret +=("\t" + "Demo bxt time: " + ((Bxt.Time)t.Value).ToString() + "\n");
                                }
                                datanode.Nodes.Add(new TreeNode("Time: " + ((Bxt.Time)t.Value).ToString())
                                {
                                    ForeColor = Color.White
                                });
                                break;
                            }
                        case Bxt.RuntimeDataType.BOUND_COMMAND:
                            {
                                if (((Bxt.BoundCommand)t.Value).command.ToUpper().Contains("+JUMP"))
                                    jp = true;
                                if (((Bxt.BoundCommand)t.Value).command.ToUpper().Contains("-JUMP"))
                                    jm = true;
                                if (((Bxt.BoundCommand)t.Value).command.ToUpper().Contains("+DUCK"))
                                    dp = true;
                                if (((Bxt.BoundCommand)t.Value).command.ToUpper().Contains("-DUCK"))
                                    dm = true;
                                if (((Bxt.BoundCommand) t.Value).command.ToUpper().Contains(";"))
                                {
                                    ret +=("\t" + "Possible script: " + ((Bxt.BoundCommand)t.Value).command + "\n");
                                }
                                datanode.Nodes.Add(new TreeNode("Bound command: " + ((Bxt.BoundCommand)t.Value).command)
                                {
                                    ForeColor = Color.White
                                });
                                break;
                            }
                        case Bxt.RuntimeDataType.ALIAS_EXPANSION:
                            {
                                ret +=("\t" + "Alias [" + ((Bxt.AliasExpansion)t.Value).name + "]: " + ((Bxt.AliasExpansion)t.Value).command + "\n");
                                datanode.Nodes.Add( new TreeNode("Alias name: " + ((Bxt.AliasExpansion)t.Value).name + "Command:" + ((Bxt.AliasExpansion)t.Value).command) { ForeColor = Color.White });
                                break;
                            }
                        case Bxt.RuntimeDataType.SCRIPT_EXECUTION:
                            {
                                datanode.Nodes.Add(new TreeNode("Script: " + ((Bxt.ScriptExecution)t.Value).filename)
                                {
                                    ForeColor = Color.White,
                                    Nodes =
                                    {
                                        new TreeNode(((Bxt.ScriptExecution) t.Value).contents) {ForeColor = Color.White}
                                    }
                                });
                                break;
                            }
                        case Bxt.RuntimeDataType.COMMAND_EXECUTION:
                            {
                                if (((Bxt.CommandExecution) t.Value).command.ToUpper().Contains("+JUMP"))
                                    if (jp)
                                        jp = false;
                                    else
                                        ret += ("\t" + "Possible autojump: " + ((Bxt.CommandExecution) t.Value).command + "\n");
                                if (((Bxt.CommandExecution)t.Value).command.ToUpper().Contains("-JUMP"))
                                    if (jm)
                                        jm = false;
                                    else
                                        ret += ("\t" + "Possible autojump: " + ((Bxt.CommandExecution)t.Value).command + "\n");
                                if (((Bxt.CommandExecution)t.Value).command.ToUpper().Contains("+DUCK"))
                                    if (dp)
                                        dp = false;
                                    else
                                        ret += ("\t" + "Possible autojump: " + ((Bxt.CommandExecution)t.Value).command + "\n");
                                if (((Bxt.CommandExecution)t.Value).command.ToUpper().Contains("-DUCK"))
                                    if (dm)
                                        dm = false;
                                    else
                                        ret += ("\t" + "Possible autojump: " + ((Bxt.CommandExecution)t.Value).command + "\n");
                                if (((Bxt.CommandExecution) t.Value).command.ToUpper().ToUpper().Contains("BXT"))
                                {
                                    ret +=("\t" + "Disallowed bxt command: " + ((Bxt.CommandExecution)t.Value).command + "\n");
                                }
                                datanode.Nodes.Add(new TreeNode("Command: " + ((Bxt.CommandExecution)t.Value).command)
                                {
                                    ForeColor = Color.White
                                });
                                break;
                            }
                        case Bxt.RuntimeDataType.GAME_END_MARKER:
                            {
                                datanode.Nodes.Add(new TreeNode("-- GAME END --") { ForeColor = Color.White });
                                break;
                            }
                        case Bxt.RuntimeDataType.LOADED_MODULES:
                            {
                                var modulesnode = new TreeNode("Loaded modules [" + ((Bxt.LoadedModules)t.Value).filenames.Count + "]") { ForeColor = Color.White };
                                modulesnode.Nodes.AddRange(((Bxt.LoadedModules)t.Value).filenames.Select(x => new TreeNode(x) { ForeColor = Color.White }).ToArray());
                                datanode.Nodes.Add(modulesnode);
                                break;
                            }
                        case Bxt.RuntimeDataType.CUSTOM_TRIGGER_COMMAND:
                            {
                                var trigger = (Bxt.CustomTriggerCommand)t.Value;
                                ret +=("\t" + $"Costum trigger X1:{trigger.corner_max.X} Y1:{trigger.corner_max.Y} Z1:{trigger.corner_max.Z} X2:{trigger.corner_min.X} Y2:{trigger.corner_min.Y} Z2:{trigger.corner_min.Z}" + "\n");
                                datanode.Nodes.Add(new TreeNode($"Costum trigger X1:{trigger.corner_max.X} Y1:{trigger.corner_max.Y} Z1:{trigger.corner_max.Z} X2:{trigger.corner_min.X} Y2:{trigger.corner_min.Y} Z2:{trigger.corner_min.Z}")
                                {
                                    ForeColor = Color.White,
                                    Nodes = { new TreeNode("Command: " + trigger.command) { ForeColor = Color.White } }
                                });
                                break;
                            }
                        default:
                            {
                                datanode.Nodes.Add(new TreeNode("Invalid bxt data!") { ForeColor = Color.Red });
                                break;
                            }
                    }
                }
                demonode.Nodes.Add(datanode);
            }
            BXTTreeView.Nodes.Add(demonode);
            return ret;
        }

        private void Verification_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void Verification_DragDrop(object sender, DragEventArgs e)
        {
            var dropfiles = (string[]) e.Data.GetData(DataFormats.FileDrop);
            Verify(dropfiles);
            e.Effect = DragDropEffects.None;
        }
    }
}