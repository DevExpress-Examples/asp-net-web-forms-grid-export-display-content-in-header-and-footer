using System;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Drawing;

public partial class _Default : System.Web.UI.Page {
    private Image headerImage;

    protected void Page_Load(object sender, EventArgs e) { }
    protected void pdf_Click(object sender, EventArgs e) {
        Export("pdf");
    }
    protected void rtf_Click(object sender, EventArgs e) {
        Export("rtf");
    }
    protected void xls_Click(object sender, EventArgs e) {
        Export("xls");
    }

    void Export(string format) {
        PrintingSystemBase ps = new PrintingSystemBase();

        using (headerImage = Image.FromFile(Server.MapPath("~/Images/DevExpress-Logo-Large-Color.png"))) {
            LinkBase header = new LinkBase();
            header.CreateDetailHeaderArea += Header_CreateDetailHeaderArea;

            PrintableComponentLinkBase link1 = new PrintableComponentLinkBase();
            link1.Component = this.ASPxGridViewExporter1;
            link1.CreateReportFooterArea += Link1_CreateReportFooterArea;
            CompositeLinkBase compositeLink = new CompositeLinkBase(ps);
            compositeLink.Links.AddRange(new object[] { header, link1 });

            compositeLink.CreateDocument();
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
            ps.Dispose();
        }
    }
    void Header_CreateDetailHeaderArea(object sender, CreateAreaEventArgs e) {
        e.Graph.BorderWidth = 0;
        Rectangle r = new Rectangle(0, 0, headerImage.Width, headerImage.Height);
        e.Graph.DrawImage(headerImage, r);
        r = new Rectangle(0, headerImage.Height, 200, 50);
        e.Graph.DrawString("Additional Header information here....", r);
    }
    void Link1_CreateReportFooterArea(object sender, CreateAreaEventArgs e) {
        e.Graph.BorderWidth = 0;
        Rectangle r = new Rectangle(0, 20, 200, 50);
        e.Graph.Font = new Font("Times New Roman", 12, FontStyle.Italic);
        e.Graph.ForeColor = Color.Gray;
        e.Graph.DrawString("This is footer", r);
    }

    void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, MemoryStream stream) {
        if (Page == null || Page.Response == null)
            return;
        string disposition = saveAsFile ? "attachment" : "inline";
        Page.Response.Clear();
        Page.Response.Buffer = false;
        Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", fileFormat));
        Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
        Page.Response.AppendHeader("Content-Disposition",
            string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
        Page.Response.BinaryWrite(stream.ToArray());
        Page.Response.End();
    }
}
