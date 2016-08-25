using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VolvoWrench.Netdec
{
    public partial class DemoDecoder : Form
    {
        public DemoFile CurrentFile;
        public Stream File;
        public DemoDecoder(Stream f)
        {
            InitializeComponent();
                File = f;
                messageList.Items.Clear();
                CurrentFile = new DemoFile(f);
                f.Close();

                foreach (var msg in CurrentFile.Messages)
                {
                    messageList.Items.Add(new DemoMessageItem(msg));
                }
        }

        private void ParseIntoTree(DemoFile.DemoMessage msg)
        {
            var node = new TreeNode($"{msg.Type}, tick {msg.Tick}, {msg.Data.Length} bytes");
            node.Expand();
            node.BackColor = DemoMessageItem.GetTypeColor(msg.Type);

            switch (msg.Type)
            {
                case DemoFile.MessageType.ConsoleCmd:
                    node.Nodes.Add(Encoding.ASCII.GetString(msg.Data));
                    break;
                case DemoFile.MessageType.UserCmd:
                    UserCmd.ParseIntoTreeNode(msg.Data, node);
                    break;
                case DemoFile.MessageType.Signon:
                case DemoFile.MessageType.Packet:
                    Packet.Parse(msg.Data, node);
                    break;
                case DemoFile.MessageType.DataTables:
                    DataTables.Parse(msg.Data, node);
                    break;
                case DemoFile.MessageType.SyncTick:
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

            foreach(DemoMessageItem item in messageList.SelectedItems) {
                ParseIntoTree(item.Msg);
            }
        }
    }

    class DemoMessageItem : ListViewItem
    {
        public DemoFile.DemoMessage Msg;

        public DemoMessageItem(DemoFile.DemoMessage msg)
        {
            Msg = msg;

            BackColor = GetTypeColor(msg.Type);
            Text = Msg.Tick.ToString();
            SubItems.Add(Msg.Type.ToString());
            SubItems.Add(Msg.Data.Length.ToString());
        }

        public static Color GetTypeColor(DemoFile.MessageType type)
        {
            switch(type) {
                case DemoFile.MessageType.Signon:
                case DemoFile.MessageType.Packet:
                    return Color.Indigo;
                case DemoFile.MessageType.UserCmd:
                    return Color.DarkGreen;
                case DemoFile.MessageType.ConsoleCmd:
                    return Color.DarkRed;
                default:
                    return Color.Black;
            }
        }
    }
}
