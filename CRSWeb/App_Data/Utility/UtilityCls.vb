
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.IO
Public Class UtilityCls
    Public Shared g_Log As Log

    Public Overloads Shared Sub InitLog()
        g_Log = New Log()
    End Sub

    Public Overloads Shared Sub InitLog(ByVal AddFilePrefix As String)
        g_Log = New Log(AddFilePrefix)
    End Sub

    Public Shared Sub WriteLog(ByVal LogType As AbstractLog.Type, ByVal Msg As String)
        'g_Log.WriteLog(LogType, Msg)
    End Sub
End Class
