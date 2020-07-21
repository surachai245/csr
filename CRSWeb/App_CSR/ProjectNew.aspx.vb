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
Imports System.IO
Imports System.Web.Configuration
Imports System.Drawing
Imports System.Drawing.Image
Imports Image = System.Drawing.Image

Public Class ProjectNew
    Inherits System.Web.UI.Page
    Dim culUS As CultureInfo = New CultureInfo("en-US")
    Dim objUser As UserProfile

    Public Property SelectedEmptyLocation() As DataTable
        Get
            Dim o As Object = ViewState("_SelectedEmptyLocation")
            If (IsNothing(o)) Then
                Return Nothing
            Else
                Return o
            End If
        End Get
        Set(ByVal value As DataTable)
            ViewState("_SelectedEmptyLocation") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        objUser = Session("UserProfileData")
        If Not IsPostBack Then
            Dim ProjectId As String = ""
            Try
                ProjectId = Request.QueryString("ProjectId").ToString()
            Catch ex As Exception

            End Try
            If (ProjectId <> "") Then
                'btInsert.Text = "Update"
                SelectData(ProjectId)
            End If
            SelectTab()
            tbURL.ToolTip = "ระบบจะดำเนินการ Gen URL ให้เมื่อกรอกข้อมูลครบถ้วน"
        End If
        ClientScript.RegisterStartupScript(Me.GetType(), "CAll", "ActiveMenu('home');", True)
        FileUpload1.Attributes.Add("OnChange", "javascript:__doPostBack('" & btTmp.UniqueID & "','')")
        FileUpload3.Attributes.Add("OnChange", "javascript:__doPostBack('" & btTmp2.UniqueID & "','')")
        FileUpload4.Attributes.Add("OnChange", "javascript:__doPostBack('" & btTmp3.UniqueID & "','')")
    End Sub

    Sub SelectTab()
        'Tab
        Dim SelectTeb As DataTable = GetData.GetTBCodeCtrl()
        If (SelectTeb.Rows.Count > 0) Then
            lbTab1.Text = SelectTeb(0)("CodeDesc")
            lbTab2.Text = SelectTeb(1)("CodeDesc")
            lbTab3.Text = SelectTeb(2)("CodeDesc")
            lbTab4.Text = SelectTeb(3)("CodeDesc")
            lbTab5.Text = SelectTeb(4)("CodeDesc")
            lbTab6.Text = SelectTeb(5)("CodeDesc")
            hfIdTab1.Value = SelectTeb(0)("CodeCtrlId")
            hfIdTab2.Value = SelectTeb(1)("CodeCtrlId")
            hfIdTab3.Value = SelectTeb(2)("CodeCtrlId")
            hfIdTab4.Value = SelectTeb(3)("CodeCtrlId")
            hfIdTab5.Value = SelectTeb(4)("CodeCtrlId")
            hfIdTab6.Value = SelectTeb(5)("CodeCtrlId")
            hfIdTab7.Value = SelectTeb(5)("CodeCtrlId")

        End If
    End Sub
    Sub SelectData(ByVal ProjectId As String)
        Dim ds As DataSet = New DataSet()
        Dim para As SqlParameterCollection = New SqlCommand().Parameters
        para.Add("@Id", SqlDbType.Int).Value = ProjectId
        para.Add("@Name", SqlDbType.NVarChar).Value = ""
        para.Add("@DateFr", SqlDbType.NVarChar).Value = ""
        para.Add("@DateTo", SqlDbType.NVarChar).Value = ""
        para.Add("@ActiveFlag", SqlDbType.NVarChar).Value = ""

        ds = SqlUtility.SqlExecuteSP("SPGetProject", para)
        Dim dv As New DataView(ds.Tables(0))
        If (dv.Count > 0) Then
            'Main Project 
            hfProjectId.Value = dv(0)("ProjectId").ToString()
            tbProjectName_tab1.Text = dv(0)("ProjectName").ToString()
            tbDateFr_tab1.Text = If(dv(0)("DateFr").ToString() <> "", EmployeeProfile.ScrnDateDisplayen(dv(0)("DateFr").ToString(), "dd/MM/yyyy"), "")
            tbDateTo_tab1.Text = If(dv(0)("DateTo").ToString() <> "", EmployeeProfile.ScrnDateDisplayen(dv(0)("DateTo").ToString(), "dd/MM/yyyy"), "")
            tbSubjectMail_tab1.Text = dv(0)("SubjectDetail").ToString()
            tbURL.Text = dv(0)("URL").ToString()
            rdActiveFlag_tab1.SelectedValue = dv(0)("ActiveFlag").ToString()

            'Tab 1 
            Dim SelectTeb As DataTable = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "1")
            Dim dvtab As New DataView(SelectTeb)
            If (dvtab.Count > 0) Then
                tbFontName_tab1.Text = dvtab(0)("FontName").ToString()
                tbColor_tab1.Text = dvtab(0)("FontColor").ToString()
                tbBackgroud_tab1.Text = dvtab(0)("BGColor").ToString()
                lbPicture.Text = dvtab(0)("Picture").ToString()
                hfFileName.Value = dvtab(0)("FileName").ToString()
                Dim MapDirectory As String = dvtab(0)("MapDirectory").ToString()
                Try
                    If (hfFileName.Value <> "" And MapDirectory <> "") Then
                        Dim confPath As String = ""
                        confPath = LHUtility.GetConfig("CSRPathDestination")
                        Dim desPath = confPath & "/" & MapDirectory & "/" & hfFileName.Value
                        hfFileName.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
                    End If
                Catch ex As Exception
                    hfFileName.Value = dvtab(0)("FileName").ToString()
                End Try

            End If

            'Tab 2
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "2")
            Dim dvtab2 As New DataView(SelectTeb)
            If (dvtab2.Count > 0) Then
                tbBackgroud_tab2.Text = dvtab2(0)("BGColor").ToString()
                tbFontColor_tab2.Text = dvtab2(0)("FontColor").ToString()
            End If

            'Tab 3
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "3")
            Dim dvtab3 As New DataView(SelectTeb)
            If (dvtab3.Count > 0) Then
                tbBackgroud_tab3.Text = dvtab3(0)("BGColor").ToString()
                tbFontColor_tab3.Text = dvtab3(0)("FontColor").ToString()
                tbTextMessage_tab3.Text = dvtab3(0)("TextMessage").ToString()
                tbTextFontName_tab3.Text = dvtab3(0)("TextFontName").ToString()
                tbTextFontColor_tab3.Text = dvtab3(0)("TextFontColor").ToString()
                lbPicture3.Text = dvtab3(0)("Picture").ToString()
                hfFileName3.Value = dvtab3(0)("FileName").ToString()
                Try
                    Dim MapDirectory As String = dvtab3(0)("MapDirectory").ToString()
                    If (hfFileName3.Value <> "" And MapDirectory <> "") Then
                        Dim confPath As String = ""
                        confPath = LHUtility.GetConfig("CSRPathDestination")
                        Dim desPath = confPath & "/" & MapDirectory & "/" & hfFileName3.Value
                        hfFileName3.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
                    End If
                Catch ex As Exception
                    hfFileName3.Value = dvtab3(0)("FileName").ToString()
                End Try

            End If

            'Tab 4
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "4")
            Dim dvtab4 As New DataView(SelectTeb)
            If (dvtab4.Count > 0) Then
                tbBackgroud_tab4.Text = dvtab4(0)("BGColor").ToString()
                tbTextMessage_tab4.Text = dvtab4(0)("TextMessage").ToString()
                tbTextFontName_tab4.Text = dvtab4(0)("TextFontName").ToString()
                tbTextFontColor_tab4.Text = dvtab4(0)("TextFontColor").ToString()
                lbPicture4.Text = dvtab4(0)("Picture").ToString()
                hfFileName4.Value = dvtab4(0)("FileName").ToString()
                Try
                    Dim MapDirectory As String = dvtab4(0)("MapDirectory").ToString()
                    If (hfFileName4.Value <> "" And MapDirectory <> "") Then
                        Dim confPath As String = ""
                        confPath = LHUtility.GetConfig("CSRPathDestination")
                        Dim desPath = confPath & "/" & MapDirectory & "/" & hfFileName4.Value
                        hfFileName4.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
                    End If
                Catch ex As Exception
                    hfFileName4.Value = dvtab4(0)("FileName").ToString()
                End Try

            End If

            'Tab 5
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "5")
            Dim dvtab5 As New DataView(SelectTeb)
            If (dvtab5.Count > 0) Then
                tbBackgroud_tab5.Text = dvtab5(0)("BGColor").ToString()
                tbFontColor_tab5.Text = dvtab5(0)("FontColor").ToString()
            End If

            'Tab 4
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "6")
            Dim dvtab6 As New DataView(SelectTeb)
            If (dvtab6.Count > 0) Then
                tbBackgroud_tab6.Text = dvtab6(0)("BGColor").ToString()
                tbTextMessage_tab6.Text = dvtab6(0)("TextMessage").ToString()
                tbTextFontName_tab6.Text = dvtab6(0)("TextFontName").ToString()
                tbFontColor_tab6.Text = dvtab6(0)("TextFontColor").ToString()
                tbTextMessage_tab6.Font.Size = 36
            End If

        End If
    End Sub

    Public Function imgToByteArray(ByVal img As Image) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function

    Protected Sub btInsert_Click(sender As Object, e As EventArgs) Handles btInsert.Click

        If (Check()) Then
            'Add Data
            Dim obj As New SysProject
            obj.ProjectId = hfProjectId.Value
            obj.ProjectName = tbProjectName_tab1.Text.Trim()
            obj.DateFr = EmployeeProfile.ScrnDateConvertDB(tbDateFr_tab1.Text)
            obj.DateTo = EmployeeProfile.ScrnDateConvertDB(tbDateTo_tab1.Text)
            obj.SubjectDetail = tbSubjectMail_tab1.Text.Trim()
            obj.ActiveFlag = rdActiveFlag_tab1.SelectedValue
            obj.Tab1_URL = ""
            obj.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            obj.CreateBy = objUser.UserId

            'Tab1 
            obj.Tab1_CodeCtrlId = hfIdTab1.Value
            obj.Tab1_FontName = tbFontName_tab1.Text.Trim()
            obj.Tab1_FontColor = tbColor_tab1.Text.Trim()
            obj.Tab1_BGColor = tbBackgroud_tab1.Text.Trim()
            obj.Tab1_Picture = If(lbPicture.Text <> "", lbPicture.Text, "")
            obj.Tab1_FileName = If(hfFileName.Value <> "", hfFileName.Value, "")
            obj.MapDirectory = DateTime.Now.ToString("yyyy/MM")

            'Tab2
            obj.Tab2_CodeCtrlId = hfIdTab2.Value
            obj.Tab2_BGColor = tbBackgroud_tab2.Text
            obj.Tab2_FontColor = tbFontColor_tab2.Text

            'Tab3
            obj.Tab3_CodeCtrlId = hfIdTab3.Value
            obj.Tab3_BGColor = tbBackgroud_tab3.Text
            obj.Tab3_FontColor = tbFontColor_tab3.Text
            obj.Tab3_TextMessage = tbTextMessage_tab3.Text.Trim()
            obj.Tab3_TextFontName = tbTextFontName_tab3.Text
            obj.Tab3_TextFontColor = tbTextFontColor_tab3.Text
            obj.Tab3_Picture = If(lbPicture3.Text <> "", lbPicture3.Text, "")
            obj.Tab3_FileName = If(hfFileName3.Value <> "", hfFileName3.Value, "")

            'Tab4
            obj.Tab4_CodeCtrlId = hfIdTab4.Value
            obj.Tab4_BGColor = tbBackgroud_tab4.Text
            obj.Tab4_TextMessage = tbTextMessage_tab4.Text.Trim()
            obj.Tab4_TextFontName = tbTextFontName_tab4.Text
            obj.Tab4_TextFontColor = tbTextFontColor_tab4.Text
            obj.Tab4_Picture = If(lbPicture4.Text <> "", lbPicture4.Text, "")
            obj.Tab4_FileName = If(hfFileName4.Value <> "", hfFileName4.Value, "")

            'Tab 5 
            obj.Tab5_CodeCtrlId = hfIdTab5.Value
            obj.Tab5_BGColor = tbBackgroud_tab5.Text
            obj.Tab5_FontColor = tbFontColor_tab5.Text

            'tab6 
            obj.Tab6_CodeCtrlId = hfIdTab6.Value
            obj.Tab6_BGColor = tbBackgroud_tab6.Text
            obj.Tab6_TextMessage = tbTextMessage_tab6.Text.Trim()
            obj.Tab6_TextFontName = tbTextFontName_tab6.Text
            obj.Tab6_TextFontColor = tbFontColor_tab6.Text

            'Insert / Update Project 
            Dim SelectTeb As DataTable = GetData.GetTBProject(obj.ProjectId)
            If SelectTeb IsNot Nothing Then
                If SelectTeb.Rows.Count > 0 Then
                    UpdateProject(obj)
                Else
                    InsertProject(obj)
                End If
            Else
                InsertProject(obj)
            End If

            'URL
            If (CheckGenURL()) Then
                Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
                If url.IndexOf("ProjectId") > 0 Then
                    url = url.Replace("ProjectNew.aspx", "CenterPage.aspx")
                Else
                    url = url.Replace("ProjectNew.aspx", "CenterPage.aspx?ProjectId=" & hfProjectId.Value)
                End If
                'Dim path As String = HttpContext.Current.Request.Url.AbsolutePath
                'Dim host As String = HttpContext.Current.Request.Url.Host
                obj.Tab1_URL = url
                Dim objSql = New SqlHelper()
                objSql.NewUpdateStatement("ProjectList")
                objSql.Where("ProjectId = @ProjectId")
                objSql.WhereParam("ProjectId", obj.ProjectId)
                objSql.SetColumnValue("URL", obj.Tab1_URL)
                objSql.Execute()
                tbURL.Text = obj.Tab1_URL
            Else
                obj.Tab1_URL = ""
            End If

            'btDelete.Attributes.Add("class", "nav-link ui-tabs-anchor active")
            ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage();", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('error','','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
            ' btInsert.Attributes.Add("onCilck", "javascript:Swal.fire('The Internet?','That thing is still around?','question')")
        End If


    End Sub

    Sub InsertProject(ByVal obj As SysProject)
        Dim objSql As New SqlHelper()

        objSql.NewInsertIdentityStatement("ProjectList") 'NewInsertStatement
        objSql.SetColumnValue("ProjectName", obj.ProjectName)
        If obj.DateFr <> "" Then
            objSql.SetColumnValue("DateFr", obj.DateFr)
        Else
            objSql.SetColumnValue("DateFr", DBNull.Value)
        End If
        If obj.DateTo <> "" Then
            objSql.SetColumnValue("DateTo", obj.DateTo)
        Else
            objSql.SetColumnValue("DateTo", DBNull.Value)
        End If
        objSql.SetColumnValue("SubjectDetail", obj.SubjectDetail)
        objSql.SetColumnValue("ActiveFlag", obj.ActiveFlag)
        objSql.SetColumnValue("URL", "")
        objSql.SetColumnValue("CreateDate", obj.CreateDate)
        objSql.SetColumnValue("CreateBy", obj.CreateBy)
        objSql.SetColumnValue("UpdateDate", obj.CreateDate)
        objSql.SetColumnValue("UpdateBy", obj.CreateBy)
        Dim ProjectId = CInt(objSql.Execute())
        hfProjectId.Value = ProjectId

        'Tab 1
        objSql = New SqlHelper()
        objSql.NewInsertStatement("ProjectStyle")
        objSql.SetColumnValue("ProjectId", ProjectId)
        objSql.SetColumnValue("CodeCtrlId", obj.Tab1_CodeCtrlId)
        objSql.SetColumnValue("FontName", obj.Tab1_FontName)
        objSql.SetColumnValue("FontColor", obj.Tab1_FontColor)
        objSql.SetColumnValue("BGColor", obj.Tab1_BGColor)

        Dim UpFile = UploadFile(obj.MapDirectory, obj.Tab1_FileName)
        If (UpFile) Then
            objSql.SetColumnValue("Picture", obj.Tab1_Picture)
            objSql.SetColumnValue("FileName", obj.Tab1_FileName)
            objSql.SetColumnValue("MapDirectory", obj.MapDirectory)
            Try
                Dim confPath As String = ""
                confPath = LHUtility.GetConfig("CSRPathDestination")
                hfFileName.Value = obj.Tab1_FileName
                Dim desPath = confPath & "/" & obj.MapDirectory & "/" & hfFileName.Value
                hfFileName.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
            Catch ex As Exception

            End Try
        End If
        objSql.Execute()

        'Tab2
        objSql = New SqlHelper()
        objSql.NewInsertStatement("ProjectStyle")
        objSql.SetColumnValue("ProjectId", ProjectId)
        objSql.SetColumnValue("CodeCtrlId", obj.Tab2_CodeCtrlId)
        objSql.SetColumnValue("FontColor", obj.Tab2_FontColor)
        objSql.SetColumnValue("BGColor", obj.Tab2_BGColor)
        objSql.Execute()

        'Tab3
        objSql = New SqlHelper()
        objSql.NewInsertStatement("ProjectStyle")
        objSql.SetColumnValue("ProjectId", ProjectId)
        objSql.SetColumnValue("CodeCtrlId", obj.Tab3_CodeCtrlId)
        objSql.SetColumnValue("BGColor", obj.Tab3_BGColor)
        objSql.SetColumnValue("FontColor", obj.Tab3_FontColor)
        objSql.SetColumnValue("TextMessage", obj.Tab3_TextMessage)
        objSql.SetColumnValue("TextFontName", obj.Tab3_TextFontName)
        objSql.SetColumnValue("TextFontColor", obj.Tab3_TextFontColor)

        Dim UpFile2 = UploadFile(obj.MapDirectory, obj.Tab3_FileName)
        If (UpFile2) Then
            objSql.SetColumnValue("Picture", obj.Tab3_Picture)
            objSql.SetColumnValue("FileName", obj.Tab3_FileName)
            objSql.SetColumnValue("MapDirectory", obj.MapDirectory)
            Try
                Dim confPath As String = ""
                confPath = LHUtility.GetConfig("CSRPathDestination")
                hfFileName3.Value = obj.Tab3_FileName
                Dim desPath = confPath & "/" & obj.MapDirectory & "/" & hfFileName3.Value
                hfFileName3.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
            Catch ex As Exception

            End Try
        End If

        objSql.Execute()

        'Tab4
        objSql = New SqlHelper()
        objSql.NewInsertStatement("ProjectStyle")
        objSql.SetColumnValue("ProjectId", ProjectId)
        objSql.SetColumnValue("CodeCtrlId", obj.Tab4_CodeCtrlId)
        objSql.SetColumnValue("BGColor", obj.Tab4_BGColor)
        objSql.SetColumnValue("TextMessage", obj.Tab4_TextMessage)
        objSql.SetColumnValue("TextFontName", obj.Tab4_TextFontName)
        objSql.SetColumnValue("TextFontColor", obj.Tab4_TextFontColor)
        Dim UpFile3 = UploadFile(obj.MapDirectory, obj.Tab4_FileName)
        If (UpFile3) Then
            objSql.SetColumnValue("Picture", obj.Tab4_Picture)
            objSql.SetColumnValue("FileName", obj.Tab4_FileName)
            objSql.SetColumnValue("MapDirectory", obj.MapDirectory)
            Try
                Dim confPath As String = ""
                confPath = LHUtility.GetConfig("CSRPathDestination")
                hfFileName4.Value = obj.Tab4_FileName
                Dim desPath = confPath & "/" & obj.MapDirectory & "/" & hfFileName4.Value
                hfFileName4.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
            Catch ex As Exception

            End Try
        End If
        objSql.Execute()

        'Tab5
        objSql = New SqlHelper()
        objSql.NewInsertStatement("ProjectStyle")
        objSql.SetColumnValue("ProjectId", ProjectId)
        objSql.SetColumnValue("CodeCtrlId", obj.Tab5_CodeCtrlId)
        objSql.SetColumnValue("FontColor", obj.Tab5_FontColor)
        objSql.SetColumnValue("BGColor", obj.Tab5_BGColor)
        objSql.Execute()

        'Tab6
        objSql = New SqlHelper()
        objSql.NewInsertStatement("ProjectStyle")
        objSql.SetColumnValue("ProjectId", ProjectId)
        objSql.SetColumnValue("CodeCtrlId", obj.Tab6_CodeCtrlId)
        objSql.SetColumnValue("BGColor", obj.Tab6_BGColor)
        objSql.SetColumnValue("TextMessage", obj.Tab6_TextMessage)
        objSql.SetColumnValue("TextFontName", obj.Tab6_TextFontName)
        objSql.SetColumnValue("TextFontColor", obj.Tab6_TextFontColor)
        objSql.Execute()

    End Sub

    Private Function UploadFile(MapDirectory As String, SourceFileName As String) As Boolean
        Try
            Dim desPath As String = ""
            desPath = LHUtility.GetConfig("CSRPathDestination") & "/" & MapDirectory

            '''''Copy File To Destination Path
            If desPath <> "" Then
                Dim desFile As String = desPath & "/" & SourceFileName
                Dim srcFile As String = Server.MapPath("Document/temp/" & SourceFileName)
                If Not Directory.Exists(desPath) Then
                    Directory.CreateDirectory(desPath)
                End If
                If Not File.Exists(desFile) Then
                    'File.Copy(srcFile, desFile, True)
                    File.Move(srcFile, desFile)
                End If
            End If
            Return True
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Sub UpdateProject(ByVal obj As SysProject)
        Dim objSql As New SqlHelper()

        objSql = New SqlHelper()
        objSql.NewUpdateStatement("ProjectList")
        objSql.Where("ProjectId = @ProjectId")
        objSql.WhereParam("ProjectId", obj.ProjectId)
        objSql.SetColumnValue("ProjectName", obj.ProjectName)
        If obj.DateFr <> "" Then
            objSql.SetColumnValue("DateFr", obj.DateFr)
        Else
            objSql.SetColumnValue("DateFr", DBNull.Value)
        End If
        If obj.DateTo <> "" Then
            objSql.SetColumnValue("DateTo", obj.DateTo)
        Else
            objSql.SetColumnValue("DateTo", DBNull.Value)
        End If
        objSql.SetColumnValue("SubjectDetail", obj.SubjectDetail)
        objSql.SetColumnValue("ActiveFlag", obj.ActiveFlag)
        objSql.SetColumnValue("URL", obj.Tab1_URL)
        objSql.SetColumnValue("UpdateDate", obj.CreateDate)
        objSql.SetColumnValue("UpdateBy", obj.CreateBy)
        objSql.Execute()

        'Tab1
        objSql = New SqlHelper()
        objSql.NewUpdateStatement("ProjectStyle")
        objSql.Where("ProjectId = @ProjectId and CodeCtrlId = @CodeCtrlId")
        objSql.WhereParam("ProjectId", obj.ProjectId)
        objSql.WhereParam("CodeCtrlId", obj.Tab1_CodeCtrlId)
        objSql.SetColumnValue("FontName", obj.Tab1_FontName)
        objSql.SetColumnValue("FontColor", obj.Tab1_FontColor)
        objSql.SetColumnValue("BGColor", obj.Tab1_BGColor)

        Dim UpFile = UploadFile(obj.MapDirectory, obj.Tab1_FileName)
        If (UpFile) Then
            objSql.SetColumnValue("Picture", obj.Tab1_Picture)
            objSql.SetColumnValue("FileName", obj.Tab1_FileName)
            objSql.SetColumnValue("MapDirectory", obj.MapDirectory)
            Try
                Dim confPath As String = ""
                confPath = LHUtility.GetConfig("CSRPathDestination")
                hfFileName.Value = obj.Tab1_FileName
                Dim desPath = confPath & "/" & obj.MapDirectory & "/" & hfFileName.Value
                hfFileName.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
            Catch ex As Exception

            End Try
        End If

        objSql.Execute()

        'Tab2
        objSql = New SqlHelper()
        objSql.NewUpdateStatement("ProjectStyle")
        objSql.Where("ProjectId = @ProjectId and CodeCtrlId = @CodeCtrlId")
        objSql.WhereParam("ProjectId", obj.ProjectId)
        objSql.WhereParam("CodeCtrlId", obj.Tab2_CodeCtrlId)
        objSql.SetColumnValue("FontColor", obj.Tab2_FontColor)
        objSql.SetColumnValue("BGColor", obj.Tab2_BGColor)
        objSql.Execute()

        'Tab3
        objSql = New SqlHelper()
        objSql.NewUpdateStatement("ProjectStyle")
        objSql.Where("ProjectId = @ProjectId and CodeCtrlId = @CodeCtrlId")
        objSql.WhereParam("ProjectId", obj.ProjectId)
        objSql.WhereParam("CodeCtrlId", obj.Tab3_CodeCtrlId)
        objSql.SetColumnValue("BGColor", obj.Tab3_BGColor)
        objSql.SetColumnValue("FontColor", obj.Tab3_FontColor)
        objSql.SetColumnValue("TextMessage", obj.Tab3_TextMessage)
        objSql.SetColumnValue("TextFontName", obj.Tab3_TextFontName)
        objSql.SetColumnValue("TextFontColor", obj.Tab3_TextFontColor)

        Dim UpFile2 = UploadFile(obj.MapDirectory, obj.Tab3_FileName)
        If (UpFile2) Then
            objSql.SetColumnValue("Picture", obj.Tab3_Picture)
            objSql.SetColumnValue("FileName", obj.Tab3_FileName)
            objSql.SetColumnValue("MapDirectory", obj.MapDirectory)
            Try
                Dim confPath As String = ""
                confPath = LHUtility.GetConfig("CSRPathDestination")
                hfFileName3.Value = obj.Tab3_FileName
                Dim desPath = confPath & "/" & obj.MapDirectory & "/" & hfFileName3.Value
                hfFileName3.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
            Catch ex As Exception

            End Try
        End If

        objSql.Execute()

        'Tab4
        objSql = New SqlHelper()
        objSql.NewUpdateStatement("ProjectStyle")
        objSql.Where("ProjectId = @ProjectId and CodeCtrlId = @CodeCtrlId")
        objSql.WhereParam("ProjectId", obj.ProjectId)
        objSql.WhereParam("CodeCtrlId", obj.Tab4_CodeCtrlId)
        objSql.SetColumnValue("BGColor", obj.Tab4_BGColor)
        objSql.SetColumnValue("TextMessage", obj.Tab4_TextMessage)
        objSql.SetColumnValue("TextFontName", obj.Tab4_TextFontName)
        objSql.SetColumnValue("TextFontColor", obj.Tab4_TextFontColor)
        Dim UpFile3 = UploadFile(obj.MapDirectory, obj.Tab4_FileName)
        If (UpFile3) Then
            objSql.SetColumnValue("Picture", obj.Tab4_Picture)
            objSql.SetColumnValue("FileName", obj.Tab4_FileName)
            objSql.SetColumnValue("MapDirectory", obj.MapDirectory)
            Try
                Dim confPath As String = ""
                confPath = LHUtility.GetConfig("CSRPathDestination")
                hfFileName4.Value = obj.Tab4_FileName
                Dim desPath = confPath & "/" & obj.MapDirectory & "/" & hfFileName4.Value
                hfFileName4.Value = "data:image;base64," & Convert.ToBase64String(System.IO.File.ReadAllBytes(desPath))
            Catch ex As Exception

            End Try
        End If

        objSql.Execute()

        'Tab5
        objSql = New SqlHelper()
        objSql.NewUpdateStatement("ProjectStyle")
        objSql.Where("ProjectId = @ProjectId and CodeCtrlId = @CodeCtrlId")
        objSql.WhereParam("ProjectId", obj.ProjectId)
        objSql.WhereParam("CodeCtrlId", obj.Tab5_CodeCtrlId)
        objSql.SetColumnValue("FontColor", obj.Tab5_FontColor)
        objSql.SetColumnValue("BGColor", obj.Tab5_BGColor)
        objSql.Execute()


        'Tab6
        objSql = New SqlHelper()
        objSql.NewUpdateStatement("ProjectStyle")
        objSql.Where("ProjectId = @ProjectId and CodeCtrlId = @CodeCtrlId")
        objSql.WhereParam("ProjectId", obj.ProjectId)
        objSql.WhereParam("CodeCtrlId", obj.Tab6_CodeCtrlId)
        objSql.SetColumnValue("BGColor", obj.Tab6_BGColor)
        objSql.SetColumnValue("TextMessage", obj.Tab6_TextMessage)
        objSql.SetColumnValue("TextFontName", obj.Tab6_TextFontName)
        objSql.SetColumnValue("TextFontColor", obj.Tab6_TextFontColor)
        objSql.Execute()


    End Sub

    Function Check() As Boolean
        Dim CheckFlag As Boolean = True
        If (hdnSelectedTab.Value = "0") Then 'tab 1 
            If (tbProjectName_tab1.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbDateFr_tab1.Text = "") Then
                CheckFlag = False
            End If
            If (tbDateTo_tab1.Text = "") Then
                CheckFlag = False
            End If
            If (tbSubjectMail_tab1.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbFontName_tab1.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbColor_tab1.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbBackgroud_tab1.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (hfFileName.Value = "0") Then
                CheckFlag = False
            End If
        ElseIf (hdnSelectedTab.Value = "1") Then 'tab 2
            If (tbFontColor_tab2.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbBackgroud_tab2.Text = "") Then
                CheckFlag = False
            End If
        ElseIf (hdnSelectedTab.Value = "2") Then 'tab 3
            If (tbBackgroud_tab3.Text = "") Then
                CheckFlag = False
            End If
            If (tbFontColor_tab3.Text = "") Then
                CheckFlag = False
            End If
            If (tbTextMessage_tab3.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbTextFontName_tab3.Text = "") Then
                CheckFlag = False
            End If
            If (tbTextFontColor_tab3.Text = "") Then
                CheckFlag = False
            End If
            If (hfFileName3.Value = "") Then
                CheckFlag = False
            End If
        ElseIf (hdnSelectedTab.Value = "3") Then 'tab 4
            If (tbBackgroud_tab4.Text = "") Then
                CheckFlag = False
            End If
            If (tbTextMessage_tab4.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbTextFontName_tab4.Text = "") Then
                CheckFlag = False
            End If
            If (tbTextFontColor_tab4.Text = "") Then
                CheckFlag = False
            End If
            If (hfFileName4.Value = "") Then
                CheckFlag = False
            End If
        ElseIf (hdnSelectedTab.Value = "4") Then 'tab 5
            If (tbFontColor_tab5.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbBackgroud_tab5.Text = "") Then
                CheckFlag = False
            End If
        ElseIf (hdnSelectedTab.Value = "5") Then 'tab 6
            If (tbFontColor_tab6.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbBackgroud_tab6.Text = "") Then
                CheckFlag = False
            End If
            If (tbTextMessage_tab6.Text.Trim() = "") Then
                CheckFlag = False
            End If
            If (tbTextFontName_tab6.Text = "") Then
                CheckFlag = False
            End If
        End If

        Return CheckFlag
    End Function

    Function CheckGenURL() As Boolean
        Dim CheckFlag As Boolean = True
        If (tbProjectName_tab1.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbDateFr_tab1.Text = "") Then
            CheckFlag = False
        End If
        If (tbDateTo_tab1.Text = "") Then
            CheckFlag = False
        End If
        If (tbSubjectMail_tab1.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbFontName_tab1.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbColor_tab1.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbBackgroud_tab1.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbFontColor_tab2.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbBackgroud_tab2.Text = "") Then
            CheckFlag = False
        End If
        If (tbBackgroud_tab3.Text = "") Then
            CheckFlag = False
        End If
        If (tbFontColor_tab3.Text = "") Then
            CheckFlag = False
        End If
        If (tbTextMessage_tab3.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbTextFontName_tab3.Text = "") Then
            CheckFlag = False
        End If
        If (tbTextFontColor_tab3.Text = "") Then
            CheckFlag = False
        End If
        If (tbBackgroud_tab4.Text = "") Then
            CheckFlag = False
        End If
        If (tbTextMessage_tab4.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbTextFontName_tab4.Text = "") Then
            CheckFlag = False
        End If
        If (tbTextFontColor_tab4.Text = "") Then
            CheckFlag = False
        End If
        If (tbFontColor_tab5.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbBackgroud_tab5.Text = "") Then
            CheckFlag = False
        End If
        If (tbFontColor_tab6.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbBackgroud_tab6.Text = "") Then
            CheckFlag = False
        End If
        If (tbTextMessage_tab6.Text.Trim() = "") Then
            CheckFlag = False
        End If
        If (tbTextFontName_tab6.Text = "") Then
            CheckFlag = False
        End If
        Return CheckFlag
    End Function

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        objUser.TitlePage = "Home"
        Response.Redirect("~/App_CSR/Home.aspx")
    End Sub

    Protected Sub btTmp_Click(sender As Object, e As EventArgs)
        If FileUpload1.HasFile Then
            Dim strLargeSize As String = ""
            Dim strNotSupport As String = ""
            Dim strSize As String = ""
            Dim strMsg As String = ""

            Dim ImageFiles As HttpFileCollection = Request.Files
            Dim filename As String = FileUpload1.FileName.ToString()

            If (FileUpload1.PostedFile.ContentLength > 52428800) Then
                strLargeSize += filename + "<br />"
            End If

            Dim WidthSize As Integer = 0
            Dim HeightSize As Integer = 0
            Try
                Dim s As Stream = FileUpload1.PostedFile.InputStream
                Dim i As Image = System.Drawing.Image.FromStream(s)
                WidthSize = i.PhysicalDimension.Width
                HeightSize = i.PhysicalDimension.Height
            Catch ex As Exception

            End Try

            If (WidthSize <> 1024 And HeightSize <> 350) Then
                strSize += filename + "<br />"
            End If

            Dim extension = Path.GetExtension(filename).ToLower()
            If Not extension.Contains("jpg") AndAlso Not extension.Contains("jpeg") AndAlso Not extension.Contains("gif") AndAlso Not extension.Contains("png") AndAlso Not extension.Contains("bmp") Then
                strNotSupport += filename + "<br />"
            End If
            If strLargeSize = "" And strNotSupport = "" And strSize = "" Then
                Dim tmpPath As String = "Document/temp/"
                If Not Directory.Exists(Server.MapPath(tmpPath)) Then
                    Directory.CreateDirectory(Server.MapPath(tmpPath))
                End If
                Dim a = Guid.NewGuid().ToString()
                Dim desPath As String = Server.MapPath(tmpPath & "/" & a & extension)
                FileUpload1.PostedFile.SaveAs(desPath)
                lbPicture.Text = filename
                hfFileName.Value = a & extension
            End If

            If strLargeSize <> "" Then
                strMsg += strLargeSize + "ไฟล์แนบมีขนาดใหญ่กว่า 50 Mb. กรุณาแนบไฟล์ใหม่"
            ElseIf strNotSupport <> "" Then
                strMsg += strNotSupport + "รองรับเฉพาะ JPG, JPEG, GIF, PNG, BMP"
            ElseIf strSize <> "" Then
                strMsg += strSize + "รูปภาพต้องมีขนาด 1024*350"
            End If

            If (strMsg <> "") Then
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('error','','" & strMsg & "');", True)
            End If

        End If
    End Sub


    Protected Sub btTmp2_Click(sender As Object, e As EventArgs)
        If FileUpload3.HasFile Then
            Dim strLargeSize As String = ""
            Dim strNotSupport As String = ""
            Dim strMsg As String = ""

            Dim ImageFiles As HttpFileCollection = Request.Files
            Dim filename As String = FileUpload3.FileName.ToString()

            If (FileUpload3.PostedFile.ContentLength > 52428800) Then
                strLargeSize += filename + "<br />"
            End If

            Dim extension = Path.GetExtension(filename).ToLower()
            If Not extension.Contains("jpg") AndAlso Not extension.Contains("jpeg") AndAlso Not extension.Contains("gif") AndAlso Not extension.Contains("png") AndAlso Not extension.Contains("bmp") Then
                strNotSupport += filename + "<br />"
            End If
            If strLargeSize = "" And strNotSupport = "" Then
                Dim tmpPath As String = "Document/temp/"
                If Not Directory.Exists(Server.MapPath(tmpPath)) Then
                    Directory.CreateDirectory(Server.MapPath(tmpPath))
                End If
                Dim a = Guid.NewGuid().ToString()
                Dim desPath As String = Server.MapPath(tmpPath & "/" & a & extension)
                FileUpload3.PostedFile.SaveAs(desPath)
                lbPicture3.Text = filename
                hfFileName3.Value = a & extension
            End If

            If strLargeSize <> "" Then
                strMsg += strLargeSize + "ไฟล์แนบมีขนาดใหญ่กว่า 50 Mb. กรุณาแนบไฟล์ใหม่"
            ElseIf strNotSupport <> "" Then
                strMsg += strNotSupport + "รองรับเฉพาะ JPG, JPEG, GIF, PNG, BMP"
            End If
            If (strMsg <> "") Then
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('error','','" & strMsg & "');", True)
            End If


        End If
    End Sub

    Protected Sub btTmp3_Click(sender As Object, e As EventArgs)
        If FileUpload4.HasFile Then
            Dim strLargeSize As String = ""
            Dim strNotSupport As String = ""
            Dim strMsg As String = ""

            Dim ImageFiles As HttpFileCollection = Request.Files
            Dim filename As String = FileUpload4.FileName.ToString()

            If (FileUpload4.PostedFile.ContentLength > 52428800) Then
                strLargeSize += filename + "<br />"
            End If

            Dim extension = Path.GetExtension(filename).ToLower()
            If Not extension.Contains("jpg") AndAlso Not extension.Contains("jpeg") AndAlso Not extension.Contains("gif") AndAlso Not extension.Contains("png") AndAlso Not extension.Contains("bmp") Then
                strNotSupport += filename + "<br />"
            End If
            If strLargeSize = "" And strNotSupport = "" Then
                Dim tmpPath As String = "Document/temp/"
                If Not Directory.Exists(Server.MapPath(tmpPath)) Then
                    Directory.CreateDirectory(Server.MapPath(tmpPath))
                End If
                Dim a = Guid.NewGuid().ToString()
                Dim desPath As String = Server.MapPath(tmpPath & "/" & a & extension)
                FileUpload4.PostedFile.SaveAs(desPath)
                lbPicture4.Text = filename
                hfFileName4.Value = a & extension
            End If

            If strLargeSize <> "" Then
                strMsg += strLargeSize + "ไฟล์แนบมีขนาดใหญ่กว่า 50 Mb. กรุณาแนบไฟล์ใหม่"
            ElseIf strNotSupport <> "" Then
                strMsg += strNotSupport + "รองรับเฉพาะ JPG, JPEG, GIF, PNG, BMP"
            End If
            If (strMsg <> "") Then
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('error','','" & strMsg & "');", True)
            End If
        End If
    End Sub

End Class