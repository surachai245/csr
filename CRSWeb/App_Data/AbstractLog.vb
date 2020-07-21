Option Explicit On
Option Strict On

Imports System.IO

Public MustInherit Class AbstractLog
    Implements IDisposable
    Dim sw As StreamWriter

#Region " Structure and Enum "
    '---------------------------------------------------------------------------
    Public Enum Level As Short
        None = 0
        Normal = 1
        Debug = 2
    End Enum
    '---------------------------------------------------------------------------
    Public Enum Type As Short
        LogInformation = 1
        LogWarning = 2
        LogError = 3
        LogDebug = 4
    End Enum
    '---------------------------------------------------------------------------
#End Region

#Region " Constant "

    Protected LOG_PREFIX_INFOR As String = "INFOR"
    Protected LOG_PREFIX_WARNS As String = "WARNS"
    Protected LOG_PREFIX_ERROR As String = "#ERROR#"
    Protected LOG_PREFIX_DEBUG As String = "DEBUG"

#End Region

#Region " Property "

    MustOverride Property LogLevel() As Level
    MustOverride Property Path() As String

#End Region

#Region " Public Method "
    '---------------------------------------------------------------------------
    Public Overridable Sub Dispose() Implements IDisposable.Dispose

    End Sub
    '---------------------------------------------------------------------------
    Public MustOverride Overloads Sub WriteLog(ByVal LogType As Type,
                                                ByVal LogMessage As String
                                                )
    '---------------------------------------------------------------------------
    'This method is not of MustOverride type because it may not necessary, 
    'but String.Format is resourceful and better Overrides if used.
    Public Overridable Overloads Sub WriteLog(
                                        ByVal LogType As Type,
                                        ByVal LogMessage As String,
                                        ByVal ParamArray Param() As Object
                                        )
        WriteLog(LogType, String.Format(LogMessage, Param))
    End Sub
    '---------------------------------------------------------------------------
#End Region

#Region " Private Method "
    '---------------------------------------------------------------------------
    Protected Overridable Sub AppendLine(ByVal FileName As String,
                                            ByVal Text As String
                                            )
        'Dim sw As StreamWriter
        Try
            sw = New StreamWriter(FileName, True)
            sw.WriteLine(Text)
        Catch ex As Exception
            'Do nothing
        Finally
            If Not (sw Is Nothing) Then
                sw.Close()
                sw = Nothing
            End If
        End Try
    End Sub
    '---------------------------------------------------------------------------
#End Region

End Class
