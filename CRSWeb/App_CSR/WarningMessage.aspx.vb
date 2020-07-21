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

Public Class WarningMessage
    Inherits System.Web.UI.Page
    Dim objProject As SysProject
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                objProject = Session("ProjestDetail")
                previewbody5.Attributes.Add("style", "background-color:" + objProject.Tab6_BGColor)
                body.Attributes.Add("style", "background-color:" + objProject.Tab6_BGColor)
                divTxt.InnerHtml = objProject.Tab6_TextMessage
                divTxt.Attributes.Add("style", "color:" + objProject.Tab6_TextFontColor + ";font-family:" + objProject.Tab6_TextFontName)
            Catch ex As Exception

            End Try
        End If
    End Sub

End Class