Public Class Main
    'Global variables
    Dim PolygonArray As New List(Of Tpolygon)
    Dim TempPolygon As New Tpolygon
    Dim currx, curry, selectedpoly, selectedpoint As Integer
    Dim fill As Boolean ' toogle fill <-> hollow
    Dim editmode, editpointmode, addmode As Boolean
    'Graphic handler
    Dim bit As Bitmap
    Dim g As Graphics
    Dim mypen As Pen
    'Save location
    Dim FILE_PATH As String = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file")
    Dim FILE_NAME As String = IO.Path.Combine(FILE_PATH, "file1.txt")
    'Load location
    Dim fileReader As String

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'initialize the graphic thingy
        bit = New Bitmap(PictureBox.Width, PictureBox.Height)
        g = Graphics.FromImage(bit)
        mypen = New Pen(Color.Black, 5)
        fill = False
        editmode = False
        editpointmode = False
        addmode = False
    End Sub

    Private Sub MouseMovement(sender As Object, e As MouseEventArgs) Handles PictureBox.MouseMove
        'Get the cursor position, pass it to label
        lblX.Text = e.X
        lblY.Text = e.Y
    End Sub

    Private Sub Getcursor()
        'Get the cursor position from label, pass it to variable
        currx = lblX.Text
        curry = lblY.Text
    End Sub

    Private Sub DrawPolygon(sender As Object, e As EventArgs) Handles PictureBox.MouseClick
        'Draw the vertex of the polygon
        If (editmode = False) Then
            Getcursor()
            Dim temppoint = New Point(currx, curry)
            TempPolygon.InputVertex(temppoint)
        Else
            If (editpointmode = False) Then
                MsgBox("End the edit mode to proceed!")
            Else
                Getcursor()
                Dim temppoint = New Point(currx, curry)
                If addmode = False Then
                    PolygonArray(selectedpoly).replaceVertex(temppoint, selectedpoint)
                Else
                    PolygonArray(selectedpoly).InputVertex(temppoint)
                    'Update PointList
                    PointList.Items.Clear()
                    For i As Integer = 0 To PolygonArray(selectedpoly).vertices.Count - 1
                        PointList.Items.Add("Point " + i.ToString)
                    Next
                End If
                Display()
                TerminateEditMode()
            End If
            End If
    End Sub

    Private Sub EndDrawPolygon(sender As Object, e As EventArgs) Handles PictureBox.MouseDoubleClick
        'Draw the last vertex of the polygon
        If (editmode = False) Then

            If (TempPolygon.isPolygon()) Then
                'execute if the polygon is 
                TempPolygon.isfilled = fill
                TempPolygon.ChangeColor(lblColor.BackColor)
                PolygonArray.Add(TempPolygon)
                UpdatePolyList()
                Display()
                TempPolygon = New Tpolygon
            Else
                MsgBox("Need 1 more vertex!")
            End If

        Else
            MsgBox("End the edit mode to proceed!")
        End If
    End Sub





    Private Sub ToggleFill(sender As Object, e As EventArgs) Handles btnToggle.Click
        If editmode = False Then
            fill = Not (fill)
            If fill = False Then
                lblToggle.Text = "Mode : Hollow"
            Else
                lblToggle.Text = "Mode : Fill"
            End If
        Else
            PolygonArray(selectedpoly).ChangeFillStatus()
            Display()
            TerminateEditMode()
        End If
    End Sub

    Private Sub DeletePolygon(sender As Object, e As EventArgs) Handles btndelpoly.Click
        If editmode = True Then
            PolygonArray.RemoveAt(selectedpoly)
            Display()
            TerminateEditMode()
            'Update the listbox
            PolyList.Items.Clear()
            For i As Integer = 0 To PolygonArray.Count - 1
                PolyList.Items.Add("Polygon " + i.ToString)
            Next
            PointList.Items.Clear()
        Else
            MsgBox("Choose the polygon to be deleted!")
        End If
    End Sub

    Private Sub DeletePoint(sender As Object, e As EventArgs) Handles btndelpoint.Click
        If editmode = True And editpointmode = True Then
            If PolygonArray(selectedpoly).size > 2 Then
                PolygonArray(selectedpoly).vertices.RemoveAt(selectedpoint)
                PolygonArray(selectedpoly).size = PolygonArray(selectedpoly).size - 1
                'Update the point listbox
                PointList.Items.Clear()
                For i As Integer = 0 To PolygonArray(selectedpoly).vertices.Count - 1
                    PointList.Items.Add("Point " + i.ToString)
                Next
                Display()
                TerminateEditMode()
            Else
                MsgBox("The polygon has only 3 Vertex!!")
            End If
        Else
            MsgBox("Choose the point to be deleted!")
        End If
    End Sub

    Private Sub UpdatePolyList()
        'Update the listbox for polygon
        PolyList.Items.Clear()
        For i As Integer = 0 To PolygonArray.Count - 1
            PolyList.Items.Add("Polygon " + i.ToString)
        Next
    End Sub

    Private Sub UpdatePointList(sender As Object, e As EventArgs) Handles PolyList.SelectedIndexChanged
        'activate the edit mode
        editmode = True
        editpointmode = False
        addmode = False
        btnEndEdit.Visible = True
        btnEndEdit.Enabled = True
        'Update the listbox for point of certain polygon
        Dim temp(2) As String
        temp = Split(PolyList.SelectedItem.ToString, " ")
        selectedpoly = Val(temp(1))
        PointList.Items.Clear()
        For i As Integer = 0 To PolygonArray(selectedpoly).vertices.Count - 1
            PointList.Items.Add("Point " + i.ToString)
        Next
    End Sub

    Private Sub EditPointList(sender As Object, e As EventArgs) Handles PointList.SelectedIndexChanged
        'activate the edit mode
        editmode = True
        editpointmode = True
        addmode = False
        btnEndEdit.Visible = True
        btnEndEdit.Enabled = True
        'Able to edit the selected point of certain polygon
        Dim temp(2) As String
        temp = Split(PointList.SelectedItem.ToString, " ")
        selectedpoint = Val(temp(1))
        txteditX.Text = PolygonArray(selectedpoly).Vertices(selectedpoint).X
        txteditY.Text = PolygonArray(selectedpoly).Vertices(selectedpoint).Y

    End Sub

    Public Sub Display()
        'Display the polygon
        'clear first
        g.Clear(Color.White)
        For i As Integer = 0 To PolygonArray.Count - 1
            'DrawpolygonAlt(PolygonArray(i), g)
            If (PolygonArray(i).isfilled = True) Then
                mypen = New Pen(PolygonArray(i).tcolor, 1)
                FillPolygon(PolygonArray(i), g, mypen)
            Else
                PolygonArray(i).Drawpolygon(g)
            End If
        Next
        mypen.Dispose()
        PictureBox.Image = bit
    End Sub

    Private Sub EndEditMode(sender As Object, e As EventArgs) Handles btnEndEdit.Click
        TerminateEditMode()
    End Sub

    Private Sub TerminateEditMode()
        editmode = False
        editpointmode = False
        addmode = False
        btnEndEdit.Visible = False
        btnEndEdit.Enabled = False
    End Sub

    Private Sub EnableAddMode(sender As Object, e As EventArgs) Handles btnAdd.Click
        If editmode = True And editpointmode = True Then
            addmode = True
        Else
            MsgBox("Enter the edit mode first by clicking the point listbox!!")
        End If
    End Sub

    Private Sub Colorpicker(sender As Object, e As EventArgs) Handles btnColorpick.Click
        Dim cDialog As New ColorDialog()

        If editmode = False Then
            cDialog.Color = lblColor.BackColor ' initial selection is current color.
            If (cDialog.ShowDialog() = DialogResult.OK) Then
                lblColor.BackColor = cDialog.Color ' update with user selected color.
            End If
        Else
            cDialog.Color = PolygonArray(selectedpoly).tcolor
            If (cDialog.ShowDialog() = DialogResult.OK) Then
                PolygonArray(selectedpoly).ChangeColor(cDialog.Color)
                Display()
                TerminateEditMode()
            End If
        End If
    End Sub

End Class
