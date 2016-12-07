using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolvoWrench.Demo_stuff.GoldSource;
using VolvoWrench.Demo_stuff.L4D2Branch;
using VolvoWrench.Demo_stuff.Source;

namespace VolvoWrench.Demo_stuff
{
    /// <summary>
    /// Type of the demo
    /// </summary>
    public enum Parseresult
    {
        /// <summary>
        /// Not a demo/Unsupported
        /// </summary>
        UnsupportedFile,
        /// <summary>
        /// GoldSource demo
        /// </summary>
        GoldSource,
        /// <summary>
        /// HLS:OOE Demo
        /// </summary>
        Hlsooe,
        /// <summary>
        /// Demo from the L4D2 Branch eg.: Portal 2,Left 4 Dead 2, Alien Swarm
        /// </summary>
        L4D2Branch,
        /// <summary>
        /// Portal 1 demo
        /// </summary>
        Portal,
        /// <summary>
        /// Source engine demo
        /// </summary>
        Source
    }

    /// <summary>
    /// Data about the demo
    /// </summary>
    public class CrossParseResult
    {
        /// <summary>
        /// The data about the Source engine demo
        /// </summary>
        public SourceDemoInfo Sdi;
        /// <summary>
        /// The data about the GoldSource demo
        /// </summary>
        public GoldSourceDemoInfo GsDemoInfo;
        /// <summary>
        /// The data about the HLS:OOE demo
        /// </summary>
        public GoldSourceDemoInfoHlsooe HlsooeDemoInfo;
        /// <summary>
        /// The data about the L4D2 Branch demo
        /// </summary>
        public L4D2BranchDemoInfo L4D2BranchInfo;
        /// <summary>
        /// Type of the demo
        /// </summary>
        public Parseresult Type;

        /// <summary>
        /// Full constructor
        /// </summary>
        public CrossParseResult(GoldSourceDemoInfoHlsooe gsdi, Parseresult pr, SourceDemoInfo sdi, GoldSourceDemoInfo gd, L4D2BranchDemoInfo lbi)
        {
            HlsooeDemoInfo = gsdi;
            Type = pr;
            Sdi = sdi;
            GsDemoInfo = gd;
            L4D2BranchInfo = lbi;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public CrossParseResult() { }
    }

    /// <summary>
    /// Checking the type of the demo and parsing it accordingly
    /// </summary>
    public static class CrossDemoParser
    {
        /// <summary>
        /// Parsing multiple demos asynchronously
        /// </summary>
        /// <param name="filenames">String array with the paths to the files</param>
        /// <returns></returns>
        public static CrossParseResult[] MultiDemoParse(string[] filenames)
        {
            var results = new List<CrossParseResult> {new CrossParseResult()};
            //filenames.Select(AsyncParse).ToArray();
            return results.ToArray();
        }

        /// <summary>
        /// This does an asyncronous demo parse.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static async Task<CrossParseResult> AsyncParse(string filepath)
        {
            return await new Task<CrossParseResult>(() => Parse(filepath));
        }

        /// <summary>
        /// Parses a demo file from any engine
        /// </summary>
        /// <param name="filename">Path to the file</param>
        /// <returns></returns>
        public static CrossParseResult Parse(string filename)
        {
            var cpr = new CrossParseResult();
            

            switch (CheckDemoType(filename))
            {
                case Parseresult.GoldSource:
                    cpr.Type = Parseresult.GoldSource;
                    cpr.GsDemoInfo = GoldSourceParser.ParseGoldSourceDemo(filename);
                    break;
                case Parseresult.UnsupportedFile:
                    cpr.Type = Parseresult.UnsupportedFile;
                    Main.Log("Demotype check resulted in an unsupported file.");
                    break;
                case Parseresult.Source:
                    cpr.Type = Parseresult.Source;
                        var a = new SourceParser(new MemoryStream(File.ReadAllBytes(filename)));
                        cpr.Sdi = a.Info;
                    if (cpr.Sdi.GameDirectory == "portal")
                    {
                        cpr.Type = Parseresult.Portal;
                        var lp = new L4D2BranchParser();
                        cpr.L4D2BranchInfo = lp.Parse(filename);
                    }
                    break;
                case Parseresult.Hlsooe:
                    cpr.Type = Parseresult.Hlsooe;
                    cpr.HlsooeDemoInfo = GoldSourceParser.ParseDemoHlsooe(filename);
                    break;
                case Parseresult.L4D2Branch:
                    cpr.Type = Parseresult.L4D2Branch;
                    var l = new L4D2BranchParser();
                    cpr.L4D2BranchInfo = l.Parse(filename);
                    break;
                default:
                    cpr.Type = Parseresult.UnsupportedFile;
                    Main.Log(
                        "No idea how the fuck did this happen but default happened at switch(CheckDemoType(filename))");
                    break;
            }
            if(cpr.Type == Parseresult.GoldSource)
            if (cpr.GsDemoInfo.ParsingErrors.Count > 0)
            {
                if (MessageBox.Show(@"Would you like to open the demo doctor?", @"Demo errors detected!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    using (var dd = new DemoDoctor(filename))
                        dd.ShowDialog();              
            }
            return cpr;
        }

        /// <summary>
        /// Checks the demo type a Parseresult is returned
        /// </summary>
        /// <param name="file">The path to the demo file to check</param>
        /// <returns></returns>
        public static Parseresult CheckDemoType(string file)
        {
            var attr = new FileInfo(file);
            if (attr.Length < 540)
            {
                return Parseresult.UnsupportedFile;
            }
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read,FileShare.Read))
            using (var br = new BinaryReader(fs))
            {
                var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).TrimEnd('\0');
                switch (mw)
                {
                    case "HLDEMO": return br.ReadByte() <= 2 ? Parseresult.Hlsooe : Parseresult.GoldSource;
                    case "HL2DEMO": return br.ReadInt32() < 4 ? Parseresult.Source: Parseresult.L4D2Branch;
                    default: return Parseresult.UnsupportedFile;
                }
            }
        }
    }
}