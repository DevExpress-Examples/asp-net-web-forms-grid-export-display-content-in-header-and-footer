Imports System
Imports System.IO
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports System.Drawing

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private headerImage As Image

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    End Sub
    Protected Sub pdf_Click(ByVal sender As Object, ByVal e As EventArgs)
        Export("pdf")
    End Sub
    Protected Sub rtf_Click(ByVal sender As Object, ByVal e As EventArgs)
        Export("rtf")
    End Sub
    Protected Sub xls_Click(ByVal sender As Object, ByVal e As EventArgs)
        Export("xls")
    End Sub

    Private Sub Export(ByVal format As String)
        Dim ps As New PrintingSystemBase()

        headerImage = Image.FromFile(Server.MapPath("~/Images/DevExpress-Logo-Large-Color.png"))
        Using headerImage
            Dim header As New LinkBase()
            AddHandler header.CreateDetailHeaderArea, AddressOf Header_CreateDetailHeaderArea

            Dim link1 As New PrintableComponentLinkBase()
            link1.Component = Me.ASPxGridViewExporter1
            AddHandler link1.CreateReportFooterArea, AddressOf Link1_CreateReportFooterArea
            Dim compositeLink As New CompositeLinkBase(ps)
            compositeLink.Links.AddRange(New Object() { header, link1 })

            compositeLink.CreateDocument()
            Using stream As New MemoryStream()
                Select Case format
                    Case "xls"
                        compositeLink.ExportToXls(stream)
                        WriteToResponse("filename", True, format, stream)
                    Case "pdf"
                        compositeLink.ExportToPdf(stream)
                        WriteToResponse("filename", True, format, stream)
                    Case "rtf"
                        compositeLink.ExportToRtf(stream)
                        WriteToResponse("filename", True, format, stream)
                    Case Else
                End Select
            End Using
            ps.Dispose()
        End Using
    End Sub
    Private Sub Header_CreateDetailHeaderArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
        e.Graph.BorderWidth = 0
        Dim r As New Rectangle(0, 0, headerImage.Width, headerImage.Height)
        e.Graph.DrawImage(headerImage, r)
        r = New Rectangle(0, headerImage.Height, 200, 50)
        e.Graph.DrawString("Additional Header information here....", r)
    End Sub
    Private Sub Link1_CreateReportFooterArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
        e.Graph.BorderWidth = 0
        Dim r As New Rectangle(0, 20, 200, 50)
        e.Graph.Font = New Font("Times New Roman", 12, FontStyle.Italic)
        e.Graph.ForeColor = Color.Gray
        e.Graph.DrawString("This is footer", r)
    End Sub

    Private Sub WriteToResponse(ByVal fileName As String, ByVal saveAsFile As Boolean, ByVal fileFormat As String, ByVal stream As MemoryStream)
        If Page Is Nothing OrElse Page.Response Is Nothing Then
            Return
        End If
        Dim disposition As String = If(saveAsFile, "attachment", "inline")
        Page.Response.Clear()
        Page.Response.Buffer = False
        Page.Response.AppendHeader("Content-Type", String.Format("application/{0}", fileFormat))
        Page.Response.AppendHeader("Content-Transfer-Encoding", "binary")
        Page.Response.AppendHeader("Content-Disposition", String.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat))
        Page.Response.BinaryWrite(stream.ToArray())
        Page.Response.End()
    End Sub
End Class
