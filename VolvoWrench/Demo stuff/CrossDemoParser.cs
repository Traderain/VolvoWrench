using VolvoWrench.Netdec;

namespace VolvoWrench.Demo_stuff
{
    public enum parseresult
    {
        unsupported_file,
        GoldSource,
        Source
    }

    public struct CROSS_PARSE
    {
        public GoldSourceDemoInfo GDI;
        public parseresult res;
        public SourceDemoInfo SDI;
    }

    internal class CrossDemoParser
    {
        public CROSS_PARSE Parse(string filename)
        {
            switch (CheckDemoType(filename))
            {
                case parseresult.GoldSource:
                {
                    break;
                }
                case parseresult.unsupported_file:
                {
                    break;
                }
                case parseresult.Source:
                {
                    break;
                }
                default:
                {
                    Main.Log(
                        "No idea how the fuck did this happen but default happened at switch(CheckDemoType(filename))");
                    break;
                }
            }
            //TODO: Implement this.
            return new CROSS_PARSE();
        }

        public parseresult CheckDemoType(string filename)
        {
            //TODO: Implement this.
            return parseresult.unsupported_file;
        }
    }
}