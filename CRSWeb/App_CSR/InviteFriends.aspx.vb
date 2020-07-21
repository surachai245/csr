Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Linq
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class InviteFriends
    Inherits System.Web.UI.Page
    Dim objProject As SysProject
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                objProject = Session("ProjestDetail")
                previewtitle3.InnerText() = objProject.ProjectName
                previewtitle3.Attributes.Add("style", "color:" + objProject.Tab1_FontColor + ";font-family:" + objProject.Tab1_FontName)
                previewbody3.Attributes.Add("style", "background-color:" + objProject.Tab4_BGColor)
                body.Attributes.Add("style", "background-color:" + objProject.Tab4_BGColor)
                previewdetail2.InnerText() = objProject.Tab4_TextMessage
                previewdetail2.Attributes.Add("style", "color:" + objProject.Tab4_TextFontColor + ";font-family:" + objProject.Tab4_TextFontName)
                ' previewimg3.Attributes.Add("src", "Document/temp/" & objProject.Tab4_FileName)

                Try
                    If (objProject.Tab4_FileName <> "" And objProject.MapDirectory <> "") Then
                        Dim confPath As String = ""
                        confPath = LHUtility.GetConfig("CSRPathDestination")
                        Dim desPath = confPath & "/" & objProject.MapDirectory & "/" & objProject.Tab4_FileName
                        Dim FileName = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
                        previewimg3.Attributes.Add("src", FileName)
                    End If
                Catch ex As Exception
                End Try

            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub btFriend_Click(sender As Object, e As EventArgs) Handles btFriend.Click
        Response.Redirect("InviteFriendsDetail.aspx")
    End Sub
    Protected Sub btClose_Click(sender As Object, e As EventArgs) Handles btClose.Click
        Page.ClientScript.RegisterOnSubmitStatement(Page.GetType, "closePage", "window.onunload = CloseWindow();")
        'Me.Page.ClientScript.RegisterStartupScript(Page.GetType, "CloseForm", "<script language ='javascript'>window.close();</script>")
        ' Response.Write("<script language='javascript'> { window.close(); }</script>")
    End Sub

End Class