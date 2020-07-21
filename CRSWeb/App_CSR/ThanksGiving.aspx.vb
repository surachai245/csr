Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Image = System.Drawing.Image
Imports System.Web.Configuration
Public Class ThanksGiving
    Inherits System.Web.UI.Page
    Dim objProject As SysProject
    Dim objEmployee As SysEmployee
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            objProject = Session("ProjestDetail")
            objEmployee = Session("EmployeeDetail")
            previewtitle2.InnerText() = objProject.ProjectName
            previewtitle2.Attributes.Add("style", "color:" + objProject.Tab1_FontColor + ";font-family:" + objProject.Tab1_FontName)
            previewbody2.Attributes.Add("style", "background-color:" + objProject.Tab3_BGColor)
            body.Attributes.Add("style", "background-color:" + objProject.Tab3_BGColor)
            ' previewimg1.Attributes.Add("src", "file://w2hrdpho102/Compu/hr/Picture/" & objEmployee.PictureURL) '***

            'รูปพนักงาน 
            Dim tClient As WebClient = New WebClient
            Dim Pic As String = WebConfigurationManager.AppSettings("Picture")
            Dim url = Pic & objEmployee.PictureURL
            Dim tImage As Image = Image.FromStream(New MemoryStream(tClient.DownloadData(url)))
            Dim bArr As Byte() = imgToByteArray(tImage)
            previewimgEmp21.ImageUrl = "data:image;base64," + Convert.ToBase64String(bArr)

            previewtxt8.InnerText() = objEmployee.FullName
            previewtxt9.InnerText() = objEmployee.Position + " " + objEmployee.Section
            previewtxt10.InnerText() = objEmployee.SECTER ' objEmployee.Department + " " + objEmployee.Division
            previewtxt11.InnerText() = objEmployee.EMail
            previewtxt12.InnerText() = "เบอร์ต่อ " + objEmployee.Phone
            previewtxt8.Attributes.Add("style", "color:" + objProject.Tab3_FontColor)
            previewtxt9.Attributes.Add("style", "color:" + objProject.Tab3_FontColor)
            previewtxt10.Attributes.Add("style", "color:" + objProject.Tab3_FontColor)
            previewtxt11.Attributes.Add("style", "color:" + objProject.Tab3_FontColor)
            previewtxt12.Attributes.Add("style", "color:" + objProject.Tab3_FontColor)
            'previewimg2.Attributes.Add("src", "Document/temp/" & objProject.Tab3_FileName)

            Try
                If (objProject.Tab3_FileName <> "" And objProject.MapDirectory <> "") Then
                    Dim confPath As String = ""
                    confPath = LHUtility.GetConfig("CSRPathDestination")
                    Dim desPath = confPath & "/" & objProject.MapDirectory & "/" & objProject.Tab3_FileName
                    Dim FileName = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
                    previewimg2.Attributes.Add("src", FileName)
                End If
            Catch ex As Exception
            End Try

            divTxt.InnerHtml = objProject.Tab3_TextMessage
            divTxt.Attributes.Add("style", "color:" + objProject.Tab3_TextFontColor + ";font-family:" + objProject.Tab3_TextFontName)
        End If
    End Sub
    Protected Sub btGiving_Click(sender As Object, e As EventArgs) Handles btGiving.Click
        Response.Redirect("InviteFriends.aspx")
    End Sub
    Public Function imgToByteArray(ByVal img As Image) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function
End Class