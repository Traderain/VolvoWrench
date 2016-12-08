using System.ComponentModel;
using System.Windows.Forms;

namespace VolvoWrench.Demo_Stuff.Source
{
    partial class DemoDecoder
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.messageList = new System.Windows.Forms.ListView();
            this.Tick = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MsgSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.messageTree = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.messageList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.messageTree);
            this.splitContainer1.Size = new System.Drawing.Size(827, 705);
            this.splitContainer1.SplitterDistance = 252;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // messageList
            // 
            this.messageList.BackColor = System.Drawing.SystemColors.MenuText;
            this.messageList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Tick,
            this.Type,
            this.MsgSize});
            this.messageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageList.ForeColor = System.Drawing.SystemColors.Info;
            this.messageList.FullRowSelect = true;
            this.messageList.Location = new System.Drawing.Point(0, 0);
            this.messageList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.messageList.Name = "messageList";
            this.messageList.Size = new System.Drawing.Size(827, 252);
            this.messageList.TabIndex = 0;
            this.messageList.UseCompatibleStateImageBehavior = false;
            this.messageList.View = System.Windows.Forms.View.Details;
            this.messageList.SelectedIndexChanged += new System.EventHandler(this.messageList_SelectedIndexChanged);
            // 
            // Index
            // 
            this.Tick.Text = "Index";
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 403;
            // 
            // MsgSize
            // 
            this.MsgSize.Text = "TokenTableFileTableOffset";
            this.MsgSize.Width = 87;
            // 
            // messageTree
            // 
            this.messageTree.BackColor = System.Drawing.SystemColors.MenuText;
            this.messageTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageTree.ForeColor = System.Drawing.SystemColors.Info;
            this.messageTree.FullRowSelect = true;
            this.messageTree.Location = new System.Drawing.Point(0, 0);
            this.messageTree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.messageTree.Name = "messageTree";
            this.messageTree.ShowLines = false;
            this.messageTree.Size = new System.Drawing.Size(827, 448);
            this.messageTree.TabIndex = 0;
            // 
            // DemoDecoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 705);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DemoDecoder";
            this.ShowIcon = false;
            this.Text = "Demo decoder";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private SplitContainer splitContainer1;
        private ListView messageList;
        private ColumnHeader Tick;
        private ColumnHeader Type;
        private ColumnHeader MsgSize;
        private TreeView messageTree;
    }
}

