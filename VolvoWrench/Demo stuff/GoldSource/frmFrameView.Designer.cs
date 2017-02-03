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
            this.frameTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // frameTreeView
            // 
            this.frameTreeView.BackColor = System.Drawing.SystemColors.InfoText;
            this.frameTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameTreeView.Location = new System.Drawing.Point(0, 0);
            this.frameTreeView.Name = "frameTreeView";
            this.frameTreeView.Size = new System.Drawing.Size(484, 497);
            this.frameTreeView.TabIndex = 0;
            // 
            // frmFrameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(484, 497);
            this.Controls.Add(this.frameTreeView);
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

        private System.Windows.Forms.TreeView frameTreeView;
    }
}