<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Licence = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ProcessList = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'Licence
        '
        Me.Licence.Icon = CType(resources.GetObject("Licence.Icon"), System.Drawing.Icon)
        Me.Licence.Text = "NotifyIcon1"
        Me.Licence.Visible = True
        '
        'ProcessList
        '
        Me.ProcessList.FormattingEnabled = True
        Me.ProcessList.Location = New System.Drawing.Point(12, 12)
        Me.ProcessList.Name = "ProcessList"
        Me.ProcessList.Size = New System.Drawing.Size(260, 238)
        Me.ProcessList.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.ProcessList)
        Me.Name = "Form1"
        Me.Text = "Hello"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Licence As NotifyIcon
    Friend WithEvents ProcessList As ListBox
End Class
