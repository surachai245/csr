Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Mail
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Image = System.Drawing.Image
Imports System.Web.Configuration

Public Class MainLayout
    Inherits MasterPage
    Dim objUser As UserProfile = HttpContext.Current.Session("UserProfileData")
    Protected Sub Page_Load(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            Try
                lbFullName.Text = objUser.FullName
                lbTitlePage.Text = objUser.TitlePage
                Dim tClient As WebClient = New WebClient
                Dim Pic As String = WebConfigurationManager.AppSettings("Picture")
                Dim url = Pic & objUser.PictureURL
                Dim tImage As Image = Image.FromStream(New MemoryStream(tClient.DownloadData(url)))
                Dim bArr As Byte() = imgToByteArray(tImage)
                ' previewimgEmp2.Attributes.Add("src", tImage.ToString) '***
                previewimgEmp21.ImageUrl = "data:image;base64," + Convert.ToBase64String(bArr)

            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub lbHome_Click(sender As Object, e As EventArgs) Handles lbHome.Click
        objUser.TitlePage = "Home"
        Response.Redirect("~/App_CSR/Home.aspx")
    End Sub
    Protected Sub lbReport_Click(sender As Object, e As EventArgs) Handles lbReport.Click
        objUser.TitlePage = "Report"
        Response.Redirect("~/App_CSR/Report.aspx")
    End Sub

    Protected Sub lbAdmin_Click(sender As Object, e As EventArgs) Handles lbAdmin.Click
        objUser.TitlePage = "จัดการข้อมูล Admin"
        Response.Redirect("~/App_CSR/AdminList.aspx")
    End Sub

    Public Function imgToByteArray(ByVal img As Image) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function

End Class