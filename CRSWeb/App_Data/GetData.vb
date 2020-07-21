
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Drawing
Imports System.Web.Configuration
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Globalization
Public Class GetData
    Public Shared Function GetTBCodeCtrl() As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select CodeCtrlId,CodeDesc from CodeCtrl"
            dt = SqlUtility.SqlToTable(sql)
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetTBProject(ByVal ProjectId As String) As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select * from ProjectList,ProjectStyle where ProjectList.ProjectId = ProjectStyle.ProjectId and ProjectList.ProjectId = {0}"
            dt = SqlUtility.SqlToTable(String.Format(sql, ProjectId))
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetTBProjectReport() As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select * from ProjectList order by ProjectId DESC "
            dt = SqlUtility.SqlToTable(String.Format(sql))
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Shared Function GetTBProjectStyle(ByVal ProjectId As String, ByVal Code As String) As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select * from ProjectStyle where ProjectId = {0} and CodeCtrlId = {1}"
            dt = SqlUtility.SqlToTable(String.Format(sql, ProjectId, Code))
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Shared Function UpdateActive(ByVal ActiveFlag As String, ByVal ProjectId As String) As Boolean
        Try
            Dim sql As String = String.Format("Update ProjectList Set ActiveFlag = '" & ActiveFlag & "' where ProjectId = " & ProjectId & "")
            SqlUtility.SqlExecute(sql)
            Return True
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return False
        End Try
    End Function

    Public Shared Function DeleteActive(ByVal ProjectId As String) As Boolean
        Try
            Dim sql As String = String.Format("Delete ProjectList  where ProjectId = " & ProjectId & "")
            SqlUtility.SqlExecute(sql)
            Return True
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return False
        End Try
    End Function


    Public Shared Function GetTBUserAdmin(Shrech As String, UserId As String) As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            If (UserId <> "") Then
                sql = "select * from  userdata,employee where  userdata.EmpId = Employee.EmpId and UserId = '" & UserId & "'"
            Else
                If (Shrech = "") Then
                    sql = "select * from  userdata,employee where  userdata.EmpId = Employee.EmpId"
                Else
                    sql = "select * from  userdata,employee where  userdata.EmpId = Employee.EmpId and (Empcode Like '%" & Shrech & "%' Or FullName Like '%" & Shrech & "%' Or Phone Like '%" & Shrech & "%') "
                End If
            End If


            dt = SqlUtility.SqlToTable(sql)
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Shared Function DeleteUserAdmin(ByVal UserId As String) As Boolean
        Try
            'Dim sql As String = String.Format("Update UserData set IsAdmin = '" & Flag & "' where UserId = '" & UserId & "' ")

            Dim sql As String = String.Format("Delete UserData  where UserId = '" & UserId & "' ")
            SqlUtility.SqlExecute(sql)
            Return True
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return False
        End Try
    End Function


    Public Shared Function GetChkUserAdmin(UserId As String, Empcode As String, Username As String) As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            If (UserId <> "") Then
                sql = "select * from  userdata,employee where  userdata.EmpId = Employee.EmpId and UserId = '" & UserId & "'"
            Else
                sql = "select * from  employee where Empcode = '" & Empcode & "' and Username = '" & Username & "' "
            End If
            dt = SqlUtility.SqlToTable(sql)
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetEmployee() As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select  * from employee"
            dt = SqlUtility.SqlToTable(sql)
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Shared Function GetEmployeeProfile(ByVal Username As String) As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select  * from employee where Username ='" & Username & "'"
            dt = SqlUtility.SqlToTable(sql)
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Shared Function InsertEmployee(ByVal obj As SysEmployee) As Boolean
        Try
            Dim objSql = New SqlHelper()
            objSql.NewInsertStatement("Employee")
            objSql.SetColumnValue("Empcode", obj.empcode.Trim())
            objSql.SetColumnValue("UserName", obj.UserName)
            objSql.SetColumnValue("FullName", obj.FullName)
            objSql.SetColumnValue("Position", obj.Position)
            objSql.SetColumnValue("Section", obj.Section)
            objSql.SetColumnValue("Department", obj.Department)
            objSql.SetColumnValue("Division", obj.SECTER)
            objSql.SetColumnValue("Phone", obj.Phone)
            objSql.SetColumnValue("EMail", obj.EMail)
            objSql.SetColumnValue("PictureURL", obj.PictureURL)
            objSql.SetColumnValue("UpdateDate", obj.UpdateDate)
            objSql.Execute()
            Return True
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return False
        End Try
    End Function
    Public Shared Function InsertRefFriend(ByVal Refempid As String, ByVal Msg As String, ByVal Empid As String, ByVal Projectid As String) As Boolean
        Try
            Dim objSql = New SqlHelper()
            objSql.NewInsertStatement("RefFriend")
            objSql.SetColumnValue("ProjectId", Projectid)
            objSql.SetColumnValue("RefEmpid", Refempid)
            objSql.SetColumnValue("TextInvite", Msg)
            objSql.SetColumnValue("SendDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
            objSql.SetColumnValue("CreateBy", Empid)
            objSql.Execute()
            Return True
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return False
        End Try
    End Function
    Public Shared Function UpdateEmployee(ByVal obj As SysEmployee) As Boolean
        Try
            Dim objSql = New SqlHelper()
            objSql.NewUpdateStatement("Employee")
            objSql.Where("Empcode = @Empcode")
            objSql.WhereParam("Empcode", obj.empcode)
            objSql.SetColumnValue("UserName", obj.UserName)
            objSql.SetColumnValue("FullName", obj.FullName)
            objSql.SetColumnValue("Position", obj.Position)
            objSql.SetColumnValue("Section", obj.Section)
            objSql.SetColumnValue("Department", obj.Department)
            objSql.SetColumnValue("Division", obj.SECTER)
            objSql.SetColumnValue("Phone", obj.Phone)
            'objSql.SetColumnValue("EMail", obj.EMail)
            objSql.SetColumnValue("PictureURL", obj.PictureURL)
            'objSql.SetColumnValue("Company", obj.Company)
            objSql.SetColumnValue("UpdateDate", obj.UpdateDate)
            objSql.Execute()
            Return True
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return False
        End Try
    End Function

    Public Shared Function GetFirstData() As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select  * from userdata"
            dt = SqlUtility.SqlToTable(sql)
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Shared Sub FnSentEmailSystem(ByVal message As MailMessage)
        If WebConfigurationManager.AppSettings("MailTest") = "True" Then
            Try
                Dim client As SmtpClient
                If WebConfigurationManager.AppSettings("MailCheck") = "True" Then
                    client = New SmtpClient(WebConfigurationManager.AppSettings("MailUse"))
                    client.Credentials = New Net.NetworkCredential(WebConfigurationManager.AppSettings("MailUserName"), WebConfigurationManager.AppSettings("MailPassWord"), WebConfigurationManager.AppSettings("ActiveDomain"))
                Else
                    client = New SmtpClient(WebConfigurationManager.AppSettings("MailUse"))
                End If
                client.UseDefaultCredentials = True
                client.Send(message)
            Catch ex As Exception
                UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            End Try
        End If
    End Sub

    Public Shared Function GetChkRedirect(ByVal ProjectId As String, ByVal Empcode As String) As DataTable
        Dim dt As DataTable
        Try
            Dim sql As String
            sql = "select * from ProjectMember,ProjectList,Employee where ProjectMember.ProjectId = ProjectList.ProjectId and ProjectMember.EmpId = Employee.Empid and ProjectMember.ProjectId = {0} and Empcode = {1}"
            dt = SqlUtility.SqlToTable(String.Format(sql, ProjectId, Empcode))
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Shared Function InsertProjectMember(ByVal ProjectId As String, ByVal EmpId As String, ByVal AttendExpected As String, ByVal AttendFlag As String, ByVal CreatedDate As String, ByVal CreatedBy As String) As Boolean
        Try
            Dim objSql = New SqlHelper()
            objSql.NewInsertStatement("ProjectMember")
            objSql.SetColumnValue("ProjectId", ProjectId)
            objSql.SetColumnValue("EmpId", EmpId)
            objSql.SetColumnValue("AttendExpected", AttendExpected)
            objSql.SetColumnValue("AttendFlag", AttendFlag)
            objSql.SetColumnValue("CreatedDate", CreatedDate)
            objSql.SetColumnValue("CreatedBy", CreatedBy)
            objSql.Execute()
            Return True
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return False
        End Try
    End Function
    Public Shared Function GetReportData(ProjectId As String, txtDateFr As String, txtDateTo As String) As DataTable
        Dim dt As DataTable
        Try
            'DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            Dim sql As String
            sql = "select ProjectMember.EmpId,ProjectList.ProjectId,ProjectName,DateFr,DateTo,CreateDate,FullName,Position,Section,Department,Division,Phone,PictureURL,AttendExpected from ProjectMember inner join ProjectList on ProjectMember.ProjectId = ProjectList.ProjectId inner join Employee on ProjectMember.EmpId = Employee.Empid where ProjectList.ProjectId is not null "
            If (ProjectId <> "") Then
                sql += "and ProjectList.ProjectId like '" & ProjectId & "'"
            End If
            If (txtDateFr <> "") Then
                sql += "and DateFr = '" & txtDateFr & "'"
            End If
            If (txtDateTo <> "") Then
                sql += "and DateTo = '" & txtDateTo & "'"
            End If
            sql += "order by CreateDate desc"
            dt = SqlUtility.SqlToTable(sql)
            Return dt
        Catch ex As Exception
            UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
            Return Nothing
        End Try
    End Function

End Class
