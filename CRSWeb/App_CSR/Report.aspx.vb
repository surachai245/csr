Imports System.Web.Configuration
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports System.Net
Imports Image2 = System.Drawing.Image

Public Class Report
    Inherits System.Web.UI.Page
    Dim culUS As CultureInfo = New CultureInfo("en-US")
    Dim objUser As UserProfile
    Private ucDialog1 As Object
    Private objLog As Log
    Public Property LHEnum As Object

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

    Public Property CrystalReportViewer1 As Object
    Public Property CrystalDecisions As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Me).RegisterPostBackControl(Excel)
        objUser = Session("UserProfileData")
        If Not IsPostBack Then
            'loadData()
            Try
                Dim dt As DataTable = GetData.GetTBProjectReport()
                If (dt.Rows.Count > 0) Then
                    ddlProject.DataSource = dt
                    ddlProject.DataTextField = "ProjectName"
                    ddlProject.DataValueField = "ProjectId"
                    ddlProject.DataBind()
                    ddlProject.Items.Insert(0, New ListItem("", ""))
                End If
            Catch ex As Exception

            End Try
        End If
        ClientScript.RegisterStartupScript(Me.GetType(), "CAll", "ActiveMenu('report');", True)
    End Sub
    Private Sub loadData()
        Dim dt As DataTable
        Dim dateS As String = ""
        Dim dateE As String = ""
        If txtDateFr.Text <> "" Then
            Dim start As Date = Convert.ToDateTime(txtDateFr.Text)
            dateS = start.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        End If
        If txtDateTo.Text <> "" Then
            Dim DEnd As Date = Convert.ToDateTime(txtDateTo.Text)
            dateE = DEnd.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        End If
        dt = GetData.GetReportData(ddlProject.SelectedValue, dateS, dateE)
        If dt.Rows.Count > 0 Then
            Me.dtProject = dt
            GvProjectList.DataSource = dt
            GvProjectList.DataBind()
        Else
            GvProjectList.DataSource = dt
            GvProjectList.DataBind()
            ClientScript.RegisterStartupScript(Me.GetType(), "NoData", "alertMessage('error','','ไม่พบข้อมูล');", True)
        End If

    End Sub
    Protected Sub btsearch_Click(sender As Object, e As EventArgs) Handles btsearch.Click
        loadData()
    End Sub
    Protected Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        If (GvProjectList.Rows.Count > 0) Then
            loadData()
            GvProjectList.AllowPaging = False
            GvProjectList.DataBind()
            ExportToExcel(GvProjectList)
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "NoData", "alertMessage('error','','กรุณาเลือกข้อมูล');", True)
        End If
    End Sub
    Protected Sub GvProjectList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GvProjectList.PageIndexChanging
        If Me.dtProject IsNot Nothing Then
            GvProjectList.PageIndex = e.NewPageIndex
            GvProjectList.DataSource = Me.dtProject
            GvProjectList.DataBind()
        End If
    End Sub
    Protected Sub GvProjectList_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GvProjectList.Sorting
        Try
            Dim dt = DirectCast(Me.dtProject, DataTable)
            GridView_Sorting(e, GvProjectList, dt)
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
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

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

    Protected Sub ExportToExcel(ByVal gv As GridView)
        Try
            If gv.Rows.Count > 0 Then
                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("content-disposition", "attachment;filename=ReportData" + Convert.ToDateTime(Now).ToString("ddMMyyHH") + ".xls")
                Response.Charset = ""
                Response.ContentType = "application/vnd.ms-excel"
                Using sw As New StringWriter()
                    Dim hw As New HtmlTextWriter(sw)
                    gv.AllowPaging = False
                    gv.Columns(0).Visible = False
                    gv.Columns(9).Visible = False
                    For Each cell As TableCell In gv.HeaderRow.Cells
                        cell.BackColor = gv.HeaderStyle.BackColor
                    Next
                    For Each row As GridViewRow In gv.Rows
                        For Each cell As TableCell In row.Cells
                            If row.RowIndex Mod 2 = 0 Then
                                cell.BackColor = gv.AlternatingRowStyle.BackColor
                            Else
                                cell.BackColor = gv.RowStyle.BackColor
                            End If
                            cell.CssClass = "textmode"
                        Next
                    Next
                    gv.RenderControl(hw)
                    'style to format numbers to string
                    Dim style As String = "<style> .textmode { } </style>"
                    Response.Write(style)
                    Response.Output.Write(sw.ToString())
                    Response.Flush()
                    'Response.End()
                    Response.SuppressContent = True
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End Using
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "NoData", "alertMessage('error','','ไม่พบข้อมูลExport');", True)
            End If
        Catch ex As Exception
            objLog.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        End Try

    End Sub
    Protected Sub GvProjectList_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvProjectList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim drv As DataRowView = e.Row.DataItem
            Dim lbPhoto As Image = e.Row.FindControl("lbPhoto")
            Dim Pic As String = WebConfigurationManager.AppSettings("Picture")
            Dim tClient As WebClient = New WebClient
            Dim url = Pic & drv("PictureURL").ToString
            Dim tImage As Image2 = Image2.FromStream(New MemoryStream(tClient.DownloadData(url)))
            Dim bArr As Byte() = imgToByteArray(tImage)
            lbPhoto.ImageUrl = "data:image;base64," + Convert.ToBase64String(bArr)
        End If
    End Sub
    Public Function imgToByteArray(ByVal img As Image2) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function
    Function GenPDFs() As String
        Dim msg = ""
        Try
            Dim rpt As New ReportDocument()
            Dim ds As New DataSet()
            Dim sourceName = "ReportProjectname"
            Dim sQuery As String = String.Empty
            Dim fullPath = Server.MapPath("~/Report/" & sourceName & ".rpt")
            Dim pdfPath = Server.MapPath("~/TempUpload/" & sourceName & ".pdf")
            Dim dt As DataTable = Me.dtProject
            If dt.Rows.Count > 0 Then
                Dim docID = Guid.NewGuid().ToString()
                rpt.Load(Server.MapPath("~/Report/" & sourceName & ".rpt"))
                rpt.SetDataSource(dt)
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath)
                rpt.Dispose()
                rpt.Close()
                Dim tmpFile = CreateFolderTemp() + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(pdfPath)
                File.Copy(pdfPath, tmpFile)
                Session("pdfPath") = tmpFile
                ScriptManager.RegisterStartupScript(Me.Page, GetType(Page), "", "<script type='text/javascript'>popup('" + ResolveUrl("~/App_CSR/ViewPDF.aspx") + "', 'Report', screen.width, screen.height);</script>", False)
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "NoData", "alertMessage('error','','ไม่พบข้อมูลรายงาน');", True)
            End If
        Catch ex As Exception
            objLog.WriteLog(AbstractLog.Type.LogError, ex.ToString())
        End Try
        Return msg
    End Function
    Function CreateFolderTemp() As String
        Dim tmpFolder As String = Server.MapPath("\\TempUpload\\")
        If Not System.IO.Directory.Exists(tmpFolder) Then
            System.IO.Directory.CreateDirectory(tmpFolder)
        End If
        Return tmpFolder
    End Function

    Protected Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        If (GvProjectList.Rows.Count > 0) Then
            GenPDFs()
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "NoData", "alertMessage('error','','กรุณาเลือกข้อมูล');", True)
        End If
    End Sub
End Class