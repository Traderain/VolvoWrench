using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VolvoWrench.Netdec
{
    internal class Frame
    {
        private int tick;
        private float time;
        private string type;

        private enum DemoFrameType
        {
            STARTUP_PACKET = 1,
            NETWORK_PACKET = 2,
            JUMPTIME = 3,
            CONSOLE_COMMAND = 4,
            USERCMD = 5,
            STRINGTABLES = 6,
            NETWORK_DATA_TABLE = 7,
            NEXT_SECTION = 8
        }
    }

    internal class GoldSourceDemo
    {
        public List<DemoDirectoryEntry> DirectoryEntries;
        public int EntryCount;
        public DemoHeader header;
        public List<string> ParsingErrors;

        public struct DemoHeader
        {
            public int demoProtocol;
            public int DirectoryOffset;
            public string GameDirectory;
            public string magic;
            public string MapName;
            public int netprotocol;
        }

        public struct DemoDirectoryEntry
        {
            public int filelength;
            public int frameCount;
            public int offset;
            public float playbackTime;
            public int type;
        }
    }

    public struct GoldSourceDemoInfo
    {
    }

    internal class GoldSourceParser
    {
        private void ParseDemo() //Add error out
        {
            var GDemo = new GoldSourceDemo();
            using (var br = new BinaryReader(new FileStream("file.dem", FileMode.Open))) //TODO: Add file
            {
                var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).Trim('\0');
                if (mw == "HLDEMO")
                {
                    GDemo.header.demoProtocol = br.ReadInt32();
                    GDemo.header.netprotocol = br.ReadInt32();
                    GDemo.header.MapName = Encoding.ASCII.GetString(br.ReadBytes(260));
                    GDemo.header.GameDirectory = Encoding.ASCII.GetString(br.ReadBytes(260));
                    GDemo.header.DirectoryOffset = br.ReadInt32();
                    //Header Parsed... now we read the directory entries
                    br.BaseStream.Seek(GDemo.header.DirectoryOffset, SeekOrigin.Begin);
                    GDemo.EntryCount = br.ReadInt32();
                    for (var i = 0; i < GDemo.EntryCount; i++)
                    {
                        var tempvar = new GoldSourceDemo.DemoDirectoryEntry();
                        tempvar.type = br.ReadInt32();
                        tempvar.playbackTime = br.ReadSingle();
                        tempvar.frameCount = br.ReadInt32();
                        tempvar.offset = br.ReadInt32();
                        tempvar.filelength = br.ReadInt32();
                    }
                    //Demo directory entries parsed... now we parse the frames.
                    foreach (var entry in GDemo.DirectoryEntries)
                    {
                        //TODO: Finnish this.
                    }
                }
                else
                {
                    GDemo.ParsingErrors.Add("Non goldsource demo file");
                    br.Close();
                }
            }
        }
    }
}