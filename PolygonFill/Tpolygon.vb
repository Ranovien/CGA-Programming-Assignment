Public Class Tpolygon
    Public Vertices As List(Of Point) ' array of points
    Public TColor As Color ' color for filling
    Public isFilled As Boolean 'status of polygon
    Public Size As Integer ' size of array

    Public Sub New() ' constructor
        Vertices = New List(Of Point)
        TColor = Color.Blue
        isFilled = False
        Size = -1
    End Sub

    Public Function ispolygon()
        Return (Size > 1)
    End Function

    Public Sub InputVertex(point As Point) ' input a vertex
        Vertices.Add(point)
        Size = Size + 1
    End Sub

    Public Sub ChangeColor(color As Color) ' change the color of filling
        TColor = color
    End Sub

    Public Sub ChangeFillStatus() ' change the polygon from fill/hollow
        isFilled = Not isFilled
    End Sub

    Public Sub Drawpolygon(ByRef g As Graphics)
        Dim Pen As New Pen(TColor, 1)
        For i As Integer = 0 To Size - 1
            g.DrawLine(Pen, Vertices(i), Vertices(i + 1))
            If i = Size - 1 Then
                g.DrawLine(Pen, Vertices(i + 1), Vertices(0))
            End If
        Next
        Pen.Dispose()
    End Sub

End Class
