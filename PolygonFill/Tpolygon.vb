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

End Class
