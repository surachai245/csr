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

Public Class SearchData
    Inherits System.Web.UI.Page
    Dim objProject As SysProject
    Dim objEmployee As SysEmployee
    Private Property dvEmployee() As DataView
        Get
            Dim o As Object = Session("dvEmployee")
            If (IsNothing(o)) Then
                Return Nothing
            Else
                Return o
            End If
        End Get
        Set(ByVal value As DataView)
            Session("dvEmployee") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                objProject = Session("ProjestDetail")
                objEmployee = Session("EmployeeDetail")
                Dim SearchData = Request.QueryString("SearchData").ToString()
                If (SearchData <> "") Then
                    Dim dt = GetData.GetEmployee()
                    Dim dv As DataView = New DataView(dt)
                    dv.RowFilter = "Empcode like '%" & SearchData & "%' or Fullname like '%" & SearchData & "%' or Username like '%" & SearchData & "%' "
                    If (dv.Count > 0) Then
                        GvEmployeeList.DataSource = dv
                        Me.dvEmployee = dv
                        GvEmployeeList.DataBind()
                    Else
                        GvEmployeeList.EmptyDataText = "ไม่พบข้อมูล"
                        GvEmployeeList.DataSource = Nothing
                        GvEmployeeList.DataBind()
                    End If
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub

    Protected Sub btback_Click(sender As Object, e As EventArgs) Handles btback.Click
        Response.Redirect("InviteFriendsDetail.aspx")
    End Sub
    Protected Sub GvEmployeeList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvEmployeeList.RowCommand
        If e.CommandName = "Select" Then
            Dim commandName As String = e.CommandName
            Dim rowIndex As String = e.CommandArgument.ToString()
            Dim gvr As GridViewRow = GvEmployeeList.Rows(Convert.ToInt16(rowIndex))
            Dim Empcode = gvr.Cells(1).Text.Trim
            Dim Fullname = gvr.Cells(2).Text
            Response.Redirect("InviteFriendsDetail.aspx?Empcode=" & Empcode & "&Fullname=" & Fullname)
        End If
    End Sub
    Protected Sub GvEmployeeList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvEmployeeList.PageIndexChanging
        If Me.dvEmployee IsNot Nothing Then
            GvEmployeeList.PageIndex = e.NewPageIndex
            GvEmployeeList.DataSource = Me.dvEmployee
            GvEmployeeList.DataBind()
        End If
    End Sub
End Class