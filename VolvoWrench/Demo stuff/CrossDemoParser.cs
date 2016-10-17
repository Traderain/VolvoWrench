using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VolvoWrench.Demo_stuff
{
    public enum Parseresult
    {
        UnsupportedFile,
        GoldSource,
        Hlsooe,
        Source
    }

    public class CrossParseResult
    {
        public SourceDemoInfo Sdi;
        public GoldSourceDemoInfo GsDemoInfo;
        public GoldSourceDemoInfoHlsooe HlsooeDemoInfo;
        public Parseresult Type;

        public CrossParseResult(GoldSourceDemoInfoHlsooe gsdi, Parseresult pr, SourceDemoInfo sdi, GoldSourceDemoInfo gd)
        {
            HlsooeDemoInfo = gsdi;
            Type = pr;
            Sdi = sdi;
            GsDemoInfo = gd;
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
                    Stream cfs = File.Open(filename, FileMode.Open);
                    var a = new SourceParser(cfs);
                    cpr.Sdi = a.Info;
                    cfs.Close();
                    break;
                case Parseresult.Hlsooe:
                    cpr.Type = Parseresult.Hlsooe;
                    cpr.HlsooeDemoInfo = GoldSourceParser.ParseDemoHlsooe(filename);
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
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).TrimEnd('\0');
                switch (mw)
                {
                    case "HLDEMO": return br.ReadByte() <= 2 ? Parseresult.Hlsooe : Parseresult.GoldSource;
                    case "HL2DEMO": return Parseresult.Source;
                    default: return Parseresult.UnsupportedFile;
                }
            }
        }
    }
}