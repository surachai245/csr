Imports Microsoft.VisualBasic
Imports System.Web.Configuration
Imports System.Web.UI.Page

Public Class LHUtility
    Public Shared Function GetConfig(ByVal AppSettingsName As String) As String
        Try
            Return ConfigurationManager.AppSettings(AppSettingsName)
        Catch Ex As Exception
            Return ""
        End Try
    End Function
End Class
