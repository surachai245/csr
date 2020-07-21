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
Imports System.Windows.Forms
Imports Image = System.Drawing.Image
Imports System.Windows
Imports System.Web.Configuration
Public Class Employeeregister
    Inherits System.Web.UI.Page
    Dim objProject As SysProject
    Dim objEmployee As SysEmployee
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                objProject = Session("ProjestDetail")
                objEmployee = Session("EmployeeDetail")

                'รูปพนักงาน 
                Dim tClient As WebClient = New WebClient
                Dim Pic As String = WebConfigurationManager.AppSettings("Picture")
                Dim url = Pic & objEmployee.PictureURL
                Dim tImage As Image = Image.FromStream(New MemoryStream(tClient.DownloadData(url)))
                Dim bArr As Byte() = imgToByteArray(tImage)
                previewimgEmp21.ImageUrl = "data:image;base64," + Convert.ToBase64String(bArr)

                ' PictureBox1.Image = img1
                previewtitle1.InnerText() = objProject.ProjectName
                previewtitle1.Attributes.Add("style", "color:" + objProject.Tab1_FontColor + ";font-family:" + objProject.Tab1_FontName)
                previewbody1.Attributes.Add("style", "background-color:" + objProject.Tab2_BGColor)
                body.Attributes.Add("style", "background-color:" + objProject.Tab2_BGColor)
                previewtxt1.InnerText() = objEmployee.FullName
                previewtxt2.InnerText() = objEmployee.Position + " " + objEmployee.Section
                previewtxt3.InnerText() = objEmployee.SECTER 'objEmployee.Department + " " + objEmployee.Division
                previewtxt4.InnerText() = objEmployee.EMail
                previewtxt5.InnerText() = "เบอร์ต่อ " + objEmployee.Phone
                previewtxt1.Attributes.Add("style", "color:" + objProject.Tab2_FontColor)
                previewtxt2.Attributes.Add("style", "color:" + objProject.Tab2_FontColor)
                previewtxt3.Attributes.Add("style", "color:" + objProject.Tab2_FontColor)
                previewtxt4.Attributes.Add("style", "color:" + objProject.Tab2_FontColor)
                previewtxt5.Attributes.Add("style", "color:" + objProject.Tab2_FontColor)
                previewtxt6.Attributes.Add("style", "color:" + objProject.Tab2_FontColor)
                previewtxt7.Attributes.Add("style", "color:" + objProject.Tab2_FontColor)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub btRegister_Click(sender As Object, e As EventArgs) Handles btRegister.Click
        If (cbConfrim.Checked) Then
            objProject = Session("ProjestDetail")
            objEmployee = Session("EmployeeDetail")
            Dim ProjectMember = GetData.InsertProjectMember(objProject.ProjectId, objEmployee.empid, tbAttendExpected.Text, cbConfrim.Checked, DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), objEmployee.empid)
            If (ProjectMember) Then
                Response.Redirect("ThanksGiving.aspx")
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('','','กรุณาเลือกยืนยันการสมัคร');", True)
        End If
    End Sub

    Public Function imgToByteArray(ByVal img As Image) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function

    Protected Sub btClose_Click(sender As Object, e As EventArgs) Handles btClose.Click
        ' Me.Page.ClientScript.RegisterStartupScript(Page.GetType, "CloseForm", "<script language='javascript'>{self.close();}</script>;")
        Response.Write("<script type='text/javascript'> " +
                        "window.close(); " +
                        "</script>")
    End Sub
End Class