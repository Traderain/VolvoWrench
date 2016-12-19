using System.ComponentModel;
using System.Windows.Forms;

namespace VolvoWrench.SaveStuff
{
    partial class saveanalyzerform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(saveanalyzerform));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.info = new System.Windows.Forms.TabPage();
            this.statefiles = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.info.SuspendLayout();
            this.statefiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open save file";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(143, 41);
            this.label1.MaximumSize = new System.Drawing.Size(359, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "No file opened";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(784, 556);
            this.splitContainer1.SplitterDistance = 59;
            this.splitContainer1.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.info);
            this.tabControl1.Controls.Add(this.statefiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 493);
            this.tabControl1.TabIndex = 0;
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.Black;
            this.info.Controls.Add(this.propertyGrid1);
            this.info.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.info.Location = new System.Drawing.Point(4, 25);
            this.info.Name = "info";
            this.info.Padding = new System.Windows.Forms.Padding(3);
            this.info.Size = new System.Drawing.Size(776, 464);
            this.info.TabIndex = 0;
            this.info.Text = "Info";
            // 
            // statefiles
            // 
            this.statefiles.BackColor = System.Drawing.Color.Black;
            this.statefiles.Controls.Add(this.splitContainer2);
            this.statefiles.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statefiles.Location = new System.Drawing.Point(4, 25);
            this.statefiles.Name = "statefiles";
            this.statefiles.Padding = new System.Windows.Forms.Padding(3);
            this.statefiles.Size = new System.Drawing.Size(776, 464);
            this.statefiles.TabIndex = 1;
            this.statefiles.Text = "State files";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategorySplitterColor = System.Drawing.SystemColors.ControlDarkDark;
            this.propertyGrid1.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.HelpBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.propertyGrid1.HelpForeColor = System.Drawing.SystemColors.ButtonFace;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedItemWithFocusForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.propertyGrid1.Size = new System.Drawing.Size(770, 458);
            this.propertyGrid1.TabIndex = 1;
            this.propertyGrid1.ViewBackColor = System.Drawing.Color.Black;
            this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.InactiveBorder;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid2);
            this.splitContainer2.Size = new System.Drawing.Size(770, 458);
            this.splitContainer2.SplitterDistance = 256;
            this.splitContainer2.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(256, 458);
            this.treeView1.TabIndex = 0;
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.DisabledItemForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.propertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid2.HelpBackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.propertyGrid2.HelpForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.propertyGrid2.LineColor = System.Drawing.SystemColors.InactiveCaption;
            this.propertyGrid2.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.Size = new System.Drawing.Size(510, 458);
            this.propertyGrid2.TabIndex = 0;
            this.propertyGrid2.ViewBackColor = System.Drawing.SystemColors.InfoText;
            this.propertyGrid2.ViewForeColor = System.Drawing.SystemColors.Window;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "08 Wrench.ico");
            this.imageList1.Images.SetKeyName(1, "stack.png");
            // 
            // saveanalyzerform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(784, 556);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "saveanalyzerform";
            this.ShowIcon = false;
            this.Text = "Save analyzer";
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.info.ResumeLayout(false);
            this.statefiles.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Label label1;
        private SplitContainer splitContainer1;
        private TabControl tabControl1;
        private TabPage info;
        private PropertyGrid propertyGrid1;
        private TabPage statefiles;
        private SplitContainer splitContainer2;
        private TreeView treeView1;
        private PropertyGrid propertyGrid2;
        private ImageList imageList1;
    }
}