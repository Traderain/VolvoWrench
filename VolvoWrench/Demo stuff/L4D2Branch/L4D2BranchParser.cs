using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PortalAdjust.Demo;
using VolvoWrench.Demo_stuff.L4D2Branch.BitStreamUtil;
using DemoParser = VolvoWrench.Demo_stuff.L4D2Branch.CSGODemoInfo.DemoParser;

namespace VolvoWrench.Demo_stuff.L4D2Branch
{
    public class DemoHeader
    {
        private const int MaxOspath = 260;
        public string Filestamp { get; private set; } // Should be HL2DEMO
        public int Protocol { get; private set; } // Should be DEMO_PROTOCOL (4)
        public int NetworkProtocol { get; private set; } // Should be PROTOCOL_VERSION
        public string ServerName { get; private set; } // Name of server
        public string ClientName { get; private set; } // Name of client who recorded the game
        public string MapName { get; private set; } // Name of map
        public string GameDirectory { get; private set; } // Name of game directory (com_gamedir)
        public float PlaybackTime { get; private set; } // Time of track
        public int PlaybackTicks { get; private set; } // # of ticks in track
        public int PlaybackFrames { get; private set; } // # of frames in track
        public int SignonLength { get; private set; } // length of sigondata in bytes

        public static DemoHeader ParseFrom(IBitStream reader)
        {
            return new DemoHeader
            {
                Filestamp = reader.ReadCString(8),
                Protocol = reader.ReadSignedInt(32),
                NetworkProtocol = Math.Abs(reader.ReadSignedInt(32)),
                ServerName = reader.ReadCString(MaxOspath),
                ClientName = reader.ReadCString(MaxOspath),
                MapName = reader.ReadCString(MaxOspath),
                GameDirectory = reader.ReadCString(MaxOspath),
                PlaybackTime = Math.Abs(reader.ReadFloat()),
                PlaybackTicks = Math.Abs(reader.ReadSignedInt(32)),
                PlaybackFrames = Math.Abs(reader.ReadSignedInt(32)),
                SignonLength = Math.Abs(reader.ReadSignedInt(32))
            };
        }
    }

    public class L4D2BranchDemoInfo
    {
        public DemoParser CsgoDemoInfo;
        public Category DemoType;
        public DemoHeader Header;
        public List<string> Parsingerrors;
        public DemoParseResult PortalDemoInfo;
    }

    internal class L4D2BranchParser
    {
        public L4D2BranchDemoInfo Parse(string filename)
        {
            var info = new L4D2BranchDemoInfo {Parsingerrors = new List<string>()};
            IBitStream demoStream = new BitArrayStream(File.ReadAllBytes(filename));
            info.Header = DemoHeader.ParseFrom(demoStream);
            if (Category.Portal.Maps.Contains(info.Header.MapName))
                info.DemoType = Category.Portal;
            if (Category.Portal2Coop.Maps.Contains(info.Header.MapName))
                info.DemoType = Category.Portal2Coop;
            if (Category.Portal2CoopCourse6.Maps.Contains(info.Header.MapName))
                info.DemoType = Category.Portal2Sp;
            if(Category.Portal2Sp.Maps.Contains(info.Header.MapName))
                info.DemoType = Category.Portal2Sp;
            if (!Category.Portal.Maps.Contains(info.Header.MapName) &&
                !Category.Portal2Coop.Maps.Contains(info.Header.MapName) &&
                !Category.Portal2CoopCourse6.Maps.Contains(info.Header.MapName) &&
                !Category.Portal2Sp.Maps.Contains(info.Header.MapName))
            {
                if (info.Header.GameDirectory == "csgo")
                {
                    info.DemoType = Category.CSGO;
                    info.CsgoDemoInfo = CsgoDemoParser(filename);
                }
                else
                {
                    info.DemoType = Category.Uncommon;
                }
                info.PortalDemoInfo = new DemoParseResult();
            }
            else
            {
                info.PortalDemoInfo = PortalAdjust.Demo.DemoParser.ParseDemo(filename);
            }
            return info;
        }

        public static DemoParser CsgoDemoParser(string file)
        {
            var csgodemo = new DemoParser(File.OpenRead(file));
            csgodemo.ParseHeader();
            csgodemo.ParseToEnd();
            return csgodemo;
        }
    }
}