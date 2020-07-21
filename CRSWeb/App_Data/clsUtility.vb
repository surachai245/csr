Imports System.IO
Imports System.Data
Imports System.Windows.Forms
Imports System.DirectoryServices 'must add reference: System.DirectoryServices
Imports System.Data.OleDb
Imports System.Security.Principal

Public Class clsUtility
    Private m_sDateDelimiter As String

    Public Enum gEnumDateFormat
        ddMMyyyy = 0
        dMyyyy = 1
        ddMMyy = 2
        dMyy = 3
        MMddyyyy = 4
        Mdyyyy = 5
        MMddyy = 6
        Mdyy = 7
        yyyyMMdd = 8
    End Enum

    Public Enum gEnumDateStyle
        Christ = 0
        Budd = 1
    End Enum

    Public Enum enumAppendFileSeparator
        newline
        space
        comma
        hyphen
        blank
    End Enum

    Public Enum enumDataType
        oString
        oInteger
        oDecimal
    End Enum

    Public Enum enumInputType
        Constant
        Field
        Running
    End Enum

    Public Enum enumParentType
        GroupBox
        Form
        Panel
    End Enum

    Public Function getEnumIndex(ByVal sValue As String) As Integer
        Dim eInputType As clsUtility.enumInputType = Nothing
        Select Case sValue.ToUpper
            Case "CONSTANT" : eInputType = clsUtility.enumInputType.Constant
            Case "FIELD" : eInputType = clsUtility.enumInputType.Field
            Case "RUNNING" : eInputType = clsUtility.enumInputType.Running
            Case "OSTRING" : eInputType = clsUtility.enumDataType.oString
            Case "OINTEGER" : eInputType = clsUtility.enumDataType.oInteger
            Case "ODECIMAL" : eInputType = clsUtility.enumDataType.oDecimal
        End Select
        Return eInputType
    End Function

    Public Function getAssemblePath() As String
        Dim strSolPath As String = System.Reflection.Assembly.GetExecutingAssembly.Location
        If strSolPath.IndexOf("bin") < 0 Then
            Return strSolPath.Substring(0, strSolPath.LastIndexOf("\")) & "\"
        Else
            Return strSolPath.Substring(0, strSolPath.LastIndexOf("bin") - 1) & "\"
        End If
    End Function

    Public Function getWindowsUserLogon()
        ' Create a WindowsIdentity object for the current user using the GetCurrent method 
        Dim idWindows As WindowsIdentity = WindowsIdentity.GetCurrent()
        ' Create a WindowsPrincipal object based on the WindowsIdentity object. 
        ' The WindowsPrincipal object contains the current user's Group membership.
        Dim prinWindows As New WindowsPrincipal(idWindows)

        'Return String.Format("Name:  {0}{1}", idWindows.Name, ControlChars.CrLf)
        Return idWindows.Name
    End Function

    Public Function isUserInAD(ByVal sDomain As String, ByVal sUser As String,
        ByRef sErr As String, Optional ByVal sDomainController As String = "dc=lhb,dc=net") As Boolean
        Dim oSearcher As New DirectorySearcher
        Dim oResults As SearchResultCollection
        Dim ResultFields() As String = {"securityEquals", "cn"}
        Try
            'เฉพาะ LHB dc จะเป็น "dc=lhb,dc=net"
            sDomain = "LDAP://" & sDomain & "/" & sDomainController
            With oSearcher
                .SearchRoot = New DirectoryEntry(sDomain)
                .PropertiesToLoad.AddRange(ResultFields)
                .Filter = "cn=" & sUser
                oResults = .FindAll()
            End With
            If oResults.Count = 0 Then
                sErr = "ไม่พบ user " & sUser & "ใน domain : ติดต่อ Security admin"
                Return False
            End If
            sErr = "" : Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function isUserADAccept(ByVal sDomain As String,
                                  ByVal sUser As String, ByVal sPassword As String,
                                  ByRef sErr As String) As Boolean
        Dim oSearcher As New DirectorySearcher
        Dim oResults As SearchResultCollection
        Dim ResultFields() As String = {"securityEquals", "cn"}
        Dim iFoundResult As Integer
        Dim oDE As DirectoryEntry
        Dim IsReturn As Boolean = False
        'ไม่สามารถแยกฟังก์ชั่นไปใช้ isUserInAD ได้  
        'ไม่สามารถเข้าไปดู property ต่างๆ ได้นอกเหนือจากการเช็คถึงการมีอยู่ของ user+password ดังกล่าว  
        '  ถ้าต้องการเข้าไปเจาะรายละเอียด  ต้องเขียนโปรแกรมเพิ่ม  และต้องใช้ ADuser และ ADpassword ที่เป็นของ ADAdmin
        Try
            sDomain = "LDAP://" & sDomain & "/dc=lhb,dc=net"
            With oSearcher 'ตรวจสอบการมีอยู่ของ  sUser
                .SearchRoot = New DirectoryEntry(sDomain)
                .PropertiesToLoad.AddRange(ResultFields)
                .Filter = "cn=" & sUser
                oResults = .FindAll()
            End With
            If oResults.Count = 0 Then
                sErr = "ไม่พบ user " & sUser & "ใน domain : ติดต่อ Security admin"
                IsReturn = False
            Else
                'สร้าง object ที่ใช้ติดต่อกับ AD (Active Directory)
                oDE = New DirectoryEntry(sDomain, sUser, sPassword, AuthenticationTypes.Secure)
                iFoundResult = oDE.Children.SchemaFilter.Count
                sErr = ""
                IsReturn = True
            End If

        Catch ex As Exception
            'ถ้า password ผิด  หรือ user โดน Locked  จะเข้าตรงนี้
            sErr = "Sorry, unable to verify your information : " & ex.Message & vbCrLf &
                   "หรือรหัสพนักงานคุณอาจถูก Locked"
        Finally

        End Try

        Return IsReturn
    End Function

    Public Function isUserADAccept(ByVal sDomain As String, ByVal sDomainController As String,
                                   ByVal sUser As String, ByVal sPassword As String,
                                   ByRef sErr As String) As Boolean
        Dim oSearcher As New DirectorySearcher
        Dim oResults As SearchResultCollection
        Dim ResultFields() As String = {"securityEquals", "cn"}
        Dim iFoundResult As Integer
        Dim oDE As DirectoryEntry

        Try
            sDomain = "LDAP://" & sDomain & "/" & sDomainController
            With oSearcher 'ตรวจสอบการมีอยู่ของ  sUser
                .SearchRoot = New DirectoryEntry(sDomain)
                .PropertiesToLoad.AddRange(ResultFields)
                .Filter = "cn=" & sUser
                oResults = .FindAll()
            End With
            If oResults.Count = 0 Then
                sErr = "ไม่พบ user " & sUser & "ใน domain : ติดต่อ Security admin"
                Return False
            Else
                'สร้าง object ที่ใช้ติดต่อกับ AD (Active Directory)
                oDE = New DirectoryEntry(sDomain, sUser, sPassword, AuthenticationTypes.Secure)
                iFoundResult = oDE.Children.SchemaFilter.Count
            End If
            sErr = "" : Return True
        Catch ex As Exception
            sErr = "Sorry, unable to verify your information : " & ex.Message
            Return False
        End Try
    End Function

    Public Function setNumberSeparator(ByVal dValue As Double, ByVal sSeparator As String) As String
        Dim iPre As Integer
        Dim sInteger As String
        Dim sResult As String = "", sDecimal As String = ""

        If dValue.ToString.Length < 4 Then Return dValue.ToString

        sInteger = dValue.ToString.Split(".")(0)
        If dValue.ToString.Split(".").Length > 1 Then sDecimal = dValue.ToString.Split(".")(1)

        iPre = sInteger.Length Mod 3

        Select Case iPre
            Case 1, 2 : sResult &= sInteger.Substring(0, iPre) & sSeparator
        End Select

        For i As Integer = 0 To (sInteger.Length \ 3) - 1
            sResult &= sInteger.Substring(iPre + (i * 3), 3) & sSeparator
        Next
        sResult = sResult.Substring(0, sResult.Length - 1)

        If sDecimal <> "" Then
            Return sResult & "." & sDecimal
        Else
            Return sResult
        End If
    End Function

    Public Function setNumberSeparator(ByVal lValue As Long, ByVal sSeparator As String) As String
        Dim iPre As Integer
        Dim sInteger As String
        Dim sResult As String = "", sDecimal As String = ""

        If lValue.ToString.Length < 4 Then Return lValue.ToString

        sInteger = lValue.ToString.Split(".")(0)
        If lValue.ToString.Split(".").Length > 1 Then sDecimal = lValue.ToString.Split(".")(1)

        iPre = sInteger.Length Mod 3

        Select Case iPre
            Case 1, 2 : sResult &= sInteger.Substring(0, iPre) & sSeparator
        End Select

        For i As Integer = 0 To (sInteger.Length \ 3) - 1
            sResult &= sInteger.Substring(iPre + (i * 3), 3) & sSeparator
        Next
        sResult = sResult.Substring(0, sResult.Length - 1)

        If sDecimal <> "" Then
            Return sResult & "." & sDecimal
        Else
            Return sResult
        End If
    End Function

    Public Function setNumberSeparator(ByVal sValue As String, ByVal sSeparator As String) As String
        Dim iPre As Integer
        Dim iValue As Double
        Dim sInteger As String
        Dim sResult As String = "", sDecimal As String = ""

        iValue = sValue
        If iValue.ToString.Length < 4 Then Return iValue.ToString

        sInteger = iValue.ToString.Split(".")(0)
        If iValue.ToString.Split(".").Length > 1 Then sDecimal = iValue.ToString.Split(".")(1)

        iPre = sInteger.Length Mod 3

        Select Case iPre
            Case 1, 2 : sResult &= sInteger.Substring(0, iPre) & sSeparator
        End Select

        For i As Integer = 0 To (sInteger.Length \ 3) - 1
            sResult &= sInteger.Substring(iPre + (i * 3), 3) & sSeparator
        Next
        sResult = sResult.Substring(0, sResult.Length - 1)

        If sDecimal <> "" Then
            Return sResult & "." & sDecimal
        Else
            Return sResult
        End If
    End Function

    Public Function getRepeateCharacter(ByVal sCharacter As String, ByVal iCount As Integer) As String
        Dim sResult As String = ""
        For i As Integer = 0 To iCount - 1
            sResult &= sCharacter
        Next
        Return sResult
    End Function

    Public Function getValueFormatToCreateAS400File(ByVal oVal As Object, ByVal eDataType As enumDataType, ByVal iLength As Integer, Optional ByVal iDecimal As Integer = 0, Optional ByVal bEvaluateAtNextDecimal As Boolean = False) As String
        'if type is oDecimal...iDecimal and bEvaluateAtnNextDecimal are important
        'bEvaluateAtNextDecimal คือ ให้คิดปัดเศษหรือไม่
        'iLength คือความยาวข้อมูลทั้งหมด
        Dim sVal As String = "", sErr As String = ""

        Select Case eDataType
            Case enumDataType.oInteger
                If Not IsNumeric(oVal) Then
                    sErr = "Value is not numeric!"
                    Exit Select
                End If
                If CStr(oVal).IndexOf(".") >= 0 Then
                    sErr = "Value should be integer, should not decimal"
                    Exit Select
                End If
            Case enumDataType.oDecimal
                If Not IsNumeric(oVal) Then
                    sErr = "Value is not numeric!"
                    Exit Select
                End If
                If CStr(oVal).IndexOf(".") < 0 Then
                    eDataType = enumDataType.oInteger
                End If
        End Select

        If sErr = "" Then
            Select Case eDataType
                Case enumDataType.oString
                    'Modify by jedsada 14-08-2552 เพิ่มเช็ค length ของ text
                    If CType(oVal, String).Length <= iLength Then
                        Return oVal & Space(iLength - CType(oVal, String).Length)
                    Else
                        Return CType(oVal, String).Substring(0, iLength)
                    End If
                Case enumDataType.oInteger
                    Return getRepeateCharacter("0", (iLength - CStr(oVal).Length) - iDecimal) & CStr(oVal) & getRepeateCharacter("0", iDecimal)
                Case enumDataType.oDecimal
                    Select Case bEvaluateAtNextDecimal
                        Case False
                            sVal = CType(oVal, String)
                            If sVal.Substring(sVal.IndexOf(".") + 1).Length <= iDecimal Then
                                sVal = CStr(Format(CDbl(oVal), "##." & Me.getRepeateCharacter("0", iDecimal)))
                            End If
                            sVal = sVal.Substring(0, sVal.IndexOf(".")) & sVal.Substring(sVal.IndexOf(".") + 1, iDecimal)
                            Return getRepeateCharacter("0", (iLength - sVal.Length)) & sVal
                        Case True
                            sVal = CStr(Format(CDbl(oVal), "##." & Me.getRepeateCharacter("0", iDecimal)))
                            sVal = Replace(sVal, ".", "")
                            Return getRepeateCharacter("0", (iLength - sVal.Length)) & sVal
                    End Select
            End Select
        Else
            MsgBox(sErr, MsgBoxStyle.Exclamation, "Exclamaion")
        End If
        Return ""
    End Function

#Region "File&Paht&Drive"
    Public Function getFileFromFileDialog(Optional ByVal extension As String = "",
                                          Optional ByVal initDir As String = "",
                                          Optional ByVal dirReturn As String = "") As String

        If initDir <> "" Then
            If Not Directory.Exists(initDir) Then
                MsgBox("ไม่มีไดเร็คทอรี่ " & initDir & "  นี้อยู่จริง !", MsgBoxStyle.Information, "Information")
            End If
        End If

        Dim oOpenFileDialog As New OpenFileDialog
        If initDir <> "" Then oOpenFileDialog.InitialDirectory = initDir
        Select Case extension
            Case ""
                oOpenFileDialog.Filter = "All files (*.*)|*.*"
                oOpenFileDialog.FilterIndex = 1
            Case Else
                oOpenFileDialog.DefaultExt = extension
                oOpenFileDialog.Filter = "Data file (." & extension & ")|*." & extension & "|All files (*.*)|*.*"
                oOpenFileDialog.FilterIndex = 1
        End Select

        If oOpenFileDialog.ShowDialog = DialogResult.OK Then
            Return oOpenFileDialog.FileName
        End If
        Return dirReturn
    End Function

    Public Function getPathFromFolderDialog(ByVal initDir As String,
                                            ByVal dirReturn As String) As String
        Dim oOpenFolderDialog As New FolderBrowserDialog
        oOpenFolderDialog.SelectedPath = initDir
        If oOpenFolderDialog.ShowDialog = DialogResult.OK Then
            Return oOpenFolderDialog.SelectedPath
        End If
        Return dirReturn
    End Function

    Public Function getPathFromFolderDialog() As String
        Dim oOpenFolderDialog As New FolderBrowserDialog
        If oOpenFolderDialog.ShowDialog = DialogResult.OK Then
            Return oOpenFolderDialog.SelectedPath
        End If
        Return ""
    End Function

    Public Function CopyFile(ByVal sFile As String, ByVal dFile As String, ByRef sErrmsg As String) As Boolean
        Try
            File.Copy(sFile, dFile, True)
            Return True
        Catch ex As Exception
            sErrmsg = ex.Message
            Return False
        End Try
        Return True
    End Function

    Public Function deleteAllFile(ByVal sFiles() As String) As Boolean
        Try
            For Each sFile As String In sFiles
                File.Delete(sFile)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function moveAllFile(ByVal sSource() As String, ByVal sDest() As String) As Boolean
        Try
            For i As Integer = 0 To sSource.Length - 1
                File.Move(sSource(i), sDest(i))
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function deleteFileAndBackup(ByVal sFilesPath() As String,
                                           ByVal sBackupPath As String) As Boolean
        Try
            Dim sFileName As String
            For Each sFile As String In sFilesPath
                sFileName = Path.GetFileName(sFile)
                File.Copy(sFile, sBackupPath & sFileName, True)
                File.Delete(sFile)
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function deleteFileAndBackup(ByVal sFilePath As String,
                                        ByVal sBackupPath As String) As Boolean
        Try
            Dim sFileName As String = Path.GetFileName(sFilePath)
            File.Copy(sFilePath, sBackupPath & sFileName, True)
            File.Delete(sFilePath)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Function isFileExist(ByVal sFile As String) As Boolean
        Dim IsReturn As Boolean = False
        If File.Exists(sFile) Then
            IsReturn = True
        End If
        Return IsReturn
    End Function

    Public Function isDriveExist(ByVal sDrive As String) As Boolean
        Dim myDrive As String
        Dim IsReturn As Boolean = False
        Select Case sDrive.Length
            Case Is > 1 : myDrive = sDrive.Substring(0, 1)
            Case Else : myDrive = sDrive
        End Select
        myDrive = myDrive & ":\"

        For Each _MyDrive As String In Directory.GetLogicalDrives
            If myDrive = _MyDrive Then IsReturn = True
        Next
        Return IsReturn
    End Function

    Public Function getDataLineFromFile(ByVal sReadedFilePath As String,
                                        ByVal iLineNo As Integer) As String
        Dim sr As StreamReader
        Dim oDataLine As String
        Dim sWritedOutput As String = ""
        Dim iCountLine As Integer = 0

        sr = New StreamReader(sReadedFilePath, System.Text.Encoding.GetEncoding("windows-874"))  'File.OpenText(sReadedFilePath)
        Do While sr.Peek() >= 0
            oDataLine = sr.ReadLine()
            If iCountLine = iLineNo Then Return oDataLine
            iCountLine += 1
        Loop
        sr.Close()
        Return sWritedOutput
    End Function

    Public Function getStringFromDataline(ByVal sSource As String,
                                          ByVal firstCharPos As Integer,
                                          Optional ByVal length As Integer = -1) As String
        'start count posiition from 0
        Try
            If firstCharPos + length > sSource.Length Then
                Return sSource.Substring(firstCharPos)
            Else
                Return sSource.Substring(firstCharPos, length)
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function replaceStringInDataline(ByVal sSource As String,
                                ByVal iStartPos As Integer,
                                ByVal iLen As Integer,
                                ByVal sReplaceWithValue As String) As String
        'start count posiition from 0
        Dim sResult As String
        Try
            sResult = sSource.Substring(0, iStartPos)
            sResult &= sReplaceWithValue
            sResult &= sSource.Substring(sResult.Length)
            Return sResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function replaceStringInDataline(ByVal sSource As String,
                                ByVal iStartPos As Integer,
                                ByVal iLen As Integer,
                                ByVal sOldChar As String,
                                ByVal sNewChar As String) As String
        'start count posiition from 0
        Dim sResult As String
        Try

            sResult = sSource.Substring(0, iStartPos)
            sResult &= sSource.Substring(iStartPos, iLen).Replace(sOldChar, sNewChar)
            sResult &= sSource.Substring(sResult.Length)
            Return sResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub getAppendFile(ByVal sReadedFilePath As String,
                             ByRef sWritedOutput As String,
                             ByVal enumSparator As enumAppendFileSeparator,
                             Optional ByVal bWriteOutPutisFile As Boolean = True)
        Dim sr As StreamReader
        Dim oDataLine As String
        Dim nDataLine As String = ""

        If bWriteOutPutisFile Then
            'output is output path file
            Dim sw As StreamWriter
            If Not File.Exists(sWritedOutput) Then
                sw = File.CreateText(sWritedOutput)
                sw.Close()
            End If

            sw = New StreamWriter(sWritedOutput, False, System.Text.Encoding.GetEncoding("windows-874"))    'File.AppendText(sAppendedFilePath)
            sr = New StreamReader(sReadedFilePath, System.Text.Encoding.GetEncoding("windows-874"))  'File.OpenText(sReadedFilePath)
            Do While sr.Peek() >= 0
                oDataLine = sr.ReadLine()
                Select Case enumSparator
                    Case enumAppendFileSeparator.comma : nDataLine &= oDataLine & ","
                    Case enumAppendFileSeparator.hyphen : nDataLine &= oDataLine & "-"
                    Case enumAppendFileSeparator.newline : nDataLine &= oDataLine & vbCrLf
                    Case enumAppendFileSeparator.space : nDataLine &= oDataLine & " "
                    Case enumAppendFileSeparator.blank : nDataLine &= oDataLine
                End Select
            Loop

            Select Case enumSparator
                Case enumAppendFileSeparator.comma : nDataLine = nDataLine.Remove(nDataLine.Length - 1, 1)
                Case enumAppendFileSeparator.hyphen : nDataLine = nDataLine.Remove(nDataLine.Length - 1, 1)
                Case enumAppendFileSeparator.newline : nDataLine = nDataLine.Remove(nDataLine.LastIndexOf(vbCrLf), nDataLine.Length - nDataLine.LastIndexOf(vbCrLf))
                Case enumAppendFileSeparator.space : nDataLine = nDataLine.Remove(nDataLine.Length - 1, 1)
            End Select

            sw.Write(nDataLine)
            sr.Close()
            sw.Close()
        Else
            'output is data line string
            sr = New StreamReader(sReadedFilePath, System.Text.Encoding.GetEncoding("windows-874"))  'File.OpenText(sReadedFilePath)
            Do While sr.Peek() >= 0
                oDataLine = sr.ReadLine()
                Select Case enumSparator
                    Case enumAppendFileSeparator.comma : nDataLine &= oDataLine & ","
                    Case enumAppendFileSeparator.hyphen : nDataLine &= oDataLine & "-"
                    Case enumAppendFileSeparator.newline : nDataLine &= oDataLine & vbCrLf
                    Case enumAppendFileSeparator.space : nDataLine &= oDataLine & " "
                    Case enumAppendFileSeparator.blank : nDataLine &= oDataLine
                End Select
            Loop

            Select Case enumSparator
                Case enumAppendFileSeparator.comma : nDataLine = nDataLine.Remove(nDataLine.Length - 1, 1)
                Case enumAppendFileSeparator.hyphen : nDataLine = nDataLine.Remove(nDataLine.Length - 1, 1)
                Case enumAppendFileSeparator.newline : nDataLine = nDataLine.Remove(nDataLine.LastIndexOf(vbCrLf), nDataLine.Length - nDataLine.LastIndexOf(vbCrLf))
                Case enumAppendFileSeparator.space : nDataLine = nDataLine.Remove(nDataLine.Length - 1, 1)
            End Select

            sr.Close()

            sWritedOutput = nDataLine
        End If

    End Sub


    Public Sub fileNewLineByLength(ByRef sOutput As String,
                               ByVal sSource As String,
                               ByVal iLength As Integer,
                               Optional ByVal bSourceIsFile As Boolean = False,
                               Optional ByVal bsOutputIsData As Boolean = False)
        'sSource can be 
        '   1.  single line 's data
        '   2.  file that has only one line 's data

        Dim sDataOutput As String = ""
        Dim sDataInput As String = ""
        Dim sw As StreamWriter = Nothing
        Try
            If bsOutputIsData = False Then
                sw = New StreamWriter(sOutput, False, System.Text.Encoding.GetEncoding("windows-874"))
            End If

            If bSourceIsFile Then
                Dim sr As New StreamReader(sSource, System.Text.Encoding.GetEncoding("windows-874"))
                If sr.Peek >= 0 Then sDataInput = sr.ReadLine()
                sr.Close()
            Else
                sDataInput = sSource
            End If

            For iLen As Integer = 0 To sDataInput.Length - 1 Step +iLength
                If sDataInput.Length - iLen < iLength Then
                    sDataOutput &= sDataInput.Substring(iLen, sDataInput.Length - iLen)
                Else
                    sDataOutput &= sDataInput.Substring(iLen, iLength) & vbCrLf
                End If
            Next

            If bsOutputIsData = False Then
                sw.Write(sDataOutput)
                sw.Close()
            Else
                sOutput = sDataOutput.Replace(Chr(10), "")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function setDatalineLength(ByVal sLineData As String, ByVal iLength As Integer) As String
        'fix data ที่ส่งมาให้มี length ตามที่กำหนดด้วยการเติม space เข้าไปตามหลังจนเต็ม
        Dim sResult As String = ""
        Try
            If sLineData.Length = iLength Then
                sResult = sLineData
            ElseIf sLineData.Length < iLength Then
                sResult = sLineData & Space(iLength - sLineData.Length)
            ElseIf sLineData.Length > iLength Then
                sResult = sLineData.Substring(0, iLength)
            End If
            Return sResult
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function setDataLength(ByVal sFileInput As String, ByVal iLength As Integer) As Boolean
        'fix ทั้งไฟล์ให้มี length ตามที่กำหนดด้วยการเติม space เข้าไปตามหลังจนเต็ม
        Dim oDataLine As String
        Dim nDataLine As String = ""
        Dim sr As New StreamReader(sFileInput, System.Text.Encoding.GetEncoding("windows-874"))
        Dim IsReturn As Boolean = False
        Try
            'read data and change length to 320
            Do While sr.Peek >= 0
                oDataLine = sr.ReadLine()
                If oDataLine.Length < iLength Then
                    nDataLine &= oDataLine & Space(iLength - oDataLine.Length) & vbCrLf
                Else
                    nDataLine &= oDataLine & vbCrLf
                End If
            Loop
            sr.Close()

            'write data
            If nDataLine <> "" Then
                Dim sw As StreamWriter = New StreamWriter(sFileInput, False, System.Text.Encoding.GetEncoding("windows-874"))
                sw.Write(nDataLine)
                sw.Close()
            End If

            IsReturn = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return IsReturn
    End Function

    Public Function setFileToNoBlankRecord(ByVal sFileInput As String) As Boolean
        Dim oDataLine As String
        Dim nDataLine As String = ""
        Dim iLengthOfBlank As Integer = 0
        Dim IsReturn As Boolean = False
        Try
            Dim bFirstLine As Boolean = True
            Dim sr As New StreamReader(sFileInput, System.Text.Encoding.GetEncoding("windows-874"))
            'read data
            Do While sr.Peek() >= 0
                oDataLine = sr.ReadLine()
                If oDataLine.Length <> iLengthOfBlank Then
                    If bFirstLine Then
                        bFirstLine = False
                    Else
                        If oDataLine <> "" Then nDataLine &= vbCrLf
                    End If
                    nDataLine &= oDataLine
                End If
            Loop
            sr.DiscardBufferedData()
            sr.Close()

            'write data
            If nDataLine <> "" Then
                Dim sw As StreamWriter = New StreamWriter(sFileInput, False, System.Text.Encoding.GetEncoding("windows-874"))
                sw.Write(nDataLine)
                sw.Close()
            End If

            IsReturn = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return IsReturn
    End Function

    'Modify by Jedsada 17-08-2552
    'ฟังก็ชั่นนี้รับสตริงที่มีหลายหลายบรรทัดเข้าเพื่อตรวจสอบว่าบรรทัดไหนเป็น blank แล้วจะตัดทิ้งให้
    Public Function setStringToNoBlankRecord(ByVal sInput As String) As String
        Dim sResult As String = ""
        Dim sResult1 As String = ""
        Dim sIns() As String
        Dim sIns1() As String
        Dim sBuf As String = ""
        Try
            sIns = sInput.Split(vbCrLf)
            'loop แรกกำจัด blank ณ line ที่ไม่ใช่บรรทัดสุดท้ายก่อน  เพื่อให้เหลือบรรทัดสุดท้ายเท่านั้น
            For i As Integer = 0 To sIns.Length - 1
                sBuf = Replace(sIns(i), Chr(10), "")
                If sBuf <> "" Then sResult += sBuf & vbCrLf
            Next

            'loop นี้จะกำจัด blank ณ บรรทัดสุดท้าย
            sIns1 = sResult.Split(vbCrLf)
            For i As Integer = 0 To sIns1.Length - 1
                sBuf = Replace(sIns1(i), Chr(10), "")
                If i = sIns1.Length - 2 Then Return sResult1 & sBuf
                sResult1 += sBuf & vbCrLf
            Next
            Return sResult1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function WriteToFile(ByVal sFilePath As String,
                                ByVal sData As String,
                                ByVal isReplace As Boolean,
                                Optional ByVal isAppendNotMerge As Boolean = False) As Boolean
        Dim IsReturn As Boolean = False
        Try
            Dim fs As FileStream
            Dim info As Byte()

            If isReplace Then
                fs = New FileStream(sFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)
            Else
                If Not isAppendNotMerge Then sData = vbCrLf & sData
                fs = New FileStream(sFilePath, FileMode.Append, FileAccess.Write, FileShare.None)
            End If

            'info = New System.Text.UTF8Encoding(True).GetBytes(sData)
            info = System.Text.Encoding.GetEncoding("windows-874").GetBytes(sData)
            fs.Write(info, 0, info.Length)
            fs.Close()
            IsReturn = True
        Catch ex As Exception
            MsgBox("Error WriteToFile of file path " & sFilePath, MsgBoxStyle.Critical)
            Throw ex
        End Try
        Return IsReturn
    End Function

    Public Function WriteToFile(ByVal sFilePath As String,
                                ByVal sData As String) As Boolean
        Dim IsReturn As Boolean = False
        Try
            Dim sw As StreamWriter = New StreamWriter(sFilePath, False, System.Text.Encoding.GetEncoding("windows-874"))

            sw.Write(sData)
            sw.Close()
            IsReturn = True
        Catch ex As Exception
            Throw ex
        End Try
        Return IsReturn
    End Function

    Public Sub CreateFolderAndFileIfNotExist(ByVal sWritedOutput As String)
        Try
            Dim folderName As String = sWritedOutput.Substring(0, sWritedOutput.LastIndexOf("\"))
            If Not Directory.Exists(folderName) Then Directory.CreateDirectory(sWritedOutput.Substring(0, sWritedOutput.LastIndexOf("\")))
            CreateFileIfNotExist(sWritedOutput)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub CreateFolderIfNotExist(ByVal sWritedOutput As String)
        Try
            Dim folderName As String = sWritedOutput.Substring(0, sWritedOutput.LastIndexOf("\"))
            If Not Directory.Exists(folderName) Then Directory.CreateDirectory(sWritedOutput.Substring(0, sWritedOutput.LastIndexOf("\")))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CreateFolderifNotExist(ByVal sFolderOutput As String, ByRef sErrmsg As String) As Boolean
        Try
            Dim folderName As String = sFolderOutput.Substring(0, sFolderOutput.LastIndexOf("\"))
            If Not Directory.Exists(folderName) Then Directory.CreateDirectory(sFolderOutput.Substring(0, sFolderOutput.LastIndexOf("\")))
            Return True
            ' CreateFileIfNotExist(sWritedOutput)
        Catch ex As Exception
            sErrmsg = ex.Message
            Return False
        End Try
    End Function

    Public Sub CreateFolderifNotExist(ByVal sFolderOutput As String, ByVal bHasFileName As Boolean)
        Try
            If bHasFileName Then
                Dim folderName As String = sFolderOutput.Substring(0, sFolderOutput.LastIndexOf("\"))
                If Not Directory.Exists(folderName) Then Directory.CreateDirectory(sFolderOutput.Substring(0, sFolderOutput.LastIndexOf("\")))
            Else
                If Not Directory.Exists(sFolderOutput) Then Directory.CreateDirectory(sFolderOutput)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub CreateFileIfNotExist(ByVal sWritedOutput As String)
        Try
            Dim sw As StreamWriter
            If Not File.Exists(sWritedOutput) Then
                sw = File.CreateText(sWritedOutput)
                sw.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function getMaxLines(ByVal sFilePath As String) As Integer
        Dim iLen As Integer = 0
        Try
            Dim sr As New StreamReader(sFilePath, System.Text.Encoding.GetEncoding("windows-874"))
            'read data
            Do While sr.Peek() >= 0
                sr.ReadLine() : iLen += 1
            Loop
            sr.DiscardBufferedData()
            sr.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return iLen
    End Function
#End Region


#Region "DATE FUNCTION"

    Private Declare Function GetSystemDefaultLCID Lib "kernel32" () As Integer
    Private Const LOCALE_SSHORTDATE As Short = &H1FS ' short date format string
    Private Declare Function GetLocaleInfo Lib "kernel32" Alias "GetLocaleInfoA" (ByVal Locale As Integer, ByVal LCType As Integer, ByVal lpLCData As String, ByVal cchData As Integer) As Integer

    Public Function getLocalDateFormat() As String
        getLocalDateFormat = ""
        Try
            Dim sReturn As String = ""
            Dim LCID As Integer = GetSystemDefaultLCID()
            Dim r As Integer = GetLocaleInfo(LCID, LOCALE_SSHORTDATE, sReturn, Len(sReturn))
            If r Then
                sReturn = Space(r)
                r = GetLocaleInfo(LCID, LOCALE_SSHORTDATE, sReturn, Len(sReturn))
                Return Microsoft.VisualBasic.Left(sReturn, r - 1)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getLocalDateStyle() As gEnumDateStyle
        Dim iYear As Integer
        Dim egEnumDateStyle As gEnumDateStyle = gEnumDateStyle.Christ
        Select Case Me.getLocalDateFormat
            Case "dd/MM/yyyy", "MM/dd/yyyy"
                iYear = Now.ToShortDateString.Substring(6, 4)
                If iYear > 2500 Then egEnumDateStyle = gEnumDateStyle.Budd
            Case "dd/MM/yy", "MM/dd/yy"
                iYear = Now.ToShortDateString.Substring(6, 2)
                If iYear >= 50 Then egEnumDateStyle = gEnumDateStyle.Budd
            Case "yyyy/MM/dd", "yyyy/dd/MM"
                iYear = Now.ToShortDateString.Substring(0, 4)
                If iYear > 2500 Then egEnumDateStyle = gEnumDateStyle.Budd
            Case "yy/dd/MM", "yy/MM/dd"
                iYear = Now.ToShortDateString.Substring(0, 2)
                If iYear >= 50 Then egEnumDateStyle = gEnumDateStyle.Budd
        End Select
        Return egEnumDateStyle
    End Function

    Public Function getLocalYear() As String
        getLocalYear = ""
        Select Case Me.getLocalDateFormat
            Case "dd/MM/yyyy", "MM/dd/yyyy", "d MMM yyyy", "MMM d yyyy"
                Return Now.ToShortDateString.Substring(6, 4)
            Case "dd/MM/yy", "MM/dd/yy"
                Return Now.ToShortDateString.Substring(6, 2)
            Case "yyyy/MM/dd", "yyyy/dd/MM", "yyyy-MM-dd"
                Return Now.ToShortDateString.Substring(0, 4)
            Case "yy/dd/MM", "yy/MM/dd"
                Return Now.ToShortDateString.Substring(0, 2)
            Case "d/M/yyyy", "M/d/yyyy"
                Return Now.ToShortDateString.Substring(4, 4)
            Case "d/M/yy", "M/d/yy"
                Return Now.ToShortDateString.Substring(4, 2)
            Case "dd MMM yyyy", "MMM dd yyyy"
                Return Now.ToShortDateString.Substring(7, 4)
        End Select
    End Function

    Public Function DateUs(ByVal Dt As Date) As String
        ' formats a date for use in a SQL command
        DateUs = DateDelimiter & Format(Dt, "MM/dd/yyyy") & DateDelimiter
    End Function

    Public Function DateTimeUs(ByVal Dt As Date) As String
        ' formats a date for use in a SQL command
        DateTimeUs = DateDelimiter & Format(Dt, "MM/dd/yyyy HH:mm") & DateDelimiter
    End Function

    Public Property DateDelimiter() As String
        Get
            Return m_sDateDelimiter
        End Get
        Set(ByVal Value As String)
            m_sDateDelimiter = Value
        End Set
    End Property

    Public Function GetformatDate(ByVal p_text As String, Optional ByVal DayVal As Integer = 0) As String
        GetformatDate = ""
        Try
            p_text = p_text.ToUpper()
            If InStr(p_text, "[DD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DD]", Format(System.DateTime.Now.AddDays(DayVal), "dd"))
            End If
            If InStr(p_text, "[MM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MM]", Format(System.DateTime.Now.AddDays(DayVal), "MM"))
            End If
            If InStr(p_text, "[YY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YY]", Format(System.DateTime.Now.AddDays(DayVal), "yy"))
            End If
            If InStr(p_text, "[YYYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYYY]", Format(System.DateTime.Now.AddDays(DayVal), "yyyy"))
            End If

            If InStr(p_text, "[DDMM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DDMM]", Format(System.DateTime.Now.AddDays(DayVal), "ddMM"))
            End If
            If InStr(p_text, "[MMDD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MMDD]", Format(System.DateTime.Now.AddDays(DayVal), "MMdd"))
            End If
            If InStr(p_text, "[DDYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DDYY]", Format(System.DateTime.Now.AddDays(DayVal), "ddyy"))
            End If
            If InStr(p_text, "[YYDD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYDD]", Format(System.DateTime.Now.AddDays(DayVal), "yydd"))
            End If

            If InStr(p_text, "[DDYYYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DDYYYY]", Format(System.DateTime.Now.AddDays(DayVal), "ddyyyy"))
            End If
            If InStr(p_text, "[YYYYDD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYYYDD]", Format(System.DateTime.Now.AddDays(DayVal), "yyyydd"))
            End If

            If InStr(p_text, "[MMYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MMYY]", Format(System.DateTime.Now.AddDays(DayVal), "MMyy"))
            End If
            If InStr(p_text, "[YYMM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYMM]", Format(System.DateTime.Now.AddDays(DayVal), "yyMM"))
            End If

            If InStr(p_text, "[MMYYYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MMYYYY]", Format(System.DateTime.Now.AddDays(DayVal), "MMyyyy"))
            End If
            If InStr(p_text, "[YYYYMM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYYYMM]", Format(System.DateTime.Now.AddDays(DayVal), "yyyyMM"))
            End If

            If InStr(p_text, "[DDMMYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DDMMYY]", Format(System.DateTime.Now.AddDays(DayVal), "ddMMyy"))
            End If
            If InStr(p_text, "[DDYYMM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DDYYMM]", Format(System.DateTime.Now.AddDays(DayVal), "ddyyMM"))
            End If
            If InStr(p_text, "[MMDDYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MMDDYY]", Format(System.DateTime.Now.AddDays(DayVal), "MMddyy"))
            End If
            If InStr(p_text, "[MMYYDD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MMYYDD]", Format(System.DateTime.Now.AddDays(DayVal), "MMyydd"))
            End If
            If InStr(p_text, "[YYDDMM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYDDMM]", Format(System.DateTime.Now.AddDays(DayVal), "yyddMM"))
            End If
            If InStr(p_text, "[YYMMDD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYMMDD]", Format(System.DateTime.Now.AddDays(DayVal), "yyMMdd"))
            End If

            If InStr(p_text, "[DDMMYYYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DDMMYYYY]", Format(System.DateTime.Now.AddDays(DayVal), "ddMMyyyy"))
            End If
            If InStr(p_text, "[DDYYYYMM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[DDYYYYMM]", Format(System.DateTime.Now.AddDays(DayVal), "ddyyyyMM"))
            End If
            If InStr(p_text, "[MMDDYYYY]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MMDDYYYY]", Format(System.DateTime.Now.AddDays(DayVal), "MMddyyyy"))
            End If
            If InStr(p_text, "[MMYYYYDD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[MMYYYYDD]", Format(System.DateTime.Now.AddDays(DayVal), "MMyyyydd"))
            End If
            If InStr(p_text, "[YYYYDDMM]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYYYDDMM]", Format(System.DateTime.Now.AddDays(DayVal), "yyyyddMM"))
            End If
            If InStr(p_text, "[YYYYMMDD]", CompareMethod.Text) > 0 Then
                p_text = p_text.Replace("[YYYYMMDD]", Format(System.DateTime.Now.AddDays(DayVal), "yyyyMMdd"))
            End If

            Return p_text
        Catch ex As Exception
            MsgBox("Error GetformatDate of input " & p_text, MsgBoxStyle.Critical)
            Throw ex
        End Try
    End Function

    Public Function getDateYYYYMMDD(ByVal sSourceDate As String,
                                   Optional ByVal dateSourceFormat As gEnumDateFormat = gEnumDateFormat.ddMMyyyy,
                                   Optional ByVal dateSourceStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                   Optional ByVal dateOutputStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                   Optional ByVal sSymbol As String = "") As String

        Dim sYYYYMMDD As String = ""
        Select Case dateSourceFormat
            Case gEnumDateFormat.ddMMyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(4, 2), dateSourceStyle, dateOutputStyle) & sSourceDate.Substring(2, 2) & sSourceDate.Substring(0, 2)
            Case gEnumDateFormat.ddMMyyyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(4, 4), dateSourceStyle, dateOutputStyle) & sSourceDate.Substring(2, 2) & sSourceDate.Substring(0, 2)
            Case gEnumDateFormat.dMyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle) & Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00")
            Case gEnumDateFormat.dMyyyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle) & Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00")
            Case gEnumDateFormat.Mdyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle) & Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00")
            Case gEnumDateFormat.Mdyyyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle) & Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00")
            Case gEnumDateFormat.MMddyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(4, 2), dateSourceStyle, dateOutputStyle) & Format(sSourceDate.Substring(0, 2), "00") & Format(sSourceDate.Substring(2, 2), "00")
            Case gEnumDateFormat.MMddyyyy : sYYYYMMDD = getYYYY(sSourceDate.Substring(4, 4), dateSourceStyle, dateOutputStyle) & sSourceDate.Substring(0, 2) & sSourceDate.Substring(2, 2)
            Case gEnumDateFormat.yyyyMMdd : sYYYYMMDD = sSourceDate
        End Select

        Select Case sSymbol
            Case "" : Return sYYYYMMDD
            Case Else : Return sYYYYMMDD.Substring(0, 2) & sSymbol & sYYYYMMDD.Substring(2, 2) & sSymbol & sYYYYMMDD.Substring(4, 4)
        End Select

    End Function

    Public Function getDateDDMMYYYY(ByVal sSourceDate As String,
                                    Optional ByVal dateSourceFormat As gEnumDateFormat = gEnumDateFormat.ddMMyyyy,
                                    Optional ByVal dateSourceStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal dateOutputStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal sSymbol As String = "") As String

        Dim sDDMMYYYY As String = ""
        Select Case dateSourceFormat
            Case gEnumDateFormat.ddMMyy : sDDMMYYYY = sSourceDate.Substring(0, 2) & sSourceDate.Substring(2, 2) & getYYYY(sSourceDate.Substring(4, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.ddMMyyyy : sDDMMYYYY = sSourceDate.Substring(0, 2) & sSourceDate.Substring(2, 2) & getYYYY(sSourceDate.Substring(4, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.dMyy : sDDMMYYYY = Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00") & getYYYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.dMyyyy : sDDMMYYYY = Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00") & getYYYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.Mdyy : sDDMMYYYY = Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00") & getYYYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.Mdyyyy : sDDMMYYYY = Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00") & getYYYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.MMddyy : sDDMMYYYY = Format(sSourceDate.Substring(2, 2), "00") & Format(sSourceDate.Substring(0, 2), "00") & getYYYY(sSourceDate.Substring(4, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.MMddyyyy : sDDMMYYYY = sSourceDate
            Case gEnumDateFormat.yyyyMMdd : sDDMMYYYY = sSourceDate.Substring(6, 2) & sSourceDate.Substring(4, 2) & getYYYY(sSourceDate.Substring(0, 4), dateSourceStyle, dateOutputStyle)
        End Select

        Select Case sSymbol
            Case "" : Return sDDMMYYYY
            Case Else : Return sDDMMYYYY.Substring(0, 2) & sSymbol & sDDMMYYYY.Substring(2, 2) & sSymbol & sDDMMYYYY.Substring(4, 4)
        End Select

    End Function



    Public Function getDateDDMMYY(ByVal sSourceDate As String,
                                  Optional ByVal dateSourceFormat As gEnumDateFormat = gEnumDateFormat.ddMMyyyy,
                                  Optional ByVal dateSourceStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                  Optional ByVal dateOutputStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                  Optional ByVal sSymbol As String = "") As String

        Dim sDDMMYY As String = ""
        Select Case dateSourceFormat
            Case gEnumDateFormat.ddMMyy : sDDMMYY = sSourceDate
            Case gEnumDateFormat.ddMMyyyy : sDDMMYY = sSourceDate.Substring(0, 2) & sSourceDate.Substring(2, 2) & getYY(sSourceDate.Substring(4, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.dMyy : sDDMMYY = Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00") & getYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.dMyyyy : sDDMMYY = Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00") & getYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.Mdyy : sDDMMYY = Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00") & getYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.Mdyyyy : sDDMMYY = Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00") & getYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.MMddyy : sDDMMYY = Format(sSourceDate.Substring(2, 2), "00") & Format(sSourceDate.Substring(0, 2), "00") & getYY(sSourceDate.Substring(4, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.MMddyyyy : sDDMMYY = sSourceDate.Substring(2, 2) & sSourceDate.Substring(0, 2) & getYY(sSourceDate.Substring(4, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.yyyyMMdd : sDDMMYY = sSourceDate.Substring(6, 2) & sSourceDate.Substring(4, 2) & getYY(sSourceDate.Substring(0, 4), dateSourceStyle, dateOutputStyle)
        End Select

        Select Case sSymbol
            Case "" : Return sDDMMYY
            Case Else : Return sDDMMYY.Substring(0, 2) & sSymbol & sDDMMYY.Substring(2, 2) & sSymbol & sDDMMYY.Substring(4, 2)
        End Select

    End Function

    Public Function getDateDDMMYYYY(ByVal dDate As Date,
                                    Optional ByVal dateSourceStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal dateOutputStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal sSymbol As String = "") As String
        'DDMMYYYY
        Dim sYear As String = Me.getYYYY(Me.getLocalYear, dateSourceStyle, dateOutputStyle)
        Select Case sSymbol
            Case "" : Return Format(dDate.Day, "00") & Format(dDate.Month, "00") & sYear
            Case Else : Return Format(dDate.Day, "00") & sSymbol & Format(dDate.Month, "00") & sSymbol & sYear
        End Select
    End Function

    Public Function getDateCristDDMMYYYY(ByVal ddate As Date) As String
        Dim yyyy As String
        Dim mm As String
        Dim dd As String
        yyyy = getDateSQLStandardFormat(Now).Split("-")(0)
        mm = getDateSQLStandardFormat(Now).Split("-")(1)
        dd = getDateSQLStandardFormat(Now).Split("-")(2)
        Return dd & mm & yyyy
    End Function

    Public Function getDateCristDDMMYY(ByVal ddate As Date) As String
        Dim yy As String
        Dim mm As String
        Dim dd As String
        yy = getDateSQLStandardFormat(Now).Split("-")(0).Substring(2, 2)
        mm = getDateSQLStandardFormat(Now).Split("-")(1)
        dd = getDateSQLStandardFormat(Now).Split("-")(2)
        Return dd & mm & yy
    End Function

    Public Function getDateCristMMDDYYYY(ByVal ddate As Date) As String
        Dim yyyy As String
        Dim mm As String
        Dim dd As String
        yyyy = getDateSQLStandardFormat(Now).Split("-")(0)
        mm = getDateSQLStandardFormat(Now).Split("-")(1)
        dd = getDateSQLStandardFormat(Now).Split("-")(2)
        Return mm & dd & yyyy
    End Function

    Public Function getDateCristMMDDYY(ByVal ddate As Date) As String
        Dim yy As String
        Dim mm As String
        Dim dd As String
        yy = getDateSQLStandardFormat(Now).Split("-")(0).Substring(2, 2)
        mm = getDateSQLStandardFormat(Now).Split("-")(1)
        dd = getDateSQLStandardFormat(Now).Split("-")(2)
        Return mm & dd & yy
    End Function

    Public Function getDateDDMMYY(ByVal dDate As Date,
                                    Optional ByVal dateSourceStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal dateOutputStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal sSymbol As String = "") As String
        'DDMMYY
        Dim sYear As String = Me.getYY(Me.getLocalYear, dateSourceStyle, dateOutputStyle)
        Select Case sSymbol
            Case "" : Return Format(dDate.Day, "00") & Format(dDate.Month, "00") & sYear
            Case Else : Return Format(dDate.Day, "00") & sSymbol & Format(dDate.Month, "00") & sSymbol & sYear
        End Select
    End Function

    Public Function getDateMMDDYYYY(ByVal sSourceDate As String,
                                    Optional ByVal dateSourceFormat As gEnumDateFormat = gEnumDateFormat.ddMMyyyy,
                                    Optional ByVal dateSourceStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal dateOutputStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal sSymbol As String = "") As String

        Dim sMMDDYYYY As String = ""
        Select Case dateSourceFormat
            Case gEnumDateFormat.ddMMyy : sMMDDYYYY = sSourceDate.Substring(2, 2) & sSourceDate.Substring(0, 2) & getYYYY(sSourceDate.Substring(4, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.ddMMyyyy : sMMDDYYYY = sSourceDate.Substring(2, 2) & sSourceDate.Substring(0, 2) & getYYYY(sSourceDate.Substring(4, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.dMyy : sMMDDYYYY = Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00") & getYYYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.dMyyyy : sMMDDYYYY = Format(sSourceDate.Substring(1, 1), "00") & Format(sSourceDate.Substring(0, 1), "00") & getYYYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.Mdyy : sMMDDYYYY = Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00") & getYYYY(sSourceDate.Substring(2, 2), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.Mdyyyy : sMMDDYYYY = Format(sSourceDate.Substring(0, 1), "00") & Format(sSourceDate.Substring(1, 1), "00") & getYYYY(sSourceDate.Substring(2, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.MMddyy : sMMDDYYYY = Format(sSourceDate.Substring(0, 2), "00") & Format(sSourceDate.Substring(2, 2), "00") & getYYYY(sSourceDate.Substring(4, 4), dateSourceStyle, dateOutputStyle)
            Case gEnumDateFormat.MMddyyyy : sMMDDYYYY = sSourceDate
            Case gEnumDateFormat.yyyyMMdd : sMMDDYYYY = sSourceDate.Substring(4, 2) & sSourceDate.Substring(6, 2) & getYYYY(sSourceDate.Substring(0, 4), dateSourceStyle, dateOutputStyle)
        End Select

        Select Case sSymbol
            Case "" : Return sMMDDYYYY
            Case Else : Return sMMDDYYYY.Substring(0, 2) & sSymbol & sMMDDYYYY.Substring(2, 2) & sSymbol & sMMDDYYYY.Substring(4, 4)
        End Select

    End Function

    Public Function getDateMMDDYYYY(ByVal dDate As Date,
                                    Optional ByVal dateSourceStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal dateOutputStyle As gEnumDateStyle = gEnumDateStyle.Christ,
                                    Optional ByVal sSymbol As String = "") As String
        'MMDDYYYY
        Dim sYear As String = Me.getYY(Me.getLocalYear, dateSourceStyle, dateOutputStyle)

        Select Case sSymbol
            Case "" : Return Format(dDate.Day, "00") & Format(dDate.Month, "00") & sYear
            Case Else : Return Format(dDate.Day, "00") & sSymbol & Format(dDate.Month, "00") & sSymbol & sYear
        End Select
    End Function

    Public Function getYYYY(ByVal sYear As String,
                             ByVal dtSourceStyle As gEnumDateStyle,
                             ByVal dtOutputStyle As gEnumDateStyle) As String
        getYYYY = ""
        Select Case dtSourceStyle
            Case gEnumDateStyle.Budd
                Select Case sYear.Length
                    Case 2
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim("25" & sYear)
                            Case gEnumDateStyle.Christ : Return Trim(Str(CInt(("25" & sYear) - 543)))
                        End Select
                    Case 4
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim(sYear)
                            Case gEnumDateStyle.Christ : Return Trim(Str(CInt(sYear) - 543))
                        End Select
                End Select
            Case gEnumDateStyle.Christ
                Select Case sYear.Length
                    Case 2
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim(Str(CInt("20" & sYear) + 543))
                            Case gEnumDateStyle.Christ : Return Trim("20" & sYear)
                        End Select
                    Case 4
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim(Str(CInt(sYear) + 543))
                            Case gEnumDateStyle.Christ : Return Trim(sYear)
                        End Select
                End Select
        End Select
    End Function

    'By jedsada 14-08-2552
    Public Function getYYYY(ByVal sYYYY As String, ByVal isCristToBudd As Boolean) As String
        'ถ้า isCristToBudd = True   แสดงว่า sYYYY ส่งมาเป็น Crist
        'ถ้า isCristToBudd = False   แสดงว่า sYYYY ส่งมาเป็น Budd
        Select Case isCristToBudd
            Case True : Return Int(sYYYY) + 543
            Case Else : Return Int(sYYYY) - 543
        End Select
    End Function

    Public Function getYY(ByVal sYear As String,
                         ByVal dtSourceStyle As gEnumDateStyle,
                         ByVal dtOutputStyle As gEnumDateStyle) As String
        getYY = ""
        Select Case dtSourceStyle
            Case gEnumDateStyle.Budd
                Select Case sYear.Length
                    Case 2
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim(sYear)
                            Case gEnumDateStyle.Christ : Return Trim(Str(CInt(("25" & sYear) - 543))).Substring(2, 2)
                        End Select
                    Case 4
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim(sYear).Substring(2, 2)
                            Case gEnumDateStyle.Christ : Return Trim(Str(CInt(sYear) - 543)).Substring(2, 2)
                        End Select
                End Select
            Case gEnumDateStyle.Christ
                Select Case sYear.Length
                    Case 2
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim(Str(CInt("20" & sYear) + 543)).Substring(2, 2)
                            Case gEnumDateStyle.Christ : Return Trim("20" & sYear).Substring(2, 2)
                        End Select
                    Case 4
                        Select Case dtOutputStyle
                            Case gEnumDateStyle.Budd : Return Trim(Str(CInt(sYear) + 543)).Substring(2, 2)
                            Case gEnumDateStyle.Christ : Return Trim(sYear).Substring(2, 2)
                        End Select
                End Select
        End Select
    End Function

    'By jedsada 14-08-2552
    Public Function getYY(ByVal sYYYY As String, ByVal isCristToBudd As Boolean) As String
        'ถ้า isCristToBudd = True   แสดงว่า sYYYY ส่งมาเป็น Crist
        'ถ้า isCristToBudd = False   แสดงว่า sYYYY ส่งมาเป็น Budd
        Select Case isCristToBudd
            Case True : Return CStr(Int(sYYYY) + 543).Substring(2, 2)
            Case Else : Return CStr(Int(sYYYY) - 543).Substring(2, 2)
        End Select
    End Function

    Public Function getDateSQLStandardFormat(ByVal dDate As Date) As String
        Return Format(dDate.Year) & "-" & Format(dDate.Month, "00") & "-" & Format(dDate.Day, "00")
    End Function

    Public Function getDateWithTimeSQLStandardFormat(ByVal dDate As Date) As String
        Return Format(dDate.Year) & "-" & Format(dDate.Month, "00") & "-" & Format(dDate.Day, "00") & " " & Format(Now.Hour, "00") & ":" & Format(Now.Minute, "00")
    End Function

    Public Function getDateWithTimeSQLStandardFormat(ByVal dDate As Date, ByVal sTime As String) As String
        'sTime = "hh:mm"
        Return Format(dDate.Year) & "-" & Format(dDate.Month, "00") & "-" & Format(dDate.Day, "00") & " " & sTime
    End Function


    Public Function getLastDayOfMonths(ByVal iMonth As Integer, ByVal dateSourceFormat As gEnumDateFormat) As String
        'christ only
        getLastDayOfMonths = ""
        Dim iMM As Integer = Now.AddMonths(iMonth).Month
        Dim iYYYY As Integer = Now.AddMonths(iMonth).Year
        Dim iDD As Integer = Date.DaysInMonth(iYYYY, iMM)

        Select Case dateSourceFormat
            Case gEnumDateFormat.ddMMyy
                Return Format(iDD, "00") & Format(iMM, "00") & iYYYY.ToString.Substring(2, 2)
            Case gEnumDateFormat.ddMMyyyy
                Return Format(iDD, "00") & Format(iMM, "00") & iYYYY
            Case gEnumDateFormat.MMddyy
                Return Format(iMM, "00") & Format(iDD, "00") & iYYYY.ToString.Substring(2, 2)
            Case gEnumDateFormat.MMddyyyy
                Return Format(iMM, "00") & Format(iDD, "00") & iYYYY
            Case gEnumDateFormat.yyyyMMdd
                Return iYYYY & Format(iMM, "00") & Format(iDD, "00")
            Case gEnumDateFormat.dMyy
                Return iDD & iMM & iYYYY.ToString.Substring(2, 2)
            Case gEnumDateFormat.dMyyyy
                Return iDD & iMM & iYYYY
            Case gEnumDateFormat.Mdyy
                Return iMM & iDD & iYYYY.ToString.Substring(2, 2)
            Case gEnumDateFormat.Mdyyyy
                Return iMM & iDD & iYYYY
        End Select
    End Function

    'By jedsada 14-08-2552
    Public Function getMonthNoFromStringMonth(ByVal sStringMonth As String,
                                              ByVal isThaiMonth As Boolean) As Integer
        Dim iMonth As Integer = -1
        Select Case isThaiMonth
            Case True
                Select Case sStringMonth
                    Case "ม.ค." : iMonth = 1
                    Case "มกราคม" : iMonth = 1
                    Case "ก.พ." : iMonth = 2
                    Case "กุมภาพันธ์" : iMonth = 2
                    Case "มี.ค." : iMonth = 3
                    Case "มีนาคม" : iMonth = 3
                    Case "เม.ย." : iMonth = 4
                    Case "เมษายน" : iMonth = 4
                    Case "พ.ค." : iMonth = 5
                    Case "พฤษภาคม" : iMonth = 5
                    Case "มิ.ย." : iMonth = 6
                    Case "มิถุนายน" : iMonth = 6
                    Case "ก.ค." : iMonth = 7
                    Case "กรกฎาคม" : iMonth = 7
                    Case "ส.ค." : iMonth = 8
                    Case "สิงหาคม" : iMonth = 8
                    Case "ก.ย." : iMonth = 9
                    Case "กันยายน" : iMonth = 9
                    Case "ต.ค." : iMonth = 10
                    Case "ตุลาคม" : iMonth = 10
                    Case "พ.ย." : iMonth = 11
                    Case "พฤศจิกายน" : iMonth = 11
                    Case "ธ.ค." : iMonth = 12
                    Case "ธันวาคม" : iMonth = 12
                End Select
            Case Else
                Select Case sStringMonth.ToUpper
                    Case "JAN" : iMonth = 1
                    Case "JANUARY" : iMonth = 1
                    Case "FEB" : iMonth = 2
                    Case "FEBRUARY" : iMonth = 2
                    Case "MAR" : iMonth = 3
                    Case "MARCH" : iMonth = 3
                    Case "APR" : iMonth = 4
                    Case "APRIL" : iMonth = 4
                    Case "MAY" : iMonth = 5
                    Case "JUN" : iMonth = 6
                    Case "JUNE" : iMonth = 6
                    Case "JUL" : iMonth = 7
                    Case "JULY" : iMonth = 7
                    Case "AUG" : iMonth = 8
                    Case "AUGUST" : iMonth = 8
                    Case "SEP" : iMonth = 9
                    Case "SEPTEMBER" : iMonth = 9
                    Case "OCT" : iMonth = 10
                    Case "OCTOBER" : iMonth = 10
                    Case "NOV" : iMonth = 11
                    Case "NOVEMBER" : iMonth = 11
                    Case "DEC" : iMonth = 12
                    Case "DECEMBER" : iMonth = 12
                End Select
        End Select
        Return iMonth
    End Function

#End Region

#Region "REGISTRY FUNCTION"

    'Public Property DB_File(ByVal Section As String, _
    '                        ByVal Key As String, _
    '                        ByVal [Default] As String, _
    '                        Optional ByVal sValue As String = "") As String
    '    Get
    '        clsINIFile.GetString(Section, Key, [Default])
    '    End Get
    '    Set(ByVal Value As String)
    '        clsINIFile.WriteString(Section, Key, sValue)
    '    End Set
    'End Property

    Public Property DB_File(ByVal RegistryPath As String, ByVal RegistryTitle As String) As String
        Get
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(RegistryPath)
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegistryPath, True)
            Return regKey.GetValue(RegistryTitle, "")
        End Get
        Set(ByVal Value As String)
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(RegistryPath)
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegistryPath, True)
            regKey.SetValue(RegistryTitle, Value)
        End Set
    End Property

    Public ReadOnly Property GetRegistryValue(ByVal RegistryPath As String,
                                              ByVal RegistryTitle As String,
                                              Optional ByVal sDef As String = "") As String
        Get
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(RegistryPath)
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegistryPath, True)
            Return regKey.GetValue(RegistryTitle, sDef)
        End Get
    End Property

    Public WriteOnly Property SetRegistryValue(ByVal RegistryPath As String, ByVal RegistryTitle As String) As String
        Set(ByVal Value As String)
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(RegistryPath)
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(RegistryPath, True)
            regKey.SetValue(RegistryTitle, Value)
        End Set
    End Property
#End Region

#Region "VALIDATE FUNCTION"
    Function CheckDate(ByVal dDate As Date, ByVal desc As String, Optional ByVal minDate As Date = #1/1/2000#, Optional ByVal maxDate As Date = #1/1/2100#) As Boolean
        If dDate < minDate Then
            MsgBox(desc & " cannot be earlier than " & minDate.ToString & ".")
            Return False
        End If
        If dDate > maxDate Then
            MsgBox(desc & " cannot be later than " & maxDate.ToString & ".")
            Return False
        End If
        Return True
    End Function

    Function FixNullValue(ByVal obj As Object, ByVal dflt As Object) As Object
        If obj Is Nothing Then Return dflt
        If obj Is DBNull.Value Then Return dflt
        Return obj
    End Function


    Function FixBlankValue(ByVal sValue As String, ByVal dflt As Object) As Object
        If sValue Is Nothing Then Return dflt
        If sValue.Trim = "" Then Return dflt
        Return sValue
    End Function
#End Region

    '#Region "FARPOINT"
    '    Public Sub setFPSelsectionPolicy(ByRef mySheet As FarPoint.Win.Spread.SheetView, ByVal SheetSelectionPolicy As FarPoint.Win.Spread.Model.SelectionPolicy)
    '        mySheet.SelectionPolicy = SheetSelectionPolicy
    '    End Sub

    '    Public Sub setFPColumnWidths(ByVal shtview As FarPoint.Win.Spread.SheetView, ByRef colw() As Integer)
    '        Dim i As Integer
    '        For i = 0 To (UBound(colw))
    '            If colw(i) = 0 Then
    '                shtview.Columns(i).Visible = False
    '            Else
    '                shtview.Columns(i).Width = colw(i)
    '            End If
    '        Next
    '        For i = (UBound(colw) + 1) To (shtview.Columns.Count - 1)
    '            shtview.Columns(i).Visible = False
    '        Next
    '    End Sub

    '    Public Sub setFPColumnNames(ByVal shtview As FarPoint.Win.Spread.SheetView, ByRef cName() As String)
    '        Dim i As Integer
    '        For i = 0 To (UBound(cName))
    '            If Len(cName(i)) > 0 Then shtview.ColumnHeader.Columns(i).Label = cName(i)
    '        Next
    '    End Sub

    '    Public Sub setButtonCellTypeText(ByRef fp As FarPoint.Win.Spread.SheetView, _
    '                                      ByVal iRow As Integer, _
    '                                      ByVal iCol As Integer, _
    '                                      ByVal sProjIcon As String, _
    '                                      ByRef iFontHeight As Integer)
    '        Dim oButtonCellType As New FarPoint.Win.Spread.CellType.ButtonCellType

    '        oButtonCellType.ButtonColor = System.Drawing.Color.White
    '        oButtonCellType.Text = sProjIcon
    '        fp.Cells(iRow, iCol).CellType = oButtonCellType
    '        iFontHeight = 30
    '    End Sub

    '    Public Sub setButtonCellTypeBackgroudImage(ByRef fp As FarPoint.Win.Spread.SheetView, _
    '                                              ByVal iRow As Integer, _
    '                                              ByVal iCol As Integer, _
    '                                              ByVal sProjIcon As String, _
    '                                              ByVal sProjName As String, _
    '                                              ByVal iFontHeight As Integer, _
    '                                              ByVal sDefaultIconPath As String)
    '        Dim oButtonCellType As New FarPoint.Win.Spread.CellType.ButtonCellType

    '        oButtonCellType.ButtonColor = System.Drawing.Color.White
    '        If Not File.Exists(sProjIcon) Then sProjIcon = sDefaultIconPath
    '        oButtonCellType.Picture = Image.FromFile(sProjIcon)
    '        oButtonCellType.Text = sProjName
    '        fp.Cells(iRow, iCol).CellType = oButtonCellType
    '        fp.SetRowHeight(iRow, Image.FromFile(sProjIcon).Height + iFontHeight)
    '    End Sub

    '#End Region

#Region "Keyboard"
    Public Sub setEnterToTab(ByVal crt As Control, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim frmName As Form
        Dim nextCrt As Control

        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            frmName = CType(crt.Parent, Form)
            nextCrt = CType(frmName.GetNextControl(crt, True), Control)
            nextCrt.Focus()
        ElseIf e.KeyChar = Microsoft.VisualBasic.ChrW(27) Then
            frmName = CType(crt.Parent, Form)
            frmName.Close()
        End If
    End Sub

    Public Sub setEnterToTab(ByVal crt As Control, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal parentType As enumParentType)
        Dim nextCrt As Control
        Dim objName As Object = Nothing

        Select Case parentType
            Case enumParentType.Form
                objName = CType(crt.Parent, Form)
            Case enumParentType.GroupBox
                objName = CType(crt.Parent, GroupBox)
            Case enumParentType.Panel
                objName = CType(crt.Parent, Panel)
        End Select

        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            nextCrt = CType(objName.GetNextControl(crt, True), Control)
            nextCrt.Focus()
        ElseIf e.KeyChar = Microsoft.VisualBasic.ChrW(27) Then
            objName = CType(crt.Parent, Form)
            objName.Close()
        End If
    End Sub
#End Region

#Region "Progress status"
    Private m_Maxvalue As Integer
    Public Property ProgressBar_MaxValue()
        Get
            Return m_Maxvalue
        End Get
        Set(ByVal Value)
            m_Maxvalue = Value
        End Set
    End Property

    Public Sub SetProgressBar(ByRef myGroupBox As GroupBox,
                              ByRef myProg As ProgressBar,
                               ByVal description As String,
                               ByVal iVal As Integer)
        Application.DoEvents()
        myGroupBox.Text = description
        myProg.Value = (iVal * 100) / ProgressBar_MaxValue
    End Sub

    Public Sub ResetProgressBar(ByRef myGroupBox As GroupBox,
                                ByRef myProg As ProgressBar)
        myGroupBox.Text = "Status"
        myProg.Value = 0
    End Sub

    Public Sub initialProgressbar(ByRef grb As GroupBox,
                                      ByRef prg As ProgressBar,
                                      ByVal sProcDesc As String,
                                      ByVal sSubPercent As String,
                                      ByVal iProcNo As Integer,
                                      ByVal iProcMax As Integer)
        Dim sStatusVal As String
        Try
            'sProcDesc(iProcNo/iProcMax sSubPercent) (total%) เช่น โหลดข้อมูลเทเบิ้ล xxx (1/3(12%)) (33%)
            Application.DoEvents()
            sStatusVal = " (" & iProcNo & "/" & iProcMax & sSubPercent & ")"
            sStatusVal = sProcDesc & sStatusVal & getPercent(iProcNo, iProcMax)
            grb.Text = sStatusVal
            If prg.Maximum <> iProcMax Then prg.Maximum = iProcMax
            prg.Value = iProcNo

            If iProcNo = 0 Then grb.Text = sProcDesc
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function getPercent(ByVal iVal As Integer, ByVal iMax As Integer) As String
        Return " (" & (iVal * 100) \ iMax & "%)"
    End Function
#End Region

End Class
