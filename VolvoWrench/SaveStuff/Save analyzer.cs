using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VolvoWrench.SaveStuff
{
    public partial class saveanalyzerform : Form
    {
        public Listsave.SaveFile CurrentSaveFile;
        public List<Listsave.StateFileInfo> PFileliList;

        public saveanalyzerform()
        {
            InitializeComponent();
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            propertyGrid1.AutoScaleMode = AutoScaleMode.None;
        }

        public saveanalyzerform(string file)
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
                    PrintandAnalyze(of.FileName);
                Text = @"Save analyzer - " + Path.GetFileName(of.FileName);
            }
        }

        public void PrintandAnalyze(string s)
        {
            CurrentSaveFile = Listsave.ParseSaveFile(s);
            propertyGrid1.SelectedObject = CurrentSaveFile;
            PFileliList = CurrentSaveFile.Files;
            PopulateTreeView(CurrentSaveFile.Files);
        }

        private void PopulateTreeView(List<Listsave.StateFileInfo> FileList)
        {
            treeView1.Nodes.Clear();
            if (FileList.Count > 1)
            {
                var rootNode = new TreeNode("Savefile");
                GetDirectories(FileList, rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(List<Listsave.StateFileInfo> vlvs, TreeNode nodeToAddTo)
        {
            foreach (var subDir in vlvs)
            {
                var aNode = new TreeNode(subDir.FileName, 0, 0)
                {
                    Tag = subDir,
                    ImageKey = @"folder"
                };
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            propertyGrid1.SelectedObject = PFileliList.FirstOrDefault(x => e.Node.Text.Contains(x.FileName));
        }
    }
}