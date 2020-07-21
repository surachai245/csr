
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
Public Class Home
    Inherits System.Web.UI.Page
    Dim culUS As CultureInfo = New CultureInfo("en-US")
    Dim objUser As UserProfile

    Private Property dtProject() As DataTable
        Get
            Dim o As Object = Session("dtProject")
            If (IsNothing(o)) Then
                Return Nothing
            Else
                Return o
            End If
        End Get
        Set(ByVal value As DataTable)
            Session("dtProject") = value
        End Set
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objUser = Session("UserProfileData")
        If Not IsPostBack Then
            loadData()
        End If
        'txtDateFr.Attributes.Add("onkeypress", "javascript:return allownumbers(event);")
        ClientScript.RegisterStartupScript(Me.GetType(), "CAll", "ActiveMenu('home');", True)
    End Sub

    Private Sub loadData()
        Dim ds As DataSet = New DataSet()
        Dim dt As DataTable = New DataTable()
        Dim _datefr As String = ""
        Dim _dateto As String = ""
        'Dim DTFr As DateTime
        'Dim DTTo As DateTime

        If txtDateFr.Text.Trim <> "" Then
            ' DTFr = DateTime.ParseExact(txtDateFr.Text, "dd/MM/yyyy", culUS, System.Globalization.DateTimeStyles.None)
            _datefr = EmployeeProfile.ScrnDateConvertDB(txtDateFr.Text)
        End If

        If txtDateTo.Text.Trim <> "" Then
            'DTTo = DateTime.ParseExact(txtDateTo.Text, "dd/MM/yyyy", culUS, System.Globalization.DateTimeStyles.None)
            _dateto = EmployeeProfile.ScrnDateConvertDB(txtDateTo.Text)
        End If

        Dim para As SqlParameterCollection = New SqlCommand().Parameters
        para.Add("@Id", SqlDbType.Int).Value = 0
        para.Add("@Name", SqlDbType.NVarChar).Value = txtProjectName.Text.Trim()
        para.Add("@DateFr", SqlDbType.NVarChar).Value = _datefr
        para.Add("@DateTo", SqlDbType.NVarChar).Value = _dateto
        para.Add("@ActiveFlag", SqlDbType.NVarChar).Value = rdActiveFlag.SelectedValue
        ds = SqlUtility.SqlExecuteSP("SPGetProject", para)

        If (ds.Tables.Count > 0) Then
            dt = ds.Tables(0)
            Me.dtProject = dt

            'เพิ่มเงื่อนไข Update โครงการที่หมดอายุเป็นไม่ใช้งาน 
            For index = 0 To dt.Rows.Count
                Try
                    If (dt(index)("DateFr").ToString() <> "" And dt(index)("DateTo").ToString() <> "") Then
                        Dim DTFr = Convert.ToDateTime(EmployeeProfile.ScrnDateDisplayen(dt(index)("DateFr"), "dd/MM/yyyy"))
                        Dim DTTo = Convert.ToDateTime(EmployeeProfile.ScrnDateDisplayen(dt(index)("DateTo"), "dd/MM/yyyy"))
                        Dim DateNow = Convert.ToDateTime(EmployeeProfile.ScrnDateDisplayen(DateTime.Now.Date, "dd/MM/yyyy"))
                        If (DateNow > DTTo.Date) Then
                            GetData.UpdateActive("0", dt(index)("ProjectId").ToString())
                        Else
                            GetData.UpdateActive("1", dt(index)("ProjectId").ToString())
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next

            GvProjectList.DataSource = dt
            GvProjectList.DataBind()
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        loadData()
    End Sub
    Protected Sub GvProjectList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvProjectList.PageIndexChanging
        If Me.dtProject IsNot Nothing Then
            GvProjectList.PageIndex = e.NewPageIndex
            GvProjectList.DataSource = Me.dtProject
            GvProjectList.DataBind()
        End If
    End Sub
    Private Sub GridView_Sorting(ByRef e As System.Web.UI.WebControls.GridViewSortEventArgs, ByRef GridView As System.Web.UI.WebControls.GridView, ByRef dt As DataTable)
        Try
            Dim dtCurr As DataTable = dt
            If dtCurr IsNot Nothing Then
                'Sort the data.
                dtCurr.DefaultView.Sort = e.SortExpression & " " & GetSortDirection(e.SortExpression)
                GvProjectList.DataSource = dtCurr
                Me.dtProject = dtCurr
                GvProjectList.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub GvProjectList_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GvProjectList.Sorting
        Try
            Dim dt = DirectCast(Me.dtProject, DataTable)
            GridView_Sorting(e, GvProjectList, dt)
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String
        ' By default, set the sort direction to ascending.
        Dim sortDirection = "DESC" 'ASC

        ' Retrieve the last column that was sorted.
        Dim sortExpression = TryCast(ViewState("SortExpression"), String)

        If sortExpression IsNot Nothing Then
            ' Check if the same column is being sorted.
            ' Otherwise, the default value can be returned.
            If sortExpression = column Then
                Dim lastDirection = TryCast(ViewState("SortDirection"), String)
                If lastDirection IsNot Nothing _
                  AndAlso lastDirection = "DESC" Then

                    sortDirection = "ASC"

                End If
            End If
        End If

        ' Save new values in ViewState.
        ViewState("SortDirection") = sortDirection
        ViewState("SortExpression") = column

        Return sortDirection

    End Function

    Protected Sub btInsert_Click(sender As Object, e As EventArgs) Handles btInsert.Click
        objUser.TitlePage = "ข้อมูลโครงการ"
        Response.Redirect("~/App_CSR/ProjectNew.aspx")
    End Sub
    Protected Sub btEdit_Click(sender As Object, e As EventArgs)
        Dim btEdit As Button = sender
        Dim _row As GridViewRow = btEdit.NamingContainer
        Dim lbProjectId As Label = _row.FindControl("lbProjectId")
        Response.Redirect("ProjectNew.aspx?ProjectId=" & lbProjectId.Text)

    End Sub
    Protected Sub btDelete_Click(sender As Object, e As EventArgs)
        Dim btEdit As Button = sender
        Dim _row As GridViewRow = btEdit.NamingContainer
        Dim lbProjectId As Label = _row.FindControl("lbProjectId")
        Dim plsuccess As Panel = _row.FindControl("plsuccess")
        Dim pldanger As Panel = _row.FindControl("pldanger")

        If (plsuccess.Visible = True) Then
            GetData.UpdateActive("0", lbProjectId.Text)
        Else
            GetData.UpdateActive("1", lbProjectId.Text)
        End If

        loadData()
        ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessageconfirm();", True)
    End Sub
    Protected Sub GvProjectList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvProjectList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = e.Row.DataItem
            Dim plsuccess As Panel = e.Row.FindControl("plsuccess")
            Dim pldanger As Panel = e.Row.FindControl("pldanger")
            Dim btDelete As Button = e.Row.FindControl("btDelete")

            'If (drv("ActiveFlag").ToString() = "1") Then
            '    plsuccess.Visible = True
            '    pldanger.Visible = False
            '    btDelete.Text = "ยกเลิก"
            '    btDelete.Attributes.Add("class", "btn btn-outline-danger btn-sm")
            'Else
            '    plsuccess.Visible = False
            '    pldanger.Visible = True
            '    btDelete.Text = "เปิดใช้งาน"
            '    btDelete.Attributes.Add("class", "btn btn-outline-success btn-sm")
            'End If

            e.Row.Cells(2).Text = EmployeeProfile.ScrnDateDisplayen(drv("DateFr"), "dd/MM/yyyy")
            e.Row.Cells(3).Text = EmployeeProfile.ScrnDateDisplayen(drv("DateTo"), "dd/MM/yyyy")
            If (drv("ActiveFlag").ToString() = "1") Then
                plsuccess.Visible = True
                pldanger.Visible = False
            Else
                plsuccess.Visible = False
                pldanger.Visible = True
            End If

            btDelete.Text = "ยกเลิก"
            btDelete.Attributes.Add("class", "btn btn-outline-danger btn-sm")
        End If
    End Sub

    Protected Sub btDelete1_Click(sender As Object, e As EventArgs) Handles btDelete1.Click
        If (hdfProjectId_Modal.Value <> "") Then
            'GetData.UpdateActive(hdfSuccess_Modal.Value, hdfProjectId_Modal.Value)
            GetData.DeleteActive(hdfProjectId_Modal.Value)
            loadData()
            hdfProjectId_Modal.Value = ""
        End If

        'ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessageconfirm();", True)
    End Sub

    Private Sub GvProjectList_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvProjectList.RowCommand
        If e.CommandName = "Select" Then
            Dim _row As GridViewRow = e.CommandSource.NamingContainer
            Dim lbProjectId As Label = _row.FindControl("lbProjectId")
            Dim plsuccess As Panel = _row.FindControl("plsuccess")
            Dim pldanger As Panel = _row.FindControl("pldanger")
            If (plsuccess.Visible = True) Then
                hdfSuccess_Modal.Value = "0"
            Else
                hdfSuccess_Modal.Value = "1"
            End If
            hdfProjectId_Modal.Value = lbProjectId.Text
            ClientScript.RegisterStartupScript(Me.GetType(), "modelBox", "openModal();", True)
        End If

    End Sub
End Class