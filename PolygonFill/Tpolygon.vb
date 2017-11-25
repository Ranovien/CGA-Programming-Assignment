Public Class Tpolygon
    Public vertices As List(Of Point) ' array of points
    Public tcolor As Color ' color for filling
    Public isfilled As Boolean 'status of polygon
    Public size As Integer ' size of array

    Public Sub New() ' constructor
        vertices = New List(Of Point)
        tcolor = Color.Blue
        isfilled = False
        size = -1
    End Sub

    Public Sub New(tcolor As Integer, isfilled As String) ' constructor
        vertices = New List(Of Point)
        Me.tcolor = Color.FromArgb(tcolor)
        Me.isfilled = Convert.ToBoolean(isfilled)
        Me.size = -1
    End Sub

    Public Function isPolygon()
        Return (size > 1)
    End Function

    Public Sub InputVertex(point As Point) ' input a vertex
        vertices.Add(point)
        size = size + 1
    End Sub

    Public Sub ReplaceVertex(point As Point, i As Integer) ' replace the vertex with the new one
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

    Public Function XIntersection(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer, x3 As Integer, y3 As Integer, x4 As Integer, y4 As Integer) As Double ' only need either x-intersection Or y-intersection To find the t
        Dim num As Integer = (x1 * y2 - y1 * x2) * (x3 - x4) -
           (x1 - x2) * (x3 * y4 - y3 * x4),
        den As Integer = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4)
        Return num / den
    End Function

    Public Function IsEdgesIntersect() As Boolean
        Dim verticesLength As Integer = vertices.Count,
            intersection As Double,
            t As Double

        For i As Integer = 0 To verticesLength - 1
            For j As Integer = i + 1 To verticesLength - 1
                intersection = xIntersection(vertices(i).X, vertices(i).Y, vertices((i + 1) Mod verticesLength).X, vertices((i + 1) Mod verticesLength).Y, vertices(j).X, vertices(j).Y, vertices((j + 1) Mod verticesLength).X, vertices((j + 1) Mod verticesLength).Y)
                t = (intersection - vertices(i).X) / (vertices((i + 1) Mod verticesLength).X - vertices(i).X)
                If 0 < t AndAlso t < 1 Then
                    t = (intersection - vertices(j).X) / (vertices((j + 1) Mod verticesLength).X - vertices(j).X)
                    If 0 < t AndAlso t < 1 Then
                        Return True ' crossed
                    End If
                End If
            Next
        Next

        Return False 'did not cross
    End Function
End Class
