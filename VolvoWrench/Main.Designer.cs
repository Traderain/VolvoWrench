namespace VolvoWrench
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportDemoDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rescanFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netdecodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heatmapGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourcerunsWikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourcerunsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HotkeyTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.MenuText;
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.EnableAutoDragDrop = true;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Location = new System.Drawing.Point(0, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ShowSelectionMargin = true;
            this.richTextBox1.Size = new System.Drawing.Size(849, 562);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDemoDataToolStripMenuItem,
            this.showLogToolStripMenuItem,
            this.copyAllToolStripMenuItem,
            this.rescanFileToolStripMenuItem,
            this.renameDemoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 136);
            // 
            // exportDemoDataToolStripMenuItem
            // 
            this.exportDemoDataToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exportDemoDataToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.exportDemoDataToolStripMenuItem.Name = "exportDemoDataToolStripMenuItem";
            this.exportDemoDataToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exportDemoDataToolStripMenuItem.Text = "Export demo data";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.showLogToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.showLogToolStripMenuItem.Text = "Show log";
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.copyAllToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copyAllToolStripMenuItem.Text = "Copy all";
            this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
            // 
            // rescanFileToolStripMenuItem
            // 
            this.rescanFileToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rescanFileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.rescanFileToolStripMenuItem.Name = "rescanFileToolStripMenuItem";
            this.rescanFileToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.rescanFileToolStripMenuItem.Text = "Rescan file";
            this.rescanFileToolStripMenuItem.Click += new System.EventHandler(this.rescanFileToolStripMenuItem_Click);
            // 
            // renameDemoToolStripMenuItem
            // 
            this.renameDemoToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.renameDemoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.renameDemoToolStripMenuItem.Name = "renameDemoToolStripMenuItem";
            this.renameDemoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.renameDemoToolStripMenuItem.Text = "Rename demo";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(849, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.exportToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netdecodeToolStripMenuItem,
            this.heatmapGeneratorToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // netdecodeToolStripMenuItem
            // 
            this.netdecodeToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.netdecodeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.netdecodeToolStripMenuItem.Name = "netdecodeToolStripMenuItem";
            this.netdecodeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.netdecodeToolStripMenuItem.Text = "Netdecode";
            this.netdecodeToolStripMenuItem.Click += new System.EventHandler(this.netdecodeToolStripMenuItem_Click);
            // 
            // heatmapGeneratorToolStripMenuItem
            // 
            this.heatmapGeneratorToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.heatmapGeneratorToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.heatmapGeneratorToolStripMenuItem.Name = "heatmapGeneratorToolStripMenuItem";
            this.heatmapGeneratorToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.heatmapGeneratorToolStripMenuItem.Text = "Heatmap generator";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem,
            this.sizeToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "View";
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fontToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fontToolStripMenuItem.Text = "Font";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sizeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sizeToolStripMenuItem.Text = "Graphs";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Checked = true;
            this.helpToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.checkForUpdateToolStripMenuItem,
            this.sourcerunsWikiToolStripMenuItem,
            this.sourcerunsToolStripMenuItem});
            this.helpToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Control;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.aboutToolStripMenuItem.Image = global::VolvoWrench.Properties.Resources.bulb;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShowShortcutKeys = false;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkForUpdateToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check for update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // sourcerunsWikiToolStripMenuItem
            // 
            this.sourcerunsWikiToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sourcerunsWikiToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sourcerunsWikiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sourcerunsWikiToolStripMenuItem.Image")));
            this.sourcerunsWikiToolStripMenuItem.Name = "sourcerunsWikiToolStripMenuItem";
            this.sourcerunsWikiToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.sourcerunsWikiToolStripMenuItem.Text = "Sourceruns Wiki";
            this.sourcerunsWikiToolStripMenuItem.Click += new System.EventHandler(this.sourcerunsWikiToolStripMenuItem_Click);
            // 
            // sourcerunsToolStripMenuItem
            // 
            this.sourcerunsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sourcerunsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sourcerunsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sourcerunsToolStripMenuItem.Image")));
            this.sourcerunsToolStripMenuItem.Name = "sourcerunsToolStripMenuItem";
            this.sourcerunsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.sourcerunsToolStripMenuItem.Text = "Sourceruns";
            this.sourcerunsToolStripMenuItem.Click += new System.EventHandler(this.sourcerunsToolStripMenuItem_Click);
            // 
            // HotkeyTimer
            // 
            this.HotkeyTimer.Enabled = true;
            this.HotkeyTimer.Tick += new System.EventHandler(this.HotkeyTimer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(849, 586);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("1942 report", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.Name = "Main";
            this.Text = "VolvoWrench";
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportDemoDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rescanFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameDemoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem netdecodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heatmapGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.Timer HotkeyTimer;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourcerunsWikiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sourcerunsToolStripMenuItem;
    }
}

