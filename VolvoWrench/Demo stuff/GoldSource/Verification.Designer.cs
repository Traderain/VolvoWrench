using System.ComponentModel;
using System.Windows.Forms;

namespace VolvoWrench.Demo_stuff.GoldSource
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
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mrtb = new System.Windows.Forms.RichTextBox();
            this.demoparserslave = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.demostartCommandToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select demos";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mrtb);
            this.splitContainer1.Size = new System.Drawing.Size(1125, 554);
            this.splitContainer1.SplitterDistance = 75;
            this.splitContainer1.TabIndex = 2;
            // 
            // mrtb
            // 
            this.mrtb.BackColor = System.Drawing.SystemColors.InfoText;
            this.mrtb.ContextMenuStrip = this.contextMenuStrip1;
            this.mrtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrtb.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.mrtb.Location = new System.Drawing.Point(0, 0);
            this.mrtb.Name = "mrtb";
            this.mrtb.Size = new System.Drawing.Size(1125, 475);
            this.mrtb.TabIndex = 1;
            this.mrtb.Text = "No files selected!";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.demostartCommandToClipboardToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(312, 56);
            // 
            // demostartCommandToClipboardToolStripMenuItem
            // 
            this.demostartCommandToClipboardToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.demostartCommandToClipboardToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.demostartCommandToClipboardToolStripMenuItem.Name = "demostartCommandToClipboardToolStripMenuItem";
            this.demostartCommandToClipboardToolStripMenuItem.Size = new System.Drawing.Size(311, 26);
            this.demostartCommandToClipboardToolStripMenuItem.Text = "Demostart command to clipboard";
            this.demostartCommandToClipboardToolStripMenuItem.Click += new System.EventHandler(this.demostartCommandToClipboardToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(311, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Verification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1125, 554);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Verification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Verification";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Verification_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Verification_DragEnter);
            this.DragLeave += new System.EventHandler(this.Verification_DragLeave);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Button button1;
        private SplitContainer splitContainer1;
        private RichTextBox mrtb;
        private BackgroundWorker demoparserslave;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem demostartCommandToClipboardToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}