using System.ComponentModel;
using System.Windows.Forms;

namespace VolvoWrench
{
    sealed partial class Main
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
            this.demoToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.netdecodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heatmapGeneratorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.multidemoToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoVerificationToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoTimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goldSourceToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoDoctorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leveloverviewGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.warpPlannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourcerunsWikiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourcerunsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.richTextBox1.Size = new System.Drawing.Size(849, 475);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportDemoDataToolStripMenuItem,
            this.showLogToolStripMenuItem,
            this.copyAllToolStripMenuItem,
            this.rescanFileToolStripMenuItem,
            this.renameDemoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 114);
            // 
            // exportDemoDataToolStripMenuItem
            // 
            this.exportDemoDataToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.exportDemoDataToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.exportDemoDataToolStripMenuItem.Name = "exportDemoDataToolStripMenuItem";
            this.exportDemoDataToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.exportDemoDataToolStripMenuItem.Text = "Export demo data";
            this.exportDemoDataToolStripMenuItem.Click += new System.EventHandler(this.exportDemoDataToolStripMenuItem_Click);
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.showLogToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.showLogToolStripMenuItem.Text = "Show log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.showLogToolStripMenuItem_Click);
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
            this.renameDemoToolStripMenuItem.Click += new System.EventHandler(this.renameDemoToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.demoToolsToolStripMenuItem,
            this.saveToolsToolStripMenuItem,
            this.mapToolsToolStripMenuItem,
            this.overlayToolStripMenuItem,
            this.helpToolStripMenuItem1,
            this.settingsToolStripMenuItem});
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
            // demoToolsToolStripMenuItem
            // 
            this.demoToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.multidemoToolsToolStripMenuItem,
            this.goldSourceToolsToolStripMenuItem});
            this.demoToolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.demoToolsToolStripMenuItem.Name = "demoToolsToolStripMenuItem";
            this.demoToolsToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.demoToolsToolStripMenuItem.Text = "Demo tools";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.netdecodeToolStripMenuItem,
            this.heatmapGeneratorToolStripMenuItem1,
            this.statisticsToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.toolsToolStripMenuItem.Text = "Source Tools";
            // 
            // netdecodeToolStripMenuItem
            // 
            this.netdecodeToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.netdecodeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.netdecodeToolStripMenuItem.Name = "netdecodeToolStripMenuItem";
            this.netdecodeToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.netdecodeToolStripMenuItem.Text = "Netdecode";
            this.netdecodeToolStripMenuItem.Click += new System.EventHandler(this.netdecodeToolStripMenuItem_Click);
            // 
            // heatmapGeneratorToolStripMenuItem1
            // 
            this.heatmapGeneratorToolStripMenuItem1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.heatmapGeneratorToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.heatmapGeneratorToolStripMenuItem1.Name = "heatmapGeneratorToolStripMenuItem1";
            this.heatmapGeneratorToolStripMenuItem1.Size = new System.Drawing.Size(178, 22);
            this.heatmapGeneratorToolStripMenuItem1.Text = "Heatmap Generator";
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statisticsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            this.statisticsToolStripMenuItem.Click += new System.EventHandler(this.statisticsToolStripMenuItem_Click);
            // 
            // multidemoToolsToolStripMenuItem
            // 
            this.multidemoToolsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.multidemoToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.demoVerificationToolToolStripMenuItem,
            this.demoTimerToolStripMenuItem});
            this.multidemoToolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.multidemoToolsToolStripMenuItem.Name = "multidemoToolsToolStripMenuItem";
            this.multidemoToolsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.multidemoToolsToolStripMenuItem.Text = "Multi-demo tools";
            // 
            // demoVerificationToolToolStripMenuItem
            // 
            this.demoVerificationToolToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.demoVerificationToolToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.demoVerificationToolToolStripMenuItem.Name = "demoVerificationToolToolStripMenuItem";
            this.demoVerificationToolToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.demoVerificationToolToolStripMenuItem.Text = "Demo verification tool";
            this.demoVerificationToolToolStripMenuItem.Click += new System.EventHandler(this.demoVerificationToolToolStripMenuItem_Click);
            // 
            // demoTimerToolStripMenuItem
            // 
            this.demoTimerToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.demoTimerToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.demoTimerToolStripMenuItem.Name = "demoTimerToolStripMenuItem";
            this.demoTimerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.demoTimerToolStripMenuItem.Text = "Demo timer";
            this.demoTimerToolStripMenuItem.Click += new System.EventHandler(this.demoTimerToolStripMenuItem_Click);
            // 
            // goldSourceToolsToolStripMenuItem
            // 
            this.goldSourceToolsToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.goldSourceToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.demoDoctorToolStripMenuItem});
            this.goldSourceToolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.goldSourceToolsToolStripMenuItem.Name = "goldSourceToolsToolStripMenuItem";
            this.goldSourceToolsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.goldSourceToolsToolStripMenuItem.Text = "GoldSource Tools";
            // 
            // demoDoctorToolStripMenuItem
            // 
            this.demoDoctorToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.demoDoctorToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.demoDoctorToolStripMenuItem.Name = "demoDoctorToolStripMenuItem";
            this.demoDoctorToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.demoDoctorToolStripMenuItem.Text = "Demo doctor";
            this.demoDoctorToolStripMenuItem.Click += new System.EventHandler(this.demoDoctorToolStripMenuItem_Click);
            // 
            // saveToolsToolStripMenuItem
            // 
            this.saveToolsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.saveToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveExplorerToolStripMenuItem});
            this.saveToolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.saveToolsToolStripMenuItem.Name = "saveToolsToolStripMenuItem";
            this.saveToolsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.saveToolsToolStripMenuItem.Text = "Save tools";
            // 
            // saveExplorerToolStripMenuItem
            // 
            this.saveExplorerToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.saveExplorerToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.saveExplorerToolStripMenuItem.Name = "saveExplorerToolStripMenuItem";
            this.saveExplorerToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.saveExplorerToolStripMenuItem.Text = "Save explorer";
            this.saveExplorerToolStripMenuItem.Click += new System.EventHandler(this.saveExplorerToolStripMenuItem_Click);
            // 
            // mapToolsToolStripMenuItem
            // 
            this.mapToolsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.mapToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.leveloverviewGeneratorToolStripMenuItem,
            this.warpPlannerToolStripMenuItem});
            this.mapToolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.mapToolsToolStripMenuItem.Name = "mapToolsToolStripMenuItem";
            this.mapToolsToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.mapToolsToolStripMenuItem.Text = "Map tools";
            // 
            // leveloverviewGeneratorToolStripMenuItem
            // 
            this.leveloverviewGeneratorToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.leveloverviewGeneratorToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.leveloverviewGeneratorToolStripMenuItem.Name = "leveloverviewGeneratorToolStripMenuItem";
            this.leveloverviewGeneratorToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.leveloverviewGeneratorToolStripMenuItem.Text = "Leveloverview generator";
            this.leveloverviewGeneratorToolStripMenuItem.Click += new System.EventHandler(this.leveloverviewGeneratorToolStripMenuItem_Click);
            // 
            // warpPlannerToolStripMenuItem
            // 
            this.warpPlannerToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.warpPlannerToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.warpPlannerToolStripMenuItem.Name = "warpPlannerToolStripMenuItem";
            this.warpPlannerToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.warpPlannerToolStripMenuItem.Text = "Warp planner";
            this.warpPlannerToolStripMenuItem.Click += new System.EventHandler(this.warpPlannerToolStripMenuItem_Click);
            // 
            // overlayToolStripMenuItem
            // 
            this.overlayToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.overlayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem,
            this.settingsToolStripMenuItem1});
            this.overlayToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.overlayToolStripMenuItem.Name = "overlayToolStripMenuItem";
            this.overlayToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.overlayToolStripMenuItem.Text = "Overlay";
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.launchToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.launchToolStripMenuItem.Text = "Launch";
            this.launchToolStripMenuItem.Click += new System.EventHandler(this.launchToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.settingsToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Checked = true;
            this.helpToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
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
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShowShortcutKeys = false;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // sourcerunsWikiToolStripMenuItem
            // 
            this.sourcerunsWikiToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sourcerunsWikiToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sourcerunsWikiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sourcerunsWikiToolStripMenuItem.Image")));
            this.sourcerunsWikiToolStripMenuItem.Name = "sourcerunsWikiToolStripMenuItem";
            this.sourcerunsWikiToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.sourcerunsWikiToolStripMenuItem.Text = "Sourceruns Wiki";
            this.sourcerunsWikiToolStripMenuItem.Click += new System.EventHandler(this.sourcerunsWikiToolStripMenuItem_Click);
            // 
            // sourcerunsToolStripMenuItem
            // 
            this.sourcerunsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sourcerunsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sourcerunsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sourcerunsToolStripMenuItem.Image")));
            this.sourcerunsToolStripMenuItem.Name = "sourcerunsToolStripMenuItem";
            this.sourcerunsToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.sourcerunsToolStripMenuItem.Text = "Sourceruns";
            this.sourcerunsToolStripMenuItem.Click += new System.EventHandler(this.sourcerunsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hotkeysToolStripMenuItem,
            this.fontToolStripMenuItem1});
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // hotkeysToolStripMenuItem
            // 
            this.hotkeysToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hotkeysToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.hotkeysToolStripMenuItem.Name = "hotkeysToolStripMenuItem";
            this.hotkeysToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.hotkeysToolStripMenuItem.Text = "Hotkeys";
            this.hotkeysToolStripMenuItem.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click_1);
            // 
            // fontToolStripMenuItem1
            // 
            this.fontToolStripMenuItem1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fontToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fontToolStripMenuItem1.Name = "fontToolStripMenuItem1";
            this.fontToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.fontToolStripMenuItem1.Text = "Font";
            this.fontToolStripMenuItem1.Click += new System.EventHandler(this.fontToolStripMenuItem1_Click);
            // 
            // HotkeyTimer
            // 
            this.HotkeyTimer.Enabled = true;
            this.HotkeyTimer.Interval = 300;
            this.HotkeyTimer.Tick += new System.EventHandler(this.HotkeyTimer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(849, 499);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.Name = "Main";
            this.Text = "VolvoWrench";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.DragLeave += new System.EventHandler(this.Main_DragLeave);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox richTextBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exportDemoDataToolStripMenuItem;
        private ToolStripMenuItem showLogToolStripMenuItem;
        private ToolStripMenuItem copyAllToolStripMenuItem;
        private ToolStripMenuItem rescanFileToolStripMenuItem;
        private ToolStripMenuItem renameDemoToolStripMenuItem;
        private Timer HotkeyTimer;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem sourcerunsWikiToolStripMenuItem;
        private ToolStripMenuItem sourcerunsToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem hotkeysToolStripMenuItem;
        private ToolStripMenuItem fontToolStripMenuItem1;
        private ToolStripMenuItem demoToolsToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem netdecodeToolStripMenuItem;
        private ToolStripMenuItem heatmapGeneratorToolStripMenuItem1;
        private ToolStripMenuItem statisticsToolStripMenuItem;
        private ToolStripMenuItem multidemoToolsToolStripMenuItem;
        private ToolStripMenuItem demoVerificationToolToolStripMenuItem;
        private ToolStripMenuItem goldSourceToolsToolStripMenuItem;
        private ToolStripMenuItem demoDoctorToolStripMenuItem;
        private ToolStripMenuItem overlayToolStripMenuItem;
        private ToolStripMenuItem launchToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem1;
        private ToolStripMenuItem demoTimerToolStripMenuItem;
        private ToolStripMenuItem saveToolsToolStripMenuItem;
        private ToolStripMenuItem saveExplorerToolStripMenuItem;
        private ToolStripMenuItem mapToolsToolStripMenuItem;
        private ToolStripMenuItem leveloverviewGeneratorToolStripMenuItem;
        private ToolStripMenuItem warpPlannerToolStripMenuItem;
    }
}

