Public Class Form1
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            e.Cancel = True
            Me.Hide()
            Me.WindowState = FormWindowState.Minimized
        Catch
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Me.WindowState = FormWindowState.Minimized
        For Each OneProcess As Process In Process.GetProcesses()
            If OneProcess.MainWindowTitle <> "" Then
                ProcessList.Items.Add(OneProcess.ProcessName)
                If OneProcess.ProcessName.ToLower.Contains("poker") Then
                    OneProcess.CloseMainWindow()
                    OneProcess.Close()
                End If
            End If
        Next
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub Licence_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Licence.MouseDoubleClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ProcessList_MouseClick(sender As Object, e As MouseEventArgs) Handles ProcessList.MouseClick
        Try
            For Each OneProcess As Process In Process.GetProcesses(".")
                If OneProcess.ProcessName.ToLower.Contains("poker") Then
                    OneProcess.CloseMainWindow()
                    OneProcess.Close()

                End If
            Next
        Catch

        End Try
    End Sub
End Class
