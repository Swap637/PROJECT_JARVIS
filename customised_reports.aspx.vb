'exec proc in branch level

Imports TSSIPL.UserControls
Imports System.Collections.Generic
Imports System.Data

Partial Class customised_reports
    Inherits System.Web.UI.Page

    Private _seesion_key_param As String = "customised_reports_param"
    Private _seesion_key_param_value As String = "customised_reports_param_value"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then

            Dim options As AdDataAccessOptions
            Dim paramList As Dictionary(Of String, Object)

            options = New AdDataAccessOptions
            options.StoredProcedureName = "pr_g_getCustomisedRpt"
            options.ConnectionInfo = New AdConnectionInfo("BankHOConnectionString")

            paramList = New Dictionary(Of String, Object)
            paramList.Add("@p_reportno", Request("param"))
            options.StoredProcedureParamList = paramList

            Dim ds As DataSet = AdDataAccessComponent.Execute(options)
            Dim dt As DataTable = ds.Tables(0)
            uxParamList.DataSource = dt
            uxParamList.DataBind()
            ds = Nothing

            Session.Add(_seesion_key_param, dt)
            uxReportTitle.Text = dt.Rows(0)("ReportTitle")


            Tssipl.Bank.GlobalFunctions.DisableScreenOnPostBack(Page, New WebControl() {Me.uxDisplay})

        End If

    End Sub

    Protected Sub uxDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles uxDisplay.Click
        Dim dt As DataTable = CType(Session(_seesion_key_param), DataTable)

        Dim options As AdDataAccessOptions
        Dim paramList As Dictionary(Of String, Object)

        Dim dt_param_value As New DataTable("ParamValue")
        dt_param_value.Columns.Add("Input Parameter")
        dt_param_value.Columns.Add("Value")

        options = New AdDataAccessOptions
        options.StoredProcedureName = dt.Rows(0)("commandtext")


        If dt.Rows(0)("ReportNo").ToString.ToLower.Contains("cons") Then
            options.ConnectionInfo = New AdConnectionInfo("BankHOConnectionString")
        Else
            options.ConnectionInfo = New AdConnectionInfo(Tssipl.Bank.SessionHelper.BranchConnectionStringName)
        End If


        paramList = New Dictionary(Of String, Object)
        For Each row As GridViewRow In uxParamList.Rows
            Dim tbox As TextBox = CType(row.Cells(1).FindControl("uxParamValue"), TextBox)
            Dim paramname As String = CType(row.Cells(1).FindControl("uxParamNAme"), HiddenField).Value
            Dim dr As DataRow = Nothing
            For Each dr In dt.Rows
                If dr("paramname").ToString = paramname Then
                    Exit For
                End If
            Next
            If tbox.Text.Trim <> String.Empty Then
                paramList.Add(dr("paramname").ToString, tbox.Text)
                'create param value table
                Dim dr_param_value As DataRow = dt_param_value.NewRow
                dr_param_value(0) = dr("paramCaption").ToString
                dr_param_value(1) = tbox.Text
                dt_param_value.Rows.Add(dr_param_value)
            End If
            options.StoredProcedureParamList = paramList
        Next

        Dim dsTemp As DataSet, ds As New DataSet
        ds.Tables.Add(dt_param_value)
        Try
            dsTemp = AdDataAccessComponent.Execute(options)
            For Each dtTemp As DataTable In dsTemp.Tables
                ds.Tables.Add(dtTemp.Copy())
            Next
            ExportDataSetToExcel(ds, Me.Response)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub ExportDataSetToExcel(ByVal ds As DataSet, ByVal response As HttpResponse)
        Dim dtparam As DataTable = CType(Session(_seesion_key_param), DataTable)
        'header
        Dim headerHtml As New StringBuilder
        headerHtml.AppendFormat("<table border=1 width='800px'><tr><td   colspan='4' style='font-weight:bold;text-align:center'>{0}</td></tr>", Tssipl.Bank.SessionHelper.BankVariables("BANK_NAME").ToString.ToUpper)
        headerHtml.AppendFormat("<tr><td colspan='4' style='text-align:center'><b>{0}</td></tr>", dtparam.Rows(0)("reportTitle").ToString)
        headerHtml.AppendFormat("<tr><td>Report No</td><td>{0}</td></tr>", dtparam.Rows(0)("reportno"))
        headerHtml.AppendFormat("</table>")
        'first let's clean up the response.object
        response.Clear()
        response.Charset = ""
        response.ContentType = "application/vnd.ms-excel"
        Dim stringWrite As New System.IO.StringWriter
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)

        htmlWrite.WriteLine(headerHtml.ToString)

        'stringWrite
        Dim dg As DataGrid
        For Each dt As DataTable In ds.Tables
            If dt.Rows.Count = 1 AndAlso dt.Columns.Count = 1 Then
                Dim l As New LiteralControl
                l.Text = dt.Rows(0)(0).ToString
                l.RenderControl(htmlWrite)
            Else
                dg = New DataGrid
                dg.DataSource = dt
                dg.DataBind()
                dg.RenderControl(htmlWrite)
            End If
        Next
        Dim footer As String = "</br></br>Printed by '" & Tssipl.Bank.SessionHelper.User.LoginName & "' on " & Now.Date.ToString("dd-MMM-yyyy") & " at " & Now.Hour.ToString & ":" & Now.Minute.ToString
        htmlWrite.WriteLine(footer)

        'all that's left is to output the html
        response.Write(stringWrite.ToString)
        response.End()
    End Sub

End Class
