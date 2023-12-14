Imports Tssipl.Bank
Imports Tssipl.Bank.RetailBanking
Imports System.Data
Imports Tssipl.Bank.RetailBanking.Enumerations
Imports System.Collections.Generic
Imports System.Data.Common
Imports Tssipl.Practices.EnterpriseLibrary.Data
Partial Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim reason As String = ""
        If Not String.IsNullOrEmpty(Request("reason")) Then
            reason = Request("reason")
        End If
        If Not (Tssipl.Bank.SessionHelper.User Is Nothing) Then
            Tssipl.Bank.BusinessLogicLayer.Authentication.TraceLogoutTime(Tssipl.Bank.SessionHelper.User.UserId)

            Dim parameters As New Dictionary(Of String, Object)
            parameters.Add("@p_userId", SessionHelper.User.UserId)
            parameters.Add("@p_mode", 1)
            AdDataAccessComponent.Execute("Pr_b_UserSwitchLogTime", parameters, BizGlobalModule.HoConnectionStringName)

        End If

        FormsAuthentication.SignOut()

        Session.Clear()
        Session.Abandon()
        Response.Cookies.Add(New HttpCookie("ASP.NET_SessionId", ""))
        Response.Redirect("~/login.aspx?reason=" + reason, True)
        'Server.Transfer("~/login.aspx?reason=" + reason, False)
    End Sub
End Class
