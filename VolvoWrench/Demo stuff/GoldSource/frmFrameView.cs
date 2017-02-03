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
                foreach (var frame in info.DirectoryEntries[index].Frames)
                {
                    string[] row = { index.ToString(),
                        frame.Key.Index.ToString(),
                        frame.Key.Time.ToString(),
                        frame.Key.Type.ToString().ToUpper(),};
                    switch (frame.Key.Type)
                    {
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.DemoStart:
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.ConsoleCommand:
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.ClientData:
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.NextSection:
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.Event:
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.WeaponAnim:
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.Sound:
                        case Demo_Stuff.GoldSource.GoldSource.DemoFrameType.DemoBuffer:
                            break;
                        default:
                        {
                            var nframe = (Demo_Stuff.GoldSource.GoldSource.NetMsgFrame) frame.Value;
                            row = new string[]{
                                index.ToString(),
                                frame.Key.Index.ToString(),
                                frame.Key.Time.ToString(),
                                "NETMESSAGE",
                                nframe.RParms.Frametime.ToString(),
                                nframe.UCmd.Msec.ToString()};
                            break;
                        }
                    }
                    frameListView.Items.Add(new ListViewItem(row));
                }
            }
            frameListView.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            frameListView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            frameListView.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            frameListView.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            frameListView.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            frameListView.Columns[5].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.Width = frameListView.Columns[0].Width +
                         frameListView.Columns[1].Width +
                         frameListView.Columns[2].Width +
                         frameListView.Columns[3].Width +
                         frameListView.Columns[4].Width +
                         frameListView.Columns[5].Width;
        }
    }
}
