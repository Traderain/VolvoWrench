namespace VolvoWrench.Demo_stuff.L4D2Branch.PortalStuff
{
    sealed partial class DemoTimer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnheaderFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnheaderMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnheaderPlayer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStartTick = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStopTick = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1849, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.Snow;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(120, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(67, 4);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.InfoText;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnheaderFile,
            this.columnheaderMap,
            this.columnheaderPlayer,
            this.columnHeaderTT,
            this.columnHeaderStartTick,
            this.columnHeaderStopTick,
            this.columnHeaderAT,
            this.columnHeaderTime});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listView1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.listView1.Location = new System.Drawing.Point(0, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1849, 794);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // columnheaderFile
            // 
            this.columnheaderFile.Text = "File";
            // 
            // columnheaderMap
            // 
            this.columnheaderMap.Text = "Map";
            this.columnheaderMap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnheaderPlayer
            // 
            this.columnheaderPlayer.Text = "Player";
            this.columnheaderPlayer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeaderTT
            // 
            this.columnHeaderTT.Text = "Total Ticks";
            this.columnHeaderTT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeaderStartTick
            // 
            this.columnHeaderStartTick.Text = "Start Tick";
            this.columnHeaderStartTick.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeaderStopTick
            // 
            this.columnHeaderStopTick.Text = "Stop Tick";
            this.columnHeaderStopTick.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeaderAT
            // 
            this.columnHeaderAT.Text = "Adjusted Ticks";
            this.columnHeaderAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Time";
            this.columnHeaderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DemoTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1849, 822);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DemoTimer";
            this.ShowIcon = false;
            this.Text = "Demo Timer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DemoTimer_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DemoTimer_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnheaderFile;
        private System.Windows.Forms.ColumnHeader columnheaderMap;
        private System.Windows.Forms.ColumnHeader columnheaderPlayer;
        private System.Windows.Forms.ColumnHeader columnHeaderTT;
        private System.Windows.Forms.ColumnHeader columnHeaderStartTick;
        private System.Windows.Forms.ColumnHeader columnHeaderStopTick;
        private System.Windows.Forms.ColumnHeader columnHeaderAT;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
    }
}