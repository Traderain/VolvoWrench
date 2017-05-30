using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VolvoWrench.Demo_Stuff.GoldSource;

namespace VolvoWrench.Demo_stuff.GoldSource
{
    public partial class frmFrameView : Form
    {
        public static GoldSourceDemoInfo DemoInfo;
        public frmFrameView(GoldSourceDemoInfo info)
        {
            InitializeComponent();
            DemoInfo = info;
            ParseIntoNodes(info);
        }

        /// <summary>
        /// Parses the info into nodes
        /// </summary>
        /// <param name="info"></param>
        public void ParseIntoNodes(GoldSourceDemoInfo info)
        {
            for (var index = 0; index < info.DirectoryEntries.Count; index++)
            {
                var entrynode = new TreeNode("Directory entry [" + (index+1) + "] - " + info.DirectoryEntries[index].FrameCount);
                entrynode.ForeColor = Color.Chartreuse;
                foreach (var frame in info.DirectoryEntries[index].Frames)
                {
                    var row = index + "/" + frame.Key.FrameIndex + " " + "[" + frame.Key.Time + "s]: " +
                              frame.Key.Type.ToString().ToUpper();
                    var node = new TreeNode();
                    var subnode = new TreeNode();
                    node.ForeColor = Color.White;
                    subnode.ForeColor = Color.White;                   
                    switch (frame.Key.Type)
                    {
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.DemoStart:
                            break;
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.ConsoleCommand:
                        {
                            subnode.Text = ((Demo_Stuff.GoldSource.GoldSource.ConsoleCommandFrame) frame.Value).Command;
                            subnode.Nodes.Add(new TreeNode("Bytes: " + ((Demo_Stuff.GoldSource.GoldSource.ConsoleCommandFrame)frame.Value).BxtData?.Length){ForeColor = Color.AliceBlue});
                            node.BackColor = Color.DarkRed;
                            break;
                        }
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.ClientData:
                            break;
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.NextSection:
                        {
                            subnode.Text = @"End of the DirectoryEntry!";
                            break;
                        }
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.Event:
                            break;
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.WeaponAnim:
                            break;
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.Sound:
                            break;
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.DemoBuffer:
                            break;
                        default:
                        {
                            node.BackColor = Color.Green;
                            row = index + "/" + frame.Key.FrameIndex + " " + "[" + frame.Key.Time + "s]: " +
                              "NETMESSAGE";
                            var current = (Demo_Stuff.GoldSource.GoldSource.NetMsgFrame)frame.Value;
                            subnode.Text = 1/current.RParms.Frametime + @" FPS";
                            break;
                        }
                    }
                    node.Text = row;
                    if(subnode?.Text?.Length > 0)
                        node.Nodes.Add(subnode);
                    entrynode.Nodes.Add(node);
                }
                frameTreeView.Nodes.Add(entrynode);
            }
        }
    }
}
