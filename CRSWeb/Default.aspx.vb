Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Session.Clear()
        Response.Redirect("~/Login.aspx")
    End Sub
End Class