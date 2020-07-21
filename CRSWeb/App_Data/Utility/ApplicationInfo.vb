Imports System.Web.Configuration

Public Class ApplicationInfo
    Public Function GetApplicationVersion() As String
        Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
    End Function

    Public Function GetTag(tag As String) As String
        Try
            If (Not String.IsNullOrEmpty(WebConfigurationManager.AppSettings(tag))) Then
                Return WebConfigurationManager.AppSettings(tag).ToString()
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Private Function GetConnectionString(Name As String) As String
        Try
            Return WebConfigurationManager.ConnectionStrings(Name).ToString().Trim()
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
    Public Function GetIsTest() As Boolean
        Dim isTest As String = GetTag("IsTest").ToLower()
        If (isTest = "true") Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetVerifyMessage() As String
        Return GetTag("VerifyMessage")
    End Function
End Class
