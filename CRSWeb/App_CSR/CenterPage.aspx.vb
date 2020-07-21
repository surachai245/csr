
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
Imports System.Windows.Forms


Public Class CenterPage
    Inherits System.Web.UI.Page
    Public m_Log As New Log
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ProjectId As String = ""
            Dim Username As String = ""
            Try
                ProjectId = Request.QueryString("ProjectId").ToString()
                Try
                    Dim iduser() As String
                    iduser = Page.User.Identity.Name.Split("\")
                    Username = iduser(1)
                Catch ex As Exception
                    m_Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
                End Try

                Dim UserTest As String = WebConfigurationManager.AppSettings("UserTest")
                If (UserTest <> "") Then Username = UserTest

                If (ProjectId <> "" And Username <> "") Then
                    Dim obj = SelectData(ProjectId)
                    Dim objEmployee = SelectEmployee(Username)
                    If (obj IsNot Nothing) Then
                        Session("ProjestDetail") = obj
                        Session("EmployeeDetail") = objEmployee

                        If (objEmployee IsNot Nothing And obj IsNot Nothing) Then
                            Dim DTFr = Convert.ToDateTime(obj.DateFr)
                            Dim DTTo = Convert.ToDateTime(obj.DateTo)
                            Dim DateNow = Convert.ToDateTime(EmployeeProfile.ScrnDateDisplayen(DateTime.Now.Date, "dd/MM/yyyy"))
                            If (DateNow >= DTFr.Date And DateNow <= DTTo.Date) Then
                                Dim ChkRedirect = GetData.GetChkRedirect(obj.ProjectId, objEmployee.Empcode)
                                If (ChkRedirect IsNot Nothing) Then
                                    If (ChkRedirect.Rows.Count > 0) Then
                                        Response.Redirect("InviteFriends.aspx")
                                    Else
                                        Response.Redirect("EmployeeView.aspx")
                                    End If
                                End If
                            Else
                                Response.Redirect("WarningMessage.aspx")
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                m_Log.WriteLog(AbstractLog.Type.LogError, ex.ToString())
            End Try
        End If
    End Sub
    Private Function SelectData(ByVal ProjectId As String) As Object
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
            Dim obj As New SysProject
            obj.ProjectId = dv(0)("ProjectId").ToString()
            obj.ProjectName = dv(0)("ProjectName").ToString()
            obj.DateFr = If(dv(0)("DateFr").ToString() <> "", EmployeeProfile.ScrnDateDisplayen(dv(0)("DateFr").ToString(), "dd/MM/yyyy"), "")
            obj.DateTo = If(dv(0)("DateTo").ToString() <> "", EmployeeProfile.ScrnDateDisplayen(dv(0)("DateTo").ToString(), "dd/MM/yyyy"), "")
            obj.SubjectDetail = dv(0)("SubjectDetail").ToString()
            obj.Tab1_URL = dv(0)("URL").ToString()
            obj.ActiveFlag = dv(0)("ActiveFlag").ToString()

            'Tab 1 
            Dim SelectTeb As DataTable = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "1")
            Dim dvtab As New DataView(SelectTeb)
            If (dvtab.Count > 0) Then
                obj.Tab1_CodeCtrlId = dvtab(0)("CodeCtrlId").ToString()
                obj.Tab1_FontName = dvtab(0)("FontName").Replace("+", " ")
                obj.Tab1_FontColor = dvtab(0)("FontColor").ToString()
                obj.Tab1_BGColor = dvtab(0)("BGColor").ToString()
                obj.Tab1_Picture = dvtab(0)("Picture").ToString()
                obj.Tab1_FileName = dvtab(0)("FileName").ToString()
                obj.MapDirectory = dvtab(0)("MapDirectory").ToString()
            End If

            'Tab 2
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "2")
            Dim dvtab2 As New DataView(SelectTeb)
            If (dvtab2.Count > 0) Then
                obj.Tab2_CodeCtrlId = dvtab2(0)("CodeCtrlId").ToString()
                obj.Tab2_BGColor = dvtab2(0)("BGColor").ToString()
                obj.Tab2_FontColor = dvtab2(0)("FontColor").ToString()
            End If

            'Tab 3
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "3")
            Dim dvtab3 As New DataView(SelectTeb)
            If (dvtab3.Count > 0) Then
                obj.Tab3_CodeCtrlId = dvtab3(0)("CodeCtrlId").ToString()
                obj.Tab3_BGColor = dvtab3(0)("BGColor").ToString()
                obj.Tab3_FontColor = dvtab3(0)("FontColor").ToString()
                obj.Tab3_TextMessage = dvtab3(0)("TextMessage").ToString()
                obj.Tab3_TextFontName = dvtab3(0)("TextFontName").ToString().Replace("+", " ")
                obj.Tab3_TextFontColor = dvtab3(0)("TextFontColor").ToString()
                obj.Tab3_Picture = dvtab3(0)("Picture").ToString()
                obj.Tab3_FileName = dvtab3(0)("FileName").ToString()
            End If

            'Tab 4
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "4")
            Dim dvtab4 As New DataView(SelectTeb)
            If (dvtab4.Count > 0) Then
                obj.Tab4_CodeCtrlId = dvtab4(0)("CodeCtrlId").ToString()
                obj.Tab4_BGColor = dvtab4(0)("BGColor").ToString()
                obj.Tab4_TextMessage = dvtab4(0)("TextMessage").ToString()
                obj.Tab4_TextFontName = dvtab4(0)("TextFontName").ToString().Replace("+", " ")
                obj.Tab4_TextFontColor = dvtab4(0)("TextFontColor").ToString()
                obj.Tab4_Picture = dvtab4(0)("Picture").ToString()
                obj.Tab4_FileName = dvtab4(0)("FileName").ToString()
            End If

            'Tab 5
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "5")
            Dim dvtab5 As New DataView(SelectTeb)
            If (dvtab5.Count > 0) Then
                obj.Tab5_CodeCtrlId = dvtab5(0)("CodeCtrlId").ToString()
                obj.Tab5_BGColor = dvtab5(0)("BGColor").ToString()
                obj.Tab5_FontColor = dvtab5(0)("FontColor").ToString()
            End If

            'Tab 6
            SelectTeb = GetData.GetTBProjectStyle(dv(0)("ProjectId").ToString(), "6")
            Dim dvtab6 As New DataView(SelectTeb)
            If (dvtab6.Count > 0) Then
                obj.Tab6_CodeCtrlId = dvtab6(0)("CodeCtrlId").ToString()
                obj.Tab6_BGColor = dvtab6(0)("BGColor").ToString()
                obj.Tab6_TextMessage = dvtab6(0)("TextMessage").ToString()
                obj.Tab6_TextFontName = dvtab6(0)("TextFontName").ToString().Replace("+", " ")
                obj.Tab6_TextFontColor = dvtab6(0)("TextFontColor").ToString()
            End If

            Return obj
        Else
            Return Nothing
        End If

    End Function

    Private Function SelectEmployee(ByVal Username As String) As Object
        Dim obj As New SysEmployee
        Try
            Dim dtEmployee As DataTable = GetData.GetEmployeeProfile(Username)
            If (dtEmployee IsNot Nothing) Then
                Dim dvEmployee As DataView = New DataView(dtEmployee)
                If (dvEmployee.Count > 0) Then
                    obj.empid = dvEmployee(0)("empid").ToString().Trim()
                    obj.empcode = dvEmployee(0)("empcode").ToString().Trim()
                    obj.UserName = dvEmployee(0)("UserName").ToString()
                    obj.FullName = dvEmployee(0)("FullName").ToString()
                    obj.Position = dvEmployee(0)("Position").ToString()
                    obj.Section = dvEmployee(0)("Section").ToString()
                    obj.Department = dvEmployee(0)("Department").ToString()
                    obj.SECTER = dvEmployee(0)("Division").ToString()
                    obj.Phone = dvEmployee(0)("Phone").ToString()
                    obj.EMail = dvEmployee(0)("EMail").ToString()
                    obj.PictureURL = dvEmployee(0)("PictureURL").ToString()
                    'obj.Company = dvEmployee(0)("Company").ToString()
                    obj.UpdateDate = dvEmployee(0)("UpdateDate").ToString()
                End If
                Return obj
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

End Class