Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Net
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Public Class ViewPDF
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            viewPDF()
        End If
    End Sub

    Private Sub viewPDF()
        Try
            Dim pdfPath = Session("pdfPath")
            If Not pdfPath Is Nothing Then
                Dim client As WebClient = New WebClient()
                Dim buffer() As Byte = client.DownloadData(pdfPath)
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-length", buffer.Length.ToString())
                Response.BinaryWrite(buffer)
                File.Delete(pdfPath)
            End If

        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try

    End Sub

End Class