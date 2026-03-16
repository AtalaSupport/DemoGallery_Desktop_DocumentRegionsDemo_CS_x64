using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Atalasoft.Imaging.Codec;
using Atalasoft.Imaging;
using Atalasoft.Imaging.Codec.Pdf;
using Atalasoft.Annotate.UI;
using DocumentRegionsReader.Properties;
using System.IO;
using Atalasoft.Imaging.Metadata;
using Atalasoft.Annotate.Importers;
using Atalasoft.Annotate;
using Atalasoft.Ocr;
using System.Xml;
using Atalasoft.Barcoding.Reading;
using ScratchLib;
using Atalasoft.Imaging.Drawing;
using Atalasoft.Imaging.ImageProcessing.Document;
using Atalasoft.Imaging.ImageProcessing;
using Atalasoft.Imaging.ImageProcessing.Filters;
using Atalasoft.Annotate.Formatters;
using Atalasoft.Ocr.GlyphReader;

namespace DocumentRegionsReader
{
    public partial class Form1 : Form
    {
        static OcrEngine ocrEngine;
        Results ocrResults = new Results();
        Results omrResults = new Results();
        Results barcodeResults = new Results();
        string documentPath = "";
        bool templateLoaded = false;
        string templateDirectory = @"C:\test\TemplateDirectory\";
        AnnotationController ac;


