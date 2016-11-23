using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using VolvoWrench.Demo_stuff.GoldSource;
using VolvoWrench.Demo_stuff.L4D2Branch;
using VolvoWrench.Demo_stuff.Source;

namespace VolvoWrench.Demo_stuff
{
    public enum Parseresult
    {
        UnsupportedFile,
        GoldSource,
        Hlsooe,
        L4D2Branch,
        Portal,
        Source
    }

    public class CrossParseResult
    {
        public SourceDemoInfo Sdi;
        public GoldSourceDemoInfo GsDemoInfo;
        public GoldSourceDemoInfoHlsooe HlsooeDemoInfo;
        public L4D2BranchDemoInfo L4D2BranchInfo;
        public Parseresult Type;

        public CrossParseResult(GoldSourceDemoInfoHlsooe gsdi, Parseresult pr, SourceDemoInfo sdi, GoldSourceDemoInfo gd, L4D2BranchDemoInfo lbi)
        {
            HlsooeDemoInfo = gsdi;
            Type = pr;
            Sdi = sdi;
            GsDemoInfo = gd;
            L4D2BranchInfo = lbi;
        }

        public CrossParseResult() { }
    }

    public static class CrossDemoParser
    {
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
            return cpr;
        }

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