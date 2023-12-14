
Partial Class apperror
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CType(Master, MasterPages_Base).PageCaption = "Application Error"
        Dim sb As New StringBuilder
        If Not HttpContext.Current.AllErrors Is Nothing Then
            For Each ex As System.Exception In HttpContext.Current.AllErrors
                Dim _ex As Exception = ex
                While Not _ex Is Nothing
                    sb.AppendFormat("<p><b>Message</b>: {0}</p>", _ex.Message)
                    sb.AppendFormat("<p><b>Source</b>: {0}</p>", _ex.Source)
                    sb.AppendFormat("<p><b>StackTrace</b>: {0}</p>", _ex.StackTrace)
                    _ex = _ex.InnerException
                End While
            Next
            uxError.Text = sb.ToString
            Server.ClearError()
        End If
    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        CType(Master, MasterPages_Base).IsLoginRequired = False
        CType(Master, MasterPages_Base).IsFinancialYearRequired = False
        CType(Master, MasterPages_Base).IsModuleRequired = False
        CType(Master, MasterPages_Base).IsDayBeginRequired = False
    End Sub
End Class
