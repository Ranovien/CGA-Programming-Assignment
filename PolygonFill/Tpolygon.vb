Public Class Tpolygon
    Dim Vertices As List(Of Point) ' array of points
    Dim TColor As Color ' color for filling
    Dim isFilled As Boolean 'status of polygon
    Dim Size As Integer ' size of array

    Private Sub New() ' constructor
        Vertices = Nothing
        TColor = Color.Blue
        isFilled = False
        Size = 0
    End Sub

    Private Sub InputVertex(point As Point) ' input a vertex
        Vertices.Add(point)
        Size = Size + 1
    End Sub

    Private Sub ChangeColor(color As Color) ' change the color of filling
        TColor = color
    End Sub

    Private Sub ChangeFillStatus() ' change the polygon from fill/hollow
        isFilled = Not isFilled
    End Sub

End Class
