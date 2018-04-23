Imports System
Imports System.IO
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports System.Drawing

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private headerImage As System.Drawing.Image

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
        Dim ps As New PrintingSystem()

        Me.headerImage = System.Drawing.Image.FromFile(Server.MapPath("~\Images\pix.png"))
        Using Me.headerImage
            Dim header As New Link()
            AddHandler header.CreateDetailArea, AddressOf header_CreateDetailArea

            Dim link1 As New PrintableComponentLink()
            link1.Component = Me.ASPxGridViewExporter1

            Dim compositeLink As New CompositeLink(ps)
            compositeLink.Links.AddRange(New Object() { header, link1 })

            compositeLink.CreateDocument()
            Using stream As New MemoryStream()
                Select Case format
                    Case "xls"
                        compositeLink.PrintingSystem.ExportToXls(stream)
                        WriteToResponse("filename", True, format, stream)
                    Case "pdf"
                        compositeLink.PrintingSystem.ExportToPdf(stream)
                        WriteToResponse("filename", True, format, stream)
                    Case "rtf"
                        compositeLink.PrintingSystem.ExportToRtf(stream)
                        WriteToResponse("filename", True, format, stream)
                    Case Else
                End Select

            End Using
            ps.Dispose()
        End Using
    End Sub

    Private Sub header_CreateDetailArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
        e.Graph.BorderWidth = 0

        Dim r As New Rectangle(0, 0, headerImage.Width, headerImage.Height)
        e.Graph.DrawImage(headerImage, r)

        r = New Rectangle(0, headerImage.Height, 400, 50)
        e.Graph.DrawString("Additional Header information here....", r)

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
