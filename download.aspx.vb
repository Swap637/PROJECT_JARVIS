Imports System.IO
Partial Class download
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.DownloadFile(Request("path"), forceDownload:=True)
        'DownloadTextfile(Request("path"))
    End Sub

    Private Sub DownloadTextfile(ByVal path As String)
        '    Dim file As FileInfo
        '    file = New FileInfo(path)
        '    Dim bufferLength As Integer
        '    Dim length As Integer
        '    Dim download As Stream
        '    bufferLength = 100000
        '    length = 0
        '    If file.Exists Then
        '        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
        '        Response.ContentType = "Text(delimited).txt"
        '    End If
        '    Dim buffer As Byte()
        '    download = New FileStream(path, FileMode.Open, FileAccess.Read)
        '    If Response.IsClientConnected Then
        '        length = download.Read(buffer, 0, bufferLength)
        '        Response.OutputStream.Write(buffer, 0, length)
        '        buffer = New Byte(bufferLength)
        '    Else
        '        length = -1
        '    End If
        '    While length > 0
        '        Response.Flush()
        '        Response.End()
        '    End While

        '    If Not download Is System.DBNull.Value Then
        '        download.Close()
        '    End If



        'Dim file As FileInfo
        'file = New FileInfo(path)
        'If file.Exists Then
        '    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
        '    HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString())
        '    HttpContext.Current.Response.ContentType = "Text(delimited).txt"
        '    HttpContext.Current.Response.TransmitFile(file.FullName)
        '    HttpContext.Current.Response.Flush()
        '    HttpContext.Current.Response.End()

        'End If
        ''Return 1
    End Sub
    Private Sub DownloadFile(ByVal path As String, ByVal forceDownload As Boolean)
        'Dim path As String = MapPath(fileName)
        Dim name As String = System.IO.Path.GetFileName(path)
        Dim ext As String = System.IO.Path.GetExtension(path)
        Dim type As String = ""
        ' set known types based on file extension  
        If ext IsNot Nothing Then
            Select Case ext.ToLower()
                Case ".htm", ".html"
                    type = "text/HTML"
                    Exit Select
                Case ".xml"
                    type = "text/XML"
                    Exit Select
                Case ".txt"
                    type = "text/plain"
                    Exit Select
                Case ".doc", ".rtf"
                    type = "Application/msword"
                    Exit Select
                Case ".xls"
                    type = "application/vnd.ms-excel"
                    Exit Select
            End Select
        End If
        Response.Clear()
        If forceDownload Then
            Response.AppendHeader("content-disposition", "attachment; filename=" + name)
        End If
        If type <> "" Then
            Response.ContentType = type
        End If
        Response.WriteFile(path)
        Response.Flush()
        Response.End()
    End Sub


End Class
