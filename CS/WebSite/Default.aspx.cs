using System;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.Drawing;

public partial class _Default : System.Web.UI.Page {
    private System.Drawing.Image headerImage;

    protected void Page_Load(object sender, EventArgs e) {

    }
    protected void b1_Click(object sender, EventArgs e) {
        PrintingSystem ps = new PrintingSystem();

        using (this.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\\Images\\pix.png"))) {
            Link header = new Link();
            header.CreateDetailArea += new CreateAreaEventHandler(header_CreateDetailArea);

            DevExpress.Web.ASPxGridView.Export.Helper.GridViewLink link1 = new DevExpress.Web.ASPxGridView.Export.Helper.GridViewLink(this.ASPxGridViewExporter1);

            CompositeLink compositeLink = new CompositeLink(ps);
            compositeLink.Links.AddRange(new object[] { header, link1 });

            compositeLink.CreateDocument();
            using (MemoryStream stream = new MemoryStream()) {
                compositeLink.PrintingSystem.ExportToPdf(stream);
                WriteToResponse("filename", true, "pdf", stream);
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
