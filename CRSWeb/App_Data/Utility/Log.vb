Option Explicit On
Option Strict On

Imports System.IO
Imports System.Text
Imports System.Web.Configuration
Imports System.Globalization

Public Class Log
    Inherits AbstractLog
    Dim strLogPrefix As String

#Region " Constant "

    Private Const DefaultLogLevel As Level = Level.Normal

#End Region

#Region " Private Member "

    Private m_strLogPath As String  'Path to save
    Private m_LogLevel As Level
    Private m_strAddFilePrefix As String
    Private m_strFilePrefix As String   'Prefix of log file
    Private m_Culture As CultureInfo

#End Region

#Region " Property "
    '---------------------------------------------------------------------------
    Overrides Property LogLevel() As Level
        Get
            Return m_LogLevel
        End Get
        Set(ByVal Value As Level)
            m_LogLevel = Value
        End Set
    End Property
    '---------------------------------------------------------------------------
    Overrides Property Path() As String
        Get
            Return m_strLogPath
        End Get
        ' Empty Path means current
        ' Create if not exist
        ' Throw exception if failed (Value not change)
        Set(ByVal Value As String)
            SetPath(Value)
        End Set
    End Property

    '---------------------------------------------------------------------------
#End Region

#Region " Public Method "
    '---------------------------------------------------------------------------
    Public Sub New(ByVal Path As String,
                    ByVal FilePrefix As String,
                    Optional ByVal LogLevel As Level = DefaultLogLevel)
        m_strFilePrefix = FilePrefix.Trim()
        SetPath(Path)
        m_Culture = New CultureInfo("en-US")
        m_LogLevel = LogLevel

    End Sub

    Public Sub New(ByVal FilePrefix As String, Optional ByVal LogLevel As Level = DefaultLogLevel)
        m_strFilePrefix = FilePrefix.Trim()
        SetPath(WebConfigurationManager.AppSettings("LogPath").ToString())
        m_Culture = New CultureInfo("en-US")
        m_LogLevel = LogLevel
    End Sub

    Public Sub New(Optional ByVal LogLevel As Level = DefaultLogLevel, Optional ByVal AddFilePrefix As String = "")
        m_strFilePrefix = WebConfigurationManager.AppSettings("LogPrefix").ToString() & AddFilePrefix
        SetPath(WebConfigurationManager.AppSettings("LogPath").ToString())
        m_Culture = New CultureInfo("en-US")
        m_LogLevel = LogLevel
    End Sub


    '---------------------------------------------------------------------------
    Public Overrides Sub Dispose()
        m_Culture = Nothing
    End Sub
    '---------------------------------------------------------------------------
    Public Overloads Overrides Sub WriteLog(ByVal LogType As Type, ByVal LogMessage As String)

        SyncLock Me
            'Dim strLogPrefix As String
            Dim bWrite As Boolean = False

            Select Case (LogType)
                Case Type.LogDebug
                    strLogPrefix = LOG_PREFIX_DEBUG
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = False
                        Case Level.Debug
                            bWrite = True
                    End Select
                Case Type.LogInformation
                    strLogPrefix = LOG_PREFIX_INFOR
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = True
                        Case Level.Debug
                            bWrite = True
                    End Select
                Case Type.LogWarning
                    strLogPrefix = LOG_PREFIX_WARNS
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = True
                        Case Level.Debug
                            bWrite = True
                    End Select
                Case Type.LogError
                    strLogPrefix = LOG_PREFIX_ERROR
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = True
                        Case Level.Debug
                            bWrite = True
                    End Select
            End Select

            If bWrite Then
                Try


                    Dim dtc As DateTime = DateTime.Now()
                    Dim strUserName As String = ""
                    Dim objUser As New UserProfile
                    objUser = CType(HttpContext.Current.Session("UserProfileData"), UserProfile)

                    If objUser IsNot Nothing Then
                        strUserName = "[" & objUser.UserName & "] "
                    End If

                    AppendLine(
                        m_strLogPath & m_strFilePrefix & dtc.ToString("yyyyMMdd", m_Culture) & ".log",
                        dtc.ToString("yyyyMMdd HH:mm:ss ", m_Culture) & strUserName & strLogPrefix & " " & LogMessage
                         )
                Catch ex As Exception
                    UtilityCls.WriteLog(AbstractLog.Type.LogError, ex.ToString)
                End Try
            End If

        End SyncLock
    End Sub

    '---------------------------------------------------------------------------
    Public Overloads Overrides Sub WriteLog(ByVal LogType As Type,
                                            ByVal LogMessage As String,
                                            ByVal ParamArray Param() As Object)


        SyncLock Me

            'Dim strLogPrefix As String
            Dim bWrite As Boolean = False

            Select Case (LogType)
                Case Type.LogDebug
                    strLogPrefix = LOG_PREFIX_DEBUG
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = False
                        Case Level.Debug
                            bWrite = True
                    End Select
                Case Type.LogInformation
                    strLogPrefix = LOG_PREFIX_INFOR
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = True
                        Case Level.Debug
                            bWrite = True
                    End Select
                Case Type.LogWarning
                    strLogPrefix = LOG_PREFIX_WARNS
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = True
                        Case Level.Debug
                            bWrite = True
                    End Select
                Case Type.LogError
                    strLogPrefix = LOG_PREFIX_ERROR
                    Select Case (m_LogLevel)
                        Case Level.None
                            bWrite = False
                        Case Level.Normal
                            bWrite = True
                        Case Level.Debug
                            bWrite = True
                    End Select
            End Select

            If bWrite Then
                Dim dtc As DateTime = DateTime.Now()
                AppendLine(
                    m_strLogPath &
                    m_strFilePrefix &
                    dtc.ToString("yyyyMMdd", m_Culture) &
                    ".log" _
                    ,
                    dtc.ToString("yyyyMMdd HH:mm:ss ", m_Culture) &
                    strLogPrefix &
                    " " &
                    String.Format(LogMessage, Param)
                    )
            End If
        End SyncLock
    End Sub
    '---------------------------------------------------------------------------
    Public Sub DeleteExpireLog(ByVal ExpireDay As Integer)
        If (ExpireDay >= 0) Then
            Dim strTresholdTime As String = DateTime.Today().Subtract(TimeSpan.FromDays(ExpireDay)).ToString("yyyyMMdd", m_Culture)

            Dim Files() As String
            Files = Directory.GetFiles(m_strLogPath, m_strFilePrefix & "????????.log")
            If Not (Files Is Nothing) Then
                Dim strFilename As String
                Dim ilPrefix As Integer = m_strLogPath.Length + m_strFilePrefix.Length
                For Each strFilename In Files
                    Try
                        If String.Compare(strFilename.Substring(ilPrefix, 8), strTresholdTime) < 0 Then
                            WriteLog(Type.LogDebug, "Delete Expire File[{0}]", strFilename)
                            Try
                                File.Delete(strFilename)
                            Catch ex As Exception
                                WriteLog(Type.LogWarning, "Delete file[{0}] failed[{1}].", strFilename, ex.ToString())
                            End Try
                        End If
                    Catch ex As Exception
                        WriteLog(Type.LogError, ex.ToString())
                    End Try
                Next
            End If
            Files = Nothing
        End If
    End Sub
    '---------------------------------------------------------------------------
#End Region

#Region " Private Method "
    '---------------------------------------------------------------------------
    ' Empty Path means current
    ' Create if not exist
    ' Throw exception if failed (Value not change)
    Private Sub SetPath(ByVal strPath As String)
        Try
            If IsEmptyString(strPath) Then
                strPath = ".\"
            End If
            'Create if not Exists
            Dim dirInfo As DirectoryInfo
            If Not (Directory.Exists(strPath)) Then
                dirInfo = Directory.CreateDirectory(strPath)
            Else
                dirInfo = New DirectoryInfo(strPath)
            End If
            strPath = dirInfo.FullName
            dirInfo = Nothing
            'Add "\"
            If Not strPath.EndsWith("\") Then
                strPath &= "\"
            End If
            m_strLogPath = strPath
        Catch ex As Exception
            Throw New Exception("Set Log path failed.", ex)
        End Try
    End Sub
    '---------------------------------------------------------------------------
    Private Function IsEmptyString(ByVal value As String) As Boolean
        Dim bResult As Boolean
        If value Is Nothing Then
            bResult = True
        ElseIf value.Trim().Length = 0 Then
            bResult = True
        Else
            bResult = False
        End If
        Return bResult
    End Function
    '---------------------------------------------------------------------------
#End Region
End Class
