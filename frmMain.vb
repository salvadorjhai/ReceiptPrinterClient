Imports System.ComponentModel
Imports System.Drawing.Printing
Imports System.IO
Imports System.Net
Imports ESC_POS_USB_NET.Printer
Imports PdfiumViewer

Public Class frmMain

    Dim WithEvents _BG As New BackgroundWorker
    Dim _SERVER As HttpListener = Nothing
    Dim rev As Integer = 1

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Me.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)
        Me.Text = $"{ProductName} (rev.{rev})"

        LoadConfig()

        If chkAutoStart.Checked Then
            btnStartStop_Click(Nothing, Nothing)
        End If

        NotifyIcon1.Icon = Me.Icon
        NotifyIcon1.Text = Me.Text
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        If IsBusy Then
            e.Cancel = True
            NotifyIcon1_DoubleClick(Nothing, Nothing)
            Return
        End If

        StopServer()
    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            Me.Hide()
        End If
    End Sub

    Private Sub lblHost_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblHost.LinkClicked
        Try
            Process.Start(lblHost.Text)
            Clipboard.SetText(lblHost.Text, TextDataFormat.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
        If Me.Visible Then
            Me.Hide()
        Else
            Me.Show()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub


    Sub LoadConfig()
        Dim printers = GetListOfPrinters()
        cboPrinters.Items.Clear()
        cboPrinters.Items.AddRange(printers)
        txtPort.Text = "62223" ' default port

        If String.IsNullOrWhiteSpace(My.Settings.Printer) = False Then cboPrinters.Text = My.Settings.Printer
        If String.IsNullOrWhiteSpace(My.Settings.Host) = False Then txtIPAddress.Text = My.Settings.Host
        If String.IsNullOrWhiteSpace(My.Settings.Port) = False Then txtPort.Text = My.Settings.Port

        chkAutoStart.Checked = My.Settings.Autostart
        chkPDF.Checked = My.Settings.IsPDF
        chkIsBase64.Checked = My.Settings.IsBase64

    End Sub

    Function GetListOfPrinters() As String()
        Dim printers As New List(Of String)()
        For Each printer As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            printers.Add(printer)
        Next
        Return printers.ToArray()
    End Function

    Function GetHostAddress() As String
        Dim hostName As String = Dns.GetHostName()
        Dim hostEntry As IPHostEntry = Dns.GetHostEntry(hostName)
        For Each ip As IPAddress In hostEntry.AddressList
            If ip.ToString.StartsWith("192.168") = False Then Continue For
            If ip.AddressFamily = Sockets.AddressFamily.InterNetwork AndAlso Not IPAddress.IsLoopback(ip) Then
                Return ip.ToString()
            End If
        Next
        Return "127.0.0.1"
    End Function

    Function GetFreePort(count) As Integer()
        ' Create an array to store the free ports
        Dim ports As New List(Of Integer)

        ' Try to find a free port the specified number of times
        For i As Integer = 1 To count
            ' Create a new TcpListener on a random port
            Dim listener As New Sockets.TcpListener(IPAddress.Any, 0)

            ' Start the listener
            listener.Start()

            ' Get the port that the listener is using
            Dim port As Integer = CType(listener.LocalEndpoint, IPEndPoint).Port

            ' Add the port to the list of free ports
            ports.Add(port)

            ' Stop the listener
            listener.Stop()
        Next

        ' Return the list of free ports as an array
        Return ports.ToArray()
    End Function

    Private Sub btnFindFreePort_Click(sender As Object, e As EventArgs) Handles btnFindFreePort.Click
        txtPort.Text = GetFreePort(1).FirstOrDefault
    End Sub

    Dim IsBusy As Boolean = False
    Dim IsCancelled As Boolean = False
    Private Sub btnStartStop_Click(sender As Object, e As EventArgs) Handles btnStartStop.Click

        If IsBusy = False Then
            If cboPrinters.SelectedIndex < 0 Then
                MessageBox.Show("Please select a printer first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Try
                StartServer()

                My.Settings.Printer = cboPrinters.Text
                My.Settings.Host = txtIPAddress.Text
                My.Settings.Port = txtPort.Text
                My.Settings.Autostart = chkAutoStart.Checked
                My.Settings.IsPDF = chkPDF.Checked
                My.Settings.IsBase64 = chkIsBase64.Checked
                My.Settings.Save()

                cboPrinters.Enabled = False
                txtIPAddress.Enabled = False
                txtPort.Enabled = False
                btnFindFreePort.Enabled = False
                chkAutoStart.Enabled = False
                chkPDF.Enabled = False
                chkIsBase64.Enabled = False

            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try

        Else
            IsCancelled = True

            StopServer()
            IsBusy = False

            btnStartStop.Text = "Start"

            cboPrinters.Enabled = True
            txtIPAddress.Enabled = True
            txtPort.Enabled = True
            btnFindFreePort.Enabled = True
            chkAutoStart.Enabled = True
            chkPDF.Enabled = True
            chkIsBase64.Enabled = True

        End If
    End Sub

    ''' <summary>
    ''' sample usage (PDF) (send as byte)
    ''' var pdf = document.GeneratePdf(); // this is a byte[]
    ''' using var cl = new System.Net.WebClient();
    ''' cl.Headers.Add("Content-Type", "application/pdf");
    ''' cl.UploadData($"http://192.168.3.246:62223/", pdf);
    ''' 
    ''' sample usage (pos/raw byte) (send as byte)
    ''' using var cl = new System.Net.WebClient();
    ''' cl.Headers.Add("Content-Type", "application/octet-stream");
    ''' cl.UploadData(printerLocation, data);
    ''' </summary>

    Async Sub StartServer()

        If Not HttpListener.IsSupported Then
            Throw New Exception($"HTTP Listener not supported on this machine!")
        End If
        StopServer()

        ' listen to this url prefix
        Try
            _SERVER = New HttpListener
            _SERVER.Prefixes.Add($"http://{txtIPAddress.Text}:{txtPort.Text}/")
            _SERVER.Prefixes.Add($"http://127.0.0.1:{txtPort.Text}/")
            _SERVER.Prefixes.Add($"http://localhost:{txtPort.Text}/")
            _SERVER.Start()
        Catch ex As Exception
            Throw ex
        End Try

        Dim posPrinter = New Printer(My.Settings.Printer)

        lblHost.Text = _SERVER.Prefixes.FirstOrDefault
        IsBusy = True
        IsCancelled = False

        btnStartStop.Text = "Stop"

        Do While True
            Try
                If IsCancelled Then Exit Do
                If IsNothing(_SERVER) Then Exit Do
                If _SERVER.IsListening = False Then Exit Do

                Dim context = Await _SERVER.GetContextAsync()
                Dim request = context.Request
                Dim response = context.Response

                ' fix for CORS preflight request 
                response.AddHeader("Access-Control-Allow-Origin", "*")
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS")
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type")

                If request.HttpMethod.ToUpper = "OPTIONS" Then
                    response.StatusCode = 200
                    response.Close()
                    Continue Do
                End If

                If request.HttpMethod.ToUpper = "POST" Then

                    Try
                        ' Get POST body as byte array
                        Dim bodyBytes As Byte()
                        Dim isPdf = My.Settings.IsPDF
                        Dim IsBase64 = My.Settings.IsBase64
                        Dim IsImage = False

                        ' override if content type is set
                        If String.IsNullOrWhiteSpace(request.ContentType) = False AndAlso request.ContentType.Contains("pdf") Then isPdf = True
                        If String.IsNullOrWhiteSpace(request.ContentType) = False AndAlso request.ContentType.Contains("octet-stream") Then IsBase64 = False
                        If String.IsNullOrWhiteSpace(request.ContentType) = False AndAlso request.ContentType.Contains("image") Then
                            IsImage = True
                            isPdf = False
                        End If

                        If IsBase64 = False Or isPdf = True Or IsImage = True Then
                            ' for bytes[] (pos doc/printer, pdf)
                            Using ms As New IO.MemoryStream()
                                request.InputStream.CopyTo(ms)
                                bodyBytes = ms.ToArray()
                            End Using
                        Else
                            ' for base64 string
                            Using reader As New StreamReader(request.InputStream, request.ContentEncoding)
                                Dim base64String As String = reader.ReadToEnd()
                                bodyBytes = Convert.FromBase64String(base64String)
                            End Using
                        End If

                        ' print
                        If isPdf Then
                            Using stream = New MemoryStream(bodyBytes)
                                Using pdf = PdfDocument.Load(stream)
                                    If Debugger.IsAttached Then pdf.Save("./1.pdf")
                                    Using printdoc = pdf.CreatePrintDocument()
                                        printdoc.PrinterSettings.PrinterName = My.Settings.Printer
                                        printdoc.DefaultPageSettings.Margins = New Margins(0, 0, 0, 100)
                                        printdoc.Print()
                                    End Using

                                End Using
                            End Using
                        Else
                            'Dim hexDump = BitConverter.ToString(bodyBytes).Replace("-", " ")
                            'File.WriteAllText(".\printerdump.txt", $"Received {bodyBytes.Length} bytes: {hexDump}")
                            If IsImage = False Then
                                posPrinter.SetDocument(bodyBytes)
                                posPrinter.PrintDocument()
                            Else
                                'Dim printDoc As New PrintDocument()

                                '' Convert to Bitmap
                                'Dim bitmap As Bitmap
                                'Using ms As New MemoryStream(bodyBytes)
                                '    bitmap = New Bitmap(ms)
                                'End Using

                                '' Optional: Set printer name and paper size
                                'printDoc.PrinterSettings.PrinterName = My.Settings.Printer
                                'printDoc.DefaultPageSettings.Margins = New Margins(0, 0, 0, 0)
                                'printDoc.DefaultPageSettings.PaperSize = New PaperSize("Custom", Bitmap.Width, Bitmap.Height)

                                'AddHandler printDoc.PrintPage, Sub(sender, e)
                                '                                   e.Graphics.DrawImage(bitmap, 0, 0)
                                '                                   e.HasMorePages = False
                                '                               End Sub

                                'printDoc.Print()
                            End If
                        End If

                        ' write response
                        Dim responseString As String = $"OK"
                        Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(responseString)
                        context.Response.ContentLength64 = buffer.Length
                        context.Response.ContentType = "text/plain"
                        context.Response.StatusCode = 200
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length)
                        context.Response.OutputStream.Close()

                    Catch ex As Exception

                        ' write response
                        Dim responseString As String = $"Error"
                        Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(responseString)
                        context.Response.ContentLength64 = buffer.Length
                        context.Response.ContentType = "text/plain"
                        context.Response.StatusCode = 500
                        context.Response.OutputStream.Write(buffer, 0, buffer.Length)
                        context.Response.OutputStream.Close()

                    End Try

                End If

                If request.HttpMethod.ToUpper = "GET" Then
                    Dim responseString As String = $"PRINTER CLIENT IS RUNNING AT: {lblHost.Text}"
                    Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(responseString)
                    context.Response.ContentLength64 = buffer.Length
                    context.Response.ContentType = "text/plain"
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length)
                    context.Response.OutputStream.Close()
                End If

            Catch ex As Exception
            End Try

        Loop

    End Sub

    Sub StopServer()
        If _SERVER IsNot Nothing Then
            If _SERVER.IsListening Then
                _SERVER.Stop()
                _SERVER = Nothing
            End If
        End If
    End Sub

End Class