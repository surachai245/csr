Imports System.Web.Configuration
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Data


Public Class Login
    Inherits System.Web.UI.Page
    Public gUtil As clsUtility = Nothing
    Dim client As New ServiceReference1.LicenseServiceClient
    Public gCulEn As New System.Globalization.CultureInfo("en-US")
    Public m_Log As New Log
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtUsername.Focus()
        If Not IsPostBack Then
            Session.Remove("UserProfileData")

            'Insert Employee (LHBank And LHFG)
            'กรณีมีพนักงานเพิ่มให้เช็คเพื่อ insert ถ้าเท่ากันแล้ว ให้เช็คเพื่อ Update วันละครั้งพอ เพื่อมีการเปลี่ยนตำแหน่ง 
            Dim LHFG = client.GetEmployeeLHFG()
            ServiceInsertEmployee(LHFG, "LHFG")
        End If

    End Sub

    Sub ServiceInsertEmployee(LH As ServiceReference1.Employee(), Company As String)
        'Dim LHFG = client.GetEmployeeLHFG
        If (LH.Length > 0) Then
            Dim dtEmployee As DataTable = GetData.GetEmployee()
            If (dtEmployee IsNot Nothing) Then
                Dim dvEmployee As DataView = New DataView(dtEmployee)
                If (dvEmployee.Count <> LH.Length) Then
                    Dim obj = New SysEmployee
                    For index = 0 To LH.Length - 1
                        Dim indexsub = LH(index).EM_CODE.Substring(0, 1)
                        'If (indexsub <> "5" And indexsub <> "7") Then
                        obj.empcode = LH(index).EM_CODE.Trim()
                        If (indexsub = "0") Then
                            obj.UserName = "P" + LH(index).EM_CODE.Substring(1, LH(index).EM_CODE.Length - 1)
                        Else
                            obj.UserName = "P" + LH(index).EM_CODE
                        End If
                        obj.FullName = LH(index).EM_TNAME & " " & LH(index).EM_TSURNAME
                        obj.Position = LH(index).OP_TNAME
                        obj.Section = LH(index).SECTION
                        obj.Department = LH(index).DEPARTMENT
                        Dim Secter As String = ""
                        Secter = LH(index).SECTER.Trim()
                        If (Secter = "-") Then
                            obj.SECTER = LH(index).GROUP
                        Else
                            obj.SECTER = LH(index).SECTER
                        End If
                        Try
                            obj.Phone = LH(index).PHONE_OFFICE
                        Catch ex As Exception
                            obj.Phone = ""
                        End Try
                        obj.EMail = LH(index).EM_MAIL
                        obj.PictureURL = LH(index).EM_PICTURE
                        obj.Company = Company
                        obj.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                        dvEmployee.RowFilter = "Empcode ='" & obj.empcode & "'"
                        If (dvEmployee.Count = 0) Then
                            GetData.InsertEmployee(obj)
                        Else
                            Dim dt As DateTime = Convert.ToDateTime(EmployeeProfile.ScrnDateDisplayen(dvEmployee(0)("UpdateDate").ToString(), "dd/MM/yyyy"))
                            Dim DateNow As DateTime = Convert.ToDateTime(EmployeeProfile.ScrnDateDisplayen(DateTime.Now.Date, "dd/MM/yyyy"))
                            If (dt.Date <> DateNow) Then
                                GetData.UpdateEmployee(obj) 'เผื่อมีเปลี่ยนตำแหน่ง
                            End If
                        End If
                        ' End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub Login()
        Try
            txtMsg.Text = ""
            If txtUsername.Text.Trim() <> "" AndAlso txtPassword.Text.Trim() <> "" Then
                If IsUserInAD() Then
                    Dim paraHR As SqlParameterCollection = New SqlCommand().Parameters
                    'paraHR.Add("@ModeTest", SqlDbType.NVarChar).Value = "ModeTest"
                    paraHR.Add("@Username", SqlDbType.NVarChar).Value = txtUsername.Text.Trim()
                    Dim dtHR As DataTable = SqlUtility.SqlExecuteSP("SP_EmployeeList", paraHR).Tables(0)
                    If dtHR.Rows.Count > 0 Then
                        Dim UserLogin As UserProfile = EmployeeProfile.LogOn(dtHR)
                        If UserLogin IsNot Nothing Then
                            UserLogin.TitlePage = "Home"
                            Session("UserProfileData") = UserLogin
                            Response.Redirect("~/App_CSR/Home.aspx")
                        End If
                    Else
                        Dim fristdata As DataTable = GetData.GetFirstData()
                        If (fristdata.Rows.Count = 0) Then
                            Response.Redirect("~/App_CSR/AdminDetail.aspx")
                        Else
                            txtMsg.Text = "กรุณาตรวจสอบข้อมูลให้ถูกต้อง"
                        End If
                    End If
                Else
                    txtMsg.Text = "Username หรือ Password ไม่ถูกต้อง"
                    Exit Sub
                End If
            Else
                txtMsg.Text = "กรุณากรอก UserName และ Password!!!"
                Exit Sub
            End If
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
        End Try
    End Sub

    Public Function IsUserInAD() As Boolean
        System.Threading.Thread.CurrentThread.CurrentCulture = gCulEn
        gUtil = New clsUtility

        Dim sErr As String = String.Empty
        Dim sUsername As String = Trim(Me.txtUsername.Text.Trim())
        Dim sPassword As String = Trim(Me.txtPassword.Text.Trim())
        Try
            If gUtil.isUserADAccept(WebConfigurationManager.AppSettings("ActiveDomain").ToString(), sUsername, sPassword, sErr) Then

                Return True
            Else
                m_Log.WriteLog(AbstractLog.Type.LogWarning, "LogOn Fail UserName : " & txtUsername.Text & vbTab & sErr)
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "", True)
                Return False
            End If
        Catch ex As Exception
            m_Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
            Throw ex
        End Try
    End Function

    Protected Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        Dim ApplicationInfo As New ApplicationInfo()
        txtMsg.Text = ""
        Try
            If (ApplicationInfo.GetIsTest()) Then
                LoginTest()
            Else
                Login()
            End If
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
        End Try
    End Sub

    Private Sub LoginTest()
        Try
            Dim Msg As String = ""
            If (txtUsername.Text.Trim() <> "" And txtPassword.Text.Trim() <> "") Then
                Dim paraHR As SqlParameterCollection = New SqlCommand().Parameters
                'paraHR.Add("@ModeTest", SqlDbType.NVarChar).Value = "ModeTest"
                paraHR.Add("@Username", SqlDbType.NVarChar).Value = txtUsername.Text.Trim()
                Dim dtHR As DataTable = SqlUtility.SqlExecuteSP("SP_EmployeeList", paraHR).Tables(0)
                If dtHR.Rows.Count > 0 Then
                    Dim UserLogin As UserProfile = EmployeeProfile.LogOn(dtHR)
                    If UserLogin IsNot Nothing Then
                        UserLogin.TitlePage = "Home"
                        Session("UserProfileData") = UserLogin
                        Response.Redirect("~/App_CSR/Home.aspx")
                    End If
                Else
                    Dim fristdata As DataTable = GetData.GetFirstData()
                    If (fristdata.Rows.Count = 0) Then
                        Response.Redirect("~/App_CSR/AdminDetail.aspx")
                    Else
                        Msg = "กรุณาตรวจสอบข้อมูลให้ถูกต้อง"
                    End If
                End If
            Else
                Msg = "กรุณาใส่ข้อมูล Username/Password ให้ครบถ้วน"
            End If

            If Msg <> "" Then
                txtMsg.Text = Msg
            End If
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
        End Try
    End Sub

    'Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
    '    'Me.Page.ClientScript.RegisterStartupScript(Page.GetType, "CloseForm", "<script language='javascript'>{self.close();}</script>;")
    '    'Response.Write("<script type='text/javascript'> " +
    '    '   "window.opener = 'Self';" +
    '    '                "window.open('','_parent','');" +
    '    '                "window.close(); " +
    '    '                "</script>")
    '    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CloseForm", "<script language='javascript'>{self.close();}</script>;", True)
    '    Page.ClientScript.RegisterOnSubmitStatement(Me.GetType(), "closePage", "window.onunload = CloseWindow();")

    'End Sub
End Class