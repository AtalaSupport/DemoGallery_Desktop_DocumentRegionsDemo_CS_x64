using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Atalasoft.Imaging.Codec;
using Atalasoft.Imaging.Codec.Pdf;
using Atalasoft.Annotate;
using Atalasoft.Annotate.UI;
using System.IO;
using Atalasoft.Annotate.Formatters;
using Atalasoft.Imaging.ImageProcessing.Transforms;
using Atalasoft.Imaging.ImageProcessing.Document;
using Atalasoft.Imaging.ImageProcessing.Filters;
using Atalasoft.Annotate.Exporters;
using Atalasoft.Imaging;
using Atalasoft.Imaging.ImageProcessing;
using DocumentRegionsCreator.Properties;
using ScratchLib;

namespace DocumentRegionsCreator
{
    public partial class DocPro : Form
    {
        static DocPro()
        {
            RegisteredDecoders.Decoders.Add(new PdfDecoder() { Resolution = 300 } );

        }
        #region preDefColors
        Color OcrFill = Color.FromArgb(75, Color.LightBlue);
        Color OcrOutline = Color.Blue;
        Color FormsFill = Color.FromArgb(75, Color.LightGray);
        Color FormsOutline = Color.Gray;
        Color BarcodeFill = Color.FromArgb(75, Color.Orange);
        Color BarcodeOutline = Color.DarkOrange;
        #endregion
        #region GlobalGoodness
        int[,] counter;//this will keep track of how many annos have been created, on each page, of each type
        string path;
        bool pdf = false;
        string templateDirectory = @"C:\test\TemplateDirectory\";
        #endregion
        public DocPro()
        {
            InitializeComponent();
            //Events
            documentAnnotationViewer1.SelectFirstPageOnOpen = true;
            documentAnnotationViewer1.Annotations.AnnotationCreated += new AnnotationEventHandler(Annotations_AnnotationCreated);
            documentAnnotationViewer1.Annotations.SelectionChanged += new EventHandler(Annotations_SelectionChanged);
            documentAnnotationViewer1.SelectedIndexChanged += new EventHandler(documentAnnotationViewer1_SelectedIndexChanged);

            documentAnnotationViewer1.Annotations.Resizing += new AnnotationEventHandler(Annotations_Changing);
            documentAnnotationViewer1.Annotations.Moving += new AnnotationEventHandler(Annotations_Changing);

            documentAnnotationViewer1.AnnotationDataProvider = new NullAnnotationDataProvider(); //We use this to make sure we don't load annotations from the documents we open.
            documentAnnotationViewer1.AnnotationSaveOptionsHandler = new AnnotationSaveOptionsHandler(AnnotationOptions);

            tabControl1.TabPages.Clear();
            
            //Used for delete, which requires a refactor.
            //treeView1.KeyUp += new KeyEventHandler(treeView1_KeyUp);
        }

