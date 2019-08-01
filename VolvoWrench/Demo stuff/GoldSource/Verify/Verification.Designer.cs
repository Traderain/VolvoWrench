using System.ComponentModel;
using System.Windows.Forms;

namespace VolvoWrench.Demo_Stuff.GoldSource
{
    sealed partial class Verification
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.demostartCommandToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoparserslave = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDemosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CategoryCB = new System.Windows.Forms.ToolStripComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BXTTreeView = new System.Windows.Forms.TreeView();
            this.mrtb = new System.Windows.Forms.RichTextBox();
            this.viewRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.demostartCommandToClipboardToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(255, 48);
            // 
            // demostartCommandToClipboardToolStripMenuItem
            // 
            this.demostartCommandToClipboardToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.demostartCommandToClipboardToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.demostartCommandToClipboardToolStripMenuItem.Name = "demostartCommandToClipboardToolStripMenuItem";
            this.demostartCommandToClipboardToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.demostartCommandToClipboardToolStripMenuItem.Text = "Demostart command to clipboard";
            this.demostartCommandToClipboardToolStripMenuItem.Click += new System.EventHandler(this.demostartCommandToClipboardToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.categoryToolStripMenuItem,
            this.CategoryCB,
            this.viewRulesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(844, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDemosToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openDemosToolStripMenuItem
            // 
            this.openDemosToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openDemosToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.openDemosToolStripMenuItem.Name = "openDemosToolStripMenuItem";
            this.openDemosToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.openDemosToolStripMenuItem.Text = "Open demos";
            this.openDemosToolStripMenuItem.Click += new System.EventHandler(this.openDemosToolStripMenuItem_Click);
            // 
            // categoryToolStripMenuItem
            // 
            this.categoryToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.categoryToolStripMenuItem.Name = "categoryToolStripMenuItem";
            this.categoryToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.categoryToolStripMenuItem.Text = "Category:";
            // 
            // CategoryCB
            // 
            this.CategoryCB.BackColor = System.Drawing.SystemColors.InfoText;
            this.CategoryCB.ForeColor = System.Drawing.SystemColors.Control;
            this.CategoryCB.Items.AddRange(new object[] {
            "Scriptless",
            "Scripted",
            "Scripted w/ SW"});
            this.CategoryCB.Name = "CategoryCB";
            this.CategoryCB.Size = new System.Drawing.Size(121, 23);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BXTTreeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mrtb);
            this.splitContainer1.Size = new System.Drawing.Size(844, 423);
            this.splitContainer1.SplitterDistance = 281;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // BXTTreeView
            // 
            this.BXTTreeView.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BXTTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BXTTreeView.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.BXTTreeView.LineColor = System.Drawing.Color.White;
            this.BXTTreeView.Location = new System.Drawing.Point(0, 0);
            this.BXTTreeView.Margin = new System.Windows.Forms.Padding(2);
            this.BXTTreeView.Name = "BXTTreeView";
            this.BXTTreeView.Size = new System.Drawing.Size(281, 423);
            this.BXTTreeView.TabIndex = 0;
            // 
            // mrtb
            // 
            this.mrtb.BackColor = System.Drawing.SystemColors.MenuText;
            this.mrtb.ContextMenuStrip = this.contextMenuStrip1;
            this.mrtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrtb.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.mrtb.Location = new System.Drawing.Point(0, 0);
            this.mrtb.Margin = new System.Windows.Forms.Padding(2);
            this.mrtb.Name = "mrtb";
            this.mrtb.Size = new System.Drawing.Size(560, 423);
            this.mrtb.TabIndex = 0;
            this.mrtb.Text = "";
            // 
            // viewRulesToolStripMenuItem
            // 
            this.viewRulesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.viewRulesToolStripMenuItem.Name = "viewRulesToolStripMenuItem";
            this.viewRulesToolStripMenuItem.Size = new System.Drawing.Size(72, 23);
            this.viewRulesToolStripMenuItem.Text = "View rules";
            this.viewRulesToolStripMenuItem.Click += new System.EventHandler(this.ViewRulesToolStripMenuItem_Click);
            // 
            // Verification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(844, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Verification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Verification";
            this.Shown += new System.EventHandler(this.Verification_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Verification_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Verification_DragEnter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private BackgroundWorker demoparserslave;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem demostartCommandToClipboardToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openDemosToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TreeView BXTTreeView;
        private RichTextBox mrtb;
        private ToolStripMenuItem categoryToolStripMenuItem;
        private ToolStripComboBox CategoryCB;
        private ToolStripMenuItem viewRulesToolStripMenuItem;
    }
}