        static Form1()
        {
            try
            {
                GlyphReaderLoader loader = new GlyphReaderLoader();
                RegisteredDecoders.Decoders.Add(new PdfDecoder() { Resolution = 300, RenderSettings = new RenderSettings() { AnnotationSettings = AnnotationRenderSettings.None } });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        public Form1()
        {
            InitializeComponent();

            try
            {
                ocrEngine = new GlyphReaderEngine();
                ocrEngine.PreprocessingOptions.AutoRotate = false;
                ocrEngine.PreprocessingOptions.Deskew = false;
                ocrEngine.PreprocessingOptions.Despeckle = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //events
            documentAnnotationViewer1.SelectedIndexChanged += new EventHandler(documentAnnotationViewer1_SelectedIndexChanged);
            documentAnnotationViewer1.Annotations.SelectionChanged += new EventHandler(Annotations_SelectionChanged);
            documentAnnotationViewer1.ImageControl.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.BestFit;

            tabControl1.TabPages.Clear();
        }


        void Annotations_SelectionChanged(object sender, EventArgs e)
        {
            if (documentAnnotationViewer1.Annotations.SelectedAnnotations.Length > 0)
            {
                TextAnnotation anno = (TextAnnotation)documentAnnotationViewer1.Annotations.SelectedAnnotations[0];
                anno.CanMove = false;
                anno.CanResize = false;
                anno.CanRotate = false;
                SelectNode(anno.Data.GetExtraProperty("type"), anno.Data.GetExtraProperty("index"), anno.Data.GetExtraProperty("page"));
            }
            else
                if (documentAnnotationViewer1.Annotations.CountAnnotations() > 0)
                    treeView1.SelectedNode = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex];
        }

        private void SelectNode(string type, string index, string page)
        {
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

        void documentAnnotationViewer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
            if (documentAnnotationViewer1.CurrentImageIndex >= 0 && templateLoaded)
            {
                BuildPageNodes(documentAnnotationViewer1.CurrentImageIndex);
                treeView1.SelectedNode = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex];
            }

        }
        #region ButtonClicks
        private void Open_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    documentAnnotationViewer1.Open(dlg.FileName);
                    documentPath = dlg.FileName;
                    documentAnnotationViewer1.SelectThumbnail(0);
                }
            }
            LoadTemplates();
        }

        private void LoadTemplates()
        {
            string[] filePaths;

            try
            {
                filePaths = Directory.GetFiles(templateDirectory, "*.drt");
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                {
                    dlg.Description = "Please choose your template directory";
                    dlg.SelectedPath = templateDirectory;
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        templateDirectory = dlg.SelectedPath + @"\";
                        filePaths = Directory.GetFiles(templateDirectory, "*.drt");
                    }
                    else
                    {
                        filePaths = new string[0];
                    }
                }
            }

            if (filePaths.Length == 0)
            {
                TemplateSelector.Items.Add("No Templates Found");
                return;
            }
            TemplateSelector.SelectedIndexChanged += new EventHandler(TemplateSelector_SelectedIndexChanged);
            foreach (string file in filePaths)
            {
                TemplateSelector.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        void TemplateSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox box = (ToolStripComboBox)sender;
            string template = templateDirectory + box.Text + ".drt";
            treeView1.Nodes.Clear();
            InitialzeTree();
            LoadRegions(template);

            using (FileStream fs = File.OpenRead(template))
                documentAnnotationViewer1.LoadAnnotationData(fs, -1, new XmpFormatter(), true);


            documentAnnotationViewer1.ClearSelection();
            documentAnnotationViewer1.SelectThumbnail(0);


        }


        private void ProcessRegions(AnnotationController ac, Stream fs)
        {

            int page = 0;
            foreach (LayerAnnotation layer in ac.Layers)
            {


                foreach (AnnotationUI anno in layer.Items)
                {
                    TextAnnotation regionAnno = (TextAnnotation)anno;
                    if (regionAnno != null)
                    {
                        string type = regionAnno.Data.GetExtraProperty("type");
                        switch (type)
                        {
                            case "ocr":
                                RecognitionResult ocrResult = new RecognitionResult(Rectangle.Round(regionAnno.Bounds), regionAnno.Text, page, Convert.ToInt32(regionAnno.Data.GetExtraProperty("index")));
                                ocrResults.Add(ocrResult);
                                break;
                            case "omr":
                                //// Original code was using annotation Bounds but the bounds object includes room for grips (including rotation) which were inflating sizes
                                //RecognitionResult omrResult = new RecognitionResult(Rectangle.Round(regionAnno.Bounds), regionAnno.Text, page, Convert.ToInt32(regionAnno.Data.GetExtraProperty("index")));
                                RecognitionResult omrResult = new RecognitionResult(new Rectangle(new Point((int)regionAnno.Location.X, (int)regionAnno.Location.Y), new Size((int)regionAnno.Size.Width, (int)regionAnno.Size.Height)), regionAnno.Text, page, Convert.ToInt32(regionAnno.Data.GetExtraProperty("index")));

                                omrResult.OmrRatio = (float)Convert.ToDouble(regionAnno.Data.GetExtraProperty("ratio"));
                                omrResults.Add(omrResult);
                                break;
                            case "barcode":
                                RecognitionResult barcodeResult = new RecognitionResult(Rectangle.Round(regionAnno.Bounds), regionAnno.Text, page, Convert.ToInt32(regionAnno.Data.GetExtraProperty("index")));
                                barcodeResults.Add(barcodeResult);
                                break;
                        }

                    }
                }
                if (ocrResults.GetCountForPage(page) > 0)
                    Ocr(ocrResults, fs, page);
                if (omrResults.GetCountForPage(page) > 0)
                    Omr(omrResults, fs, page);
                if (barcodeResults.GetCountForPage(page) > 0)
                    Barcode(barcodeResults, fs, page);
                page++;
            }
            templateLoaded = true;


        }

        private void Barcode(Results barcodeResults, Stream fs, int page)
        {
            fs.Position = 0;
            AtalaImage image = new AtalaImage(fs, page, null);
            BarCodeReader br = new BarCodeReader(image);
            ReadOpts options = new ReadOpts();
            options.Direction = Directions.East;
            options.Symbology = Symbologies.All1D;

            for (int i = 0; i < barcodeResults.Count; i++)
            {
                RecognitionResult current = barcodeResults.Get(page, i);
                if (current != null)
                {
                    options.RectOfInterest = current.bounds;
                    BarCode[] bars = br.ReadBars(options);
                    if (bars.Length > 0)
                        current.setResults(bars[0].DataString);
                }
            }

        }

        private void Omr(Results omrResults, Stream fs, int page)
        {
            fs.Position = 0;
            AtalaImage image = new AtalaImage(fs, page, null);
            for (int i = 0; i < omrResults.Count; i++)
            {
                RecognitionResult current = omrResults.Get(page, i);
                if (current != null)
                {
                    float newRatio = Reusables.getHistogramRatio(image, current.bounds);//this is thedecimal percantage of black pixels.
                    //We're going to use 20% as our threshold. So, if the current page has 20% more black than the template it's true.
                    if (newRatio > (current.OmrRatio * 1.2))
                    {
                        current.setResults("true \n template value: " + current.OmrRatio.ToString("F3") + ";\n current value: " + newRatio.ToString("F3") + ";\n threshold value: " + (current.OmrRatio * 1.2).ToString("F3"));
                    }
                    else
                    {
                        current.setResults("false, \n template value: " + current.OmrRatio.ToString("F3") + ";\n current value: " + newRatio.ToString("F3") + ";\n threshold value: " + (current.OmrRatio * 1.2).ToString("F3"));
                    }
                }
            }
        }

        private void Ocr(Results ocrRegions, Stream fs, int page)
        {
            fs.Position = 0;
            AtalaImage image = new AtalaImage(fs, page, null);
            OcrDocument[] results = new OcrDocument[ocrRegions.Count];
            ocrEngine.Initialize();
            for (int i = 0; i < ocrRegions.Count; i++)
            {
                RecognitionResult current = ocrRegions.Get(page, i);
                if (current != null)
                {
                    OcrPage ocrPage = ocrEngine.Recognize(image, current.bounds);
                    current.setResults(ocrPage.GetTextString(false));
                }

            }
            ocrEngine.ShutDown();
        }

        private void SetUpNode(TreeNode node, int i)
        {
            TreeNode OcrTree = new TreeNode("OCR Regions");
            OcrTree.Name = "region,ocr," + i.ToString();
            OcrTree.ImageKey = "ocr";
            OcrTree.SelectedImageKey = "ocr";

            TreeNode FormsTree = new TreeNode("OMR Regions");
            FormsTree.Name = "region,omr," + i.ToString();
            FormsTree.ImageKey = "omr";
            FormsTree.SelectedImageKey = "omr";

            TreeNode BarcodeTree = new TreeNode("Barcode Region");
            BarcodeTree.Name = "region,bc," + i.ToString();
            BarcodeTree.ImageKey = "bc";
            BarcodeTree.SelectedImageKey = "bc";

            node.Nodes.Add(OcrTree);
            node.Nodes.Add(FormsTree);
            node.Nodes.Add(BarcodeTree);
        }

        private void BuildPageNodes(int p)
        {
            if (treeView1.Nodes[p].GetNodeCount(true) <= 3 && documentAnnotationViewer1.Annotations.Layers.Count != 0)
            {
                foreach (AnnotationUI anno in documentAnnotationViewer1.Annotations.Layers[0].Items)
                {
                    TextAnnotation regionAnno = (TextAnnotation)anno;
                    if (regionAnno != null)
                    {
                        string type = regionAnno.Data.GetExtraProperty("type");
                        if (type != null)
                        {
                            AddAnnoNode(regionAnno, p, type, regionAnno.Text);
                        }
                    }

                }
            }
        }

        private void AddAnnoNode(TextAnnotation regionAnno, int p, string type, string description)
        {
            TreeNode node = new TreeNode();



            switch (type)
            {
                case "ocr":
                    node.Text = "#" + p.ToString() + ":" + description;
                    node.Name = type + "," + treeView1.Nodes[p].Nodes[0].Nodes.Count.ToString() + "," + documentAnnotationViewer1.CurrentImageIndex.ToString();
                    treeView1.Nodes[p].Nodes[0].Nodes.Add(node);
                    break;

                case "omr":
                    node.Text = "#" + p.ToString() + ":" + description;
                    node.Name = type + "," + treeView1.Nodes[p].Nodes[1].Nodes.Count.ToString() + "," + documentAnnotationViewer1.CurrentImageIndex.ToString();
                    treeView1.Nodes[p].Nodes[1].Nodes.Add(node);
                    break;

                case "barcode":
                    node.Text = "#" + p.ToString() + ":" + description;
                    node.Name = type + "," + treeView1.Nodes[p].Nodes[2].Nodes.Count.ToString() + "," + documentAnnotationViewer1.CurrentImageIndex.ToString();
                    treeView1.Nodes[p].Nodes[2].Nodes.Add(node);
                    break;
                default:
                    MessageBox.Show("something has gone terrribly wrong");
                    break;
            }


        }

        private void InitialzeTree()
        {
            ImageList list = new ImageList();
            list.ImageSize = new System.Drawing.Size(24, 24);

            list.Images.Add("page", Resources.Report_2);
            list.Images.Add("ocr", Resources.OCR_outline);
            list.Images.Add("omr", Resources.OMR_outline);
            list.Images.Add("bc", Resources.BC_outline);
            treeView1.ImageList = list;

            treeView1.HideSelection = true;

            for (int i = 0; i < documentAnnotationViewer1.Count; i++)
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

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControl1.TabPages.Clear();
            switch (e.Node.Level)
            {
                case 0:
                    documentAnnotationViewer1.SelectThumbnail(e.Node.Index);
                    documentAnnotationViewer1.EnsureVisible(e.Node.Index);
                    CreatePageTab(e.Node.Index);
                    break;
                case 1:
                    documentAnnotationViewer1.SelectThumbnail(e.Node.Parent.Index);
                    documentAnnotationViewer1.EnsureVisible(e.Node.Parent.Index);
                    CreateTypeTab(e.Node.Index);
                    //region node, dislay;
                    break;
                case 2:
                    //it's region, display
                    documentAnnotationViewer1.SelectThumbnail(e.Node.Parent.Parent.Index);
                    documentAnnotationViewer1.EnsureVisible(e.Node.Parent.Parent.Index);
                    SelectAnno(e.Node.Index, e.Node.Name.Split(',')[0]);
                    CreateRegionTab(e.Node);
                    break;
                default:
                    break;


            }


        }

        private void SelectAnno(int index, string type)
        {
            foreach (AnnotationUI anno in documentAnnotationViewer1.Annotations.Layers[0].Items)
            {
                TextAnnotation regionAnno = (TextAnnotation)anno;
                if (regionAnno != null)
                {
                    if (anno.Data.GetExtraProperty("index") == index.ToString() && (type == anno.Data.GetExtraProperty("type")))
                    {
                        anno.Selected = true;
                        anno.CanRotate = false;
                        anno.CanMove = false;
                        anno.CanResize = false;
                    }
                    else
                        anno.Selected = false;

                }

            }

        }

        private void CreateRegionTab(TreeNode node)
        {
            string[] TextValues = node.Text.Split(':');//type then index
            string[] NameValues = node.Name.Split(',');

            TabPage resultsPage = new TabPage() { Text = TextValues[1] };
            resultsPage.Size = new Size(253, 243);

            Label label = new Label() { Text = "Results: ", Location = new Point(10, 10) };

            TextBox results = new TextBox();
            results.Multiline = true;
            results.Location = new Point(10, label.Height + 15);
            results.Size = new Size(200, resultsPage.Height - label.Height);
            results.ScrollBars = ScrollBars.Horizontal;
            //results.Dock = DockStyle.Fill;
            switch (NameValues[0])
            {
                case "ocr":
                    results.Text = ocrResults.Get(Convert.ToInt32(NameValues[2]), Convert.ToInt32(NameValues[1])).getResults();
                    break;
                case "omr":
                    results.Text = omrResults.Get(Convert.ToInt32(NameValues[2]), Convert.ToInt32(NameValues[1])).getResults();
                    break;
                case "barcode":
                    results.Text = barcodeResults.Get(Convert.ToInt32(NameValues[2]), Convert.ToInt32(NameValues[1])).getResults();
                    break;
                default:
                    break;
            }
            Control[] controls = { label, results };
            resultsPage.Controls.AddRange(controls);
            tabControl1.TabPages.Add(resultsPage);
        }

        void size_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void t_TextChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void CreateTypeTab(int p)
        {
            string[] NameValues = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[p].Name.Split(',');
            string type = NameValues[1];
            TabPage page = new TabPage() { Text = treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[p].Text };
            page.Size = new Size(253, 243);

            Label label = new Label() { Text = "Number of Results: " + treeView1.Nodes[documentAnnotationViewer1.CurrentImageIndex].Nodes[p].GetNodeCount(false).ToString(), Location = new Point(10, 0), Size = new Size(240, 30) };
            Control[] ctrls;
            page.Controls.Add(label);
            int i = 0;
            int j = 0;


            switch (type)
            {
                case "ocr":
                    RecognitionResult[] ocrPageResults = ocrResults.GetRegionsForPage(documentAnnotationViewer1.CurrentImageIndex);
                    ctrls = new Control[ocrResults.GetCountForPage(documentAnnotationViewer1.CurrentImageIndex) * 2];
                    foreach (RecognitionResult result in ocrPageResults)
                    {
                        Label resultLabel = new Label() { Text = "Result #" + (i + 1).ToString() + ": " + result.description, Size = new Size(240, 20), Location = new Point(10, 30 + (i * 80)) };
                        TextBox ocrText = new TextBox() { Text = result.getResults(), Multiline = true, Size = new Size(240, 50), Location = new Point(10, 50 + (i * 80)) };
                        ctrls[j] = resultLabel;
                        ctrls[j + 1] = ocrText;
                        i++;
                        j = j + 2;
                    }
                    page.Controls.AddRange(ctrls);
                    break;
                case "omr":
                    RecognitionResult[] omrPageResults = omrResults.GetRegionsForPage(documentAnnotationViewer1.CurrentImageIndex);
                    ctrls = new Control[omrResults.GetCountForPage(documentAnnotationViewer1.CurrentImageIndex) * 2];
                    foreach (RecognitionResult result in omrPageResults)
                    {
                        Label resultLabel = new Label() { Text = "Result #" + (i + 1).ToString() + ": " + result.description, Size = new Size(240, 20), Location = new Point(10, 30 + (i * 80)) };
                        TextBox omrText = new TextBox() { Text = result.getResults(), Multiline = true, Size = new Size(240, 50), Location = new Point(10, 50 + (i * 80)) };
                        ctrls[j] = resultLabel;
                        ctrls[j + 1] = omrText;
                        i++;
                        j = j + 2;
                    }
                    page.Controls.AddRange(ctrls);
                    break;
                case "bc":
                    RecognitionResult[] barcodePageResults = barcodeResults.GetRegionsForPage(documentAnnotationViewer1.CurrentImageIndex);
                    ctrls = new Control[barcodeResults.GetCountForPage(documentAnnotationViewer1.CurrentImageIndex) * 2];
                    foreach (RecognitionResult result in barcodePageResults)
                    {
                        Label resultLabel = new Label() { Text = "Result #" + (i + 1).ToString() + ": " + result.description, Size = new Size(240, 20), Location = new Point(10, 30 + (i * 80)) };
                        TextBox bcText = new TextBox() { Text = result.getResults(), Multiline = true, Size = new Size(240, 50), Location = new Point(10, 50 + (i * 80)) };
                        ctrls[j] = resultLabel;
                        ctrls[j + 1] = bcText;
                        i++;
                        j = j + 2;
                    }
                    page.Controls.AddRange(ctrls);
                    break;
                default:
                    break;
            }

            tabControl1.TabPages.Add(page);
        }

        private void CreatePageTab(int pageNum)
        {
            TabPage page = new TabPage() { Text = treeView1.Nodes[pageNum].Text };
            page.Size = new Size(253, 243);

            Label label1 = new Label() { Text = "Number of OCR Regions: " + treeView1.Nodes[pageNum].Nodes[0].GetNodeCount(false).ToString(), Location = new Point(10, 10), Size = new Size(240, 20) };
            Label label2 = new Label() { Text = "Number of OMR Regions: " + treeView1.Nodes[pageNum].Nodes[1].GetNodeCount(false).ToString(), Location = new Point(10, 50), Size = new Size(240, 20) };
            Label label3 = new Label() { Text = "Number of Barcode regions: " + treeView1.Nodes[pageNum].Nodes[2].GetNodeCount(false).ToString(), Location = new Point(10, 70), Size = new Size(240, 20) };
            Control[] controls = { label1, label2, label3 };
            page.Controls.AddRange(controls);
            tabControl1.TabPages.Add(page);
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                tabControl1.TabPages.Clear();
                documentAnnotationViewer1.SelectThumbnail(e.Node.Parent.Parent.Index);
                documentAnnotationViewer1.EnsureVisible(e.Node.Parent.Parent.Index);
                SelectAnno(e.Node.Index, e.Node.Name.Split(',')[0]);
                CreateRegionTab(e.Node);
            }
        }





        #endregion

        private void ProcessAllRegions_Click(object sender, EventArgs e)
        {
            StreamWriter writer;
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "TEXT (*.txt)|*.txt";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = File.Create(dlg.FileName))
                    {
                        writer = new StreamWriter(fs);
                        WriteResults(writer);
                        writer.Close();
                        writer.Dispose();
                    }
                }

            }



        }


        private void WriteResults(StreamWriter writer)
        {
            for (int i = 0; i < documentAnnotationViewer1.Count; i++)
            {
                writer.WriteLine("###Recognition Results Page: " + (i + 1).ToString() + "###");
                writer.WriteLine("#################################" + Environment.NewLine + Environment.NewLine);
                //Ocr for page 
                int ocrCount = ocrResults.GetCountForPage(i);
                writer.WriteLine(">>>OCR Regions, count: " + ocrCount.ToString() + " <<<" + Environment.NewLine);
                RecognitionResult[] ocrPageResults = ocrResults.GetRegionsForPage(i);
                for (int j = 0; j < ocrCount; j++)
                {
                    writer.WriteLine("Region #" + j.ToString() + ", Description: " + ocrPageResults[j].description);
                    writer.WriteLine("Results: " + ocrPageResults[j].getResults());

                }
                //Omr for page
                int omrCount = omrResults.GetCountForPage(i);
                writer.WriteLine(">>>OMR Regions, count: " + omrCount.ToString() + " <<<" + Environment.NewLine);
                RecognitionResult[] omrPageResults = omrResults.GetRegionsForPage(i);
                for (int j = 0; j < omrCount; j++)
                {
                    writer.WriteLine("Region #" + j.ToString() + ", Description: " + omrPageResults[j].description);
                    writer.WriteLine("Results: " + omrPageResults[j].getResults());

                }
                //Barcode for page
                int barcodeCount = barcodeResults.GetCountForPage(i);
                writer.WriteLine(">>>Barcode Regions, count: " + barcodeCount.ToString() + " <<<" + Environment.NewLine);
                RecognitionResult[] barcodePageResults = barcodeResults.GetRegionsForPage(i);
                for (int j = 0; j < barcodeCount; j++)
                {
                    writer.WriteLine("Region #" + j.ToString() + ", Description: " + barcodePageResults[j].description);
                    writer.WriteLine("Results: " + barcodePageResults[j].getResults());

                }

            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Selection;
            documentAnnotationViewer1.ImageControl.Selection.Changed += new Atalasoft.Imaging.WinControls.RubberbandEventHandler(Selection_Changed);
        }

        void Selection_Changed(object sender, Atalasoft.Imaging.WinControls.RubberbandEventArgs e)
        {
            Canvas canvas = new Canvas(documentAnnotationViewer1.ImageControl.Image);
            canvas.DrawRectangle(documentAnnotationViewer1.ImageControl.Selection.Bounds, new AtalaPen(Color.Black));
        }

        private void Save_Click(object sender, EventArgs e)
        {
            //Load Template

            treeView1.Nodes.Clear();
            InitialzeTree();

            string templatePath = "";
            using (OpenFileDialog dlg = new OpenFileDialog() { Title = "Select a region template to load...", Filter = "Document Region Template (*.drt)|*.drt" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //documentAnnotationViewer1.Annotations.Load(dlg.FileName, AnnotationDataFormat.Xmp);
                    LoadRegions(dlg.FileName);
                    templatePath = dlg.FileName;

                }
            }


            using (FileStream fs = File.OpenRead(templatePath))
                documentAnnotationViewer1.LoadAnnotationData(fs, -1, new XmpFormatter(), true);

            documentAnnotationViewer1.ClearSelection();
            documentAnnotationViewer1.SelectThumbnail(0);

        }

        private void LoadRegions(string p)
        {
            ac = new AnnotationController();
            ac.Load(p, AnnotationDataFormat.Xmp);
            using (FileStream fs = File.OpenRead(documentPath))
            {
                ProcessRegions(ac, fs);

            }

        }
        #region Image Cleanup
        private void RotateLeft_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.RotateDocument(DocumentRotation.Rotate270);
        }

        private void RotateRight_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.RotateDocument(DocumentRotation.Rotate90);
        }

        private void Deskew_Click(object sender, EventArgs e)
        {
            AutoDeskewCommand cmd = new AutoDeskewCommand();
            if (cmd.IsPixelFormatSupported(documentAnnotationViewer1.ImageControl.Image.PixelFormat))
                documentAnnotationViewer1.ApplyCommand(cmd);
            else
                MessageBox.Show("This command can only be applied to images to the following pixel formats:" + cmd.SupportedPixelFormats.ToString());
        }

        private void Despeckle_Click(object sender, EventArgs e)
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

        private void HolePunchRemoval_Click(object sender, EventArgs e)
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

        private void Arrow_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.None;
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.None;
        }

        private void zoom_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.None;
            documentAnnotationViewer1.ImageControl.MouseTool = Atalasoft.Imaging.WinControls.MouseToolType.Zoom;
        }

        private void BestFit_Click(object sender, EventArgs e)
        {
            documentAnnotationViewer1.ImageControl.AutoZoom = Atalasoft.Imaging.WinControls.AutoZoomMode.BestFit;
        }
    }
}
