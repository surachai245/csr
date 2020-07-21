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

Public Class AdminList
    Inherits System.Web.UI.Page
    Dim culUS As CultureInfo = New CultureInfo("en-US")
    Dim objUser As UserProfile

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objUser = Session("UserProfileData")
        If Not IsPostBack Then
            loadData("")
        End If
        ClientScript.RegisterStartupScript(Me.GetType(), "CAll", "ActiveMenu('admin');", True)
    End Sub

    Private Sub loadData(Shrech As String)
        Dim SelectUserAdmin As DataTable = GetData.GetTBUserAdmin(Shrech, "")
        If (SelectUserAdmin IsNot Nothing) Then
            GvAdmin.DataSource = SelectUserAdmin
            GvAdmin.DataBind()
        Else
            GvAdmin.DataSource = Nothing
            GvAdmin.DataBind()
        End If
    End Sub
    Protected Sub btInsert_Click(sender As Object, e As EventArgs) Handles btInsert.Click
        objUser.TitlePage = "เพิ่ม/แก้ไข ข้อมูล Admin"
        Response.Redirect("~/App_CSR/AdminDetail.aspx")
    End Sub
    Protected Sub btSharch_Click(sender As Object, e As EventArgs) Handles btSharch.Click
        loadData(tbSearch.Text)
    End Sub
    Protected Sub btEdit_Click(sender As Object, e As EventArgs)
        Dim btEdit As Button = sender
        Dim _row As GridViewRow = btEdit.NamingContainer
        Dim lbUserId As Label = _row.FindControl("lbUserId")
        Response.Redirect("AdminDetail.aspx?UserId=" & lbUserId.Text)
    End Sub
    Protected Sub btDelete_Click(sender As Object, e As EventArgs)
        Dim btEdit As Button = sender
        Dim _row As GridViewRow = btEdit.NamingContainer
        Dim lbUserId As Label = _row.FindControl("lbUserId")
        Dim plsuccess As Panel = _row.FindControl("plsuccess")
        Dim pldanger As Panel = _row.FindControl("pldanger")

        If (plsuccess.Visible = True) Then
            GetData.DeleteUserAdmin(lbUserId.Text)
        Else
            GetData.DeleteUserAdmin(lbUserId.Text)
        End If
        loadData(tbSearch.Text)
        ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('success','','ลบข้อมูลเรียบร้อย');", True)
    End Sub

    Protected Sub GvAdmin_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvAdmin.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = e.Row.DataItem
            Dim plsuccess As Panel = e.Row.FindControl("plsuccess")
            Dim pldanger As Panel = e.Row.FindControl("pldanger")
            Dim btDelete As Button = e.Row.FindControl("btDelete")

            If (drv("IsAdmin").ToString() = "1") Then
                plsuccess.Visible = True
                pldanger.Visible = False
                'btDelete.Text = "ยกเลิก"
                'btDelete.Attributes.Add("class", "btn btn-outline-danger btn-sm")
            Else
                plsuccess.Visible = False
                pldanger.Visible = True
                'btDelete.Text = "เปิดใช้งาน"
                'btDelete.Attributes.Add("class", "btn btn-outline-success btn-sm")
            End If
            btDelete.Text = "ยกเลิก"
            btDelete.Attributes.Add("class", "btn btn-outline-danger btn-sm")

        End If
    End Sub
End Class