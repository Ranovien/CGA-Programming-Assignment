Public Class Main
    'Global variables
    Dim PolygonArray As New List(Of Tpolygon)
    Dim TempPolygon As New Tpolygon
    Dim currx, curry, selectedpoly, selectedpoint As Integer
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
        myPen = New Pen(Color.Black, 5)
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
        Getcursor()
        Dim temppoint = New Point(currx, curry)
        TempPolygon.InputVertex(temppoint)
    End Sub

    Private Sub EndDrawPolygon(sender As Object, e As EventArgs) Handles PictureBox.MouseDoubleClick
        'Draw the last vertex of the polygon
        If (TempPolygon.isPolygon()) Then
            'execute if the polygon is valid
            PolygonArray.Add(TempPolygon)
            UpdatePolyList()
            Display()
            TempPolygon = New Tpolygon
        Else
            MsgBox("Need 1 more vertex!")
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
        'Update the listbox for point of certain polygon
        Dim temp(2) As String
        temp = Split(PolyList.SelectedItem.ToString, " ")
        selectedpoly = Val(temp(1))
        PointList.Items.Clear()
        For i As Integer = 0 To PolygonArray(selectedpoly).Vertices.Count - 1
            PointList.Items.Add("Point " + i.ToString)
        Next
    End Sub

    Private Sub EditPointList(sender As Object, e As EventArgs) Handles PointList.SelectedIndexChanged
        'Able to edit the selected point of certain polygon
        Dim temp(2) As String
        temp = Split(PointList.SelectedItem.ToString, " ")
        selectedpoint = Val(temp(1))
        txteditX.Text = PolygonArray(selectedpoly).Vertices(selectedpoint).X
        txteditY.Text = PolygonArray(selectedpoly).Vertices(selectedpoint).Y

    End Sub

    Public Sub Display()
        'Display the polygon
        'the Polygon fill may be here

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


End Class
