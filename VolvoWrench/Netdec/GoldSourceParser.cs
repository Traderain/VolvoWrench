using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VolvoWrench.Netdec
{
    class Frame
    {
        string type;
        float time;
        int tick;
        enum DemoFrameType
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
    class GoldSourceDemo
    {
        public struct DemoHeader
        {
            public string magic;
            public int demoProtocol;
            public int netprotocol;
            public string MapName;
            public string GameDirectory;
            public int DirectoryOffset;
        }
        public struct DemoDirectoryEntry
        {
            public int type;
            public float playbackTime;
            public int offset;
            public int frameCount;
            public int filelength;
        }
        public DemoHeader header;
        public List<string> ParsingErrors;
        public int EntryCount;
        public List<DemoDirectoryEntry> DirectoryEntries;


    }
    class GoldSourceParser
    {
        void ParseDemo()//Add error out
        {
            GoldSourceDemo GDemo = new GoldSourceDemo();
            using (BinaryReader br = new BinaryReader(new FileStream("file.dem", FileMode.Open))) //TODO: Add file
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
                    br.BaseStream.Seek(GDemo.header.DirectoryOffset,SeekOrigin.Begin);
                    GDemo.EntryCount = br.ReadInt32();
                    for (int i = 0; i < GDemo.EntryCount; i++)
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
