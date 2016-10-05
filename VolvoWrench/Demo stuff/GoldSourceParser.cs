using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media.Media3D;

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
            public int tick;
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
        public List<DemoFrame> Frames;
    }

    public struct GoldSourceDemoInfo
    {
        public DemoHeader Header;
        public List<DemoDirectoryEntry> DirectoryEntries;
        public List<string> ParsingErrors;
        //TODO: fix
    }

    public class GoldSourceParser
    {
        public static GoldSourceDemoInfo ParseDemo(string s) //Add error out
        {
            var gDemo = new GoldSourceDemoInfo();
            gDemo.Header = new DemoHeader();
            var EntryCount = 0;
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
                    EntryCount = br.ReadInt32();
                    for (var i = 0; i < EntryCount; i++)
                    {
                        var tempvar = new DemoDirectoryEntry();
                        tempvar.Type = br.ReadInt32();
                        tempvar.PlaybackTime = br.ReadSingle();
                        tempvar.FrameCount = br.ReadInt32();
                        tempvar.Offset = br.ReadInt32();
                        tempvar.Filelength = br.ReadInt32();
                        gDemo.DirectoryEntries.Add(tempvar);

                    }
                    //Demo directory entries parsed... now we parse the frames.
                    foreach (var entry in gDemo.DirectoryEntries)
                    {
                        var nextSectionRead = false;
                        for (var i = 0; i < entry.FrameCount; i++)
                        {
                            var frameType = (DemoFrameType)br.ReadSByte();
                            var time = br.ReadSingle();
                            var tick = br.ReadInt32();
                            if (!nextSectionRead)
                            {
                                switch (frameType)
                                {
                                    case DemoFrameType.StartupPacket:
                                        break;
                                    case DemoFrameType.NetworkPacket:
                                        break;
                                    case DemoFrameType.Jumptime:
                                        break;
                                    case DemoFrameType.ConsoleCommand:
                                        break;
                                    case DemoFrameType.Usercmd:
                                        break;
                                    case DemoFrameType.Stringtables:
                                        break;
                                    case DemoFrameType.NetworkDataTable:
                                        break;
                                    case DemoFrameType.NextSection:
                                        nextSectionRead = true;
                                        break;
                                    default:
                                        Main.Log($"Error: Frame type: + {frameType} at parsing.");
                                        break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
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