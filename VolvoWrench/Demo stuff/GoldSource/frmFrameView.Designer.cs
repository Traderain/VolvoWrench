namespace VolvoWrench.Demo_stuff.GoldSource
{
    partial class frmFrameView
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
            this.chFrame = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.frameListView = new System.Windows.Forms.ListView();
            this.chEntry = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // chFrame
            // 
            this.chFrame.Text = "Frame";
            this.chFrame.Width = 44;
            // 
            // chTime
            // 
            this.chTime.Text = "Time";
            // 
            // chType
            // 
            this.chType.Text = "Type";
            // 
            // chFT
            // 
            this.chFT.Text = "Frametime";
            // 
            // chMS
            // 
            this.chMS.Text = "MS";
            // 
            // frameListView
            // 
            this.frameListView.BackColor = System.Drawing.SystemColors.InfoText;
            this.frameListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEntry,
            this.chFrame,
            this.chTime,
            this.chType,
            this.chFT,
            this.chMS});
            this.frameListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameListView.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.frameListView.FullRowSelect = true;
            this.frameListView.GridLines = true;
            this.frameListView.Location = new System.Drawing.Point(0, 0);
            this.frameListView.Name = "frameListView";
            this.frameListView.ShowItemToolTips = true;
            this.frameListView.Size = new System.Drawing.Size(484, 497);
            this.frameListView.TabIndex = 0;
            this.frameListView.UseCompatibleStateImageBehavior = false;
            this.frameListView.View = System.Windows.Forms.View.Details;
            // 
            // chEntry
            // 
            this.chEntry.Text = "Entry";
            this.chEntry.Width = 37;
            // 
            // frmFrameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(484, 497);
            this.Controls.Add(this.frameListView);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFrameView";
            this.ShowIcon = false;
            this.Text = "Frame view";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader chFrame;
        private System.Windows.Forms.ColumnHeader chTime;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chFT;
        private System.Windows.Forms.ColumnHeader chMS;
        private System.Windows.Forms.ListView frameListView;
        private System.Windows.Forms.ColumnHeader chEntry;
    }
}