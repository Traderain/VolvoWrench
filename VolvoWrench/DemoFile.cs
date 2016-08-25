using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace netdecode
{
    class DemoFile
    {
        public struct DemoInfo
        {
            public int DemoProtocol, NetProtocol, TickCount, FrameCount, SignonLength;
            public string ServerName, ClientName, MapName, GameDirectory;
            public float Seconds;
        }

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

        public class DemoMessage
        {
            public MessageType Type;
            public int Tick;
            public byte[] Data;
        }

        Stream fstream;
        public DemoInfo Info;
        public List<DemoMessage> Messages;

        public DemoFile(Stream s)
        {
            fstream = s;
            Messages = new List<DemoMessage>();
            Parse();
        }

        void Parse()
        {
            var reader = new BinaryReader(fstream);
            var id = reader.ReadBytes(8);

            if (Encoding.ASCII.GetString(id) != "HL2DEMO\0")
                throw new Exception("Unsupported file format.");

            Info.DemoProtocol = reader.ReadInt32();
            if (Info.DemoProtocol >> 8 > 0)
                throw new Exception("Demos recorded on L4D branch games are currently unsupported.");

            Info.NetProtocol = reader.ReadInt32();

            Info.ServerName = new string(reader.ReadChars(260)).Replace("\0", "");
            Info.ClientName = new string(reader.ReadChars(260)).Replace("\0", "");
            Info.MapName = new string(reader.ReadChars(260)).Replace("\0", "");
            Info.GameDirectory = new string(reader.ReadChars(260)).Replace("\0", "");

            Info.Seconds = reader.ReadSingle();
            Info.TickCount = reader.ReadInt32();
            Info.FrameCount = reader.ReadInt32();

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
                        msg.Data = new byte[0]; // lol wut
                        break;
                    default:
                        throw new Exception("Unknown demo message type encountered.");
                }

                Messages.Add(msg);
            }
        }
    }

    public class UserCmd
    {
/*
        static string ParseButtons(uint buttons)
        {
            string res = "(none)";
            // TODO: IMPLEMENT
            return res;
        }
*/

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
            if (bb.ReadBool()) node.Nodes.Add("Up move: " + bb.ReadFloat().ToString());
            if (bb.ReadBool()) node.Nodes.Add("Buttons: 0x" + bb.ReadBits(32).ToString("X8"));
            if (bb.ReadBool()) node.Nodes.Add("Impulse: " + bb.ReadBits(8));
            // TODO: weaponselect/weaponsubtype, mousedx/dy
        }
    }

}
