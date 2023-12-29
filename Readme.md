<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128539384/13.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3184)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to display content in the Header and Footer sections when exporting ASPxGridView

### Steps to implement:

1. Add ASPxGridView and [ASPxGridViewExporter](https://documentation.devexpress.com/AspNet/DevExpress.Web.ASPxGridViewExporter.class) to a page ([Default.aspx](https://github.com/DevExpress-Examples/how-to-display-content-in-the-header-and-footer-sections-when-exporting-aspxgridview-e3184/blob/16.1.7%2B/CS/WebSite/Default.aspx)).
2. Add a button and handle its Click event: this button will export the grid.
3. Create the [LinkBase](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.class) object for the header area in code behind. It will add the header to export.
4. Handle its [CreateDetailHeaderArea](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.CreateDetailHeaderArea.event) event. This event allows you to customize the header area. 
3. Use the [event properties](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.CreateAreaEventArgs.Graph.property) to render additional objects (images, text) in the header area:
```cs
 void Header_CreateDetailHeaderArea(object sender, CreateAreaEventArgs e) {
        e.Graph.BorderWidth = 0;
        Rectangle r = new Rectangle(0, 0, headerImage.Width, headerImage.Height);
        e.Graph.DrawImage(headerImage, r); 
        r = new Rectangle(0, headerImage.Height, 200, 50);
        e.Graph.DrawString("Additional Header information here....", r);
    }
```
```vb
 Private Sub Header_CreateDetailHeaderArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
		e.Graph.BorderWidth = 0
		Dim r As New Rectangle(0, 0, headerImage.Width, headerImage.Height)
		e.Graph.DrawImage(headerImage, r)
		r = New Rectangle(0, headerImage.Height, 200, 50)
		e.Graph.DrawString("Additional Header information here....", r)
 End Sub
```
4. Create the [PrintableComponentLinkBase](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrintingLinks.PrintableComponentLinkBase.class) object and use the ASPxGridViewExporter control as **PrintableComponentLinkBase.Component**. This object will add ASPxGridView to export.
5. Create [CompositeLinkBase](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrintingLinks.CompositeLinkBase.members) and combine the header and grid links using this component. Call the [ExportTo[FORMAT]](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.ExportToXls.overloads) method to export the result:
```cs
            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { header, link1 });
            ...
            compositeLink.ExportToXls(stream);
```
```vb
            Dim compositeLink As New CompositeLinkBase(ps)
			compositeLink.Links.AddRange(New Object() { header, link1 })
			compositeLink.ExportToXls(stream)
```
6. It is also possible to use the [PrintableComponentLinkBase.CreateReportFooterArea](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.CreateReportFooterArea.event) and [CreateReportHeaderArea](https://documentation.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.LinkBase.CreateReportHeaderArea.event) events to customize header/footer. This example shows how to use one of them (CreateReportFooterArea) for footer customization (see the [Default.aspx.cs](https://github.com/DevExpress-Examples/how-to-display-content-in-the-header-and-footer-sections-when-exporting-aspxgridview-e3184/blob/16.1.7%2B/CS/WebSite/Default.aspx.cs) ([VB](https://github.com/DevExpress-Examples/how-to-display-content-in-the-header-and-footer-sections-when-exporting-aspxgridview-e3184/blob/16.1.7%2B/VB/WebSite/Default.aspx.vb)) file).


### See Also:
[How to display images in the Header and Footer sections when exporting ASPxGridView](https://www.devexpress.com/Support/Center/p/E1935)



