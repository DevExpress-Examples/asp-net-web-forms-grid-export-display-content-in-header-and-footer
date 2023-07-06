<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128539384/16.1.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3184)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Grid View for ASP.NET Web Forms - How to add content in the header and footer sections when exporting grid
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e3184/)**
<!-- run online end -->

The [DevExpress.XtraPrinting](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrinting) library allows you to add header and footer in exported document in two ways:

* Create a [printing link](https://docs.devexpress.com/WindowsForms/104/controls-and-libraries/printing-exporting/concepts/basic-terms/printing-links) for each element (header, footer, and main control) and combine them into one document.
* Create a [printing link](https://docs.devexpress.com/WindowsForms/104/controls-and-libraries/printing-exporting/concepts/basic-terms/printing-links) for the main control and handle the link's [CreateReportHeaderArea](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.CreateReportHeaderArea.event) and [CreateReportFooterArea](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.CreateReportFooterArea.event) events to customize document header and footer.

In this example, the first approach is used to create a document header, and the second approach is used to create a document footer.
## 1. Create a header as a separate printing link

[Printing links](https://docs.devexpress.com/WindowsForms/104/controls-and-libraries/printing-exporting/concepts/basic-terms/printing-links) transforms a control's data into bricks of appropriate types, and arranges them into a printing document. To create a printing link, follow the steps below:

1. Create a [LinkBase](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase) object for the header area.
    ```cs
    LinkBase header = new LinkBase();
    ```
2. Handle its [CreateDetailHeaderArea](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.CreateDetailHeaderArea) event.
    ```cs
    header.CreateDetailHeaderArea += Header_CreateDetailHeaderArea;
    ```
3. Use the [event properties](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.CreateAreaEventArgs.Graph) to render additional objects (images, text) in the header area:
    ```cs
    void Header_CreateDetailHeaderArea(object sender, CreateAreaEventArgs e) {
            e.Graph.BorderWidth = 0;
            Rectangle r = new Rectangle(0, 0, headerImage.Width, headerImage.Height);
            e.Graph.DrawImage(headerImage, r); 
            r = new Rectangle(0, headerImage.Height, 200, 50);
            e.Graph.DrawString("Additional Header information here....", r);
        }
    ```

## Create a footer in the grid event handler

1. Create a [PrintableComponentLinkBase](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrintingLinks.PrintableComponentLinkBase) object and assign the  ASPxGridView control to the  [Component](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrintingLinks.PrintableComponentLinkBase.Component) property.

    ```cs
    PrintableComponentLinkBase link1 = new PrintableComponentLinkBase();
    link1.Component = ASPxGridView1;
    ```

2. Handle the [CreateReportFooterArea](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.CreateReportFooterArea.event) event to customize  grid footer.

    ```cs
    link1.CreateReportFooterArea += Link1_CreateReportFooterArea;
    // ...
    void Link1_CreateReportFooterArea(object sender, CreateAreaEventArgs e) {
        e.Graph.BorderWidth = 0;
        Rectangle r = new Rectangle(0, 20, 200, 50);
        e.Graph.Font = new Font("Times New Roman", 12, FontStyle.Italic);
        e.Graph.ForeColor = Color.Gray;
        e.Graph.DrawString("This is footer", r);
    }
    ```

## Combine printing links and export the result

1. Create a [CompositeLinkBase](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrintingLinks.CompositeLinkBase) object and combine printable elements (header and the grid control). 

    ```cs
    CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
    compositeLink.Links.AddRange(new object[] { header, link1 });

    compositeLink.CreateDocument();
    ````

2. Call a [ExportTo[FORMAT]](https://docs.devexpress.com/CoreLibraries/devexpress.xtraprinting.linkbase.exporttoxls.overloads) method to export the result.
    ```cs
    using (MemoryStream stream = new MemoryStream()) {
        switch (format) {
            case "xls":
                compositeLink.ExportToXls(stream);
                WriteToResponse("filename", true, format, stream);
                break;
            case "pdf":
                compositeLink.ExportToPdf(stream);
                WriteToResponse("filename", true, format, stream);
                break;
            case "rtf":
                compositeLink.ExportToRtf(stream);
                WriteToResponse("filename", true, format, stream);
                break;
            default:
                break;
        }
    }
    ```

## Files to Review

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))

## More Examples

* [How to display images in the Header and Footer sections when exporting ASPxGridView](https://github.com/DevExpress-Examples/how-to-display-images-in-the-header-and-footer-sections-when-exporting-aspxgridview-e1935)


