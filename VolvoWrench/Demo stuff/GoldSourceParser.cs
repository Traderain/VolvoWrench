using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media.Media3D;

namespace VolvoWrench.Demo_stuff
{
    public class Hlsooe
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

        public interface IFrame
        {
        }

        public class DemoFrame
        {
            public int Tick;
            public float Time;
            public DemoFrameType Type;
        }

        public class ConsoleCommandFrame : IFrame
        {
            public string Command;
        }

        public class StringTablesFrame : IFrame
        {
            public string Data;
        }

        public class JumpTimeFrame : IFrame
        {
        }

        public class NextSectionFrame : IFrame
        {
        }

        public class ErrorFrame : IFrame
        {
            public string ErrorData;
            public int Flags;
            public int IncomingAcknowledged;
            public int IncomingReliableAcknowledged;
            public int IncomingReliableSequence;
            public int IncomingSequence;
            public int LastReliableSequence;
            public Point3D LocalViewAngles;
            public Point3D LocalViewAngles2;
            public string Msg;
            public int OutgoingSequence;
            public int ReliableSequence;
            public Point3D ViewAngles;
            public Point3D ViewAngles2;
            public Point3D ViewOrigin2;
            public Point3D ViewOrigins;
        }

        public class NetworkDataTableFrame : IFrame
        {
            public string Data;
        }

        public class UserCmdFrame : IFrame
        {
            public string Data;
            public int OutgoingSequence;
            public int Slot;
        }

        public class NetMsgFrame : IFrame
        {
            public int Flags;
            public int IncomingAcknowledged;
            public int IncomingReliableAcknowledged;
            public int IncomingReliableSequence;
            public int IncomingSequence;
            public int LastReliableSequence;
            public Point3D LocalViewAngles;
            public Point3D LocalViewAngles2;
            public string Msg;
            public int OutgoingSequence;
            public int ReliableSequence;
            public Point3D ViewAngles;
            public Point3D ViewAngles2;
            public Point3D ViewOrigin2;
            public Point3D ViewOrigins;
        }

        public class StartupPacketFrame : IFrame
        {
            public int Flags;
            public int IncomingAcknowledged;
            public int IncomingReliableAcknowledged;
            public int IncomingReliableSequence;
            public int IncomingSequence;
            public int LastReliableSequence;
            public Point3D LocalViewAngles;
            public Point3D LocalViewAngles2;
            public string Msg;
            public int OutgoingSequence;
            public int ReliableSequence;
            public Point3D ViewAngles;
            public Point3D ViewAngles2;
            public Point3D ViewOrigin2;
            public Point3D ViewOrigins;
        }

        public class DemoHeader : IFrame
        {
            public int DemoProtocol;
            public int DirectoryOffset;
            public string GameDirectory;
            public string Magic;
            public string MapName;
            public int Netprotocol;
        }

