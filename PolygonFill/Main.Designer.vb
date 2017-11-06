<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.PictureBox = New System.Windows.Forms.PictureBox()
        Me.PolyList = New System.Windows.Forms.ListBox()
        Me.PointList = New System.Windows.Forms.ListBox()
        Me.txteditX = New System.Windows.Forms.TextBox()
        Me.txteditY = New System.Windows.Forms.TextBox()
        Me.btndelpoly = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btndelpoint = New System.Windows.Forms.Button()
        Me.lblXEdit = New System.Windows.Forms.Label()
        Me.lblYEdit = New System.Windows.Forms.Label()
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnColorpick = New System.Windows.Forms.Button()
        Me.lblXpos = New System.Windows.Forms.Label()
        Me.lblYpos = New System.Windows.Forms.Label()
        Me.lblX = New System.Windows.Forms.Label()
        Me.lblY = New System.Windows.Forms.Label()
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox
        '
        Me.PictureBox.BackColor = System.Drawing.SystemColors.HighlightText
        Me.PictureBox.Location = New System.Drawing.Point(13, 13)
        Me.PictureBox.Name = "PictureBox"
        Me.PictureBox.Size = New System.Drawing.Size(796, 540)
        Me.PictureBox.TabIndex = 0
        Me.PictureBox.TabStop = False
        '
        'PolyList
        '
        Me.PolyList.FormattingEnabled = True
        Me.PolyList.ItemHeight = 20
        Me.PolyList.Location = New System.Drawing.Point(834, 13)
        Me.PolyList.Name = "PolyList"
        Me.PolyList.Size = New System.Drawing.Size(152, 264)
        Me.PolyList.TabIndex = 1
        '
        'PointList
        '
        Me.PointList.FormattingEnabled = True
        Me.PointList.ItemHeight = 20
        Me.PointList.Location = New System.Drawing.Point(1008, 13)
        Me.PointList.Name = "PointList"
        Me.PointList.Size = New System.Drawing.Size(106, 264)
        Me.PointList.TabIndex = 2
        '
        'txteditX
        '
        Me.txteditX.Location = New System.Drawing.Point(933, 323)
        Me.txteditX.Name = "txteditX"
        Me.txteditX.Size = New System.Drawing.Size(128, 26)
        Me.txteditX.TabIndex = 3
        '
        'txteditY
        '
        Me.txteditY.Location = New System.Drawing.Point(933, 355)
        Me.txteditY.Name = "txteditY"
        Me.txteditY.Size = New System.Drawing.Size(128, 26)
        Me.txteditY.TabIndex = 4
        '
        'btndelpoly
        '
        Me.btndelpoly.Location = New System.Drawing.Point(834, 283)
        Me.btndelpoly.Name = "btndelpoly"
        Me.btndelpoly.Size = New System.Drawing.Size(152, 34)
        Me.btndelpoly.TabIndex = 5
        Me.btndelpoly.Text = "Delete Polygon"
        Me.btndelpoly.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(834, 323)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(58, 58)
        Me.btnEdit.TabIndex = 6
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btndelpoint
        '
        Me.btndelpoint.Location = New System.Drawing.Point(1008, 283)
        Me.btndelpoint.Name = "btndelpoint"
        Me.btndelpoint.Size = New System.Drawing.Size(106, 34)
        Me.btndelpoint.TabIndex = 7
        Me.btndelpoint.Text = "Delete Point"
        Me.btndelpoint.UseVisualStyleBackColor = True
        '
        'lblXEdit
        '
        Me.lblXEdit.AutoSize = True
        Me.lblXEdit.Location = New System.Drawing.Point(899, 328)
        Me.lblXEdit.Name = "lblXEdit"
        Me.lblXEdit.Size = New System.Drawing.Size(28, 20)
        Me.lblXEdit.TabIndex = 8
        Me.lblXEdit.Text = "X :"
        '
        'lblYEdit
        '
        Me.lblYEdit.AutoSize = True
        Me.lblYEdit.Location = New System.Drawing.Point(899, 358)
        Me.lblYEdit.Name = "lblYEdit"
        Me.lblYEdit.Size = New System.Drawing.Size(28, 20)
        Me.lblYEdit.TabIndex = 9
        Me.lblYEdit.Text = "Y :"
        '
        'btnToggle
        '
        Me.btnToggle.Location = New System.Drawing.Point(834, 400)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(99, 31)
        Me.btnToggle.TabIndex = 10
        Me.btnToggle.Text = "Fill"
        Me.btnToggle.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(834, 527)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 31)
        Me.btnSave.TabIndex = 11
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(1008, 527)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(106, 31)
        Me.btnLoad.TabIndex = 12
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'btnColorpick
        '
        Me.btnColorpick.Location = New System.Drawing.Point(834, 437)
        Me.btnColorpick.Name = "btnColorpick"
        Me.btnColorpick.Size = New System.Drawing.Size(99, 31)
        Me.btnColorpick.TabIndex = 13
        Me.btnColorpick.Text = "Pick Color"
        Me.btnColorpick.UseVisualStyleBackColor = True
        '
        'lblXpos
        '
        Me.lblXpos.AutoSize = True
        Me.lblXpos.Location = New System.Drawing.Point(830, 485)
        Me.lblXpos.Name = "lblXpos"
        Me.lblXpos.Size = New System.Drawing.Size(28, 20)
        Me.lblXpos.TabIndex = 14
        Me.lblXpos.Text = "X :"
        '
        'lblYpos
        '
        Me.lblYpos.AutoSize = True
        Me.lblYpos.Location = New System.Drawing.Point(986, 485)
        Me.lblYpos.Name = "lblYpos"
        Me.lblYpos.Size = New System.Drawing.Size(28, 20)
        Me.lblYpos.TabIndex = 15
        Me.lblYpos.Text = "Y :"
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(864, 485)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(14, 20)
        Me.lblX.TabIndex = 16
        Me.lblX.Text = "-"
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(1020, 485)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(14, 20)
        Me.lblY.TabIndex = 17
        Me.lblY.Text = "-"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1126, 570)
        Me.Controls.Add(Me.lblY)
        Me.Controls.Add(Me.lblX)
        Me.Controls.Add(Me.lblYpos)
        Me.Controls.Add(Me.lblXpos)
        Me.Controls.Add(Me.btnColorpick)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnToggle)
        Me.Controls.Add(Me.lblYEdit)
        Me.Controls.Add(Me.lblXEdit)
        Me.Controls.Add(Me.btndelpoint)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btndelpoly)
        Me.Controls.Add(Me.txteditY)
        Me.Controls.Add(Me.txteditX)
        Me.Controls.Add(Me.PointList)
        Me.Controls.Add(Me.PolyList)
        Me.Controls.Add(Me.PictureBox)
        Me.Name = "Main"
        Me.Text = "Polygon Fill"
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox As PictureBox
    Friend WithEvents PolyList As ListBox
    Friend WithEvents PointList As ListBox
    Friend WithEvents txteditX As TextBox
    Friend WithEvents txteditY As TextBox
    Friend WithEvents btndelpoly As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btndelpoint As Button
    Friend WithEvents lblXEdit As Label
    Friend WithEvents lblYEdit As Label
    Friend WithEvents btnToggle As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnLoad As Button
    Friend WithEvents btnColorpick As Button
    Friend WithEvents lblXpos As Label
    Friend WithEvents lblYpos As Label
    Friend WithEvents lblX As Label
    Friend WithEvents lblY As Label
End Class
