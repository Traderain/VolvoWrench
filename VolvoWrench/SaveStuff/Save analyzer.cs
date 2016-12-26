using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VolvoWrench.SaveStuff
{
    public partial class Saveanalyzerform : Form
    {
        public Listsave.SaveFile CurrentSaveFile;
        public List<Listsave.StateFileInfo> PFileliList;
        public string SaveFileName;

        public Saveanalyzerform()
        {
            InitializeComponent();
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            propertyGrid1.AutoScaleMode = AutoScaleMode.None;
        }

        public Saveanalyzerform(string file)
        {
            InitializeComponent();
            PrintandAnalyze(file);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            using (var of = new OpenFileDialog())
            {
                of.Multiselect = false;
                of.Filter = @"Demo files (*.sav) | *.sav";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    SaveFileName = of.FileName;
                    PrintandAnalyze(of.FileName);
                    Text = @"Save analyzer - " + Path.GetFileName(of.FileName);
                }
            }
        }

        public void PrintandAnalyze(string s)
        {
            CurrentSaveFile = Listsave.ParseSaveFile(s);
            propertyGrid1.SelectedObject = CurrentSaveFile;
            PFileliList = CurrentSaveFile.Files;
            PopulateTreeView(CurrentSaveFile.Files);
        }

        private void PopulateTreeView(IReadOnlyCollection<Listsave.StateFileInfo> fileList)
        {
            treeView1.Nodes.Clear();
            if (fileList.Count > 1)
            {
                var rootNode = new TreeNode(Path.GetFileName(SaveFileName));
                GetDirectories(fileList, rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private static void GetDirectories(IEnumerable<Listsave.StateFileInfo> vlvs, TreeNode nodeToAddTo)
        {
            foreach (var aNode in vlvs.Select(subDir => new TreeNode(subDir.FileName, 0, 0)
            {
                Tag = subDir,
                ImageKey = @"folder"
            }))
            {
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            propertyGrid2.SelectedObject = PFileliList.FirstOrDefault(x => e.Node.Text.Contains(x.FileName));
        }
    }
}