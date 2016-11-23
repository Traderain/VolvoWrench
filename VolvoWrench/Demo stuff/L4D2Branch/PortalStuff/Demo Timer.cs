using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VolvoWrench.Demo_stuff.L4D2Branch.PortalStuff
{
    public sealed partial class DemoTimer : Form
    {
        public int TickSum;
        public TimeSpan TimeSum;

        public DemoTimer()
        {
            InitializeComponent();
            AllowDrop = true;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            this.Width = listView1.Columns[0].Width +
             listView1.Columns[1].Width +
             listView1.Columns[2].Width +
             listView1.Columns[3].Width +
             listView1.Columns[4].Width +
             listView1.Columns[5].Width +
             listView1.Columns[6].Width +
             listView1.Columns[7].Width;
            listView1.View = View.Details;
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[5].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[6].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[7].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.Width = listView1.Columns[0].Width +
                         listView1.Columns[1].Width +
                         listView1.Columns[2].Width +
                         listView1.Columns[3].Width +
                         listView1.Columns[4].Width +
                         listView1.Columns[5].Width +
                         listView1.Columns[6].Width +
                         listView1.Columns[7].Width + 50;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var of = new OpenFileDialog();
            of.Filter = @"Demo files (.dem) | *.dem";
            of.Multiselect = true;
            if (of.ShowDialog() == DialogResult.OK)
            {
                UpdateDgv(of.FileNames.Where(x => File.Exists(x) && Path.GetExtension(x) == ".dem").ToArray());
            }
        }

        private void DemoTimer_DragDrop(object sender, DragEventArgs e)
        {
            var dropfiles = ((string[])e.Data.GetData(DataFormats.FileDrop)).Where(x => File.Exists(x) && Path.GetExtension(x) == ".dem").ToArray();
            UpdateDgv(dropfiles);
        }

        private void DemoTimer_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        public void UpdateDgv(string[] files)
        {
            TickSum = 0;
            TimeSum = TimeSpan.FromSeconds(0);
            listView1.Items.Clear();
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            ListViewItem head = new ListViewItem();
            foreach (var s in files)
            {
                var r = CrossDemoParser.Parse(s);
                if (r.Type == Parseresult.Portal || r.Type == Parseresult.L4D2Branch)
                {
                    ListViewItem listViewItem2 = new ListViewItem(new string[8]);
                    var dd = new ListViewItem();
                    listViewItem2.SubItems[0].Text = Path.GetFileNameWithoutExtension(s);
                    listViewItem2.SubItems[1].Text = r.L4D2BranchInfo.Header.GameDirectory + "/" + r.L4D2BranchInfo.PortalDemoInfo.MapName;
                    listViewItem2.SubItems[2].Text = r.L4D2BranchInfo.PortalDemoInfo.PlayerName;
                    listViewItem2.SubItems[3].Text = r.L4D2BranchInfo.PortalDemoInfo.TotalTicks.ToString();
                    listViewItem2.SubItems[4].Text = r.L4D2BranchInfo.PortalDemoInfo.StartAdjustmentTick + "(" + r.L4D2BranchInfo.PortalDemoInfo.StartAdjustmentType + ")";
                    listViewItem2.SubItems[5].Text = r.L4D2BranchInfo.PortalDemoInfo.EndAdjustmentTick + "(" + r.L4D2BranchInfo.PortalDemoInfo.EndAdjustmentType + ")";
                    listViewItem2.SubItems[6].Text = r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks.ToString();
                    listViewItem2.SubItems[7].Text = TimeSpan.FromSeconds(r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks*(1f/(r.L4D2BranchInfo.Header.PlaybackTicks/r.L4D2BranchInfo.Header.PlaybackTime))).ToString("g");
                    TickSum += r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks;
                    TimeSum += TimeSpan.FromSeconds(r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks*(1f/(r.L4D2BranchInfo.Header.PlaybackTicks / r.L4D2BranchInfo.Header.PlaybackTime)));
                    listView1.Items.Add(listViewItem2);
                }
            }
            var k = new ListViewItem(new string[8]);
            k.SubItems[0].Text = "- TOTAL -";
            k.SubItems[6].Text = TickSum.ToString();
            k.SubItems[7].Text = TimeSum.ToString("g");
            listView1.Items.Add(k);
            listView1.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[5].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.Columns[6].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Columns[7].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.Width = listView1.Columns[0].Width +
                         listView1.Columns[1].Width +
                         listView1.Columns[2].Width +
                         listView1.Columns[3].Width +
                         listView1.Columns[4].Width +
                         listView1.Columns[5].Width +
                         listView1.Columns[6].Width +
                         listView1.Columns[7].Width + 100;
        }
    }
}
