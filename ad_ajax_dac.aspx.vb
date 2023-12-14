Imports System.Data
Partial Class ad_ajax_dac
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("action") = "execproc" Then
            ExecProc()
        End If
    End Sub

#Region "ajax"
     

    Private Sub ExecProc()
        Dim errorFlag As Boolean = False, errorString As String = ""
        Dim procname As String = ""
        Dim params As New System.Collections.Generic.Dictionary(Of String, Object)
        Dim paramsXml As New StringBuilder
        For Each key As String In Request.QueryString.Keys
            If key IsNot Nothing Then
                If key = "execproc_procname" Then
                    procname = Request(key)
                End If
                If key.StartsWith("execproc_") And key <> "execproc_procname" Then
                    params.Add(key.Replace("execproc_", ""), Request(key))
                End If
            End If
        Next

        Dim ds As DataSet
        Dim xml As String = ""
        Try
            Dim conInfo As AdConnectionInfo

            If String.IsNullOrEmpty(Request("connection_string_name")) Then
                conInfo = New AdConnectionInfo(Tssipl.Bank.SessionHelper.BranchConnectionStringName)
            Else
                conInfo = New AdConnectionInfo(Request("connection_string_name"))
            End If
            ds = AdDataAccessComponent.Execute(procname, params, conInfo)

            Dim dt As New DataTable("PARAMS")
            'create columns
            For Each key As String In params.Keys
                dt.Columns.Add(key.ToUpper, GetType(String))
            Next
            'populate row 
            Dim dr As DataRow = dt.NewRow
            For Each key As String In params.Keys
                dr(key) = params(key)
            Next
            dt.Rows.Add(dr)

            ds.Tables.Add(dt)

            xml = PrepareXml(ds)

        Catch ex As Exception
            errorFlag = True
            errorString = ex.Message
        End Try
        If errorFlag Then
            ReplyError(errorString)
        Else
            ReplySuccess(xml)
        End If

    End Sub

    Private Function PrepareXml(ByVal ds As DataSet) As String
        For Each dt As DataTable In ds.Tables
            dt.TableName = dt.TableName.ToUpper
            For Each dc As DataColumn In dt.Columns
                dc.ColumnMapping = MappingType.Attribute
                dc.ColumnName = dc.ColumnName.ToUpper
            Next
        Next
        Return ds.GetXml()
    End Function

    Private Function PrepareXml(ByVal text As String) As String
        Return String.Format("<INFO>{0}</INFO>", text)
    End Function

    Private Function PrepareXml(ByVal text As String, ByVal tagname As String) As String
        Return String.Format("<{1}>{0}</{1}>", text, tagname.ToUpper)
    End Function

    Private Sub ReplySuccess(ByVal xml As String)
        Response.Expires = -1
        Response.ContentType = "text/xml"
        Response.Write(xml)
        Response.End()
    End Sub

    Private Sub ReplyError(ByVal errorString As String)
        Response.Expires = -1
        Response.StatusCode = 500
        Response.Write(errorString)
        Response.End()
    End Sub

    Private Function FindControlRecursive(ByVal root As Control, ByVal id As String) As Control
        If root.ID = id Then Return root
        For Each c As Control In root.Controls
            Dim t As Control = FindControlRecursive(c, id)
            If Not (t Is Nothing) Then Return t
        Next
        Return Nothing
    End Function

#End Region
End Class
