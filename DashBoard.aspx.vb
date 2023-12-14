
Partial Class DashBoard
    Inherits System.Web.UI.Page

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim navUrl As String = String.Empty

        Try
            navUrl = Request("navUrl")
            Dim str As String = "<script>window.open('" & navUrl & "',null,'height=600,width=800,resizable=yes,status=yes,toolbar=no,menubar=no,location=no');</script>"
            ClientScript.RegisterStartupScript(Page.GetType, "OpenWindow", str)
        Catch ex As Exception
            Tssipl.UserControls.MessageBox.Show(ex.Message)
        Finally
            'Response.Redirect("../Authentication/MainMenu.aspx")
        End Try
    End Sub
End Class
