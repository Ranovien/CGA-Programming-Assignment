Module MainModule

    Dim edgetable As EdgeTable()
    Dim tempET As EdgeTable
    Dim AET As New LinkedList(Of EdgeTable)

    Public Sub DrawpolygonAlt(a As Tpolygon, ByRef g As Graphics)
        Dim Pen As New Pen(a.TColor, 5)
        For i As Integer = 0 To a.Size - 1
            g.DrawLine(Pen, a.Vertices(i), a.Vertices(i + 1))
            'MsgBox(a.Vertices(i).X)
            If i = a.Size - 1 Then
                g.DrawLine(Pen, a.Vertices(i + 1), a.Vertices(0))
            End If
            'MsgBox("draw")
        Next
        Pen.Dispose()
    End Sub

    Public Sub FillPolygon(a As Tpolygon, ByRef g As Graphics, pen As Pen)
        'Fill the edge table
        'Tranverse the AEL
    End Sub

    Public Sub FillSET(a As Tpolygon)
        If a.isAbleToFIlled Then
            Dim min As Integer = getMinimumY(a.Vertices)
            Dim max As Integer = getMaximumY(a.Vertices)
            Dim size As Integer = max - min + 1
            resizeArray(edgetable, size)
            Dim d As Integer
            For i As Integer = 0 To a.Size - 1
                d = i + 1
                If i = a.Size - 1 Then
                    d = 0
                End If
                If Not (a.Vertices(i).Y = a.Vertices(d).Y) Then
                    tempET = New EdgeTable
                    tempET.normalize = min
                    tempET.ymin = If(a.Vertices(i).Y < a.Vertices(d).Y, a.Vertices(i).Y, a.Vertices(d).Y)
                    tempET.ymax = If(a.Vertices(i).Y > a.Vertices(d).Y, a.Vertices(i).Y, a.Vertices(d).Y)
                    tempET.xmin = If(a.Vertices(i).Y = tempET.ymin, a.Vertices(i).X, a.Vertices(d).X)
                    tempET.dx = a.Vertices(d).X - a.Vertices(i).X
                    tempET.dy = a.Vertices(d).Y - a.Vertices(i).Y
                    If tempET.dy < 0 Then
                        tempET.dy = -tempET.dy
                        tempET.dx = -tempET.dx
                    End If
                    Dim index = tempET.ymin - min

                    If (edgetable(index) Is Nothing) Then
                        edgetable(tempET.ymin - min) = New EdgeTable
                        edgetable(tempET.ymin - min) = tempET
                    Else
                        'While (edgetable(index).Nxt)
                        'End While
                    End If
            Next

        End If
    End Sub

    Public Function getMinimumY(v As List(Of Point))
        Dim min As Integer
        For i As Integer = 0 To v.Count - 1
            If i = 0 Then
                min = v(i).Y
            Else
                If v(i).Y < min Then
                    min = v(i).Y
                End If
            End If
        Next
        Return min
    End Function

    Public Function getMaximumY(v As List(Of Point))
        Dim max As Integer
        For i As Integer = 0 To v.Count - 1
            If i = 0 Then
                max = v(i).Y
            Else
                If v(i).Y > max Then
                    max = v(i).Y
                End If
            End If
        Next
        Return max
    End Function

    Public Sub resizeArray(s As EdgeTable(), size As Integer)
        ReDim s(size)
        For i As Integer = 0 To size
            s(i) = Nothing
        Next
    End Sub
End Module
