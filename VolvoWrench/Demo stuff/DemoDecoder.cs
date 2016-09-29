using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VolvoWrench.Netdec
{
    public partial class DemoDecoder : Form
    {
        public SourceParser CurrentFile;
        public Stream File;

        public DemoDecoder(Stream f)
        {
            InitializeComponent();
            File = f;
            messageList.Items.Clear();
            CurrentFile = new SourceParser(f);
            f.Close();

            foreach (var msg in CurrentFile.Messages)
            {
                messageList.Items.Add(new DemoMessageItem(msg));
            }
        }

        private void ParseIntoTree(SourceParser.DemoMessage msg)
        {
            var node = new TreeNode($"{msg.Type}, tick {msg.Tick}, {msg.Data.Length} bytes");
            node.Expand();
            node.BackColor = DemoMessageItem.GetTypeColor(msg.Type);

            switch (msg.Type)
            {
                case SourceParser.MessageType.ConsoleCmd:
                    node.Nodes.Add(Encoding.ASCII.GetString(msg.Data));
                    break;
                case SourceParser.MessageType.UserCmd:
                    UserCmd.ParseIntoTreeNode(msg.Data, node);
                    break;
                case SourceParser.MessageType.Signon:
                case SourceParser.MessageType.Packet:
                    Packet.Parse(msg.Data, node);
                    break;
                case SourceParser.MessageType.DataTables:
                    DataTables.Parse(msg.Data, node);
                    break;
                case SourceParser.MessageType.SyncTick:
                    node.Nodes.Add("Sync client clock to demo tick");
                    break;
                default:
                    node.Nodes.Add($"Unhandled demo message type - {msg.Data} - {msg.Type} - {msg.Tick}"); //TODO: Fix

                    break;
            }

            messageTree.Nodes.Add(node);
        }

        private void messageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            messageTree.Nodes.Clear();

            foreach (DemoMessageItem item in messageList.SelectedItems)
            {
                ParseIntoTree(item.Msg);
            }
        }
    }

    internal class DemoMessageItem : ListViewItem
    {
        public SourceParser.DemoMessage Msg;

        public DemoMessageItem(SourceParser.DemoMessage msg)
        {
            Msg = msg;

            BackColor = GetTypeColor(msg.Type);
            Text = Msg.Tick.ToString();
            SubItems.Add(Msg.Type.ToString());
            SubItems.Add(Msg.Data.Length.ToString());
        }

        public static Color GetTypeColor(SourceParser.MessageType type)
        {
            switch (type)
            {
                case SourceParser.MessageType.Signon:
                case SourceParser.MessageType.Packet:
                    return Color.Indigo;
                case SourceParser.MessageType.UserCmd:
                    return Color.DarkGreen;
                case SourceParser.MessageType.ConsoleCmd:
                    return Color.DarkRed;
                default:
                    return Color.Black;
            }
        }
    }
}