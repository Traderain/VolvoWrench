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
        public GoldSourceDemoInfoHlsooe HlsooeDemoInfo;
        public GoldSourceDemoInfo GsDemoInfo;
        public SourceDemoInfo Sdi;
        public Parseresult Res = Parseresult.UnsupportedFile;

        public CrossParse(GoldSourceDemoInfoHlsooe gsdi, Parseresult pr, SourceDemoInfo sdi,GoldSourceDemoInfo gd)
        {
            this.HlsooeDemoInfo = gsdi;
            this.Res = pr;
            this.Sdi = sdi;
            this.GsDemoInfo = gd;
        }
        public CrossParse() {}
    }

    public static class CrossDemoParser
    {
        public static CrossParse Parse(string filename)
        {
            var cpr = new CrossParse();
            switch (CheckDemoType(filename))
            {
                case Parseresult.GoldSource:
                {
                    GoldSourceParser.ParseDemoHlsooe(filename);
                    break;
                }
                case Parseresult.UnsupportedFile:
                {
                    Main.Log("Demotype check resulted in an unsupported file.");
                    break;
                }
                case Parseresult.Source:
                {
                    Stream cfs = File.Open(filename, FileMode.Open);
                    var a = new SourceParser(cfs);
                    cpr.Sdi = a.Info;
                    cfs.Close();
                    break;
                }
                default:
                {
                    Main.Log(
                        "No idea how the fuck did this happen but default happened at switch(CheckDemoType(filename))");
                    break;
                }
            }
            return new CrossParse();
        }

        public static Parseresult CheckDemoType(string file)
        {
            Parseresult dt;
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).TrimEnd('\0');
                switch (mw)
                {
                    case "HLDEMO":
                        dt = br.ReadByte() <= 2 ? Parseresult.Hlsooe : Parseresult.GoldSource;
                        break;
                    case "HL2DEMO":
                        dt = Parseresult.Source;
                        break;
                    default:
                        dt = Parseresult.UnsupportedFile;
                        break;
                }
            }
            return dt;
        }
    }
}