        public class DemoDirectoryEntry
        {
            public int Filelength;
            public int FrameCount;
            public Dictionary<DemoFrame, IFrame> Frames;
            public int Offset;
            public float PlaybackTime;
            public int Type;
        }
    }

    public class GoldSource
    {
        public enum DemoFrameType
        {
            DemoStart = 2,
            ConsoleCommand = 3,
            ClientData = 4,
            NextSection = 5,
            Event = 6,
            WeaponAnim = 7,
            Sound = 8,
            DemoBuffer = 9
        }

        public enum EngineVersions
        {
            Unknown,
            HalfLife1104,
            HalfLife1106,
            HalfLife1107,
            HalfLife1108,
            HalfLife1109,
            HalfLife1108or1109,
            HalfLife1110,
            HalfLife1111, // Steam
            HalfLife1110or1111
        }

        public string EngineName(int name)
        {
            var s = "Half-Life v";

            switch ((EngineVersions) name)
            {
                case EngineVersions.HalfLife1104:
                    s += "1.1.0.4";
                    break;

                case EngineVersions.HalfLife1106:
                    s += "1.1.0.6";
                    break;

                case EngineVersions.HalfLife1107:
                    s += "1.1.0.7";
                    break;

                case EngineVersions.HalfLife1108:
                    s += "1.1.0.8";
                    break;

                case EngineVersions.HalfLife1109:
                    s += "1.1.0.9";
                    break;

                case EngineVersions.HalfLife1108or1109:
                    s += "1.1.0.8 or v1.1.0.9";
                    break;

                case EngineVersions.HalfLife1110:
                    s += "1.1.1.0";
                    break;

                case EngineVersions.HalfLife1111:
                    s += "1.1.1.1";
                    break;

                case EngineVersions.HalfLife1110or1111:
                    s += "1.1.1.0 or v1.1.1.1";
                    break;

                default:
                    return "Half-Life Unknown Version";
            }

            return s;
        }

        public class DemoHeader
        {
            public int DemoProtocol;
            public int DirectoryOffset;
            public string GameDir;
            public uint MapCrc;
            public string MapName;
            public int NetProtocol;
        };

        public struct DemoDirectoryEntry
        {
            public int CdTrack;
            public string Description;
            public int FileLength;
            public int Flags;
            public int FrameCount;
            public Dictionary<DemoFrame, IFrame> Frames;
            public int Offset;
            public float TrackTime;
            public int Type;
        }

        public interface IFrame
        {
        }

        public struct Point4D
        {
            public int W;
            public int X;
            public int Y;
            public int Z;
        }

        public struct DemoFrame
        {
            public int FrameIndex;
            public float Time;
            public DemoFrameType Type;
            public int Index;
        };

        // DEMO_START: no extra data.
        public struct NextSectionFrame : IFrame
        {
        }

        public struct ConsoleCommandFrame : IFrame
        {
            public string Command;
        };

        public struct ClientDataFrame : IFrame
        {
            public float Fov;
            public Point3D Origin;
            public Point3D Viewangles;
            public int WeaponBits;
        };

        // NEXT_SECTION: no extra data.

        public struct EventFrame : IFrame
        {
            public float Delay;
            public int Flags;
            public int Index;
            public EventArgs EventArguments;

            public struct EventArgs
            {
                public Point3D Angles;
                public int Bparam1;
                public int Bparam2;
                public int Ducking;
                public int EntityIndex;
                public int Flags;
                public float Fparam1;
                public float Fparam2;
                public int Iparam1;
                public int Iparam2;
                public Point3D Origin;
                public Point3D Velocity;
            }
        };

        public struct WeaponAnimFrame : IFrame
        {
            public int Anim;
            public int Body;
        };

        public struct SoundFrame : IFrame
        {
            public float Attenuation;
            public int Channel;
            public int Flags;
            public int Pitch;
            public byte[] Sample;
            public float Volume;
        };

        public struct DemoBufferFrame : IFrame
        {
            public byte[] Buffer;
        };

        public struct DemoStartFrame : IFrame { }

        // Otherwise, netmsg.
        public struct NetMsgFrame : IFrame
        {
            public int IncomingAcknowledged;
            public int IncomingReliableAcknowledged;
            public int IncomingReliableSequence;
            public int IncomingSequence;
            public int LastReliableSequence;
            public string Msg;
            public int OutgoingSequence;
            public int ReliableSequence;

            public float Timestamp;
            public Point3D View;
            public int Viewmodel;
           
            public RefParams RParms;
            public UserCmd UCmd;
            public MoveVars MVars;

                public struct RefParams
                {
                    public Point3D ClViewangles;
                    public Point3D Crosshairangle;
                    public int Demoplayback;
                    public Point3D Forward;
                    public float Frametime;
                    public int Hardware;
                    public int Health;
                    public float Idealpitch;
                    public int Intermission;
                    public int Maxclients;
                    public int MaxEntities;
                    public int NextView;
                    public int Onground;
                    public int OnlyClientDraw;
                    public int Paused;
                    public int Playernum;
                    public int PtrCmd;
                    public int PtrMovevars;
                    public Point3D Punchangle;
                    public Point3D Right;
                    public Point3D Simorg;
                    public Point3D Simvel;
                    public int Smoothing;
                    public int Spectator;
                    public float Time;
                    public Point3D Up;
                    public Point3D Viewangles;
                    public int Viewentity;
                    public Point3D Viewheight;
                    public Point3D Vieworg;
                    public Point4D Viewport;
                    public float Viewsize;
                    public int Waterlevel;
                }

                public struct UserCmd
                {
                    public sbyte Align1;
                    public sbyte Align2;
                    public sbyte Align3;
                    public sbyte Align4;
                    public int Buttons;
                    public float Forwardmove;
                    public int ImpactIndex;
                    public Point3D ImpactPosition;
                    public sbyte Impulse;
                    public int LerpMsec;
                    public sbyte Lightlevel;
                    public sbyte Msec;
                    public float Sidemove;
                    public float Upmove;
                    public Point3D Viewangles;
                    public sbyte Weaponselect;
                }

                public struct MoveVars
                {
                    public float Accelerate;
                    public float Airaccelerate;
                    public float Bounce;
                    public float Edgefriction;
                    public float Entgravity;
                    public int Footsteps;
                    public float Friction;
                    public float Gravity;
                    public float Maxspeed;
                    public float Maxvelocity;
                    public float Rollangle;
                    public float Rollspeed;
                    public float SkycolorB;
                    public float SkycolorG;
                    public float SkycolorR;
                    public string SkyName;
                    public float SkyvecX;
                    public float SkyvecY;
                    public float SkyvecZ;
                    public float Spectatormaxspeed;
                    public float Stepsize;
                    public float Stopspeed;
                    public float Wateraccelerate;
                    public float Waterfriction;
                    public float WaveHeight;
                    public float Zmax;
            }
        }
    }

    public struct GoldSourceDemoInfo
    {
        public List<GoldSource.DemoDirectoryEntry> DirectoryEntries;
        public GoldSource.DemoHeader Header;
        public List<string> ParsingErrors;
    }

    public class GoldSourceDemoInfoHlsooe
    {
        public List<Hlsooe.DemoDirectoryEntry> DirectoryEntries;
        public Hlsooe.DemoHeader Header;
        public List<string> ParsingErrors;
    }

    public class GoldSourceParser
    {
       
        
        public static bool UnexpectedEof(BinaryReader b, long lengthtocheck)
        {
            return (b.BaseStream.Position + lengthtocheck) > b.BaseStream.Length;
        }

        public static GoldSourceDemoInfoHlsooe ParseDemoHlsooe(string s)
        {
            
            var hlsooeDemo = new GoldSourceDemoInfoHlsooe
            {
                Header = new Hlsooe.DemoHeader(),
                ParsingErrors = new List<string>(),
                DirectoryEntries = new List<Hlsooe.DemoDirectoryEntry>()
            };
            try
            {
                using (var br = new BinaryReader(new FileStream(s, FileMode.Open)))
                {
                    if (UnexpectedEof(br, (8 + 4 + 4 + 260 + 260 + 4)))//520 + 12 + 8 = 540 -> Header size
                    {
                        hlsooeDemo.ParsingErrors.Add("Unexpected end of file at the header!");
                        return hlsooeDemo;
                    }
                    var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).Trim('\0').Replace("\0", string.Empty);
                    if (mw == "HLDEMO")
                    {
                        hlsooeDemo.Header.DemoProtocol = br.ReadInt32();
                        hlsooeDemo.Header.Netprotocol = br.ReadInt32();
                        hlsooeDemo.Header.MapName = Encoding.ASCII.GetString(br.ReadBytes(260))
                            .Trim('\0')
                            .Replace("\0", string.Empty);
                        hlsooeDemo.Header.GameDirectory =
                            Encoding.ASCII.GetString(br.ReadBytes(260)).Trim('\0').Replace("\0", string.Empty);
                        hlsooeDemo.Header.DirectoryOffset = br.ReadInt32();
                        //Header Parsed... now we read the directory entries
                        br.BaseStream.Seek(hlsooeDemo.Header.DirectoryOffset, SeekOrigin.Begin);
                        if (UnexpectedEof(br, (4)))
                        {
                            hlsooeDemo.ParsingErrors.Add("Unexpected end of file after the header!");
                            return hlsooeDemo;
                        }
                        var entryCount = br.ReadInt32();
                        for (var i = 0; i < entryCount; i++)
                        {
                            if (UnexpectedEof(br, (4 + 4 + 4 + 4 + 4)))
                            {
                                hlsooeDemo.ParsingErrors.Add("Unexpected end of when reading frames!");
                                return hlsooeDemo;
                            }
                            var tempvar = new Hlsooe.DemoDirectoryEntry
                            {
                                Type = br.ReadInt32(),
                                PlaybackTime = br.ReadSingle(),
                                FrameCount = br.ReadInt32(),
                                Offset = br.ReadInt32(),
                                Filelength = br.ReadInt32(),
                                Frames = new Dictionary<Hlsooe.DemoFrame, Hlsooe.IFrame>()
                            };
                            hlsooeDemo.DirectoryEntries.Add(tempvar);
                        }
                        //Demo directory entries parsed... now we parse the frames.
                        foreach (var entry in hlsooeDemo.DirectoryEntries)
                        {
                            if (entry.Offset > br.BaseStream.Length)
                            {
                                hlsooeDemo.ParsingErrors.Add("Couldn't seek to directoryentry the file is corrupted.");
                                return hlsooeDemo;
                            }
                            br.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
                            var nextSectionRead = false;
                            for (var i = 0; i < entry.FrameCount; i++)
                            {
                                if (!nextSectionRead)
                                {
                                    if (UnexpectedEof(br, (1 + 4 + 4)))
                                    {
                                        hlsooeDemo.ParsingErrors.Add("Failed to read next frame details after frame no.: " + i);
                                        return hlsooeDemo;
                                    }
                                    var currentDemoFrame = new Hlsooe.DemoFrame
                                    {
                                        Type = (Hlsooe.DemoFrameType)br.ReadSByte(),
                                        Time = br.ReadSingle(),
                                        Tick = br.ReadInt32()
                                    };
                                    #region FrameType Switch
                                    switch (currentDemoFrame.Type)
                                    {
                                        case Hlsooe.DemoFrameType.StartupPacket:
                                            if (UnexpectedEof(br, (8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 4 + 4 + 4 + 4 + 4 + 4 + 4)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Failed to read startup packet at frame:" + i);
                                                return hlsooeDemo;
                                            }
                                            var g = new Hlsooe.StartupPacketFrame
                                            {
                                                Flags = br.ReadInt32(),
                                                ViewOrigins = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                ViewAngles = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                LocalViewAngles =
                                                    new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                ViewOrigin2 = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                IncomingSequence = br.ReadInt32(),
                                                IncomingAcknowledged = br.ReadInt32(),
                                                IncomingReliableAcknowledged = br.ReadInt32(),
                                                IncomingReliableSequence = br.ReadInt32(),
                                                OutgoingSequence = br.ReadInt32(),
                                                ReliableSequence = br.ReadInt32(),
                                                LastReliableSequence = br.ReadInt32()
                                            };
                                            entry.Frames.Add(currentDemoFrame, g);
                                            break;
                                        case Hlsooe.DemoFrameType.NetworkPacket:
                                            if (UnexpectedEof(br, (8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 4 + 4 + 4 + 4 + 4 + 4 + 4)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Failed to read netmessage at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            var b = new Hlsooe.NetMsgFrame
                                            {
                                                Flags = br.ReadInt32(),
                                                ViewOrigins = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                ViewAngles = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                LocalViewAngles =
                                                    new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                ViewOrigin2 = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                IncomingSequence = br.ReadInt32(),
                                                IncomingAcknowledged = br.ReadInt32(),
                                                IncomingReliableAcknowledged = br.ReadInt32(),
                                                IncomingReliableSequence = br.ReadInt32(),
                                                OutgoingSequence = br.ReadInt32(),
                                                ReliableSequence = br.ReadInt32(),
                                                LastReliableSequence = br.ReadInt32()
                                            };
                                            entry.Frames.Add(currentDemoFrame, b);
                                            break;
                                        case Hlsooe.DemoFrameType.Jumptime:
                                            //No extra stuff
                                            entry.Frames.Add(currentDemoFrame, new Hlsooe.JumpTimeFrame());
                                            break;
                                        case Hlsooe.DemoFrameType.ConsoleCommand:
                                            if (UnexpectedEof(br, (4)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected enf of file when reading console command length at fram:" + i);
                                                return hlsooeDemo;
                                            }
                                            var a = new Hlsooe.ConsoleCommandFrame();
                                            var commandlength = br.ReadInt32();
                                            if (UnexpectedEof(br, (commandlength)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading the console command at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            a.Command = new string(br.ReadChars(commandlength)).Trim('\0');
                                            entry.Frames.Add(currentDemoFrame, a);
                                            break;
                                        case Hlsooe.DemoFrameType.Usercmd:
                                            if (UnexpectedEof(br, (4 + 4 + 2)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading UserCMD header at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            var c = new Hlsooe.UserCmdFrame
                                            {
                                                OutgoingSequence = br.ReadInt32(),
                                                Slot = br.ReadInt32()
                                            };
                                            var usercmdlength = br.ReadInt16();
                                            if (UnexpectedEof(br, (usercmdlength)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading userCMD at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            c.Data =
                                                Encoding.ASCII.GetString(br.ReadBytes(usercmdlength))
                                                    .Trim('\0')
                                                    .Replace("\0", string.Empty);
                                            entry.Frames.Add(currentDemoFrame, c);
                                            break;
                                        case Hlsooe.DemoFrameType.Stringtables:
                                            var e = new Hlsooe.StringTablesFrame();
                                            if (UnexpectedEof(br, (4)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading stringtablelength at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            var stringtablelength = br.ReadInt32();
                                            if (UnexpectedEof(br, (stringtablelength)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading stringtable data at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            var edata = new string(br.ReadChars(stringtablelength));
                                            e.Data = edata;
                                            entry.Frames.Add(currentDemoFrame, e);
                                            break;
                                        case Hlsooe.DemoFrameType.NetworkDataTable:
                                            var d = new Hlsooe.NetworkDataTableFrame();
                                            if (UnexpectedEof(br, (4)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading networktable length at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            var networktablelength = br.ReadInt32();
                                            if (UnexpectedEof(br, (4)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading NetWorkTable data at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            d.Data = new string(br.ReadChars(networktablelength)).Trim('\0');
                                            entry.Frames.Add(currentDemoFrame, d);
                                            break;
                                        case Hlsooe.DemoFrameType.NextSection:
                                            nextSectionRead = true;
                                            entry.Frames.Add(currentDemoFrame, new Hlsooe.NextSectionFrame());
                                            break;
                                        default:
                                            Main.Log($"Error: Frame type: + {currentDemoFrame.Type} at parsing.");
                                            if (UnexpectedEof(br, (8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 8 + 4 + 4 + 4 + 4 + 4 + 4 + 4)))
                                            {
                                                hlsooeDemo.ParsingErrors.Add("Unexpected end of file when reading default frame at frame: " + i);
                                                return hlsooeDemo;
                                            }
                                            var err = new Hlsooe.ErrorFrame
                                            {
                                                Flags = br.ReadInt32(),
                                                ViewOrigins = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                ViewAngles = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                LocalViewAngles =
                                                    new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                ViewOrigin2 = new Point3D(br.ReadDouble(), br.ReadDouble(), br.ReadDouble()),
                                                IncomingSequence = br.ReadInt32(),
                                                IncomingAcknowledged = br.ReadInt32(),
                                                IncomingReliableAcknowledged = br.ReadInt32(),
                                                IncomingReliableSequence = br.ReadInt32(),
                                                OutgoingSequence = br.ReadInt32(),
                                                ReliableSequence = br.ReadInt32(),
                                                LastReliableSequence = br.ReadInt32()
                                            };
                                            entry.Frames.Add(currentDemoFrame, err);
                                            break;
                                    }
                                    #endregion
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
                        hlsooeDemo.ParsingErrors.Add("Non goldsource demo file");
                        br.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Main.Log("Exception happened at hlsooe parser: " + e.Message);
                hlsooeDemo.ParsingErrors.Add(e.Message);
            }
            return hlsooeDemo;
        }

        public static GoldSourceDemoInfo ParseGoldSourceDemo(string s)
        {
            var gDemo = new GoldSourceDemoInfo
            {
                Header = new GoldSource.DemoHeader(),
                ParsingErrors = new List<string>(),
                DirectoryEntries = new List<GoldSource.DemoDirectoryEntry>()
            };
            try
            {
                using (var br = new BinaryReader(new FileStream(s, FileMode.Open)))
                {
                    var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).Trim('\0').Replace("\0", string.Empty);
                    if (mw == "HLDEMO")
                    {
                        if (UnexpectedEof(br, (4 + 4 + 260 + 260 + 4)))
                        {
                            gDemo.ParsingErrors.Add("Unexpected end of file at the header!");
                            return gDemo;
                        }
                        gDemo.Header.DemoProtocol = br.ReadInt32();
                        gDemo.Header.NetProtocol = br.ReadInt32();
                        gDemo.Header.MapName = Encoding.ASCII.GetString(br.ReadBytes(260))
                            .Trim('\0')
                            .Replace("\0", string.Empty);
                        gDemo.Header.GameDir = Encoding.ASCII.GetString(br.ReadBytes(260))
                            .Trim('\0')
                            .Replace("\0", string.Empty);
                        gDemo.Header.MapCrc = br.ReadUInt32();
                        gDemo.Header.DirectoryOffset = br.ReadInt32();
                        //Header Parsed... now we read the directory entries
                        if (UnexpectedEof(br, (gDemo.Header.DirectoryOffset - br.BaseStream.Position)))
                        {
                            gDemo.ParsingErrors.Add("Unexpected end of file when seeking to directory offset!");
                            return gDemo;
                        }
                        br.BaseStream.Seek(gDemo.Header.DirectoryOffset, SeekOrigin.Begin);
                        if (UnexpectedEof(br, (4)))
                        {
                            gDemo.ParsingErrors.Add("Unexpected end of file when reading entry count!");
                            return gDemo;
                        }
                        var entryCount = br.ReadInt32();
                        for (var i = 0; i < entryCount; i++)
                        {
                            if (UnexpectedEof(br, (4 + 64 + 4 + 4 + 4 + 4 + 4 + 4)))
                            {
                                gDemo.ParsingErrors.Add("Unexpected end of file when reading the directory entries!");
                                return gDemo;
                            }
                            var tempvar = new GoldSource.DemoDirectoryEntry
                            {
                                Type = br.ReadInt32(),
                                Description =
                                    Encoding.ASCII.GetString(br.ReadBytes(64)).Trim('\0').Replace("\0", string.Empty),
                                Flags = br.ReadInt32(),
                                CdTrack = br.ReadInt32(),
                                TrackTime = br.ReadSingle(),
                                FrameCount = br.ReadInt32(),
                                Offset = br.ReadInt32(),
                                FileLength = br.ReadInt32(),
                                Frames = new Dictionary<GoldSource.DemoFrame, GoldSource.IFrame>()
                            };
                            gDemo.DirectoryEntries.Add(tempvar);
                        }
                        //Demo directory entries parsed... now we parse the frames.
                        foreach (var entry in gDemo.DirectoryEntries)
                        {
                            if (UnexpectedEof(br, (entry.Offset -  br.BaseStream.Position)))
                            {
                                gDemo.ParsingErrors.Add("Unexpected end of file when seeking to directory entry!");
                                return gDemo;
                            }
                            br.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
                            var nextSectionRead = false;
                            var ind = 0;
                            for (var i = 0; i < entry.FrameCount; i++)
                            {
                                ind++;
                                nextSectionRead = false;
                                if (!nextSectionRead)
                                {
                                    if (UnexpectedEof(br, (1 + 4 + 4)))
                                    {
                                        gDemo.ParsingErrors.Add("Unexpected end of file when reading the header of the frame: " + ind + 1);
                                        return gDemo;
                                    }
                                    var currentDemoFrame = new GoldSource.DemoFrame
                                    {
                                        Type = (GoldSource.DemoFrameType)br.ReadSByte(),
                                        Time = br.ReadSingle(),
                                        FrameIndex = br.ReadInt32(),
                                        Index = ind
                                    };
                                    #region Frame Switch
                                    switch (currentDemoFrame.Type)
                                    {
                                        case GoldSource.DemoFrameType.DemoStart: //No extra dat
                                            break;
                                        case GoldSource.DemoFrameType.ConsoleCommand:
                                            var ccframe = new GoldSource.ConsoleCommandFrame();
                                            if (UnexpectedEof(br, (64)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when reading console command at frame: " + ind);
                                                return gDemo;
                                            }
                                            ccframe.Command = Encoding.ASCII.GetString(br.ReadBytes(64))
                                                .Trim('\0')
                                                .Replace("\0", string.Empty);
                                            entry.Frames.Add(currentDemoFrame, ccframe);
                                            break;
                                        case GoldSource.DemoFrameType.ClientData:
                                            var cdframe = new GoldSource.ClientDataFrame();
                                            if (UnexpectedEof(br, (4 + 4 + 4 + 4 + 4 + 4 + 4 + 4)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when reading clientdataframe at frame: " + ind);
                                                return gDemo;
                                            }
                                            cdframe.Origin.X = br.ReadSingle();
                                            cdframe.Origin.Y = br.ReadSingle();
                                            cdframe.Origin.Z = br.ReadSingle();
                                            cdframe.Viewangles.X = br.ReadSingle();
                                            cdframe.Viewangles.Y = br.ReadSingle();
                                            cdframe.Viewangles.Z = br.ReadSingle();
                                            cdframe.WeaponBits = br.ReadInt32();
                                            cdframe.Fov = br.ReadSingle();
                                            entry.Frames.Add(currentDemoFrame, cdframe);
                                            break;
                                        case GoldSource.DemoFrameType.NextSection:
                                            nextSectionRead = true;
                                            entry.Frames.Add(currentDemoFrame, new GoldSource.NextSectionFrame());
                                            break;
                                        case GoldSource.DemoFrameType.Event:
                                            var eframe = new GoldSource.EventFrame();
                                            if (UnexpectedEof(br, (22 * 4)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file at when reading eventframe on frame: " + ind);
                                                return gDemo;
                                            }
                                            eframe.Flags = br.ReadInt32();
                                            eframe.Index = br.ReadInt32();
                                            eframe.Delay = br.ReadSingle();
                                            eframe.EventArguments.Flags = br.ReadInt32();
                                            eframe.EventArguments.EntityIndex = br.ReadInt32();
                                            eframe.EventArguments.Origin.X = br.ReadSingle();
                                            eframe.EventArguments.Origin.Y = br.ReadSingle();
                                            eframe.EventArguments.Origin.Z = br.ReadSingle();
                                            eframe.EventArguments.Angles.X = br.ReadSingle();
                                            eframe.EventArguments.Angles.Y = br.ReadSingle();
                                            eframe.EventArguments.Angles.Z = br.ReadSingle();
                                            eframe.EventArguments.Velocity.X = br.ReadSingle();
                                            eframe.EventArguments.Velocity.Y = br.ReadSingle();
                                            eframe.EventArguments.Velocity.Z = br.ReadSingle();
                                            eframe.EventArguments.Ducking = br.ReadInt32();
                                            eframe.EventArguments.Fparam1 = br.ReadSingle();
                                            eframe.EventArguments.Fparam2 = br.ReadSingle();
                                            eframe.EventArguments.Iparam1 = br.ReadInt32();
                                            eframe.EventArguments.Iparam2 = br.ReadInt32();
                                            eframe.EventArguments.Bparam1 = br.ReadInt32();
                                            eframe.EventArguments.Bparam2 = br.ReadInt32();
                                            entry.Frames.Add(currentDemoFrame, eframe);
                                            break;
                                        case GoldSource.DemoFrameType.WeaponAnim:
                                            var waframe = new GoldSource.WeaponAnimFrame();
                                            if (UnexpectedEof(br, (4 + 4)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when reading weaponanim at frame: " + ind);
                                                return gDemo;
                                            }
                                            waframe.Anim = br.ReadInt32();
                                            waframe.Body = br.ReadInt32();
                                            entry.Frames.Add(currentDemoFrame, waframe);
                                            break;
                                        case GoldSource.DemoFrameType.Sound:
                                            var sframe = new GoldSource.SoundFrame();
                                            if (UnexpectedEof(br, (4 + 4)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when reading sound channel at frame: " + ind);
                                                return gDemo;
                                            }
                                            sframe.Channel = br.ReadInt32();
                                            var samplelength = br.ReadInt32();
                                            if (UnexpectedEof(br, (samplelength + 4 + 4 + 4 + 4)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when reading sound data at frame: " + ind);
                                                return gDemo;
                                            }
                                            sframe.Sample = br.ReadBytes(samplelength);
                                            sframe.Attenuation = br.ReadSingle();
                                            sframe.Volume = br.ReadSingle();
                                            sframe.Flags = br.ReadInt32();
                                            sframe.Pitch = br.ReadInt32();
                                            entry.Frames.Add(currentDemoFrame, sframe);
                                            break;
                                        case GoldSource.DemoFrameType.DemoBuffer:
                                            var bframe = new GoldSource.DemoBufferFrame();
                                            if (UnexpectedEof(br, (4)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when demobuffer data at frame: " + ind);
                                                return gDemo;
                                            }
                                            var buggerlength = br.ReadInt32();
                                            if (UnexpectedEof(br, (buggerlength)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when reading buffer data at frame: " + ind);
                                                return gDemo;
                                            }
                                            bframe.Buffer = br.ReadBytes(buggerlength);
                                            entry.Frames.Add(currentDemoFrame, bframe);
                                            break;
                                        default:
                                            Main.Log("Unknow frame type read at " + br.BaseStream.Position);
                                            var nf = new GoldSource.NetMsgFrame();
                                            if (UnexpectedEof(br, (468)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when default frame at frame: " + ind);
                                                return gDemo;
                                            }
                                            nf.Timestamp = br.ReadSingle();
                                            nf.RParms.Vieworg.X = br.ReadSingle();
                                            nf.RParms.Vieworg.Y = br.ReadSingle();
                                            nf.RParms.Vieworg.Z = br.ReadSingle();
                                            nf.RParms.Viewangles.X = br.ReadSingle();
                                            nf.RParms.Viewangles.Y = br.ReadSingle();
                                            nf.RParms.Viewangles.Z = br.ReadSingle();
                                            nf.RParms.Forward.X = br.ReadSingle();
                                            nf.RParms.Forward.Y = br.ReadSingle();
                                            nf.RParms.Forward.Z = br.ReadSingle();
                                            nf.RParms.Right.X = br.ReadSingle();
                                            nf.RParms.Right.Y = br.ReadSingle();
                                            nf.RParms.Right.Z = br.ReadSingle();
                                            nf.RParms.Up.X = br.ReadSingle();
                                            nf.RParms.Up.Y = br.ReadSingle();
                                            nf.RParms.Up.Z = br.ReadSingle();
                                            nf.RParms.Frametime = br.ReadSingle();
                                            nf.RParms.Time = br.ReadSingle();
                                            nf.RParms.Intermission = br.ReadInt32();
                                            nf.RParms.Paused = br.ReadInt32();
                                            nf.RParms.Spectator = br.ReadInt32();
                                            nf.RParms.Onground = br.ReadInt32();
                                            nf.RParms.Waterlevel = br.ReadInt32();
                                            nf.RParms.Simvel.X = br.ReadSingle();
                                            nf.RParms.Simvel.Y = br.ReadSingle();
                                            nf.RParms.Simvel.Z = br.ReadSingle();
                                            nf.RParms.Simorg.X = br.ReadSingle();
                                            nf.RParms.Simorg.Y = br.ReadSingle();
                                            nf.RParms.Simorg.Z = br.ReadSingle();
                                            nf.RParms.Viewheight.X = br.ReadSingle();
                                            nf.RParms.Viewheight.Y = br.ReadSingle();
                                            nf.RParms.Viewheight.Z = br.ReadSingle();
                                            nf.RParms.Idealpitch = br.ReadSingle();
                                            nf.RParms.ClViewangles.X = br.ReadSingle();
                                            nf.RParms.ClViewangles.Y = br.ReadSingle();
                                            nf.RParms.ClViewangles.Z = br.ReadSingle();
                                            nf.RParms.Health = br.ReadInt32();
                                            nf.RParms.Crosshairangle.X = br.ReadSingle();
                                            nf.RParms.Crosshairangle.Y = br.ReadSingle();
                                            nf.RParms.Crosshairangle.Z = br.ReadSingle();
                                            nf.RParms.Viewsize = br.ReadSingle();
                                            nf.RParms.Punchangle.X = br.ReadSingle();
                                            nf.RParms.Punchangle.Y = br.ReadSingle();
                                            nf.RParms.Punchangle.Z = br.ReadSingle();
                                            nf.RParms.Maxclients = br.ReadInt32();
                                            nf.RParms.Viewentity = br.ReadInt32();
                                            nf.RParms.Playernum = br.ReadInt32();
                                            nf.RParms.MaxEntities = br.ReadInt32();
                                            nf.RParms.Demoplayback = br.ReadInt32();
                                            nf.RParms.Hardware = br.ReadInt32();
                                            nf.RParms.Smoothing = br.ReadInt32();
                                            nf.RParms.PtrCmd = br.ReadInt32();
                                            nf.RParms.PtrMovevars = br.ReadInt32();
                                            nf.RParms.Viewport.X = br.ReadInt32();
                                            nf.RParms.Viewport.Y = br.ReadInt32();
                                            nf.RParms.Viewport.Z = br.ReadInt32();
                                            nf.RParms.Viewport.W = br.ReadInt32();
                                            nf.RParms.NextView = br.ReadInt32();
                                            nf.RParms.OnlyClientDraw = br.ReadInt32();

                                            nf.UCmd.LerpMsec = br.ReadInt16();
                                            nf.UCmd.Msec = br.ReadSByte();
                                            nf.UCmd.Align1 = br.ReadSByte();
                                            nf.UCmd.Viewangles.X = br.ReadSingle();
                                            nf.UCmd.Viewangles.Y = br.ReadSingle();
                                            nf.UCmd.Viewangles.Z = br.ReadSingle();
                                            nf.UCmd.Forwardmove = br.ReadSingle();
                                            nf.UCmd.Sidemove = br.ReadSingle();
                                            nf.UCmd.Upmove = br.ReadSingle();
                                            nf.UCmd.Lightlevel = br.ReadSByte();
                                            nf.UCmd.Align2 = br.ReadSByte();
                                            nf.UCmd.Buttons = br.ReadInt16();
                                            nf.UCmd.Impulse = br.ReadSByte();
                                            nf.UCmd.Weaponselect = br.ReadSByte();
                                            nf.UCmd.Align3 = br.ReadSByte();
                                            nf.UCmd.Align4 = br.ReadSByte();
                                            nf.UCmd.ImpactIndex = br.ReadInt32();
                                            nf.UCmd.ImpactPosition.X = br.ReadSingle();
                                            nf.UCmd.ImpactPosition.Y = br.ReadSingle();
                                            nf.UCmd.ImpactPosition.Z = br.ReadSingle();

                                            nf.MVars.Gravity = br.ReadSingle();
                                            nf.MVars.Stopspeed = br.ReadSingle();
                                            nf.MVars.Maxspeed = br.ReadSingle();
                                            nf.MVars.Spectatormaxspeed = br.ReadSingle();
                                            nf.MVars.Accelerate = br.ReadSingle();
                                            nf.MVars.Airaccelerate = br.ReadSingle();
                                            nf.MVars.Wateraccelerate = br.ReadSingle();
                                            nf.MVars.Friction = br.ReadSingle();
                                            nf.MVars.Edgefriction = br.ReadSingle();
                                            nf.MVars.Waterfriction = br.ReadSingle();
                                            nf.MVars.Entgravity = br.ReadSingle();
                                            nf.MVars.Bounce = br.ReadSingle();
                                            nf.MVars.Stepsize = br.ReadSingle();
                                            nf.MVars.Maxvelocity = br.ReadSingle();
                                            nf.MVars.Zmax = br.ReadSingle();
                                            nf.MVars.WaveHeight = br.ReadSingle();
                                            nf.MVars.Footsteps = br.ReadInt32();
                                            nf.MVars.SkyName = Encoding
                                                .ASCII
                                                .GetString(br.ReadBytes(32))
                                                .Trim('\0')
                                                .Replace("\0", string.Empty);
                                            nf.MVars.Rollangle = br.ReadSingle();
                                            nf.MVars.Rollspeed = br.ReadSingle();
                                            nf.MVars.SkycolorR = br.ReadSingle();
                                            nf.MVars.SkycolorG = br.ReadSingle();
                                            nf.MVars.SkycolorB = br.ReadSingle();
                                            nf.MVars.SkyvecX = br.ReadSingle();
                                            nf.MVars.SkyvecY = br.ReadSingle();
                                            nf.MVars.SkyvecZ = br.ReadSingle();

                                            nf.View.X = br.ReadSingle();
                                            nf.View.Y = br.ReadSingle();
                                            nf.View.Z = br.ReadSingle();
                                            nf.Viewmodel = br.ReadInt32();

                                            nf.IncomingSequence = br.ReadInt32();
                                            nf.IncomingAcknowledged = br.ReadInt32();
                                            nf.IncomingReliableAcknowledged = br.ReadInt32();
                                            nf.IncomingReliableSequence = br.ReadInt32();
                                            nf.OutgoingSequence = br.ReadInt32();
                                            nf.ReliableSequence = br.ReadInt32();
                                            nf.LastReliableSequence = br.ReadInt32();
                                            var msglength = br.ReadInt32();
                                            if (UnexpectedEof(br, (msglength)))
                                            {
                                                gDemo.ParsingErrors.Add("Unexpected end of file when default frame message at frame: " + ind);
                                                return gDemo;
                                            }
                                            nf.Msg = Encoding
                                                .ASCII
                                                .GetString(br.ReadBytes(msglength))
                                                .Trim('\0')
                                                .Replace("\0", string.Empty);
                                            entry.Frames.Add(currentDemoFrame, nf);
                                            break;
                                    }
                                    #endregion
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
            }
            catch (Exception e)
            { 
                //This can't actually happen but I might just log it just incase.
                Main.Log("Exception happened in the goldsource parser: " + e.Message);
                gDemo.ParsingErrors.Add(e.Message);
            }
            return gDemo;
        }
    }
}