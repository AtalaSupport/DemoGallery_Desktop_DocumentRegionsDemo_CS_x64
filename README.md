# Document Regions Demo
This is a demo that is actually 2 projects:
- DocumentRegionsCreator
- DocumentRegionsReader

The creator allows you to define a template that will be used by the reader to 
implement zonal OCR and optionally other types of template based recognition

The reader is a sample tool that shows how to consume the template and apply it 
to the actual recognition tasks

This sample app is C# only. There is no VB.NET Version.

## Prerequisites
This demo assumes you have the Atalasoft DotImage SDK installed and licensed for 
DotImage Document Imaging. In order to use the OCR features, you must have a license
for our OCR with GlyphReader engine. In order to use the Barcode Reading features,
you must have a license for our Bacode Reading addon.

You may also request a 30 day evaluation when installing / activating.

[Download DotImage](https://www.atalasoft.com/BeginDownload/DotImageDownloadPage)

## Cloning
We recommend the following to ensure you clone with the required submodule

Example: git for windows
```bash
git clone https://github.com/AtalaSupport/DemoGallery_Desktop_DocumentRegionsDemo_CS_x64.git DocumentRegionsDemo
```