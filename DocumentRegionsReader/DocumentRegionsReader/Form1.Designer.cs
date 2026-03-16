namespace DocumentRegionsReader
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Open = new System.Windows.Forms.ToolStripButton();
            this.Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TemplateSelector = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ProcessAllRegions = new System.Windows.Forms.ToolStripButton();
            this.HolePunchRemoval = new System.Windows.Forms.ToolStripButton();
            this.Despeckle = new System.Windows.Forms.ToolStripButton();
            this.Deskew = new System.Windows.Forms.ToolStripButton();
            this.RotateRight = new System.Windows.Forms.ToolStripButton();
            this.RotateLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.Arrow = new System.Windows.Forms.ToolStripButton();
            this.zoom = new System.Windows.Forms.ToolStripButton();
            this.BestFit = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.documentAnnotationViewer1 = new Atalasoft.Annotate.UI.DocumentAnnotationViewer();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.Save,
            this.toolStripSeparator1,
            this.TemplateSelector,
            this.toolStripSeparator2,
            this.ProcessAllRegions,
            this.HolePunchRemoval,
            this.Despeckle,
            this.Deskew,
            this.RotateRight,
            this.RotateLeft,
            this.toolStripSeparator3,
            this.Arrow,
            this.zoom,
            this.BestFit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(750, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Open
            // 
            this.Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Open.Image = global::DocumentRegionsReader.Properties.Resources.Open;
            this.Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(23, 22);
            this.Open.Text = "Open";
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Save
            // 
            this.Save.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Save.Image = global::DocumentRegionsReader.Properties.Resources.Save;
            this.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(23, 22);
            this.Save.Text = "Load";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // TemplateSelector
            // 
            this.TemplateSelector.Name = "TemplateSelector";
            this.TemplateSelector.Size = new System.Drawing.Size(121, 25);
            this.TemplateSelector.Text = "Select a template";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ProcessAllRegions
            // 
            this.ProcessAllRegions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ProcessAllRegions.Image = global::DocumentRegionsReader.Properties.Resources.Script;
            this.ProcessAllRegions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProcessAllRegions.Name = "ProcessAllRegions";
            this.ProcessAllRegions.Size = new System.Drawing.Size(23, 22);
            this.ProcessAllRegions.Text = "ProcessAllRegions";
            this.ProcessAllRegions.ToolTipText = "Process all regions and output to a text file.";
            this.ProcessAllRegions.Click += new System.EventHandler(this.ProcessAllRegions_Click);
            // 
            // HolePunchRemoval
            // 
            this.HolePunchRemoval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.HolePunchRemoval.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.HolePunchRemoval.Image = global::DocumentRegionsReader.Properties.Resources.HolePunches;
            this.HolePunchRemoval.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HolePunchRemoval.Name = "HolePunchRemoval";
            this.HolePunchRemoval.Size = new System.Drawing.Size(23, 22);
            this.HolePunchRemoval.Text = "HolePunchRemoval";
            this.HolePunchRemoval.ToolTipText = "Remove Hole Punches";
            this.HolePunchRemoval.Click += new System.EventHandler(this.HolePunchRemoval_Click);
            // 
            // Despeckle
            // 
            this.Despeckle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Despeckle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Despeckle.Image = global::DocumentRegionsReader.Properties.Resources.Specks;
            this.Despeckle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Despeckle.Name = "Despeckle";
            this.Despeckle.Size = new System.Drawing.Size(23, 22);
            this.Despeckle.Text = "Despeckle";
            this.Despeckle.ToolTipText = "REmove Speckles From Image";
            this.Despeckle.Click += new System.EventHandler(this.Despeckle_Click);
            // 
            // Deskew
            // 
            this.Deskew.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Deskew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Deskew.Image = global::DocumentRegionsReader.Properties.Resources.Skewed;
            this.Deskew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Deskew.Name = "Deskew";
            this.Deskew.Size = new System.Drawing.Size(23, 22);
            this.Deskew.Text = "Deskew";
            this.Deskew.ToolTipText = "Deskew Image";
            this.Deskew.Click += new System.EventHandler(this.Deskew_Click);
            // 
            // RotateRight
            // 
            this.RotateRight.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RotateRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RotateRight.Image = global::DocumentRegionsReader.Properties.Resources.Rotate_Right;
            this.RotateRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RotateRight.Name = "RotateRight";
            this.RotateRight.Size = new System.Drawing.Size(23, 22);
            this.RotateRight.Text = "Rotate Right";
            this.RotateRight.ToolTipText = "Rotate Image Right";
            this.RotateRight.Click += new System.EventHandler(this.RotateRight_Click);
            // 
            // RotateLeft
            // 
            this.RotateLeft.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RotateLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RotateLeft.Image = global::DocumentRegionsReader.Properties.Resources.Rotate_Left;
            this.RotateLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RotateLeft.Name = "RotateLeft";
            this.RotateLeft.Size = new System.Drawing.Size(23, 22);
            this.RotateLeft.Text = "Rotate Left";
            this.RotateLeft.ToolTipText = "Rotate Image Left";
            this.RotateLeft.Click += new System.EventHandler(this.RotateLeft_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // Arrow
            // 
            this.Arrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Arrow.Image = global::DocumentRegionsReader.Properties.Resources.arrow_up_left;
            this.Arrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Arrow.Name = "Arrow";
            this.Arrow.Size = new System.Drawing.Size(23, 22);
            this.Arrow.Text = "Select";
            this.Arrow.Click += new System.EventHandler(this.Arrow_Click);
            // 
            // zoom
            // 
            this.zoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoom.Image = global::DocumentRegionsReader.Properties.Resources.view_24;
            this.zoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(23, 22);
            this.zoom.Text = "Zoom";
            this.zoom.Click += new System.EventHandler(this.zoom_Click);
            // 
            // BestFit
            // 
            this.BestFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BestFit.Image = global::DocumentRegionsReader.Properties.Resources.Fit_Width;
            this.BestFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BestFit.Name = "BestFit";
            this.BestFit.Size = new System.Drawing.Size(23, 22);
            this.BestFit.Text = "BestFit";
            this.BestFit.Click += new System.EventHandler(this.BestFit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.documentAnnotationViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(750, 542);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(250, 542);
            this.splitContainer2.SplitterDistance = 226;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(250, 226);
            this.treeView1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(250, 312);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(242, 286);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(242, 286);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // documentAnnotationViewer1
            // 
            this.documentAnnotationViewer1.AnnotationDataProvider = null;
            this.documentAnnotationViewer1.AnnotationSaveOptionsHandler = null;
            this.documentAnnotationViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentAnnotationViewer1.ImageControl.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ScaleToGray;
            this.documentAnnotationViewer1.ImageControl.BackColor = System.Drawing.SystemColors.Control;
            this.documentAnnotationViewer1.ImageControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.documentAnnotationViewer1.ImageControl.Magnifier.BackColor = System.Drawing.Color.White;
            this.documentAnnotationViewer1.ImageControl.Magnifier.BorderColor = System.Drawing.Color.Black;
            this.documentAnnotationViewer1.ImageControl.Magnifier.Size = new System.Drawing.Size(100, 100);
            this.documentAnnotationViewer1.Location = new System.Drawing.Point(0, 0);
            this.documentAnnotationViewer1.Name = "documentAnnotationViewer1";
            this.documentAnnotationViewer1.Separator.BackColor = System.Drawing.SystemColors.ControlLight;
            this.documentAnnotationViewer1.Size = new System.Drawing.Size(496, 542);
            this.documentAnnotationViewer1.TabIndex = 0;
            this.documentAnnotationViewer1.Text = "documentAnnotationViewer1";
            this.documentAnnotationViewer1.ThumbnailControl.BackColor = System.Drawing.SystemColors.Window;
            this.documentAnnotationViewer1.ThumbnailControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.documentAnnotationViewer1.ThumbnailControl.DragSelectionColor = System.Drawing.Color.Red;
            this.documentAnnotationViewer1.ThumbnailControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentAnnotationViewer1.ThumbnailControl.ForeColor = System.Drawing.SystemColors.WindowText;
            this.documentAnnotationViewer1.ThumbnailControl.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight;
            this.documentAnnotationViewer1.ThumbnailControl.HighlightTextColor = System.Drawing.SystemColors.HighlightText;
            this.documentAnnotationViewer1.ThumbnailControl.Margins = new Atalasoft.Imaging.WinControls.Margin(4, 4, 4, 4);
            this.documentAnnotationViewer1.ThumbnailControl.SelectionRectangleBackColor = System.Drawing.Color.Transparent;
            this.documentAnnotationViewer1.ThumbnailControl.SelectionRectangleDashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.documentAnnotationViewer1.ThumbnailControl.SelectionRectangleLineColor = System.Drawing.Color.Black;
            this.documentAnnotationViewer1.ThumbnailControl.ThumbnailLayout = Atalasoft.Imaging.WinControls.ThumbnailLayout.Horizontal;
            this.documentAnnotationViewer1.ThumbnailControl.ThumbnailOffset = new System.Drawing.Point(0, 0);
            this.documentAnnotationViewer1.ThumbnailControl.ThumbnailSize = new System.Drawing.Size(100, 100);
            this.documentAnnotationViewer1.ThumbnailDockStyle = Atalasoft.Imaging.WinControls.ControlDockStyle.Bottom;
            this.documentAnnotationViewer1.UndoLevels = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 567);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DocPro Reader";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripButton Open;
        private System.Windows.Forms.ToolStripButton Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ProcessAllRegions;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Atalasoft.Annotate.UI.DocumentAnnotationViewer documentAnnotationViewer1;
        private System.Windows.Forms.ToolStripComboBox TemplateSelector;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton HolePunchRemoval;
        private System.Windows.Forms.ToolStripButton Despeckle;
        private System.Windows.Forms.ToolStripButton Deskew;
        private System.Windows.Forms.ToolStripButton RotateRight;
        private System.Windows.Forms.ToolStripButton RotateLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton Arrow;
        private System.Windows.Forms.ToolStripButton zoom;
        private System.Windows.Forms.ToolStripButton BestFit;
    }
}

