using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoInfo;
using DemoInfo.BitStreamImpl;

namespace VolvoWrench.Demo_stuff
{
    public class DemoHeader
    {
        const int MAX_OSPATH = 260;

        public string Filestamp { get; private set; }       // Should be HL2DEMO
        public int Protocol { get; private set; }       // Should be DEMO_PROTOCOL (4)
        public int NetworkProtocol { get; private set; }				// Should be PROTOCOL_VERSION

        public string ServerName { get; private set; }		            // Name of server
        public string ClientName { get; private set; }		            // Name of client who recorded the game
        public string MapName { get; private set; }			        // Name of map
        public string GameDirectory { get; private set; }	            // Name of game directory (com_gamedir)
        public float PlaybackTime { get; private set; }				// Time of track
        public int PlaybackTicks { get; private set; }				// # of ticks in track
        public int PlaybackFrames { get; private set; }			// # of frames in track
        public int SignonLength { get; private set; }				// length of sigondata in bytes

        public static DemoHeader ParseFrom(IBitStream reader)
        {
            return new DemoHeader()
            {
                Filestamp = reader.ReadCString(8),
                Protocol = reader.ReadSignedInt(32),
                NetworkProtocol = reader.ReadSignedInt(32),
                ServerName = reader.ReadCString(MAX_OSPATH),

                ClientName = reader.ReadCString(MAX_OSPATH),
                MapName = reader.ReadCString(MAX_OSPATH),
                GameDirectory = reader.ReadCString(MAX_OSPATH),
                PlaybackTime = reader.ReadFloat(),

                PlaybackTicks = reader.ReadSignedInt(32),
                PlaybackFrames = reader.ReadSignedInt(32),
                SignonLength = reader.ReadSignedInt(32),
            };
        }
    }

    public class L4D2BranchDemoInfo
    {
        public DemoHeader Header;
        public List<string> parsingerrors;
    }

    class L4D2BranchParser
    {
        public L4D2BranchDemoInfo Parse(string filename)
        {
            L4D2BranchDemoInfo Info = new L4D2BranchDemoInfo();
            Info.parsingerrors = new List<string>();
            IBitStream DemoStream = new BitArrayStream(File.ReadAllBytes(filename));
            Info.Header = DemoHeader.ParseFrom(DemoStream);
            //TODO: Parse msg
            return Info;
        }
    }
}
