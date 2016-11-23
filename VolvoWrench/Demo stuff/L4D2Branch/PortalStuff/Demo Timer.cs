using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VolvoWrench.Demo_stuff.L4D2Branch.PortalStuff
{
    public sealed partial class DemoTimer : Form
    {
        public List<DemoDescription> FileList;
        public int TickSum;
        public TimeSpan TimeSum;

        public DemoTimer()
        {
            InitializeComponent();
            FileList = new List<DemoDescription>();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AllowDrop = true;
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
            FileList = new List<DemoDescription>();
            foreach (var s in files)
            {
                var r = CrossDemoParser.Parse(s);
                if (r.Type == Parseresult.Portal || r.Type == Parseresult.L4D2Branch)
                {
                    var dd = new DemoDescription
                    {
                        File = Path.GetFileNameWithoutExtension(s),
                        Map = r.L4D2BranchInfo.Header.GameDirectory + "/" + r.L4D2BranchInfo.PortalDemoInfo.MapName,
                        Player = r.L4D2BranchInfo.PortalDemoInfo.PlayerName,
                        TotalTicks = r.L4D2BranchInfo.PortalDemoInfo.TotalTicks.ToString(),
                        StartTick = r.L4D2BranchInfo.PortalDemoInfo.StartAdjustmentTick + "(" + r.L4D2BranchInfo.PortalDemoInfo.StartAdjustmentType + ")",
                        StopTick = r.L4D2BranchInfo.PortalDemoInfo.EndAdjustmentTick + "(" + r.L4D2BranchInfo.PortalDemoInfo.EndAdjustmentType + ")",
                        AdjustedTicks = r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks.ToString(),
                        Time = TimeSpan.FromSeconds(r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks*(1f/(r.L4D2BranchInfo.Header.PlaybackTicks/r.L4D2BranchInfo.Header.PlaybackTime))).ToString("g")
                    };
                    TickSum += r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks;
                    TimeSum += TimeSpan.FromSeconds(r.L4D2BranchInfo.PortalDemoInfo.AdjustedTicks*(1f/(r.L4D2BranchInfo.Header.PlaybackTicks / r.L4D2BranchInfo.Header.PlaybackTime)));
                    FileList.Add(dd);
                }
            }
            var k = new DemoDescription();
            k.File = "- TOTAL -";
            k.AdjustedTicks = TickSum.ToString();
            k.Time = TimeSum.ToString("g");
            FileList.Add(k);
            dataGridView1.DataSource = FileList.Select(x => new {File = x.File,
                Map = x.Map,
                Player = x.Player,
                TotalTicks = x.TotalTicks,
                StartTick = x.StartTick,
                StopTick = x.StopTick,
                AdjustedTicks = x.AdjustedTicks,
                Time = x.Time}).ToArray();
            for (var i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[i].ReadOnly = true;
            }
        }
    }

    public struct DemoDescription
    {
        public string File;
        public string Map;
        public string Player;
        public string TotalTicks;
        public string StartTick;
        public string StopTick;
        public string AdjustedTicks;
        public string Time;
    }
}
