
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization

Public Class EmployeeProfile
    Private Shared thCulture As CultureInfo = CultureInfo.GetCultureInfo("th-TH")
    Private Shared enCulture As CultureInfo = CultureInfo.GetCultureInfo("en-US")
    Public Shared Function LogOn(ByVal dtEmployee As DataTable) As UserProfile
        If dtEmployee.Rows.Count > 0 Then
            Dim _userProfile As New UserProfile
            Try
                Dim dr As DataRow = dtEmployee.Rows(0)
                _userProfile.UserId = Null.SetNullInteger(dr("UserId"))
                _userProfile.UserName = Null.SetNullString(dr("UserName"))
                _userProfile.FullName = Null.SetNullString(dr("FullName"))
                _userProfile.Empcode = Null.SetNullString(dr("Empcode"))
                _userProfile.Position = Null.SetNullString(dr("Position"))
                _userProfile.Section = Null.SetNullString(dr("Section"))
                _userProfile.Department = Null.SetNullString(dr("Department"))
                _userProfile.Division = Null.SetNullString(dr("Division"))
                _userProfile.Phone = Null.SetNullString(dr("Phone"))
                _userProfile.EMail = Null.SetNullString(dr("EMail"))
                _userProfile.PictureURL = Null.SetNullString(dr("PictureURL"))
                _userProfile.TitlePage = ""
            Catch ex As Exception
            End Try
            Return _userProfile
        Else
            Return Nothing
        End If
    End Function

    Public Shared Function ScrnDateConvertDB(ByVal value As String) As String
        Dim dates As String = String.Empty
        If IsDate(value) Then
            Dim a As Date = Convert.ToDateTime(value)
            Dim d = a.ToString("dd")
            Dim m = a.ToString("MM")
            Dim y As Integer
            Integer.TryParse(a.ToString("yyyy"), y)
            If y > 2300 Then
                y -= 543
            End If
            dates = y & "-" & m & "-" & d
        Else
            Try
                Dim a As String() = value.Split("/")
                Dim d = a(0).ToString()
                Dim m = a(1).ToString()
                Dim y As Integer
                Integer.TryParse(a(2).ToString(), y)
                If y > 2300 Then
                    y -= 543
                End If
                dates = y & "-" & m & "-" & d
            Catch ex As Exception

            End Try
        End If
        Return dates
    End Function

    Public Shared Function ScrnDateDisplay(ByVal value As String, ByVal formateDate As String) As String
        Dim dates As String = String.Empty
        If IsDate(value) Then
            Dim tmpDate = Convert.ToDateTime(value)
            dates = tmpDate.ToString(formateDate, thCulture)
        End If
        Return dates
    End Function

    Public Shared Function ScrnDateDisplayen(ByVal value As String, ByVal formateDate As String) As String
        Dim dates As String = String.Empty
        If IsDate(value) Then
            Dim tmpDate = Convert.ToDateTime(value)
            dates = tmpDate.ToString(formateDate, enCulture)
        End If
        Return dates
    End Function

End Class
