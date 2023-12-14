Imports System.Data
Imports System.Reflection
Imports System.Collections.Specialized
Imports Tssipl.Practices.EnterpriseLibrary.Data
Imports System.Configuration
Imports Tssipl.Bank
Imports biz = Tssipl.Bank.BusinessLogicLayer
Imports Tssipl.Bank.RetailBanking
Imports Tssipl.Bank.RetailBanking.Enumerations
'Change History    
'----------------------------------------------------------------------------------------------------
'Version     Date     	   Author    		Changes and reason    
'1.0.0.1   10-Dec-2019    Nilesh C        To be added at the bottom of the login screen. 
'                                         1) Copyright © 2007-2019 Trust Systems & Software (I) Pvt. Ltd.
'                                         2) Powered By
'------------------------------------------------------------------------------------------------
Partial Class Authentication_login
    Inherits System.Web.UI.Page
    Implements Tssipl.WebLibrary.IPageVersion
    Public Function GetVersion() As String Implements Tssipl.WebLibrary.IPageVersion.GetVersion
        Return "1.0.0.1"
    End Function
    Public Shared Function IsAppVirtualDirectory() As Boolean
        If HttpRuntime.AppDomainAppVirtualPath = "/" Then
            Return False
        Else
            Return True
        End If
    End Function
    'Protected Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate        
    '    Try
    '        Session.Clear()

    '        '<login info and server vars>
    '        Dim loginInfo As New StringBuilder ', propValue As Object
    '        loginInfo.Append("<loginInfo>")
    '        loginInfo.AppendFormat("<{0}>{1}</{0}>", "Mac", uxMacAddress.Text)

    '        Dim excludeServerVars As New System.Collections.Generic.List(Of String)
    '        excludeServerVars.AddRange(New String() {"ALL_RAW", "ALL_HTTP", "HTTP_ACCEPT"})
    '        For i As Int32 = 0 To Request.ServerVariables.AllKeys.GetUpperBound(0)
    '            If Not excludeServerVars.Contains(Request.ServerVariables.AllKeys(i)) Then
    '                If Not String.IsNullOrEmpty(Request.ServerVariables(i)) _
    '                                            AndAlso Not String.IsNullOrEmpty(Request.ServerVariables(i)) Then
    '                    loginInfo.AppendFormat("<{0}>{1}</{0}>", Request.ServerVariables.AllKeys(i), Request.ServerVariables(i))
    '                End If
    '            End If                
    '        Next
    '        excludeServerVars = Nothing            
    '        loginInfo.Append("</loginInfo>")
    '        '<login info and server vars>

    '        HttpContext.Current.Session.Item("user") = New BussinessUser
    '        SessionHelper.FinancialYear = New FinancialYear
    '        SessionHelper.WorkingDateInfo = New WorkingDateInfo

    '        'encrypted/encoded password
    '        Dim pwd As String = Login1.Password
    '        If IsApplyRC4OnLogin() Then
    '            Dim pwdDecodedBytes As Byte() = Convert.FromBase64String(Login1.Password)
    '            Dim keyBytes As Byte() = Encoding.UTF8.GetBytes(ux_rc4_rnd_key.Value + RC4.mHexKey)
    '            Dim pwdDecryptedBytes As Byte() = RC4.Decrypt(keyBytes, pwdDecodedBytes)
    '            pwd = Encoding.UTF8.GetString(pwdDecryptedBytes)
    '        End If

    '        'If biz.Authentication.Authenticate(Login1.UserName, Login1.Password, _
    '        If Auth.Authenticate(Login1.UserName, pwd, _
    '            Session.SessionID, loginInfo.ToString, uxMacAddress.Text, _
    '                                        SessionHelper.User, _
    '                                        SessionHelper.FinancialYear, _
    '                                        SessionHelper.WorkingDateInfo, _
    '                                        SessionHelper.OrgElementTypeId, _
    '                                        SessionHelper.BranchConnectionStringName) = True Then

    '            SessionHelper.User.SessionId = Session.SessionID
    '            SessionHelper.User.MacAddress = uxMacAddress.Text
    '            For i As Int32 = 0 To Request.ServerVariables.AllKeys.GetUpperBound(0)
    '                SessionHelper.User.ServerVariables.Set(Request.ServerVariables.AllKeys(i), Request.ServerVariables(i))
    '            Next

    '            SessionHelper.BankVariables = biz.g_BankVariablesBllc.GetBankVariables()
    '            If SessionHelper.User.OrgElementId.HasValue Then
    '                SessionHelper.BranchVariables = biz.g_BranchVariablesBllc.GetBranchVariables(SessionHelper.User.OrgElementId)
    '            End If

    '            e.Authenticated = True
    '            ''<new code for payroll>
    '            Dim c As New HttpCookie("userid", Tssipl.SecurityHelper.Cryptography.Encrypt(SessionHelper.User.UserId.ToString(), True))
    '            Response.Cookies.Add(c)
    '            '</new code for payroll>
    '            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, True)
    '        End If
    '    Catch ex As Exception
    '        Session.Clear()
    '        e.Authenticated = False
    '        FormsAuthentication.SignOut()
    '        Tssipl.UserControls.MessageBox.Show(ex.Message, ex)            
    '    End Try
    'End Sub

    Protected Sub Login1_Click(sender As Object, e As EventArgs) Handles Login1.Click
        Try
            Session.Clear()

            Dim lang As String = Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName
            Session.Add("CurrentCulture", lang)

            '<login info and server vars>
            Dim loginInfo As New StringBuilder ', propValue As Object
            loginInfo.Append("<loginInfo>")
            loginInfo.AppendFormat("<{0}>{1}</{0}>", "Mac", uxMacAddress.Text)

            Dim excludeServerVars As New System.Collections.Generic.List(Of String)
            excludeServerVars.AddRange(New String() {"ALL_RAW", "ALL_HTTP", "HTTP_ACCEPT"})
            For i As Int32 = 0 To Request.ServerVariables.AllKeys.GetUpperBound(0)
                If Not excludeServerVars.Contains(Request.ServerVariables.AllKeys(i)) Then
                    If Not String.IsNullOrEmpty(Request.ServerVariables(i)) _
                                                AndAlso Not String.IsNullOrEmpty(Request.ServerVariables(i)) Then
                        loginInfo.AppendFormat("<{0}>{1}</{0}>", Request.ServerVariables.AllKeys(i), Request.ServerVariables(i))
                    End If
                End If
            Next
            excludeServerVars = Nothing
            loginInfo.Append("</loginInfo>")
            '<login info and server vars>

            HttpContext.Current.Session.Item("user") = New BussinessUser
            SessionHelper.FinancialYear = New FinancialYear
            SessionHelper.WorkingDateInfo = New WorkingDateInfo

            'encrypted/encoded password
            Dim pwd As String = Password.Text
            If IsApplyRC4OnLogin() Then
                Dim pwdDecodedBytes As Byte() = Convert.FromBase64String(Password.Text)

                Dim keyBytes As Byte() = Encoding.UTF8.GetBytes(ux_rc4_rnd_key.Value + RC4.mHexKey)
                Dim pwdDecryptedBytes As Byte() = RC4.Decrypt(keyBytes, pwdDecodedBytes)
                pwd = Encoding.UTF8.GetString(pwdDecryptedBytes)
            End If

            'If biz.Authentication.Authenticate(Login1.UserName, Login1.Password, _
            If Auth.Authenticate(UserName.Text, pwd,
                Session.SessionID, loginInfo.ToString, uxMacAddress.Text,
                                            SessionHelper.User,
                                            SessionHelper.FinancialYear,
                                            SessionHelper.WorkingDateInfo,
                                            SessionHelper.OrgElementTypeId,
                                            SessionHelper.BranchConnectionStringName) = True Then

                SessionHelper.User.SessionId = Session.SessionID
                SessionHelper.User.MacAddress = uxMacAddress.Text
                For i As Int32 = 0 To Request.ServerVariables.AllKeys.GetUpperBound(0)
                    SessionHelper.User.ServerVariables.Set(Request.ServerVariables.AllKeys(i), Request.ServerVariables(i))
                Next

                SessionHelper.BankVariables = biz.g_BankVariablesBllc.GetBankVariables()
                If SessionHelper.User.OrgElementId.HasValue Then
                    SessionHelper.BranchVariables = biz.g_BranchVariablesBllc.GetBranchVariables(SessionHelper.User.OrgElementId)
                End If
                ' e.Authenticated = True
                ''<new code for payroll>
                Dim c As New HttpCookie("userid", Tssipl.SecurityHelper.Cryptography.Encrypt(SessionHelper.User.UserId.ToString(), True))
                Response.Cookies.Add(c)
                '</new code for payroll>
                Response.Redirect("~/Authentication/ChooseModule.aspx", False)
                Context.ApplicationInstance.CompleteRequest()
                'FormsAuthentication.RedirectFromLoginPage(UserName.Text, True)
            End If
        Catch ex As Exception
            Session.Clear()
            ' e.Authenticated = False
            FormsAuthentication.SignOut()
            Dim args As New Object()
            args = {UserName.Text, Password.Text}
            Tssipl.UserControls.MessageBox.Show(ApplicationHelper.GetMessage(Nothing, ex.Message, Nothing, args))
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        'Register a script reference
        Page.ClientScript.RegisterClientScriptInclude(Me.GetType(), "login_js_script_1", "login.aspx.script1.js?r=" + System.Guid.NewGuid().ToString())
        '
        If IsApplyRC4OnLogin() Then
            ClientScript.RegisterClientScriptBlock(Me.GetType(), "rc4_module", RC4.GetJsModuleScript(), True)
        End If
        If Not String.IsNullOrEmpty(Request("reason")) Then
            Dim reasonMessage As String = ""
            Select Case Request("reason")
                Case "financialyearnotfound"
                    reasonMessage = GetLocalResourceObject("login_Fin_yr_nt_define") '"Financial Year not defined."
                Case "sessionexpired"
                    reasonMessage = GetLocalResourceObject("login_Session_expired")     '"Session expired. Login again."
                Case "page_error"
                    reasonMessage = GetLocalResourceObject("login_Some_error_occured") '"Some error occured. Login again."
            End Select
            Tssipl.UserControls.MessageBox.Show(reasonMessage, Tssipl.UserControls.MessageBox.eMessageType.Error)
        End If

        If Page.IsPostBack = False Then
            ShowConnectionInfo()
        End If
        If Not Page.IsPostBack Then

            Dim ServerPath As String = Request.ApplicationPath
            If ServerPath = "/" Then
                ServerPath = String.Empty
            Else
                ServerPath = Request.ApplicationPath
            End If
            uxChangePasswordlnk.Attributes.Add("onclick", String.Format("return OpenModal('{0}/HO/admin/ChangePassword.aspx','350','650')", ServerPath))
        End If
        If Page.IsPostBack AndAlso
           Not String.IsNullOrEmpty(ConfigurationSettings.AppSettings.Get("bytes_transfer")) AndAlso
           ConfigurationSettings.AppSettings.Get("bytes_transfer") = True Then
            Try
                Dim logString As String = String.Format("{0} {1} : {2} {3}", Now.ToString, Request.ContentLength, Request.Path, ControlChars.NewLine)
                System.IO.File.AppendAllText(Server.MapPath("~/bytes_transfer.log"), logString) '_                
            Catch ex As Exception
                uxSecureInfo.Text = String.Concat(uxSecureInfo.Text, "<br/>", ex.Message)
            End Try
        End If
        '<1.0.0.1>
        If Not IsPostBack Then
            Try
                Dim Query As String = String.Format("select PoweredBy from Sys_Config")
                Dim ds As DataSet = AdDataAccessComponent.Select(Query.ToString(), BizGlobalModule.HoConnectionStringName)
                If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    uxpoweredlabel.Visible = True
                    uxpowered.Visible = True
                    uxpowered.Text = Tssipl.SecurityHelper.Cryptography.Decrypt(UnicodeEncoding.UTF8.GetString(ds.Tables(0).Rows(0)("PoweredBy")).Trim(vbNullChar), True)
                End If

            Catch ex As Exception
                'do nothing as instructed
            End Try
        End If
        '</1.0.0.1>
    End Sub
    Private Sub ShowConnectionInfo()
        Dim conString As String
        Try
            'currently connected to
            If Not String.IsNullOrEmpty(Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.ConnectionStringName) Then
                uxSecureInfo.Text = "connected to:" + Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.ConnectionStringName.ToLower
            End If
            'branch con string
            If Not ConfigurationManager.ConnectionStrings(Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.ConnectionStringName) Is Nothing Then
                conString = ConfigurationManager.ConnectionStrings(Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.ConnectionStringName).ConnectionString
                uxSecureInfo.Text = String.Concat(uxSecureInfo.Text, "|", "branch:", conString.Split(New Char() {";", "="})(1), "@", conString.Split(New Char() {";", "="})(3))
            End If
            'ho con string
            If Not ConfigurationManager.ConnectionStrings(BizGlobalModule.HoConnectionStringName) Is Nothing Then
                conString = ConfigurationManager.ConnectionStrings(BizGlobalModule.HoConnectionStringName).ConnectionString
                uxSecureInfo.Text = String.Concat(uxSecureInfo.Text, "|", "ho:", conString.Split(New Char() {";", "="})(1), "@", conString.Split(New Char() {";", "="})(3))
            End If
        Catch ex As Exception
            uxSecureInfo.Text = String.Concat(uxSecureInfo.Text, "|", ex.Message)
        End Try
        If Not String.IsNullOrEmpty(Request("reason")) Then
            uxSecureInfo.Text = String.Concat(uxSecureInfo.Text, "|", Request("reason"))
        End If
    End Sub
    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        With CType(Master, MasterPages_Base)
            .IsLoginRequired = False
            .IsDayBeginRequired = False
            .IsModuleRequired = False
            .IsFinancialYearRequired = False
        End With
        If (IsAppVirtualDirectory() AndAlso Request.Url.Segments(2).Trim.ToLower = "ho/") OrElse (Not IsAppVirtualDirectory() AndAlso Request.Url.Segments(1).Trim.ToLower = "ho/") Then
            If String.IsNullOrEmpty(Session("MasterPage_BranchConnectionStringName")) Then
                Session("MasterPage_BranchConnectionStringName") = Tssipl.Bank.SessionHelper.BranchConnectionStringName
                Tssipl.Bank.SessionHelper.BranchConnectionStringName = BizGlobalModule.HoConnectionStringName
                ShowConnectionInfo()
            End If
        Else
            If String.IsNullOrEmpty(Session("MasterPage_BranchConnectionStringName")) = False Then
                Tssipl.Bank.SessionHelper.BranchConnectionStringName = Session("MasterPage_BranchConnectionStringName")
                Session.Remove("MasterPage_BranchConnectionStringName")
                ShowConnectionInfo()
            End If
        End If
    End Sub

    Private Sub Authentication_login_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If (IsAppVirtualDirectory() AndAlso Request.Url.Segments(2).Trim.ToLower = "ho/") OrElse (Not IsAppVirtualDirectory() AndAlso Request.Url.Segments(1).Trim.ToLower = "ho/") Then
            'If Request.Url.Segments(2).Trim.ToLower = "ho/" Then
            If String.IsNullOrEmpty(Session("MasterPage_BranchConnectionStringName")) Then
                Session("MasterPage_BranchConnectionStringName") = Tssipl.Bank.SessionHelper.BranchConnectionStringName
                Tssipl.Bank.SessionHelper.BranchConnectionStringName = BizGlobalModule.HoConnectionStringName
                ShowConnectionInfo()
            End If
        Else
            If String.IsNullOrEmpty(Session("MasterPage_BranchConnectionStringName")) = False Then
                Tssipl.Bank.SessionHelper.BranchConnectionStringName = Session("MasterPage_BranchConnectionStringName")
                Session.Remove("MasterPage_BranchConnectionStringName")
                ShowConnectionInfo()
            End If
        End If
        'user of System_Administrator level always connect to ho db
        If Not (Tssipl.Bank.SessionHelper.User Is Nothing) _
                    AndAlso Tssipl.Bank.SessionHelper.User.Level = Tssipl.Bank.RetailBanking.Enumerations.eUserLevel.System_Administrator Then
            'Tssipl.Practices.EnterpriseLibrary.Data.ConfigManager.ConnectionStringName = BizGlobalModule.HoConnectionStringName
            Tssipl.Bank.SessionHelper.BranchConnectionStringName = BizGlobalModule.HoConnectionStringName
        End If
    End Sub

    Private Sub Authentication_login_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If IsApplyRC4OnLogin() Then
            ux_rc4_rnd_key.Value = RC4.GetRandomKey()
        End If

    End Sub

    Public Function IsApplyRC4OnLogin() As Boolean
        If ConfigurationManager.AppSettings("do_not_apply_rc4_on_login") = "1" Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "BLLC"
    Private Class Auth
        Public Shared Function Authenticate(ByVal loginName As String, ByVal password As String,
                                           ByVal sessionId As String, ByVal loginInfo As String, ByVal macAddress As String,
               ByRef bizUser As BussinessUser,
               ByRef financialYear As FinancialYear,
               ByRef workingDateInfo As WorkingDateInfo,
               ByRef orgElementTypeId As System.Nullable(Of Integer),
               ByRef connectionStringName As String) As Boolean

            'Dim validator As New Tssipl.Bank.BankSecurity.TrustBankApplicationValidator
            'validator.Validate()

            Dim passwordByteArray As Byte() = Tssipl.SecurityHelper.Cryptography.Encrypt(password) ', loginName.ToLower())
            password = String.Empty
            For Each i As Int32 In passwordByteArray : password += Chr(i) : Next
            passwordByteArray = Nothing

            Dim returnValue As Int32
            Dim [error] As String = String.Empty
            Try
                With bizUser
                    Dim ds As DataSet = Tssipl.Bank.SqlStoredProcedureExecutor.pr_g_AuthenticateUser(
                        returnValue, loginName, password, sessionId, loginInfo, macAddress, .UserId, .Level, .Status, .EmployeeId, .OrgElementId,
                        .OrgElementCode, .OrgElementName, orgElementTypeId, .OrgElementDepartmentId,
                           workingDateInfo.CurrentSessionNo, workingDateInfo.WorkingDateId, workingDateInfo.WorkingDate,
                        financialYear.FinancialYearId, financialYear.StartDate, financialYear.EndDate, financialYear.FinancialYearName, financialYear.CashBookStartDate,
                        connectionStringName, [error], False, BizGlobalModule.HoConnectionStringName)

                    bizUser.LoginName = loginName
                    bizUser.LoginTime = Now

                    If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                        bizUser.EmployeeName = ds.Tables(0).Rows(0)("EmployeeName")
                    End If
                End With

                If Not String.IsNullOrEmpty([error]) Then
                    Throw New ApplicationException([error])
                End If
                If returnValue > 0 Then
                    Throw New ApplicationException(returnValue)
                End If

                ''HACK-
                'for users specific to any branch
                'If Not String.IsNullOrEmpty(connectionStringName) Then
                '    BankDailyActivity.SetWorkingDateInfo(workingDateInfo, connectionStringName)
                'End If

                'Comment following line to remove bank security (FOR NIGERIA).
                'Dim obj As New Tssipl.Bank.BankSecurity.TrustBankApplicationValidator("")
                'obj.Validate()

                Try
                    'here is the code to validate branch
                    'validator.Validate(bizUser.OrgElementName)
                Catch ex As Exception
                    biz.Authentication.DisconnectUser(bizUser.UserId)
                    Throw ex
                End Try

                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
    End Class
#End Region
End Class
