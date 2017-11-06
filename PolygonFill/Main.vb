Public Class Main
    'Global variables
    Dim PolygonArray As List(Of Tpolygon)
    Dim edgetable As List(Of EdgeTable)
    Dim AET As LinkedList(Of EdgeTable)
    'Graphic handler
    Dim bit As Bitmap
    Dim g As Graphics
    Dim myPen As Pen
    'Save location
    Dim FILE_PATH As String = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file")
    Dim FILE_NAME As String = IO.Path.Combine(FILE_PATH, "file1.txt")
    'Load location
    Dim fileReader As String

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bit = New Bitmap(PictureBox.Width, PictureBox.Height)
        g = Graphics.FromImage(bit)
        myPen = New Pen(Color.Black, 1)
        'g.DrawLine(myPen, 3, 1, 3, 1)
        'g.DrawLine(myPen, 3, 2, 4, 2)
        'g.DrawLine(myPen, 2, 3, 4, 3)
        'g.DrawLine(myPen, 2, 4, 5, 4)
        'g.DrawLine(myPen, 1, 5, 3, 5)
        'PictureBox.Image = bit

    End Sub

    Private Sub MouseMovement(sender As Object, e As MouseEventArgs) Handles PictureBox.MouseMove
        'Get the cursor position
        lblX.Text = e.X
        lblY.Text = e.Y
    End Sub

End Class
