using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VolvoWrench.Demo_stuff.Source
{
    public struct Saveflag
    {
        public string Name;
        public int Tick;
        public float Time;
    }

    public struct SourceDemoInfo
    {
        public int DemoProtocol, NetProtocol, TickCount, FrameCount, SignonLength;
        public List<Saveflag> Flags;
        public List<SourceParser.DemoMessage> Messages;
        public float Seconds;
        public string ServerName, ClientName, MapName, GameDirectory;
        public List<string> ParsingErrors;
    }

    public class SourceParser
    {
        public enum MessageType
        {
            Signon = 1,
            Packet,
            SyncTick,
            ConsoleCmd,
            UserCmd,
            DataTables,
            Stop,
            // CustomData, // L4D2
            StringTables
        }

        private readonly Stream _fstream;
        public SourceDemoInfo Info;

        public SourceParser(Stream s)
        {
            _fstream = s;
            Info.Messages = new List<DemoMessage>();
            Parse();
        }

        private void Parse()
        {
            var reader = new BinaryReader(_fstream);
            Info.Flags = new List<Saveflag>();
            Info.ParsingErrors = new List<string>();
            var id = reader.ReadBytes(8);

            if (Encoding.ASCII.GetString(id) != "HL2DEMO\0")
            {
                Info.ParsingErrors.Add("Source parser: Incorrect mw");
            }

            Info.DemoProtocol = reader.ReadInt32();
            if (Info.DemoProtocol >> 2 > 0)
            {
               Info.ParsingErrors.Add("Unsupported L4D2 branch demo!");
               return;
            }

            Info.NetProtocol = reader.ReadInt32();
     
            Info.ServerName = new string(reader.ReadChars(260)).Replace("\0", "");
            Info.ClientName = new string(reader.ReadChars(260)).Replace("\0", "");
            Info.MapName = new string(reader.ReadChars(260)).Replace("\0", "");
            Info.GameDirectory = new string(reader.ReadChars(260)).Replace("\0", "");

            Info.Seconds = Math.Abs(reader.ReadSingle());
            Info.TickCount = Math.Abs(reader.ReadInt32());
            Info.FrameCount = Math.Abs(reader.ReadInt32());

            Info.SignonLength = reader.ReadInt32();

            while (true)
            {
                var msg = new DemoMessage {Type = (MessageType) reader.ReadByte()};
                if (msg.Type == MessageType.Stop)
                    break;
                msg.Tick = reader.ReadInt32();

                switch (msg.Type)
                {
                    case MessageType.Signon:
                    case MessageType.Packet:
                    case MessageType.ConsoleCmd:
                    case MessageType.UserCmd:
                    case MessageType.DataTables:
                    case MessageType.StringTables:
                        if (msg.Type == MessageType.Packet || msg.Type == MessageType.Signon)
                            reader.BaseStream.Seek(0x54, SeekOrigin.Current); // command/sequence info
                        else if (msg.Type == MessageType.UserCmd)
                            reader.BaseStream.Seek(0x4, SeekOrigin.Current); // unknown
                        msg.Data = reader.ReadBytes(reader.ReadInt32());
                        break;
                    case MessageType.SyncTick:
                        msg.Data = new byte[0];
                        break;
                    default:
                        Main.Log("Unknown demo message type encountered: " + msg.Type + "at " + reader.BaseStream.Position);
                        Info.ParsingErrors.Add("Unknown demo message type encountered: " + msg.Type + "at " + reader.BaseStream.Position);
                        return;
                }

                if (msg.Data != null)
                {
                    if (Encoding.ASCII.GetString(msg.Data).Contains("#SAVE#") ||
                        Encoding.ASCII.GetString(msg.Data).Contains("autosave"))
                    {
                        var tempf = new Saveflag
                        {
                            Tick = msg.Tick,
                            Time = (float) (msg.Tick*0.015) //TODO: Add better ticrate here too
                        };
                        if (Encoding.ASCII.GetString(msg.Data).Contains("#SAVE#"))
                            tempf.Name = "#SAVE#";
                        if (Encoding.ASCII.GetString(msg.Data).Contains("autosave"))
                            tempf.Name = "autosave";
                        Info.Flags.Add(tempf);
                    }
                }
                Info.Messages.Add(msg);
            }
        }

        public class DemoMessage
        {
            public byte[] Data;
            public int Tick;
            public MessageType Type;
        }
    }

    public class UserCmd
    {
        public static void ParseIntoTreeNode(byte[] data, TreeNode node)
        {
            var bb = new BitBuffer(data);
            if (bb.ReadBool()) node.Nodes.Add("Command number: " + bb.ReadBits(32));
            if (bb.ReadBool()) node.Nodes.Add("Tick count: " + bb.ReadBits(32));
            if (bb.ReadBool()) node.Nodes.Add("Viewangle pitch: " + bb.ReadFloat());
            if (bb.ReadBool()) node.Nodes.Add("Viewangle yaw: " + bb.ReadFloat());
            if (bb.ReadBool()) node.Nodes.Add("Viewangle roll: " + bb.ReadFloat());
            if (bb.ReadBool()) node.Nodes.Add("Foward move: " + bb.ReadFloat());
            if (bb.ReadBool()) node.Nodes.Add("Side move: " + bb.ReadFloat());
            if (bb.ReadBool()) node.Nodes.Add("Up move: " + bb.ReadFloat());
            if (bb.ReadBool())
            {
                var k = Enum.GetName(typeof (Keys), bb.ReadBits(32));
                if (k?.Length > 0)
                    node.Nodes.Add("Buttons: " + k);
            }
            if (bb.ReadBool()) node.Nodes.Add("Impulse: " + bb.ReadBits(8));
            // TODO: weaponselect/weaponsubtype, mousedx/dy
        }
    }
}