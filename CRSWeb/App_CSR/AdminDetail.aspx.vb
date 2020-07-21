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


Public Class AdminDetail
    Inherits System.Web.UI.Page
    Dim culUS As CultureInfo = New CultureInfo("en-US")
    Dim objUser As UserProfile


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objUser = Session("UserProfileData")
        If Not IsPostBack Then
            Dim UserId As String = ""
            Try
                UserId = Request.QueryString("UserId").ToString()
                If (UserId <> "") Then
                    SelectData(UserId)
                    hfUserId.Value = UserId
                End If
            Catch ex As Exception

            End Try
        End If
        ClientScript.RegisterStartupScript(Me.GetType(), "CAll", "ActiveMenu('admin');", True)
    End Sub
    Sub SelectData(ByVal UserId As String)
        Dim SelectUserAdmin As DataTable = GetData.GetTBUserAdmin("", UserId)
        If (SelectUserAdmin IsNot Nothing) Then
            tbEmpCode.Text = SelectUserAdmin(0)("EmpCode").ToString().Trim()
            tbUserName.Text = SelectUserAdmin(0)("UserName").ToString()
            tbFullName.Text = SelectUserAdmin(0)("FullName").ToString()
            tbPhone.Text = SelectUserAdmin(0)("Phone").ToString()
            tbMail.Text = SelectUserAdmin(0)("EMail").ToString()
            rdActiveFlag.SelectedValue = SelectUserAdmin(0)("IsAdmin").ToString()
        End If
    End Sub
    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("~/App_CSR/AdminList.aspx")
    End Sub
    Protected Sub btInsert_Click(sender As Object, e As EventArgs) Handles btInsert.Click
        'log in ดึง User ทั้งหมดมาอยู่แล้ว 
        If (tbEmpCode.Text.Trim <> "" And tbUserName.Text.Trim <> "") Then

            Dim SelectUserAdmin As DataTable
            SelectUserAdmin = GetData.GetChkUserAdmin("", tbEmpCode.Text.Trim, tbUserName.Text.Trim)

            If (SelectUserAdmin IsNot Nothing) Then
                If (SelectUserAdmin.Rows.Count > 0) Then
                    Dim objSql = New SqlHelper()
                    If (hfUserId.Value <> "0") Then
                        objSql.NewUpdateStatement("Userdata")
                        objSql.Where("UserId = @UserId")
                        objSql.WhereParam("UserId", hfUserId.Value)
                        objSql.SetColumnValue("EmpId", SelectUserAdmin(0)("EmpId"))
                        objSql.SetColumnValue("IsAdmin", rdActiveFlag.SelectedValue)
                        objSql.SetColumnValue("UpdateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
                        objSql.SetColumnValue("UpdateBy", objUser.UserId)
                        objSql.Execute()
                    Else
                        objSql.NewInsertIdentityStatement("Userdata")
                        objSql.SetColumnValue("EmpId", SelectUserAdmin(0)("EmpId"))
                        objSql.SetColumnValue("IsAdmin", rdActiveFlag.SelectedValue)
                        objSql.SetColumnValue("CreateDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
                        Try
                            objSql.SetColumnValue("CreateBy", objUser.UserId)
                        Catch ex As Exception
                        End Try

                        Dim UserId = CInt(objSql.Execute())
                        hfUserId.Value = UserId
                        SelectData(UserId)
                    End If
                    ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage();", True)
                    If (objUser Is Nothing) Then
                        Response.Redirect("~/Login.aspx")
                    End If
                Else
                    ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('error','','ไม่พบข้อมูลในระบบ');", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('error','','ไม่พบข้อมูลในระบบ');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('error','','กรุณากรอกข้อมูลให้ครบถ้วน');", True)
        End If
    End Sub
End Class