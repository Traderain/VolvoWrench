using System.CodeDom;
using System.Collections.Generic;
using System.IO;
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
            public string Sample;
            public float Volume;
        };

        public struct DemoBufferFrame : IFrame
        {
            public string Buffer;
        };

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
        public static GoldSourceDemoInfoHlsooe ParseDemoHlsooe(string s)
        {
            var hlsooeDemo = new GoldSourceDemoInfoHlsooe
            {
                Header = new Hlsooe.DemoHeader(),
                ParsingErrors = new List<string>(),
                DirectoryEntries = new List<Hlsooe.DemoDirectoryEntry>()
            };
            using (var br = new BinaryReader(new FileStream(s, FileMode.Open)))
            {
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
                    var entryCount = br.ReadInt32();
                    for (var i = 0; i < entryCount; i++)
                    {
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
                        br.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
                        var nextSectionRead = false;
                        for (var i = 0; i < entry.FrameCount; i++)
                        {
                            if (!nextSectionRead)
                            {
                                var currentDemoFrame = new Hlsooe.DemoFrame
                                {
                                    Type = (Hlsooe.DemoFrameType) br.ReadSByte(),
                                    Time = br.ReadSingle(),
                                    Tick = br.ReadInt32()
                                };
                                switch (currentDemoFrame.Type)
                                {
                                    case Hlsooe.DemoFrameType.StartupPacket:
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
                                        var a = new Hlsooe.ConsoleCommandFrame();
                                        var commandlength = br.ReadInt32();
                                        a.Command = new string(br.ReadChars(commandlength)).Trim('\0');
                                        entry.Frames.Add(currentDemoFrame, a);
                                        break;
                                    case Hlsooe.DemoFrameType.Usercmd:
                                        var c = new Hlsooe.UserCmdFrame
                                        {
                                            OutgoingSequence = br.ReadInt32(),
                                            Slot = br.ReadInt32()
                                        };
                                        var usercmdlength = br.ReadInt16();
                                        c.Data =
                                            Encoding.ASCII.GetString(br.ReadBytes(usercmdlength))
                                                .Trim('\0')
                                                .Replace("\0", string.Empty);
                                        entry.Frames.Add(currentDemoFrame, c);
                                        break;
                                    case Hlsooe.DemoFrameType.Stringtables:
                                        //TODO: This is horribly broken. Do something.
                                        var e = new Hlsooe.StringTablesFrame();
                                        var stringtablelength = br.ReadInt32();
                                        var edata = new string(br.ReadChars(stringtablelength));
                                        e.Data = edata;
                                        entry.Frames.Add(currentDemoFrame, e);
                                        break;
                                    case Hlsooe.DemoFrameType.NetworkDataTable:
                                        var d = new Hlsooe.NetworkDataTableFrame();
                                        var networktablelength = br.ReadInt32();
                                        d.Data = new string(br.ReadChars(networktablelength)).Trim('\0');
                                            //TODO: Somehow read u8[]
                                        entry.Frames.Add(currentDemoFrame, d);
                                        break;
                                    case Hlsooe.DemoFrameType.NextSection:
                                        nextSectionRead = true;
                                        entry.Frames.Add(currentDemoFrame, new Hlsooe.NextSectionFrame());
                                        break;
                                    default:
                                        Main.Log($"Error: Frame type: + {currentDemoFrame.Type} at parsing.");
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
            using (var br = new BinaryReader(new FileStream(s, FileMode.Open)))
            {
                var mw = Encoding.ASCII.GetString(br.ReadBytes(8)).Trim('\0').Replace("\0", string.Empty);
                if (mw == "HLDEMO")
                {
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
                    br.BaseStream.Seek(gDemo.Header.DirectoryOffset, SeekOrigin.Begin);
                    var entryCount = br.ReadInt32();
                    for (var i = 0; i < entryCount; i++)
                    {
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
                        br.BaseStream.Seek(entry.Offset, SeekOrigin.Begin);
                        var nextSectionRead = false;
                        var ind = 0;
                        for (var i = 0; i < entry.FrameCount; i++)
                        {
                            ind++;
                            nextSectionRead = false;
                            if (!nextSectionRead)
                            {
                                var currentDemoFrame = new GoldSource.DemoFrame
                                {
                                    Type = (GoldSource.DemoFrameType) br.ReadSByte(),
                                    Time = br.ReadSingle(),
                                    FrameIndex = br.ReadInt32(),
                                    Index = ind
                                };
                                switch (currentDemoFrame.Type)
                                {
                                    case GoldSource.DemoFrameType.DemoStart: //No extra data
                                        break;
                                    case GoldSource.DemoFrameType.ConsoleCommand:
                                        var ccframe = new GoldSource.ConsoleCommandFrame
                                        {
                                            Command = Encoding.ASCII.GetString(br.ReadBytes(64))
                                                .Trim('\0')
                                                .Replace("\0", string.Empty)
                                        };
                                        entry.Frames.Add(currentDemoFrame, ccframe);
                                        break;
                                    case GoldSource.DemoFrameType.ClientData:
                                        var cdframe = new GoldSource.ClientDataFrame
                                        {
                                            Origin =
                                            {
                                                X = br.ReadSingle(),
                                                Y = br.ReadSingle(),
                                                Z = br.ReadSingle()
                                            },
                                            Viewangles =
                                            {
                                                X = br.ReadSingle(),
                                                Y = br.ReadSingle(),
                                                Z = br.ReadSingle()
                                            },
                                            WeaponBits = br.ReadInt32(),
                                            Fov = br.ReadSingle()
                                        };
                                        break;
                                    case GoldSource.DemoFrameType.NextSection:
                                        nextSectionRead = true;
                                        entry.Frames.Add(currentDemoFrame, new GoldSource.NextSectionFrame());
                                        break;
                                    case GoldSource.DemoFrameType.Event:
                                        var eframe = new GoldSource.EventFrame
                                        {
                                            Flags = br.ReadInt32(),
                                            Index = br.ReadInt32(),
                                            Delay = br.ReadSingle(),
                                            EventArguments =
                                            {
                                                Flags = br.ReadInt32(),
                                                EntityIndex = br.ReadInt32(),
                                                Origin =
                                                {
                                                    X = br.ReadSingle(),
                                                    Y = br.ReadSingle(),
                                                    Z = br.ReadSingle()
                                                },
                                                Angles =
                                                {
                                                    X = br.ReadSingle(),
                                                    Y = br.ReadSingle(),
                                                    Z = br.ReadSingle()
                                                },
                                                Velocity =
                                                {
                                                    X = br.ReadSingle(),
                                                    Y = br.ReadSingle(),
                                                    Z = br.ReadSingle()
                                                },
                                                Ducking = br.ReadInt32(),
                                                Fparam1 = br.ReadSingle(),
                                                Fparam2 = br.ReadSingle(),
                                                Iparam1 = br.ReadInt32(),
                                                Iparam2 = br.ReadInt32(),
                                                Bparam1 = br.ReadInt32(),
                                                Bparam2 = br.ReadInt32()
                                            }
                                        };
                                        entry.Frames.Add(currentDemoFrame,eframe);
                                        break;
                                    case GoldSource.DemoFrameType.WeaponAnim:
                                        var waframe = new GoldSource.WeaponAnimFrame
                                        {
                                            Anim = br.ReadInt32(),
                                            Body = br.ReadInt32()
                                        };
                                        entry.Frames.Add(currentDemoFrame,waframe);
                                        break;
                                    case GoldSource.DemoFrameType.Sound:
                                        var sframe = new GoldSource.SoundFrame();
                                        sframe.Channel = br.ReadInt32();
                                        var sfl = br.ReadInt32();
                                        br.ReadBytes(sfl); //200930
                                        sframe.Sample = Encoding
                                            .ASCII
                                            .GetString(br.ReadBytes(sfl))
                                                        .Trim('\0')
                                                        .Replace("\0", string.Empty);
                                        sframe.Attenuation = br.ReadSingle();
                                        sframe.Volume = br.ReadSingle();
                                        sframe.Flags = br.ReadInt32();
                                        sframe.Pitch = br.ReadInt32();
                                        entry.Frames.Add(currentDemoFrame,sframe);
                                        break;
                                    case GoldSource.DemoFrameType.DemoBuffer:
                                        var bframe = new GoldSource.DemoBufferFrame();
                                        var bufferlength = br.ReadInt32();
                                        bframe.Buffer = new string(br.ReadChars(bufferlength));
                                        entry.Frames.Add(currentDemoFrame,bframe);
                                        break;
                                    default:
                                        Main.Log("Unknow frame type read at " + br.BaseStream.Position);
                                        var nf = new GoldSource.NetMsgFrame();
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
                                        nf.UCmd.LerpMsec = br.ReadInt32();
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
                                        nf.IncomingSequence = br.ReadInt32();
                                        nf.IncomingAcknowledged = br.ReadInt32();
                                        nf.IncomingReliableAcknowledged = br.ReadInt32();
                                        nf.IncomingReliableSequence = br.ReadInt32();
                                        nf.OutgoingSequence = br.ReadInt32();
                                        nf.ReliableSequence = br.ReadInt32();
                                        nf.LastReliableSequence = br.ReadInt32();
                                        entry.Frames.Add(currentDemoFrame,nf);
                                        break;
                                }
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
            return gDemo;
        }
    }
}