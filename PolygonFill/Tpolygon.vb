Public Class Tpolygon
    Public vertices As List(Of Point) ' array of points
    Public tcolor As Color ' color for filling
    Public isfilled As Boolean 'status of polygon
    Public size As Integer ' size of array
    Public canbefilled As Boolean ' check if the polygon is crossed or not

    Public Sub New() ' constructor
        vertices = New List(Of Point)
        tcolor = Color.Blue
        isfilled = False
        size = -1
        canbefilled = True
    End Sub
    Public Function isAbleToFIlled()
        'Check if the polygon can be filled or not
        'Inside outside?
        'by Handy
        Dim verticesLength As Integer = size, intersection As Double, t As Double
        For i As Integer = 0 To verticesLength
            For j As Integer = i To verticesLength
                intersection = xIntersection(vertices(i).X, vertices(i).Y, vertices((i + 1) Mod verticesLength).X, vertices((i + 1) Mod verticesLength).Y, vertices(j).X, vertices(j).Y, vertices((j + 1) Mod verticesLength).X, vertices((j + 1) Mod verticesLength).Y)
                t = (intersection - vertices(i).X) / (vertices((i + 1) Mod verticesLength).X - vertices(i).X)
                If (0 < t And t < 1) Then
                    t = (intersection - vertices(j).X) / (vertices((j + 1) Mod verticesLength).X - vertices(j).X)
                    If (0 < t And t < 1) Then
                        canbefilled = True
                        canbefilled = False
                        Return canbefilled
                    End If
                End If
            Next
        Next
        canbefilled = True
        Return canbefilled
    End Function

    Public Function isPolygon()
        Return (size > 1)
    End Function

    Public Sub InputVertex(point As Point) ' input a vertex
        vertices.Add(point)
        size = size + 1
    End Sub

    Public Sub replaceVertex(point As Point, i As Integer) ' replace the vertex with the new one
        ' i = index
        vertices(i) = point
    End Sub

    Public Sub ChangeColor(color As Color) ' change the color of filling
        tcolor = color
    End Sub

    Public Sub ChangeFillStatus() ' change the polygon from fill/hollow
        isfilled = Not isfilled
    End Sub

    Public Sub Drawpolygon(ByRef g As Graphics)
        Dim pen As New Pen(tcolor, 1)
        For i As Integer = 0 To size - 1
            g.DrawLine(pen, vertices(i), vertices(i + 1))
            If i = size - 1 Then
                g.DrawLine(pen, vertices(i + 1), vertices(0))
            End If
        Next
        pen.Dispose()
    End Sub

    Private Function xIntersection(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer, x3 As Integer, y3 As Integer, x4 As Integer, y4 As Integer) ' only need either x-intersection Or y-intersection To find the t
        'by Handy
        Dim num As Integer = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4),
        den As Integer = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4)
        Return num / den
    End Function
End Class
