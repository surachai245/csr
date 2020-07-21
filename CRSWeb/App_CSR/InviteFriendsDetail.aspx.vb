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
Imports System.Net.Mail
Imports System.Web.Configuration


Public Class InviteFriendsDetail
    Inherits System.Web.UI.Page
    Dim objProject As SysProject
    Dim objEmployee As SysEmployee
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                objProject = Session("ProjestDetail")
                objEmployee = Session("EmployeeDetail")

                previewtitle4.InnerText() = objProject.ProjectName
                previewtitle4.Attributes.Add("style", "color:" + objProject.Tab1_FontColor + ";font-family:" + objProject.Tab1_FontName)
                previewbody4.Attributes.Add("style", "background-color:" + objProject.Tab5_BGColor)
                body.Attributes.Add("style", "background-color:" + objProject.Tab5_BGColor)
                previewdetail4.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_1.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_2.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_3.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_4.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_5.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_6.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_7.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_8.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_9.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt_10.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                previewtxt17.Attributes.Add("style", "color:" + objProject.Tab5_FontColor)
                Dim Empcode As String = ""
                Dim Fullname As String = ""
                Try
                    Empcode = Request.QueryString("Empcode").ToString()
                    Fullname = Request.QueryString("Fullname").ToString()

                    data(Empcode, Fullname)

                Catch ex As Exception
                    data("", "")
                End Try
            Catch ex As Exception

            End Try
        End If
    End Sub

    Sub data(ByVal Empcode As String, ByVal Fullname As String)

        If (Empcode <> "" And Fullname <> "") Then
            If (Check(Empcode)) Then
                If (Session("empcodetxt_1") Is Nothing Or Session("empcodetxt_1") = "") Then
                    Session("empcodetxt_1") = Empcode
                    Session("fullnametxt_1") = Fullname
                ElseIf (Session("empcodetxt_2") Is Nothing Or Session("empcodetxt_2") = "") Then
                    Session("empcodetxt_2") = Empcode
                    Session("fullnametxt_2") = Fullname
                ElseIf (Session("empcodetxt_3") Is Nothing Or Session("empcodetxt_3") = "") Then
                    Session("empcodetxt_3") = Empcode
                    Session("fullnametxt_3") = Fullname
                ElseIf (Session("empcodetxt_4") Is Nothing Or Session("empcodetxt_4") = "") Then
                    Session("empcodetxt_4") = Empcode
                    Session("fullnametxt_4") = Fullname
                ElseIf (Session("empcodetxt_5") Is Nothing Or Session("empcodetxt_5") = "") Then
                    Session("empcodetxt_5") = Empcode
                    Session("fullnametxt_5") = Fullname
                ElseIf (Session("empcodetxt_6") Is Nothing Or Session("empcodetxt_6") = "") Then
                    Session("empcodetxt_6") = Empcode
                    Session("fullnametxt_6") = Fullname
                ElseIf (Session("empcodetxt_7") Is Nothing Or Session("empcodetxt_7") = "") Then
                    Session("empcodetxt_7") = Empcode
                    Session("fullnametxt_7") = Fullname
                ElseIf (Session("empcodetxt_8") Is Nothing Or Session("empcodetxt_8") = "") Then
                    Session("empcodetxt_8") = Empcode
                    Session("fullnametxt_8") = Fullname
                ElseIf (Session("empcodetxt_9") Is Nothing Or Session("empcodetxt_9") = "") Then
                    Session("empcodetxt_9") = Empcode
                    Session("fullnametxt_9") = Fullname
                ElseIf (Session("empcodetxt_10") Is Nothing Or Session("empcodetxt_10") = "") Then
                    Session("empcodetxt_10") = Empcode
                    Session("fullnametxt_10") = Fullname
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('','','เลือกพนักงานซ้ำ');", True)
            End If
        End If

        If (Session("empcodetxt_1") IsNot Nothing Or Session("empcodetxt_1") <> "") Then
            previewtxt_1.InnerText = Session("empcodetxt_1") + " " + Session("fullnametxt_1")
            div1.Style.Add("display", "block")
            lb1.Attributes.Add("onmouseover", "this.style.cursor='hand';")
        End If
        If (Session("empcodetxt_2") IsNot Nothing Or Session("empcodetxt_2") <> "") Then
            previewtxt_2.InnerText = Session("empcodetxt_2") + " " + Session("fullnametxt_2")
            div2.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_3") IsNot Nothing Or Session("empcodetxt_3") <> "") Then
            previewtxt_3.InnerText = Session("empcodetxt_3") + " " + Session("fullnametxt_3")
            div3.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_4") IsNot Nothing Or Session("empcodetxt_4") <> "") Then
            previewtxt_4.InnerText = Session("empcodetxt_4") + " " + Session("fullnametxt_4")
            div4.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_5") IsNot Nothing Or Session("empcodetxt_5") <> "") Then
            previewtxt_5.InnerText = Session("empcodetxt_5") + " " + Session("fullnametxt_5")
            div5.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_6") IsNot Nothing Or Session("empcodetxt_6") <> "") Then
            previewtxt_6.InnerText = Session("empcodetxt_6") + " " + Session("fullnametxt_6")
            div6.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_7") IsNot Nothing Or Session("empcodetxt_7") <> "") Then
            previewtxt_7.InnerText = Session("empcodetxt_7") + " " + Session("fullnametxt_7")
            div7.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_8") IsNot Nothing Or Session("empcodetxt_8") <> "") Then
            previewtxt_8.InnerText = Session("empcodetxt_8") + " " + Session("fullnametxt_8")
            div8.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_9") IsNot Nothing Or Session("empcodetxt_9") <> "") Then
            previewtxt_9.InnerText = Session("empcodetxt_9") + " " + Session("fullnametxt_9")
            div9.Style.Add("display", "block")
        End If
        If (Session("empcodetxt_10") IsNot Nothing Or Session("empcodetxt_10") <> "") Then
            previewtxt_10.InnerText = Session("empcodetxt_10") + " " + Session("fullnametxt_10")
            div10.Style.Add("display", "block")
        End If

    End Sub
    Protected Sub btInsert_Click(sender As Object, e As EventArgs) Handles btInsert.Click
        If (tbSearch.Text <> "") Then
            If (CheckData()) Then
                Response.Redirect("SearchData.aspx?SearchData=" & tbSearch.Text)
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('','','จำนวนเพื่อนครบกำหนด');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('','','กรุณาเลือกเพื่อนที่ต้องการค้นหา');", True)
        End If
        'pop.Show()
    End Sub

    Private Function Check(ByVal Empcode As String) As Boolean
        Dim Checkdata As Boolean = True

        If (Session("empcodetxt_1") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_2") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_3") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_4") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_5") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_6") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_7") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_8") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_9") = Empcode) Then
            Checkdata = False
        ElseIf (Session("empcodetxt_10") = Empcode) Then
            Checkdata = False
        End If

        Return Checkdata
    End Function
    Private Function CheckData() As Boolean
        Dim Check As Boolean = False

        If (Session("empcodetxt_1") Is Nothing Or Session("empcodetxt_1") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_2") Is Nothing Or Session("empcodetxt_2") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_3") Is Nothing Or Session("empcodetxt_3") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_4") Is Nothing Or Session("empcodetxt_4") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_5") Is Nothing Or Session("empcodetxt_5") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_6") Is Nothing Or Session("empcodetxt_6") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_7") Is Nothing Or Session("empcodetxt_7") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_8") Is Nothing Or Session("empcodetxt_8") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_9") Is Nothing Or Session("empcodetxt_9") = "") Then
            Check = True
        End If
        If (Session("empcodetxt_10") Is Nothing Or Session("empcodetxt_10") = "") Then
            Check = True
        End If

        Return Check
    End Function

    Protected Sub lb1_Click(sender As Object, e As EventArgs) Handles lb1.Click
        Session("empcodetxt_1") = Nothing
        Session("fullnametxt_1") = Nothing
        div1.Style.Add("display", "none")
    End Sub

    Protected Sub lb2_Click(sender As Object, e As EventArgs) Handles lb2.Click
        Session("empcodetxt_2") = Nothing
        Session("fullnametxt_2") = Nothing
        div2.Style.Add("display", "none")
    End Sub
    Protected Sub lb3_Click(sender As Object, e As EventArgs) Handles lb3.Click
        Session("empcodetxt_3") = Nothing
        Session("fullnametxt_3") = Nothing
        div3.Style.Add("display", "none")
    End Sub
    Protected Sub lb4_Click(sender As Object, e As EventArgs) Handles lb4.Click
        Session("empcodetxt_4") = Nothing
        Session("fullnametxt_4") = Nothing
        div4.Style.Add("display", "none")
    End Sub
    Protected Sub lb5_Click(sender As Object, e As EventArgs) Handles lb5.Click
        Session("empcodetxt_5") = Nothing
        Session("fullnametxt_5") = Nothing
        div5.Style.Add("display", "none")
    End Sub
    Protected Sub lb6_Click(sender As Object, e As EventArgs) Handles lb6.Click
        Session("empcodetxt_6") = Nothing
        Session("fullnametxt_6") = Nothing
        div6.Style.Add("display", "none")
    End Sub
    Protected Sub lb7_Click(sender As Object, e As EventArgs) Handles lb7.Click
        Session("empcodetxt_7") = Nothing
        Session("fullnametxt_7") = Nothing
        div7.Style.Add("display", "none")
    End Sub
    Protected Sub lb8_Click(sender As Object, e As EventArgs) Handles lb8.Click
        Session("empcodetxt_8") = Nothing
        Session("fullnametxt_8") = Nothing
        div8.Style.Add("display", "none")
    End Sub
    Protected Sub lb9_Click(sender As Object, e As EventArgs) Handles lb9.Click
        Session("empcodetxt_9") = Nothing
        Session("fullnametxt_9") = Nothing
        div9.Style.Add("display", "none")
    End Sub
    Protected Sub lb10_Click(sender As Object, e As EventArgs) Handles lb10.Click
        Session("empcodetxt_10") = Nothing
        Session("fullnametxt_10") = Nothing
        div10.Style.Add("display", "none")
    End Sub


    Protected Sub btSendMail_Click(sender As Object, e As EventArgs) Handles btSendMail.Click
        Try
            objProject = Session("ProjestDetail")
            objEmployee = Session("EmployeeDetail")

            Dim Chk As Boolean = False
            For index = 1 To 10
                Dim empcode As String = "empcodetxt_" & index
                Dim fullname As String = "fullnametxt_" & index
                If (Session(empcode) IsNot Nothing And Session(fullname) IsNot Nothing) Then
                    Dim dt = GetData.GetEmployee()
                    Dim dv As DataView = New DataView(dt)
                    dv.RowFilter = "Empcode = '" & Session(empcode) & "'"
                    If (dv.Count > 0) Then
                        Chk = True
                        GetData.InsertRefFriend(dv(0)("Empid").ToString(), txtMsg.Text.Trim, objEmployee.empid.ToString(), objProject.ProjectId)
                        SendMail(Session(empcode), Session(fullname), dv(0)("EMail").ToString(), objEmployee.EMail)
                    End If
                End If
            Next
            If (Chk) Then
                cleardata()
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('','','ดำเนินการเรียบร้อย');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('','','กรุณากรอกข้อมูลก่อนดำเนินการ');", True)
            End If

        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType(), "CAllA", "alertMessage('','','ไม่สามารถส่งเมล์ได้');", True)
        End Try

    End Sub
    Sub cleardata()
        Session("empcodetxt_1") = Nothing
        Session("fullnametxt_1") = Nothing
        div1.Style.Add("display", "none")
        Session("empcodetxt_2") = Nothing
        Session("fullnametxt_2") = Nothing
        div2.Style.Add("display", "none")
        Session("empcodetxt_3") = Nothing
        Session("fullnametxt_3") = Nothing
        div3.Style.Add("display", "none")
        Session("empcodetxt_4") = Nothing
        Session("fullnametxt_4") = Nothing
        div4.Style.Add("display", "none")
        Session("empcodetxt_5") = Nothing
        Session("fullnametxt_5") = Nothing
        div5.Style.Add("display", "none")
        Session("empcodetxt_6") = Nothing
        Session("fullnametxt_6") = Nothing
        div6.Style.Add("display", "none")
        Session("empcodetxt_7") = Nothing
        Session("fullnametxt_7") = Nothing
        div7.Style.Add("display", "none")
        Session("empcodetxt_8") = Nothing
        Session("fullnametxt_8") = Nothing
        div8.Style.Add("display", "none")
        Session("empcodetxt_9") = Nothing
        Session("fullnametxt_9") = Nothing
        div9.Style.Add("display", "none")
        Session("empcodetxt_10") = Nothing
        Session("fullnametxt_10") = Nothing
        div10.Style.Add("display", "none")
        txtMsg.Text = ""
    End Sub
    Sub SendMail(ByVal empcode As String, ByVal fullname As String, ByVal MailTo As String, ByVal MailFrom As String)
        objProject = Session("ProjestDetail")
        objEmployee = Session("EmployeeDetail")
        Try
            Dim str As String = ""
            Dim message As New MailMessage()
            message.IsBodyHtml = True
            message.From = New MailAddress(MailFrom)
            message.To.Add(New MailAddress(MailTo))
            message.Subject = objProject.SubjectDetail
            Dim URLChrome As String = WebConfigurationManager.AppSettings("URLChrome")
            ' str = "<html><font size=3>เรียน &nbsp;คุณ&nbsp;<font color=""blue""> " & fullname & " </font> <br><br>&nbsp;&nbsp;&nbsp;ได้มีข้อความเชิญท่านเพื่อเข้าร่วมโครงการ&nbsp;<font color=""blue""> " & objProject.SubjectDetail & "</font>&nbsp;จากคุณ <font color=""blue"">" & objEmployee.FullName & " </font><br><br> รายละเอียดดังนี้ <br><br>&nbsp;&nbsp;&nbsp;" & txtMsg.Text & "<br><br>ท่านสามารถสมัครเข้าร่วมโครงการได้ที่  <a href=" & URLChrome + objProject.Tab1_URL & " >สมัครเข้าร่วมโครงการ</a> <br><br>จึงเรียนมาเพื่อโปรดพิจารณา</font> </html>"
            'str = String.Format(str, "", "") 

            str = "<html>
    <body>
        <table style=""width: 950px"" border=""0"" cellpadding=""0"" cellspacing=""0"">
            <tr style=""height: 70px;"">
                <td style=""background-color: #C0C0C0"">
                    <p style=""font-size: 30px"">
                    &nbsp;" & objProject.ProjectName & "</td>
            </tr>
            <tr style=""height: 270px;"" >
                <td style=""background-color: " & objProject.Tab1_BGColor & "; text-align: center"">
                    <table align=""center"" style=""width: 90%; height: 180px; margin-top: 40px; margin-bottom: 10px; background-color: #FFFFFF"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                        <tr style=""text-align: left;padding:3px;"">
                            <td>
                                <p style=""font-size: 20px; margin-top: 30px;"">&nbsp;&nbsp;&nbsp;ฉันเชิญให้คุณสมัครเข้าร่วมโครงการ</p>
                                <hr style=""width: 95%;"">
                                <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & txtMsg.Text & " </p>
                                <hr style=""width: 95%;"">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <a href='" & URLChrome + objProject.Tab1_URL & "'>ลงทะเบียนสมัครเข้าร่วมโครงการ</a>
                                <p>&nbsp;&nbsp;&nbsp;&nbsp;</p>
                            </td>
                        </tr>
                    </table>
                    <br>
                </td>
            </tr>
        </table>
        <body>
    </html>"
            message.Body = str
            GetData.FnSentEmailSystem(message)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btClose_Click(sender As Object, e As EventArgs) Handles btClose.Click
        Page.ClientScript.RegisterOnSubmitStatement(Me.GetType(), "closePage", "window.onunload = CloseWindow();")
    End Sub
End Class