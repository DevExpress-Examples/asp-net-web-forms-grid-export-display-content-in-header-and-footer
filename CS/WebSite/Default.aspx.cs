using System;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Drawing;

public partial class _Default : System.Web.UI.Page {
    private System.Drawing.Image headerImage;

    protected void Page_Load(object sender, EventArgs e) {

    }
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
        PrintingSystem ps = new PrintingSystem();

        using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\pix.png"))) {
            Link header = new Link();
            header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);

            PrintableComponentLink link1 = new PrintableComponentLink();
            link1.Component = this.ASPxGridViewExporter1;

            CompositeLink compositeLink = new CompositeLink(ps);
            compositeLink.Links.AddRange(new object[] { header, link1 });

            compositeLink.CreateDocument();
            using (MemoryStream stream = new MemoryStream()) {
                switch (format) {
                    case "xls":
                        compositeLink.PrintingSystem.ExportToXls(stream);
                        WriteToResponse("filename", true, format, stream); break;
                    case "pdf":
                        compositeLink.PrintingSystem.ExportToPdf(stream);
                        WriteToResponse("filename", true, format, stream);
                        break;
                    case "rtf":
                        compositeLink.PrintingSystem.ExportToRtf(stream);
                        WriteToResponse("filename", true, format, stream);
                        break;
                    default:
                        break;
                }

            }
            ps.Dispose();
        }
    }

    void header_CreateDetailArea(object sender, CreateAreaEventArgs e) {
        e.Graph.BorderWidth = 0;

        Rectangle r = new Rectangle(0, 0, headerImage.Width, headerImage.Height);
        e.Graph.DrawImage(headerImage, r);

        r = new Rectangle(0, headerImage.Height, 400, 50);
        e.Graph.DrawString("Additional Header information here....", r);

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
