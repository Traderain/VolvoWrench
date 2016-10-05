using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Media3D;
using static System.Windows.Media.Media3D.Point3D;

namespace VolvoWrench.Demo_stuff
{
        public enum DemoFrameType
        {
            StartupPacket = 1,
            NetworkPacket = 2,
            Jumptime = 3,
            ConsoleCommand = 4,
            Usercmd = 5,
            Stringtables = 6,
            NetworkDataTable = 7,
            NextSection = 8
        };

        public class DemoFrame
        {
            public DemoFrameType Type;
            public float Time;
            public int Frame;
        }
        public struct ConsoleCommandFrame
        {
            public string Command;
        };
        public struct StringTablesFrame
        {
            public List<string> Data;
        };
        public struct NetworkDataTableFrame
        {
             public List<string> Data;
        };
        public struct UserCmdFrame
        {
            public int OutgoingSequence;
            public int Slot;
            public List<string> Data;
        };
        public struct NetMsgFrame
        {
            public struct DemoInfo
            {
                public int Flags;
                public Point3D ViewOrigins;
                public Point3D ViewAngles;
                public Point3D LocalViewAngles;

                public Point3D ViewOrigin2;
                public Point3D ViewAngles2;
                public Point3D LocalViewAngles2;
            };
            public int IncomingSequence;
            public int IncomingAcknowledged;
            public int IncomingReliableAcknowledged;
            public int IncomingReliableSequence;
            public int OutgoingSequence;
            public int ReliableSequence;
            public int LastReliableSequence;
            public List<string> Msg;
        };

    internal class GoldSourceDemo
    {
        public List<DemoDirectoryEntry> DirectoryEntries;
        public int EntryCount;
        public DemoHeader Header;
        public List<string> ParsingErrors;

        public struct DemoHeader
        {
            public int DemoProtocol;
            public int DirectoryOffset;
            public string GameDirectory;
            public string Magic;
            public string MapName;
            public int Netprotocol;
        }

        public struct DemoDirectoryEntry
        {
            public int Filelength;
            public int FrameCount;
            public int Offset;
            public float PlaybackTime;
            public int Type;
        }
    }

    public struct GoldSourceDemoInfo
    {
        //TODO: fix
    }

    public class GoldSourceParser
    {
        public static GoldSourceDemoInfo ParseDemo(string s) //Add error out
        {
            var gDemo = new GoldSourceDemo();
            using (var br = new BinaryReader(new FileStream(s, FileMode.Open))) //TODO: Add file
            {
                var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).Trim('\0');
                if (mw == "HLDEMO")
                {
                    gDemo.Header.DemoProtocol = br.ReadInt32();
                    gDemo.Header.Netprotocol = br.ReadInt32();
                    gDemo.Header.MapName = Encoding.ASCII.GetString(br.ReadBytes(260));
                    gDemo.Header.GameDirectory = Encoding.ASCII.GetString(br.ReadBytes(260));
                    gDemo.Header.DirectoryOffset = br.ReadInt32();
                    //Header Parsed... now we read the directory entries
                    br.BaseStream.Seek(gDemo.Header.DirectoryOffset, SeekOrigin.Begin);
                    gDemo.EntryCount = br.ReadInt32();
                    for (var i = 0; i < gDemo.EntryCount; i++)
                    {
                        var tempvar = new GoldSourceDemo.DemoDirectoryEntry();
                        tempvar.Type = br.ReadInt32();
                        tempvar.PlaybackTime = br.ReadSingle();
                        tempvar.FrameCount = br.ReadInt32();
                        tempvar.Offset = br.ReadInt32();
                        tempvar.Filelength = br.ReadInt32();
                    }
                    //Demo directory entries parsed... now we parse the frames.
                    foreach (var entry in gDemo.DirectoryEntries)
                    {
                        //TODO: Finnish this.
                    }
                }
                else
                {
                    gDemo.ParsingErrors.Add("Non goldsource demo file");
                    br.Close();
                }
            }
            return new GoldSourceDemoInfo(); //TODO: Actually return something
        }
    }
}