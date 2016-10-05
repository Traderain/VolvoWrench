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

        public class DemoFrame
        {
            public int Tick;
            public float Time;
            public DemoFrameType Type;
        }

        public struct ConsoleCommandFrame
        {
            public string Command;
        };

        public struct StringTablesFrame
        {
            public string Data;
        };

        public struct NetworkDataTableFrame
        {
            public string Data;
        };

        public struct UserCmdFrame
        {
            public string Data;
            public int OutgoingSequence;
            public int Slot;
        };

        public struct NetMsgFrame
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
            public List<DemoFrame> Frames;
            public int Offset;
            public float PlaybackTime;
            public int Type;
        }
    }

    public struct GoldSourceDemoInfoHlsooe
    {
        public List<Hlsooe.DemoDirectoryEntry> DirectoryEntries;
        public Hlsooe.DemoHeader Header;
        public List<string> ParsingErrors;
    }

    public class GoldSource
    {
        public struct DemoHeader
        {
            public int NetProtocol;
            public int DemoProtocol;
            public string MapName;
            public string GameDir;
            public int MapCrc;
            public int DirectoryOffset;
        };

        public struct DemoDirectoryEntry
        {
            public int Type;
            public string Description;
            public int Flags;
            public int CdTrack;
            public float TrackTime;
            public int FrameCount;
            public int Offset;
            public int FileLength;
        }

        public struct Point4D
        {
            public int X;
            public int Y;
            public int Z;
            public int W;
        }
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

        public struct DemoFrame
        {
            public int Frame;
            public float Time;
            public DemoFrameType Type;
        };

        // DEMO_START: no extra data.

        public struct ConsoleCommandFrame
        {
            public string Command;
        };

        public struct ClientDataFrame
        {
            public float Fov;
            public Point3D Origin;
            public Point3D Viewangles;
            public int WeaponBits;
        };

        // NEXT_SECTION: no extra data.

        public struct EventFrame
        {
            public float Delay;
            public int Flags;
            public int Index;

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

        public struct WeaponAnimFrame
        {
            public int Anim;
            public int Body;
        };

        public struct SoundFrame
        {
            public float Attenuation;
            public int Channel;
            public int Flags;
            public int Pitch;
            public string Sample;
            public float Volume;
        };

        public struct DemoBufferFrame
        {
        };

        // Otherwise, netmsg.
        public struct NetMsgFrame
        {
            public int IncomingAcknowledged;
            public int IncomingReliableAcknowledged;
            public int IncomingReliableSequence;
            public int IncomingSequence;
            public int LastReliableSequence;
            public int OutgoingSequence;
            public int ReliableSequence;
            public string Msg;

            public struct DemoInfo
            {
                public float Timestamp;
                public Point3D View;
                public int Viewmodel;

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
                    public int MaxEntities;
                    public int Maxclients;
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
                    public uint Buttons;
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
    }

    public struct GoldSourceDemoInfo
    {
         
    }

    public class GoldSourceParser
    {
        public static GoldSourceDemoInfoHlsooe ParseDemoHlsooe(string s) //Add error out
        {
            var gDemo = new GoldSourceDemoInfoHlsooe {Header = new Hlsooe.DemoHeader()};
            using (var br = new BinaryReader(new FileStream(s, FileMode.Open)))
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
                    var entryCount = br.ReadInt32();
                    for (var i = 0; i < entryCount; i++)
                    {
                        var tempvar = new Hlsooe.DemoDirectoryEntry
                        {
                            Type = br.ReadInt32(),
                            PlaybackTime = br.ReadSingle(),
                            FrameCount = br.ReadInt32(),
                            Offset = br.ReadInt32(),
                            Filelength = br.ReadInt32()
                        };
                        gDemo.DirectoryEntries.Add(tempvar);
                    }
                    //Demo directory entries parsed... now we parse the frames.
                    foreach (var entry in gDemo.DirectoryEntries)
                    {
                        var nextSectionRead = false;
                        for (var i = 0; i < entry.FrameCount; i++)
                        {
                            var frameType = (Hlsooe.DemoFrameType) br.ReadSByte();
                            var time = br.ReadSingle();
                            var tick = br.ReadInt32();
                            if (!nextSectionRead)
                            {
                                switch (frameType)
                                {
                                    case Hlsooe.DemoFrameType.StartupPacket:
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
                                        break;
                                    case Hlsooe.DemoFrameType.Jumptime:
                                        //No extra stuff
                                        break;
                                    case Hlsooe.DemoFrameType.ConsoleCommand:
                                        var a = new Hlsooe.ConsoleCommandFrame();
                                        br.ReadInt32(); //But we don't need it in theory since c# is clewer c:
                                        a.Command = br.ReadString();
                                        break;
                                    case Hlsooe.DemoFrameType.Usercmd:
                                        var c = new Hlsooe.UserCmdFrame
                                        {
                                            OutgoingSequence = br.ReadInt32(),
                                            Slot = br.ReadInt32()
                                        };
                                        br.ReadInt32(); //But we don't need it in theory since c# is clewer c:
                                        c.Data = br.ReadString();
                                        break;
                                    case Hlsooe.DemoFrameType.Stringtables:
                                        var e = new Hlsooe.StringTablesFrame();
                                        br.ReadInt32(); //But we don't need it in theory since c# is clewer c:
                                        e.Data = br.ReadString();
                                        break;
                                    case Hlsooe.DemoFrameType.NetworkDataTable:
                                        var d = new Hlsooe.NetworkDataTableFrame();
                                        br.ReadInt32(); //But we don't need it in theory since c# is clewer c:
                                        d.Data = br.ReadString();
                                        break;
                                    case Hlsooe.DemoFrameType.NextSection:
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
            return gDemo;
        }

        public static GoldSourceParser Parsedemo(string s)
        {
            //TODO: Implement this. Nearly copy paste of hlsooe.
            return new GoldSourceParser();
        }
    }
}