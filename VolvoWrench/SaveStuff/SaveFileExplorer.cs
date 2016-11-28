using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VolvoWrench.SaveStuff
{
    public partial class SaveFileExplorer : Form
    {
        public List<Listsave.ValvFile> PFileliList;

        private void PopulateTreeView(List<Listsave.ValvFile> FileList)
        {
            if (FileList.Count > 1)
            {
                var rootNode = new TreeNode("Savefile");
                    GetDirectories(FileList, rootNode);
                    treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(List<Listsave.ValvFile> vlvs,TreeNode nodeToAddTo)
        {
            foreach (Listsave.ValvFile subDir in vlvs)
            {
                var aNode = new TreeNode(subDir.FileName, 0, 0)
                {
                    Tag = subDir,
                    ImageKey = @"folder"
                };
                nodeToAddTo.Nodes.Add(aNode);
            }
        }
        public SaveFileExplorer(List<Listsave.ValvFile> fileList)
        {
            InitializeComponent();
            PFileliList = fileList;
            PopulateTreeView(fileList);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            propertyGrid1.SelectedObject = PFileliList.FirstOrDefault(x => e.Node.Text.Contains(x.FileName));
        }
    }

}