        void treeView1_KeyUp(object sender, KeyEventArgs e)
        {
            //If they hit delete or backspace (for those mac users)
            if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back))
            {
                TreeView ourTreeView = (TreeView)sender;
                
                //Make sure we are on the correct Anno for this particular node.
                SelectAnno(ourTreeView.SelectedNode);

                //We are on an anno node, and we have a real anno.
                if (documentAnnotationViewer1.Annotations.SelectedAnnotations.Count() > 0)
                {
                    //remove the anno and the treenode.
                    removeRegionAnnotation(documentAnnotationViewer1.Annotations.SelectedAnnotations[0].Data.GetExtraProperty("type"));
                    documentAnnotationViewer1.Annotations.SelectedAnnotations[0].Remove();
                    ourTreeView.SelectedNode.Remove();
                }

            }
        }

        #region delegates
        private void AnnotationOptions(DocumentAnnotationViewer viewer, AnnotationSaveOptions options)
        {
            if (pdf)
                options.EmbedAnnotations = false;

        }
        #endregion

        #region eventhandlers
        //annotation events
        void Annotations_Changing(object sender, AnnotationEventArgs e)
        {
            foreach (Control con in tabControl1.TabPages[0].Controls)
            {
                PropertyGrid grid = con as PropertyGrid;
                if (grid != null)
                {
                    //// Original code was using annotation Bounds but the bounds object includes room for grips (including rotation) which were inflating sizes
                    //grid.SelectedObject = e.Annotation.Bounds;
                    grid.SelectedObject = new RectangleF(e.Annotation.Location, e.Annotation.Size);
                }
            }

        }

        void Annotations_SelectionChanged(object sender, EventArgs e)
        {

            //here I need to select the correct node and add the relevant info to the tabcontrol
            //on second thought I should just select the appropriate node and have selecting the node populate the tabcontrol
            if (documentAnnotationViewer1.Annotations.SelectedAnnotations.Length > 0)
            {
                TextAnnotation anno = (TextAnnotation)documentAnnotationViewer1.Annotations.SelectedAnnotations[0];
                SelectNode(anno.Data.GetExtraProperty("type"), anno.Data.GetExtraProperty("index"), anno.Data.GetExtraProperty("page"));
            }
            else
                if (documentAnnotationViewer1.Annotations.CountAnnotations() > 0)
                    treeView1.SelectedNode = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex];

        }

        void Annotations_AnnotationCreated(object sender, AnnotationEventArgs e)
        {

            //when an anno is created we add a node to the treeView and select the annotation
            //documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.None;
            TextAnnotation text = (TextAnnotation)e.Annotation;
            text.Selected = true;
            string type = text.Data.GetExtraProperty("type");
            // Original demo was using Bounds but that's "greedy" .. need to use the actual annotation Location and Size
            //AddTreeNode(type, text.Text, e.Annotation.Bounds);
            AddTreeNode(type, text.Text, new RectangleF(e.Annotation.Location, e.Annotation.Size));
        }

        private void EmbedHistogramRatio(AnnotationUI annotationUI)
        {
            TextAnnotation anno = (TextAnnotation)annotationUI;
            float ratio = Reusables.getHistogramRatio(documentAnnotationViewer1.ImageControl.Image, Rectangle.Round(anno.Bounds));
            anno.Data.SetExtraProperty("ratio", ratio.ToString());


        }

        void documentAnnotationViewer1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(documentAnnotationViewer1.CurrentImageIndex > 0)
                treeView1.SelectedNode = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex];

            //treeView1.CollapseAll();
            //if(treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex] != null)
            //treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].ExpandAll();

        }
        //TreeViewEvents
        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //When a node is selected by selecting it's annotation, we need to select the cooresponding anno and update the tab control
            //if (e.Node.Level > 0)//make sure it is a child node
            SelectAnno(e.Node);
            UpdateTabControl(e.Node);

            //else
            //    CreateParentTab(e.Node);

        }



        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //When a node is clicked, we need to select the cooresponding anno and update the tab control
            
            int NodePage = Convert.ToInt32(e.Node.Name.Split(',')[2]);
            documentAnnotationViewer1.SelectThumbnail(NodePage);
            documentAnnotationViewer1.EnsureVisible(NodePage);
           
            SelectAnno(e.Node);
            UpdateTabControl(e.Node);

        }

        //tab element events
        void size_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateVal(e.ChangedItem.Label, e.ChangedItem.Value);
        }
        void t_TextChanged(object sender, EventArgs e)
        {
            TextAnnotation anno = (TextAnnotation)documentAnnotationViewer1.Annotations.SelectedAnnotations[0];
            foreach (Control ctrl in tabControl1.TabPages[0].Controls)
            {
                TextBox box = ctrl as TextBox;
                if (box != null)
                {
                    anno.Text = box.Text;
                    string[] textValues = treeView1.SelectedNode.Text.Split(':');
                    treeView1.SelectedNode.Text = textValues[0] + ":" + box.Text;
                    UpdateAnno(box.Text);
                }
            }
        }
        #endregion

        #region methods
        public TextData GetData(string type)
        {
            TextData data = new TextData();
            //TO DO: change this to add the count, page num and type....and then change anything that uses extra properties

            if (type == "ocr")
            {
                data.SetExtraProperty("type", "ocr");
                data.SetExtraProperty("index", counter[0, documentAnnotationViewer1.CurrentImageIndex].ToString());
                data.SetExtraProperty("page", documentAnnotationViewer1.CurrentImageIndex.ToString());
                data.Text = "Unlabeled";
                data.Fill = new AnnotationBrush(OcrFill);
                data.Outline = new AnnotationPen(new AnnotationBrush(OcrOutline), 1);
                data.FontBrush = new AnnotationBrush(Color.Transparent);
                counter[0, documentAnnotationViewer1.CurrentImageIndex]++;
                return data;
            }
            else if (type == "forms")
            {
                data.SetExtraProperty("type", "omr");
                data.SetExtraProperty("index", counter[1, documentAnnotationViewer1.CurrentImageIndex].ToString());
                data.SetExtraProperty("page", documentAnnotationViewer1.CurrentImageIndex.ToString());
                data.Text = "Unlabeled";
                data.Fill = new AnnotationBrush(FormsFill);
                data.Outline = new AnnotationPen(new AnnotationBrush(FormsOutline), 1);
                data.FontBrush = new AnnotationBrush(Color.Transparent);
                counter[1, documentAnnotationViewer1.CurrentImageIndex]++;
                return data;

            }
            else if (type == "barcode")
            {
                data.SetExtraProperty("type", "barcode");
                data.SetExtraProperty("index", counter[2, documentAnnotationViewer1.CurrentImageIndex].ToString());
                data.SetExtraProperty("page", documentAnnotationViewer1.CurrentImageIndex.ToString());
                data.Text = "Unlabeled";
                data.Fill = new AnnotationBrush(BarcodeFill);
                data.Outline = new AnnotationPen(new AnnotationBrush(BarcodeOutline), 1);
                data.FontBrush = new AnnotationBrush(Color.Transparent);
                counter[2, documentAnnotationViewer1.CurrentImageIndex]++;
                return data;

            }
            else return null;




        }
        public void removeRegionAnnotation(string type)
        {
            switch (type)
            {
                case "ocr":
                    counter[0, documentAnnotationViewer1.CurrentImageIndex]--;
                    break;
                case "forms":
                    counter[1, documentAnnotationViewer1.CurrentImageIndex]--;
                    break;
                case "barcode":
                    counter[2, documentAnnotationViewer1.CurrentImageIndex]--;
                    break;
                default:
                    break;
            }
        }

        private void SelectNode(string type, string index, string page)
        {
            //now we have page num
            treeView1.CollapseAll();
            treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].ExpandAll();
            int indexNum = Convert.ToInt32(index);
            int pageNum = Convert.ToInt32(page);
            int typeIndex;

            if (type == "ocr")
                typeIndex = 0;
            else if (type == "omr")
                typeIndex = 1;
            else if (type == "barcode")
                typeIndex = 2;
            else
            {
                MessageBox.Show("problem in SelectNode");
                typeIndex = 123;
            }

            treeView1.SelectedNode = treeView1.Nodes[pageNum].Nodes[typeIndex].Nodes[indexNum];
        }
        private void AddTreeNode(string type, string description, RectangleF bounds)
        {
            //here we create the tree node for the newly created annotation
            //0 = ocr, 1= forms

            TreeNode node = new TreeNode();
            if (type == "ocr")
            {
                node.Text = "#" + (treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[0].GetNodeCount(false) + 1).ToString() + " :  " + description;
                node.Name = "OCR," + treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[0].GetNodeCount(false).ToString() + "," + documentAnnotationViewer1.CurrentImageIndex.ToString();
                node.SelectedImageKey = "ocr";
                node.ImageKey = "ocr";
                treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[0].Nodes.Add(node);
                treeView1.SelectedNode = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[0].Nodes[treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[0].Nodes.Count - 1];
            }
            if (type == "omr")
            {
                node.Text = "#" + (treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[1].GetNodeCount(false) + 1).ToString() + " : " + description;
                node.Name = "OMR," + treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[1].GetNodeCount(false).ToString() + "," + documentAnnotationViewer1.CurrentImageIndex.ToString();
                node.SelectedImageKey = "omr";
                node.ImageKey = "omr";
                treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[1].Nodes.Add(node);
                treeView1.SelectedNode = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[1].Nodes[treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[1].Nodes.Count - 1];
            }
            if (type == "barcode")
            {
                node.Text = "#" + (treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[2].GetNodeCount(false) + 1).ToString() + " : " + description;
                node.Name = "BARCODE," + treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[2].GetNodeCount(false).ToString() + "," + documentAnnotationViewer1.CurrentImageIndex.ToString();
                node.SelectedImageKey = "bc";
                node.ImageKey = "bc";
                treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[2].Nodes.Add(node);
                treeView1.SelectedNode = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[2].Nodes[treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[2].Nodes.Count - 1];
            }
            treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].ExpandAll();
        }
        private void InitializeTree(int pages)
        {
            ImageList list = new ImageList();
            list.ImageSize = new System.Drawing.Size(24, 24);

            list.Images.Add("page", Resources.Report_2);
            list.Images.Add("ocr", Resources.OCR_outline);
            list.Images.Add("omr", Resources.OMR_outline);
            list.Images.Add("bc", Resources.BC_outline);
            treeView1.ImageList = list;

            treeView1.HideSelection = true;

            for (int i = 0; i < pages; i++)
            {
                TreeNode node = new TreeNode();
                SetUpNode(node, i);
                node.Text = "Page#" + (i + 1).ToString();
                node.Name = "page, page, " + documentAnnotationViewer1.CurrentImageIndex.ToString();
                node.SelectedImageKey = "page";
                node.ImageKey = "page";
                treeView1.Nodes.Add(node);

            }
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseClick);
        }

        private void SetUpNode(TreeNode node, int page)
        {

            TreeNode OcrTree = new TreeNode("OCR Regions");
            OcrTree.Name = "region, ocr," + page.ToString();
            OcrTree.ImageKey = "ocr";
            OcrTree.SelectedImageKey = "ocr";

            TreeNode FormsTree = new TreeNode("OMR Regions");
            FormsTree.Name = "region, omr," + page.ToString();
            FormsTree.ImageKey = "omr";
            FormsTree.SelectedImageKey = "omr";

            TreeNode BarcodeTree = new TreeNode("Barcode Region");
            BarcodeTree.Name = "region, bc," + page.ToString();
            BarcodeTree.ImageKey = "bc";
            BarcodeTree.SelectedImageKey = "bc";

            node.Nodes.Add(OcrTree);
            node.Nodes.Add(FormsTree);
            node.Nodes.Add(BarcodeTree);



        }
        private void UpdateTabControl(TreeNode treeNode)
        {
            //here we need to branch on the level of the node, 0=page, 1=type, 2 = actual region

            TabPage page;
            tabControl1.TabPages.Clear();
            
            if (treeNode.Level > 1)// && documentAnnotationViewer1.Annotations.SelectedAnnotations.Length > 0)//it's an annotation
            {
                //documentAnnotationViewer1.Annotations.ActiveAnnotation;

                string[] values = treeNode.Text.Split(':');
                page = new TabPage() { Text = treeNode.Name.Split(',')[0] };
                page.Size = new Size(253, 243);
                Label type = new Label() { Text = "Description", Location = new Point(10, 10) };
                TextBox t = new TextBox() { Text = values[1], Location = new Point(type.Size.Width + 10, 10) };
                t.TextChanged += new EventHandler(t_TextChanged);
                PropertyGrid size = new PropertyGrid();
                size.ToolbarVisible = false;
                size.HelpVisible = false;
                size.Size = new System.Drawing.Size(page.Size.Width - 20, 150);

                //// Original code was using annotation Bounds but the bounds object includes room for grips (including rotation) which were inflating sizes
                //size.SelectedObject = documentAnnotationViewer1.Annotations.SelectedAnnotations[0].Bounds;
                size.SelectedObject = new RectangleF(documentAnnotationViewer1.Annotations.SelectedAnnotations[0].Location, documentAnnotationViewer1.Annotations.SelectedAnnotations[0].Size);

                size.Location = new Point(10, type.Size.Height + 20);
                size.PropertyValueChanged += new PropertyValueChangedEventHandler(size_PropertyValueChanged);
                Control[] controls = { type, t, size };
                page.Controls.AddRange(controls);
            }
            else if (treeNode.Level == 1)//it's a region
            {
                page = new TabPage() { Text = treeNode.Text };
                page.Size = new Size(253, 243);

                Label label = new Label() { Text = "Number of Regions: " + treeNode.GetNodeCount(false).ToString(), Location = new Point(10, 10), Size = new Size(240, 50) };
                page.Controls.Add(label);

            }
            else if (treeNode.Level == 0)//it's a page
            {
                page = new TabPage() { Text = treeNode.Text };
                page.Size = new Size(253, 243);

                Label label1 = new Label() { Text = "Number of OCR Regions: " + treeNode.Nodes[0].GetNodeCount(false).ToString(), Location = new Point(10, 10), Size = new Size(240, 20) };
                Label label2 = new Label() { Text = "Number of OMR Regions: " + treeNode.Nodes[1].GetNodeCount(false).ToString(), Location = new Point(10, 50), Size = new Size(240, 20) };
                Label label3 = new Label() { Text = "Number of Barcode regions: " + treeNode.Nodes[2].GetNodeCount(false).ToString(), Location = new Point(10, 70), Size = new Size(240, 20) };
                documentAnnotationViewer1.SelectThumbnail(treeNode.Index);
                documentAnnotationViewer1.EnsureVisible(treeNode.Index);
                Control[] controls = { label1, label2, label3 };
                page.Controls.AddRange(controls);

            } else {
                page = new TabPage() { Name = "bad state." };
            }

            tabControl1.TabPages.Add(page);
        }
        private void UpdateAnno(string newVal)
        {
            TextAnnotation anno = (TextAnnotation)documentAnnotationViewer1.Annotations.SelectedAnnotations[0];
            if (anno != null)
                anno.Text = newVal;
        }
        private void UpdateVal(string property, object value)
        {
            if (documentAnnotationViewer1.Annotations.SelectedAnnotations.Length > 0)
            {
                TextAnnotation anno = (TextAnnotation)documentAnnotationViewer1.Annotations.SelectedAnnotations[0];
                switch (property)
                {
                    case "Height":
                        anno.Size = new SizeF(anno.Size.Width, (float)value);
                        break;
                    case "Width":
                        anno.Size = new SizeF((float)value, anno.Size.Height);
                        break;
                    case "X":
                        anno.Location = new PointF((float)value, anno.Location.Y);
                        break;
                    case "Y":
                        anno.Location = new PointF(anno.Location.X, (float)value);
                        break;
                    default:
                        MessageBox.Show("problem in UpdateVal");
                        break;
                }
            }
        }
        private void SelectAnno(TreeNode treeNode)
        {


            if (documentAnnotationViewer1.Annotations.CountAnnotations() > 0)
            {
                if (treeNode.Level == 2)//this is an actual region which cooresponds to an annotation, jump to the page and select it.
                {
                    string[] values = treeNode.Name.Split(',');//index 0 = type, index 1 = index, index 2 = page
                    documentAnnotationViewer1.SelectThumbnail(treeNode.Parent.Parent.Index);
                    documentAnnotationViewer1.EnsureVisible(treeNode.Parent.Parent.Index);

                    //int page = documentAnnotationViewer1.CurrentImageIndex;
                    foreach (AnnotationUI anno in documentAnnotationViewer1.Annotations.Layers[0].Items)
                    {
                        TextAnnotation text = (TextAnnotation)anno;
                        if ((text.Data.GetExtraProperty("index") == values[1]) && (text.Data.GetExtraProperty("type").ToUpper() == values[0].ToUpper()) && (text.Data.GetExtraProperty("page") == values[2]))
                        {
                            text.Selected = true;

                        }
                        else
                        {
                            text.Selected = false;
                        }


                    }

                }
                else//otherwise deslect all anotations and jump to the page.
                {


                }


            }
        }
        private void SaveAsPdf(string p)
        {
            PdfEncoder encoder = new PdfEncoder();

            documentAnnotationViewer1.Save(p, encoder);
            using (FileStream fs = new FileStream(p, FileMode.Open, FileAccess.ReadWrite))
            {
                AnnotationDataCollection adc;
                //fs.Position = 0;
                PdfAnnotationDataExporter exporter = new PdfAnnotationDataExporter();
                exporter.AlwaysEmbedAnnotationData = true;
                ImageInfo info = RegisteredDecoders.GetImageInfo(path);
                Dpi[] resolutions = new Dpi[info.FrameCount];
                SizeF[] sizes = new SizeF[info.FrameCount];
                for (int i = 0; i < info.FrameCount; i++)
                {
                    resolutions[i] = RegisteredDecoders.GetImageInfo(path, i).Resolution;
                    sizes[i] = RegisteredDecoders.GetImageInfo(path, i).Size;

                }
                using (MemoryStream ms = new MemoryStream())
                {
                    documentAnnotationViewer1.SaveAnnotationData(ms, -1, null);
                    ms.Position = 0;
                    adc = new AnnotationDataCollection();
                    AnnotationController ac = new AnnotationController();
                    ac.Load(ms, AnnotationDataFormat.Xmp);
                    foreach (LayerAnnotation layer in ac.Layers)
                        adc.Add(layer.Data);
                }
                fs.Position = 0;
                exporter.ExportOver(fs, sizes, AnnotationUnit.Pixel, resolutions, adc);

            }
        }

        #endregion
        #region buttonClickHandlers

        private void Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    documentAnnotationViewer1.Open(dlg.FileName);
                    using (FileStream fs = File.OpenRead(dlg.FileName))
                    {
                        
                        path = dlg.FileName;
                        //ImageDecoder dec = RegisteredDecoders.GetDecoder(fs);
                        //fs.Position = 0;
                        //int pages = dec.GetImageInfo(fs).FrameCount;
                        int pages = documentAnnotationViewer1.Count;
                        counter = new int[3, pages];
                        if (treeView1.Nodes.Count > 0)
                            treeView1.Nodes.Clear();
                        InitializeTree(pages);
                        treeView1.SelectedNode = treeView1.Nodes[0];
                    }
                }
            }

        }


        private void Save_Click(object sender, EventArgs e)
        {//This is now to be refactored so that teh annotation data is saved as an external "template" file.

            try
            {
                if (!Directory.Exists(templateDirectory))
                {
                    Directory.CreateDirectory(templateDirectory);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not create default template directory " + templateDirectory + Environment.NewLine + "Templates are loaded from this directory by default. If it is missing the reader might not function properly.");
            }
            using (SaveFileDialog dlg = new SaveFileDialog() { Title = "Save the region template as...", Filter = "Document Region Template (*.drt)|*.drt" , InitialDirectory = templateDirectory})
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        documentAnnotationViewer1.SaveAnnotationData(ms, -1, new XmpFormatter());

                        ms.Position = 0;

                        using (AnnotationController ac = new AnnotationController())
                        {
                            ac.Load(ms, new XmpFormatter());

                            foreach (LayerAnnotation layer in ac.Layers)
                                foreach (AnnotationUI anno in layer.Items)
                                {
                                    if (anno.Data.GetExtraProperty("type") == "omr")
                                        EmbedHistogramRatio(anno);
                                }

                            using (Stream fs = File.Create(dlg.FileName))
                                ac.Save(fs, new XmpFormatter());
                        }
                    }
                }

            }

            
        }


        private void BestFit_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.BestFit;
        }
        private void Arrow_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.None;
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.None;
        }

        private void Zoom_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.None;
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Zoom;
        }
        #region regionTools
        private void OCR_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.None;
            TextAnnotation OcrAnno = new TextAnnotation(GetData("ocr"));

            documentAnnotationViewer1.Annotations.CreateAnnotation(OcrAnno);
        }

        private void OMR_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.None;
            TextAnnotation FormsAnno = new TextAnnotation(GetData("forms"));

            documentAnnotationViewer1.Annotations.CreateAnnotation(FormsAnno);
        }

        private void Barcode_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.None;
            TextAnnotation FormsAnno = new TextAnnotation(GetData("barcode"));

            documentAnnotationViewer1.Annotations.CreateAnnotation(FormsAnno);
        }
        #endregion

        #region ImageCleanup
        private void RotLeft_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.RotateDocument(DocumentRotation.Rotate270);
                    }

        private void RotRight_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.RotateDocument(DocumentRotation.Rotate90);
      
        }

        private void deskew_Click(object sender, EventArgs e)
        {

            AutoDeskewCommand cmd = new AutoDeskewCommand();
            if (cmd.IsPixelFormatSupported(documentAnnotationViewer1.ImageControl.Image.PixelFormat))
                documentAnnotationViewer1.ApplyCommand(cmd);
            else
                MessageBox.Show("This command can only be applied to images to the following pixel formats:" + cmd.SupportedPixelFormats.ToString());
        }

        private void despeck_Click(object sender, EventArgs e)
        {
            ImageCommand cmd;
            if (documentAnnotationViewer1.ImageControl.Image.PixelFormat == PixelFormat.Pixel1bppIndexed)
                cmd = new DocumentDespeckleCommand();
            else
                cmd = new DespeckleCommand();

            if (cmd.IsPixelFormatSupported(documentAnnotationViewer1.ImageControl.Image.PixelFormat))
                documentAnnotationViewer1.ApplyCommand(cmd);
            else
                MessageBox.Show("This command can only be applied to images to the following pixel formats:" + cmd.SupportedPixelFormats.ToString() + PixelFormat.Pixel1bppIndexed.ToString());
        }

        private void holepunch_Click(object sender, EventArgs e)
        {
            if (documentAnnotationViewer1.ImageControl.Image.PixelFormat == PixelFormat.Pixel1bppIndexed)
            {
                HolePunchRemovalCommand cmd = new HolePunchRemovalCommand();
                documentAnnotationViewer1.ApplyCommand(cmd);
            }
            else
                MessageBox.Show("This command can only be applied to images to the following pixel formats:" + PixelFormat.Pixel1bppIndexed.ToString());
        }
        #endregion
      
        #endregion

        #region Custom DataProvider
        
        private class NullAnnotationDataProvider : IAnnotationDataProvider
        {
            public void PrepareForExtraction(AtalaImage image) { }
            public void PrepareForExtraction(RandomAccessImageSource imagesource, int page) { }
            public void PrepareForExtraction(string filepath) { }
            public void PrepareForExtraction(string filepath, int page) { }
            public void PrepareForExtraction(Stream documentStream, int page) { }

            public LayerData GetLayerData(int layer)
            {
                return null;
            }
        }

        #endregion
    }
}
