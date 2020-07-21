Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Net.Mail
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Public Class EmployeeView
    Inherits System.Web.UI.Page
    Dim objProject As SysProject
    Dim objEmployee As SysEmployee
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                objProject = Session("ProjestDetail")
                objEmployee = Session("EmployeeDetail")
                previewtitle.InnerText() = objProject.ProjectName
                'previewtitle.Attributes.Add("style", )
                previewtitle.Attributes.Add("style", "font-family:" + objProject.Tab1_FontName + ";color:" + objProject.Tab1_FontColor)
                previewbody.Attributes.Add("style", "background-color:" + objProject.Tab1_BGColor)
                body.Attributes.Add("style", "background-color:" + objProject.Tab1_BGColor)
                'previewimg.Attributes.Add("src", "Document/temp/" & objProject.Tab1_FileName)

                Try
                    If (objProject.Tab1_FileName <> "" And objProject.MapDirectory <> "") Then
                        Dim confPath As String = ""
                        confPath = LHUtility.GetConfig("CSRPathDestination")
                        Dim desPath = confPath & "/" & objProject.MapDirectory & "/" & objProject.Tab1_FileName
                        Dim FileName = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
                        previewimg.Attributes.Add("src", FileName)
                    End If
                Catch ex As Exception
                End Try

            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub btRegister_Click(sender As Object, e As EventArgs) Handles btRegister.Click
        ' SendMail()
        Response.Redirect("Employeeregister.aspx")
    End Sub

    Protected Sub btAddFriend_Click(sender As Object, e As EventArgs) Handles btAddFriend.Click
        Response.Redirect("InviteFriendsDetail.aspx")
    End Sub
End Class