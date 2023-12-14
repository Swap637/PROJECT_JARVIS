<%@ Application Language="VB" %>

<script RunAt="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.IsSessionWideConnectionString = True
        Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.SessionVariableForConnectionStringName = "BranchConnectionStringName"

        'load message list
        ApplicationHelper.MessageList = AdDataDrivenMessaging.AdMessageList.GetMessageList(Nothing, Nothing, Nothing, "BankHoConnectionString")
        BizGlobalModule.MessageList = ApplicationHelper.MessageList

        ' Code to re-initialize errormessage list when application get started.
        ApplicationHelper.ErrorMessageList = AdDataDrivenMessaging.AdMessageList.GetErrorMessageList(Nothing, Nothing, Nothing, "BankHoConnectionString")
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown        
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Application.Item("app_last_error") = Server.GetLastError()
        ' Code that runs when an unhandled error occurs         
        'Server.Transfer("~/apperror.aspx")
        'Response.Redirect("~/apperror.aspx")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        If ApplicationHelper.MessageList Is Nothing Then
            ApplicationHelper.MessageList = AdDataDrivenMessaging.AdMessageList.GetMessageList(Nothing, Nothing, Nothing, "BankHoConnectionString")
            BizGlobalModule.MessageList = ApplicationHelper.MessageList
        End If

        ' Code to re-initialize errormessage list when new session is started.
        If ApplicationHelper.ErrorMessageList Is Nothing Then
            ApplicationHelper.ErrorMessageList = AdDataDrivenMessaging.AdMessageList.GetErrorMessageList(Nothing, Nothing, Nothing, "BankHoConnectionString")
        End If

        ' Code that runs when a new session is started          
        Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.ConnectionStringName = "BankHoConnectionString"
        Tssipl.Bank.SessionHelper.BranchConnectionStringName = "BankHoConnectionString"
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.

        'If ApplicationHelper.IsExists(Application, Session.SessionID) Then
        '    ApplicationHelper.RemoveUser(Application, Session.SessionID)
        '    Tssipl.Bank.BusinessLogicLayer.Authentication.TraceLogoutTime(Session.SessionID)
        'End If

        If Not (Session("user") Is Nothing) Then
            Tssipl.Bank.BusinessLogicLayer.Authentication.TraceLogoutTime(CType(Session("user"), Tssipl.Bank.BussinessUser).UserId)
        End If


        'Check Locking existing or not        
        'Dim errorString As String = Nothing
        'Try
        '    Tssipl.Bank.SqlStoredProcedureExecutor.pr_b_LockUnlockAccount( _
        '            Nothing, 0, 0, userId, Nothing, _
        '            "Transaction Entry", errorString, False)
        'Catch ex As Exception
        '    System.IO.File.AppendAllText(Server.MapPath("~/error.log"), ex.Message)
        'End Try
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        If ConfigurationManager.AppSettings("cbs_app_lang_flag") <> "0" Then
            Dim SelectedLanguage As String = ConfigurationManager.AppSettings("cbs_app_lang")
            '
            Dim cookie As HttpCookie = Request.Cookies("cbs_app_lang_cookie")
            If cookie IsNot Nothing AndAlso String.IsNullOrEmpty(cookie.Value) = False Then
                SelectedLanguage = cookie.Value
            End If
            '
            If Not String.IsNullOrEmpty(SelectedLanguage) Then
                Dim currLang As System.Globalization.CultureInfo = New System.Globalization.CultureInfo(SelectedLanguage)
                currLang.NumberFormat.CurrencyDecimalSeparator = "."
                currLang.NumberFormat.CurrencyGroupSeparator = ","
                currLang.NumberFormat.NumberDecimalSeparator = "."
                currLang.NumberFormat.NumberGroupSeparator = ","

                Threading.Thread.CurrentThread.CurrentUICulture = currLang
                Threading.Thread.CurrentThread.CurrentCulture = currLang

                'Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo(SelectedLanguage)
                'Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(SelectedLanguage)
            End If
        End If

    End Sub
    Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        'Dim cookie As New HttpCookie("cbs_app_lang")
        'cookie.Value = Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
        'cookie.Expires = DateTime.Now.AddHours(1)
        'Response.SetCookie(cookie)
    End Sub

</script>

