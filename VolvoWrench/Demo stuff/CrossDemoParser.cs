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

    public class CrossParse
    {
        public SourceDemoInfo Sdi;
        public GoldSourceDemoInfo GsDemoInfo;
        public GoldSourceDemoInfoHlsooe HlsooeDemoInfo;
        public Parseresult Res;

        public CrossParse(GoldSourceDemoInfoHlsooe gsdi, Parseresult pr, SourceDemoInfo sdi, GoldSourceDemoInfo gd)
        {
            HlsooeDemoInfo = gsdi;
            Res = pr;
            Sdi = sdi;
            GsDemoInfo = gd;
        }

        public CrossParse() { }
    }

    public static class CrossDemoParser
    {
        public static CrossParse Parse(string filename)
        {
            var cpr = new CrossParse();
            switch (CheckDemoType(filename))
            {
                case Parseresult.GoldSource:
                    cpr.Res = Parseresult.GoldSource;
                    cpr.GsDemoInfo = GoldSourceParser.ParseGoldSourceDemo(filename);
                    break;
                case Parseresult.UnsupportedFile:
                    cpr.Res = Parseresult.UnsupportedFile;
                    Main.Log("Demotype check resulted in an unsupported file.");
                    break;
                case Parseresult.Source:
                    cpr.Res = Parseresult.Source;
                    Stream cfs = File.Open(filename, FileMode.Open);
                    var a = new SourceParser(cfs);
                    cpr.Sdi = a.Info;
                    cfs.Close();
                    break;
                case Parseresult.Hlsooe:
                    cpr.Res = Parseresult.Hlsooe;
                    cpr.HlsooeDemoInfo = GoldSourceParser.ParseDemoHlsooe(filename);
                    break;
                default:
                    cpr.Res = Parseresult.UnsupportedFile;
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