namespace DocumentRegionsCreator
{
    partial class DocPro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocPro));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Open = new System.Windows.Forms.ToolStripButton();
            this.Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Arrow = new System.Windows.Forms.ToolStripButton();
            this.Zoom = new System.Windows.Forms.ToolStripButton();
            this.BestFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.OCR = new System.Windows.Forms.ToolStripButton();
            this.OMR = new System.Windows.Forms.ToolStripButton();
            this.Barcode = new System.Windows.Forms.ToolStripButton();
            this.holepunch = new System.Windows.Forms.ToolStripButton();
            this.despeck = new System.Windows.Forms.ToolStripButton();
            this.deskew = new System.Windows.Forms.ToolStripButton();
            this.RotRight = new System.Windows.Forms.ToolStripButton();
            this.RotLeft = new System.Windows.Forms.ToolStripButton();
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
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.Save,
            this.toolStripSeparator1,
            this.Arrow,
            this.Zoom,
            this.BestFit,
            this.toolStripSeparator2,
            this.OCR,
            this.OMR,
            this.Barcode,
            this.holepunch,
            this.despeck,
            this.deskew,
            this.RotRight,
            this.RotLeft});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(750, 35);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Open
            // 
            this.Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Open.Image = global::DocumentRegionsCreator.Properties.Resources.Open;
            this.Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(32, 32);
            this.Open.Text = "Open";
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Save
            // 
            this.Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Save.Image = global::DocumentRegionsCreator.Properties.Resources.Save;
            this.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(32, 32);
            this.Save.Text = "Save";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // Arrow
            // 
            this.Arrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Arrow.Image = global::DocumentRegionsCreator.Properties.Resources.arrow_up_left;
            this.Arrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Arrow.Name = "Arrow";
            this.Arrow.Size = new System.Drawing.Size(32, 32);
            this.Arrow.Text = "Select";
            this.Arrow.Click += new System.EventHandler(this.Arrow_Click);
            // 
            // Zoom
            // 
            this.Zoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Zoom.Image = global::DocumentRegionsCreator.Properties.Resources.view_24;
            this.Zoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Zoom.Name = "Zoom";
            this.Zoom.Size = new System.Drawing.Size(32, 32);
            this.Zoom.Text = "Zoom";
            this.Zoom.Click += new System.EventHandler(this.Zoom_Click);
            // 
            // BestFit
            // 
            this.BestFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BestFit.Image = global::DocumentRegionsCreator.Properties.Resources.Fit_Width;
            this.BestFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BestFit.Name = "BestFit";
            this.BestFit.Size = new System.Drawing.Size(32, 32);
            this.BestFit.Text = "BestFit";
            this.BestFit.Click += new System.EventHandler(this.BestFit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // OCR
            // 
            this.OCR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OCR.Image = global::DocumentRegionsCreator.Properties.Resources.OCR_outline;
            this.OCR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OCR.Name = "OCR";
            this.OCR.Size = new System.Drawing.Size(32, 32);
            this.OCR.Text = "OCR";
            this.OCR.ToolTipText = "Define OCR Region";
            this.OCR.Click += new System.EventHandler(this.OCR_Click);
            // 
            // OMR
            // 
            this.OMR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OMR.Image = global::DocumentRegionsCreator.Properties.Resources.OMR_outline;
            this.OMR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OMR.Name = "OMR";
            this.OMR.Size = new System.Drawing.Size(32, 32);
            this.OMR.Text = "OMR";
            this.OMR.ToolTipText = "Define OMR Region";
            this.OMR.Click += new System.EventHandler(this.OMR_Click);
            // 
            // Barcode
            // 
            this.Barcode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Barcode.Image = global::DocumentRegionsCreator.Properties.Resources.BC_outline;
            this.Barcode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Barcode.Name = "Barcode";
            this.Barcode.Size = new System.Drawing.Size(32, 32);
            this.Barcode.Text = "Barcode";
            this.Barcode.ToolTipText = "Define Barcode Region";
            this.Barcode.Click += new System.EventHandler(this.Barcode_Click);
            // 
            // holepunch
            // 
            this.holepunch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.holepunch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.holepunch.Image = global::DocumentRegionsCreator.Properties.Resources.HolePunches;
            this.holepunch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.holepunch.Name = "holepunch";
            this.holepunch.Size = new System.Drawing.Size(32, 32);
            this.holepunch.Text = "holepunch";
            this.holepunch.ToolTipText = "Remove Hole Punches";
            this.holepunch.Click += new System.EventHandler(this.holepunch_Click);
            // 
            // despeck
            // 
            this.despeck.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.despeck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.despeck.Image = global::DocumentRegionsCreator.Properties.Resources.Specks;
            this.despeck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.despeck.Name = "despeck";
            this.despeck.Size = new System.Drawing.Size(32, 32);
            this.despeck.Text = "despeck";
            this.despeck.ToolTipText = "Remove Speckles From Image";
            this.despeck.Click += new System.EventHandler(this.despeck_Click);
            // 
            // deskew
            // 
            this.deskew.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.deskew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deskew.Image = global::DocumentRegionsCreator.Properties.Resources.Skewed;
            this.deskew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deskew.Name = "deskew";
            this.deskew.Size = new System.Drawing.Size(32, 32);
            this.deskew.Text = "deskew";
            this.deskew.ToolTipText = "Deskew Image";
            this.deskew.Click += new System.EventHandler(this.deskew_Click);
            // 
            // RotRight
            // 
            this.RotRight.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RotRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RotRight.Image = global::DocumentRegionsCreator.Properties.Resources.Rotate_Right;
            this.RotRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RotRight.Name = "RotRight";
            this.RotRight.Size = new System.Drawing.Size(32, 32);
            this.RotRight.Text = "RotRight";
            this.RotRight.ToolTipText = "Rotate Image Right";
            this.RotRight.Click += new System.EventHandler(this.RotRight_Click);
            // 
            // RotLeft
            // 
            this.RotLeft.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.RotLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RotLeft.Image = global::DocumentRegionsCreator.Properties.Resources.Rotate_Left;
            this.RotLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RotLeft.Name = "RotLeft";
            this.RotLeft.Size = new System.Drawing.Size(32, 32);
            this.RotLeft.Text = "RotLeft";
            this.RotLeft.ToolTipText = "Rotate Image Left";
            this.RotLeft.Click += new System.EventHandler(this.RotLeft_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.documentAnnotationViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(750, 532);
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
            this.splitContainer2.Size = new System.Drawing.Size(250, 532);
            this.splitContainer2.SplitterDistance = 202;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(250, 202);
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
            this.tabControl1.Size = new System.Drawing.Size(250, 326);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(242, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(242, 300);
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
            this.documentAnnotationViewer1.Size = new System.Drawing.Size(496, 532);
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
            // DocPro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 567);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocPro";
            this.Text = "DocPro";
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
        private System.Windows.Forms.ToolStripButton Open;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripButton Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Arrow;
        private System.Windows.Forms.ToolStripButton Zoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton OCR;
        private System.Windows.Forms.ToolStripButton OMR;
        private System.Windows.Forms.ToolStripButton Barcode;
        private System.Windows.Forms.ToolStripButton holepunch;
        private System.Windows.Forms.ToolStripButton despeck;
        private System.Windows.Forms.ToolStripButton deskew;
        private System.Windows.Forms.ToolStripButton RotRight;
        private System.Windows.Forms.ToolStripButton RotLeft;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Atalasoft.Annotate.UI.DocumentAnnotationViewer documentAnnotationViewer1;
        private System.Windows.Forms.ToolStripButton BestFit;
    }
}

