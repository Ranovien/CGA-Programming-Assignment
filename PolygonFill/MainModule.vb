Module MainModule

    Dim edgetable As List(Of EdgeTable)
    'The SET table
    Dim temp As EdgeTable
    'Temporary store the part of edgetable data
    Dim AET As New LinkedList(Of EdgeTable)
    'The AEL 

    Public Sub FillPolygon(a As Tpolygon, ByRef g As Graphics, pen As Pen)
        'Fill the edge table
        'Tranverse the AEL
    End Sub

    Public Sub FillSET(a As Tpolygon)
        'filling the SET if the polygon is valid (doesn't cross)
        If a.isAbleToFIlled Then
            edgetable = New List(Of EdgeTable)
            Dim min As Integer = getMinimumY(a.Vertices)
            Dim max As Integer = getMaximumY(a.Vertices)
            Dim size As Integer = max - min + 1
            resizeArray(edgetable, size)
            'resize the array for the iteration in AEL
            Dim d As Integer
            'the increment
            For i As Integer = 0 To a.Size
                d = i + 1
                If i = a.Size Then
                    d = 0
                End If
                If Not (a.Vertices(i).Y = a.Vertices(d).Y) Then
                    'If it Is Not horizontal line Then fill all data 
                    temp = New EdgeTable
                    temp.normalize = min
                    temp.ymin = If(a.Vertices(i).Y <= a.Vertices(d).Y, a.Vertices(i).Y, a.Vertices(d).Y)
                    temp.ymax = If(a.Vertices(i).Y >= a.Vertices(d).Y, a.Vertices(i).Y, a.Vertices(d).Y)
                    temp.xmin = If(a.Vertices(i).Y <= a.Vertices(d).Y, a.Vertices(i).X, a.Vertices(d).X)
                    temp.dx = a.Vertices(d).X - a.Vertices(i).X
                    temp.dy = a.Vertices(d).Y - a.Vertices(i).Y
                    temp.carry = 0
                    If temp.dy < 0 Then
                        temp.dy = -temp.dy
                        temp.dx = -temp.dx
                    End If
                    Dim index As Integer = temp.ymin - min
                    If edgetable(index) Is Nothing Then
                        edgetable(index) = temp
                    Else
                        Dim node As EdgeTable = edgetable(index)
                        While (True)
                            If node.xmin < temp.xmin Then
                                If node.Nxt Is Nothing Then
                                    edgetable(index) = temp
                                    Exit While
                                Else
                                    node = node.nxt
                                End If
                            ElseIf node.xmin > temp.xmin Then
                                'edgetable(index).AddBefore(node, tempadd)
                                Dim store As EdgeTable = node
                                edgetable(index) = temp
                                If node.nxt Is Nothing Then
                                    edgetable(index).nxt = store
                                    Exit While
                                Else
                                    temp = store
                                    node = node.nxt
                                End If
                            Else
                                If (node.dx / node.dy) > (temp.dx / temp.dy) Then
                                    'edgetable(index).AddBefore(node, tempadd)
                                    Dim store As EdgeTable = node
                                    edgetable(index) = temp
                                    If node.nxt Is Nothing Then
                                        edgetable(index).nxt = store
                                        Exit While
                                    Else
                                        temp = store
                                        node = node.nxt
                                    End If
                                    Exit While
                                Else
                                    If node.nxt Is Nothing Then
                                        'edgetable(index).AddLast(tempadd)
                                        edgetable(index).nxt = temp
                                        Exit While
                                    Else
                                        node = node.nxt
                                    End If
                                End If
                            End If
                        End While
                    End If
                End If
            Next
            displaySET(edgetable)
        End If
    End Sub

    Public Sub resizeArray(a As List(Of EdgeTable), size As Integer)
        For i As Integer = 0 To size
            a.Add(Nothing)
        Next
    End Sub

    Public Sub displaySET(temp As List(Of EdgeTable))
        For i = 0 To temp.Count - 1
            If Not (temp(i) Is Nothing) Then
                Dim node As EdgeTable = temp(i)
                While (True)
                    If Not (node Is Nothing) Then
                        Dim str As String = ""
                        str = str + node.ymin.ToString + " " + node.ymax.ToString
                        str = str + Environment.NewLine
                        MsgBox(str)
                        node = node.nxt
                    Else
                        Exit While
                    End If
                End While

            End If
        Next
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
End Module
