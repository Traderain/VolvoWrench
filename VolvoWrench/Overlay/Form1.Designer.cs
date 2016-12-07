using System.ComponentModel;

namespace VolvoWrench.Overlay
{
    partial class OverlayForm
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
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this.DemoParserSlave = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // DemoParserSlave
            // 
            this.DemoParserSlave.WorkerReportsProgress = true;
            this.DemoParserSlave.WorkerSupportsCancellation = true;
            this.DemoParserSlave.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DirectoryScannerWorker_DoWork);
            this.DemoParserSlave.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.DirectoryScannerWorker_RunWorkerCompleted);
            this.DemoParserSlave.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(DirectoryScannerWorker_ProgressChanged);
            // 
            // timer1
            // 
            this._timer1.Enabled = true;
            this._timer1.Tick += new System.EventHandler(this.hotkeytimer_Tick);
            // 
            //// Overlay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Overlay";
            this.Text = "Overlay";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

