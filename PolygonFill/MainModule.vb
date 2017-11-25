Module MainModule

    Dim edgetable As New List(Of EdgeTable)
    'The SET table
    Dim AET As New AEL
    'The AEL 
    Dim stacker As New Stack(Of EdgeTable)

    Public Sub FillPolygon(a As Tpolygon, ByRef g As Graphics, ByRef bmp As Bitmap, pen As Pen)
        edgetable.Clear()
        stacker.Clear()
        'Fill the edge table
        FillSET(a) ' no bug
        'Set new AET
        'displaySET(edgetable)
        AET = New AEL
        'Tranverse the AET
        ProcessAET(g, bmp, pen)

    End Sub

    Public Sub FillSET(a As Tpolygon)
        'filling the SET if the polygon is valid (doesn't cross)

        edgetable = New List(Of EdgeTable)
        'get the min and max to know how index that is needed
        Dim min As Integer = getMinimumY(a.vertices)
        Dim max As Integer = getMaximumY(a.vertices)
        Dim size As Integer = max - min + 1
        resizeArray(edgetable, size)  'resize the array for the iteration in AEL, may cause problem
        Dim d, horizon As Integer
        'the increment
        For i As Integer = 0 To a.size
            d = i + 1
            If i = a.size Then
                'if it's the last index, make the line with last point and start point
                d = 0
            End If
            horizon = a.vertices(d).Y - a.vertices(i).Y
            If horizon < 0 Then horizon = -(horizon)
            If Not (a.vertices(i).Y = a.vertices(d).Y) Then
                'If it Is Not horizontal line (a.vertices(i).Y = a.vertices(d).Y) Then fill all data 
                Dim temp As New EdgeTable
                temp.normalize = min
                temp.ymin = If(a.vertices(i).Y <= a.vertices(d).Y, a.vertices(i).Y, a.vertices(d).Y)
                temp.ymax = If(a.vertices(i).Y >= a.vertices(d).Y, a.vertices(i).Y, a.vertices(d).Y)
                temp.xmin = If(a.vertices(i).Y <= a.vertices(d).Y, a.vertices(i).X, a.vertices(d).X)
                temp.dx = a.vertices(d).X - a.vertices(i).X
                temp.dy = a.vertices(d).Y - a.vertices(i).Y
                temp.carry = 0
                temp.nxt = Nothing
                If temp.dy < 0 Then
                    temp.dy = -temp.dy
                    temp.dx = -temp.dx
                End If
                'the data is filled
                Dim index As Integer = temp.ymin - min
                'normalize the index that will be use
                sortedInsertion(edgetable(index), temp)
            End If
        Next

    End Sub

    Public Sub ProcessAET(ByRef g As Graphics, ByRef bmp As Bitmap, pen As Pen)
        'Loop from index 0 to Max
        Dim current As EdgeTable
        For i As Integer = 0 To edgetable.Count - 1
            'assign the temporary variable to save the current data 
            current = edgetable(i)
            'delete the single expired
            If i > 0 Then CheckSingleExpired(i)
            'insert the new edges (sorted)

            While Not (current Is Nothing)
                AET.Add(current)
                'MsgBox("break")
                current = current.nxt
                'MsgBox(AET.length.ToString)
            End While
            'draw lines (don't forget about the normalization)
            drawlines(i, g, bmp, pen)
            'delete the double expired
            'CheckDoubleExpired() 'cause bug
            ' If i > 0 Then AET.double_expired(i)
            'update 
            AET.Update()
            'sort
            AET.Sorted()
            'sortAETStackVersion() ' is failed
        Next
    End Sub

    Public Sub updateAET()
        AET.update()
    End Sub

    Public Sub sortAET()
        AET.sorted()
    End Sub

    Public Sub drawlines(y As Integer, ByRef g As Graphics, ByRef bmp As Bitmap, pen As Pen)
        If AET.length > 0 Then
            Dim data As EdgeTable = AET.head
            Dim data2 As EdgeTable = data.nxt
            Dim i As Integer = 0
            While Not (data Is Nothing Or data2 Is Nothing)
                ' MsgBox(i.ToString + ": "+data.xmin.ToString + " " + data2.xmin.ToString)
                If (data.xmin = data2.xmin) Then
                    'setpixel
                    bmp.SetPixel(data.xmin, y + data.normalize, pen.Color)
                Else
                    'MsgBox(AET.length.ToString + " -- " + (y + data.normalize).ToString)
                    g.DrawLine(pen, data.xmin, y + data.normalize, data2.xmin, y + data2.normalize)
                End If
                data = data.nxt.nxt
                i = i + 1
                If Not (data Is Nothing) Then
                    data2 = data.nxt
                Else
                    Exit While
                End If
            End While


        End If
    End Sub

    Public Sub InsertEdgesToAET(data As EdgeTable)
        'Check the scanline, if there is new edge, insert it
        AET.add(data)
    End Sub

    Public Sub CheckSingleExpired(y As Integer)
        'create a counter
        Dim currentdata As EdgeTable = AET.head
        Dim counter As Integer = 0
        If (AET.CountAET() > 0) Then
            While Not (currentdata Is Nothing)
                If currentdata.ymax = y + currentdata.normalize Then
                    'delete node
                    AET.remove(counter)
                    counter = counter - 1
                End If
                counter = counter + 1
                currentdata = currentdata.nxt
        End While
        End If
        'replace AET with new one
    End Sub

    Public Sub CheckDoubleExpired()
        'create new container
        Dim temp As EdgeTable = Nothing
        If (AET.CountAET() > 1) Then
            Dim data As EdgeTable = AET.head
            Dim data2 As EdgeTable = data.nxt
            While Not (data Is Nothing Or data2 Is Nothing)
                If (data.xmin = data2.xmin) And (data.carry = 0) And (data2.carry = 0) Then
                    'delete node by ignoring it
                    data = data.nxt.nxt
                    If Not (data Is Nothing) Then
                        data2 = data.nxt
                    End If
                Else
                    sortedInsertion(temp, data)
                    sortedInsertion(temp, data2)
                    data = data.nxt.nxt
                    If Not (data Is Nothing) Then
                        data2 = data.nxt
                    End If
                End If
            End While
        End If
        'replace AET with new one
    End Sub


    Public Sub sortedInsertion(ByRef target As EdgeTable, temp As EdgeTable)
        If target Is Nothing Then
            'if there is no data in that index
            'assign the temp to that index 
            target = temp
        Else
            stacker.Clear()
            Dim targettemp As EdgeTable = Nothing
            Dim node As EdgeTable = target
            Dim i As Integer = 0
            ' node as the current node that will be used to tranverse the SET
            While Not (node Is Nothing)
                If node.xmin < temp.xmin Then
                    'if xmin value of current node is less than the temp data
                    If node.nxt Is Nothing Then
                        'assign it, it's the last place
                        'target.nxt = temp
                        If i = 0 Then stacker.Push(node)
                        stacker.Push(temp)
                        Exit While
                    Else
                        'it's not the last place, tranverse the SET
                        stacker.Push(node)
                        node = node.nxt
                    End If
                ElseIf node.xmin > temp.xmin Then
                    'if xmin value of current node is more than the temp data
                    Dim store As EdgeTable = node
                    'store the current node's data
                    'target = temp
                    stacker.Push(temp)
                    'replace the current node with the temp data
                    If node.nxt Is Nothing Then
                        'if it's the last, just assign the stored data to the last
                        'target.nxt = store
                        stacker.Push(node)
                        Exit While
                    Else
                        'if it's not the last
                        'tranverse the SET, but use the stored data as the temp data
                        temp = store
                        node = node.nxt
                    End If
                Else
                    ' if xmin of both current node and temp data is same
                    If (node.dx / node.dy) > (temp.dx / temp.dy) Then
                        'if value of current node is more than the temp data
                        Dim store As EdgeTable = node
                        'store the current node's data
                        'target = temp
                        stacker.Push(temp)
                        'replace the current node with the temp data
                        If node.nxt Is Nothing Then
                            'if it's the last, just assign the stored data to the last
                            'target.nxt = store
                            stacker.Push(node)
                            Exit While
                        Else
                            'if it's not the last
                            'tranverse the SET, but use the stored data as the temp data
                            temp = store
                            node = node.nxt
                        End If

                    Else
                        'if the value of current node is same or less than the temp data
                        If node.nxt Is Nothing Then
                            'If it's the last, just assign the stored data to the last
                            'target.nxt = temp
                            If i = 0 Then stacker.Push(node)
                            stacker.Push(temp)
                            Exit While
                        Else
                            'if it's not the last
                            'tranverse the SET
                            stacker.Push(node)
                            node = node.nxt
                        End If
                    End If
                End If
                i = i + 1
            End While
            'MsgBox(stacker.Count())
            refillAET(stacker, targettemp)
            target = targettemp
            stacker.Clear()
        End If
    End Sub

    Public Sub refillAET(ByRef s As Stack(Of EdgeTable), ByRef target As EdgeTable)

        target = Nothing
        Dim prevtemp As New EdgeTable
        Dim temp As EdgeTable
        If s.Count > 0 Then
            temp = s.Pop()
            temp.nxt = Nothing
        Else
            temp = Nothing
        End If
        While s.Count() > 0
            prevtemp = s.Pop()
            'MsgBox(prevtemp.xmin.ToString + " " + prevtemp.ymax.ToString)
            prevtemp.nxt = temp
            temp = prevtemp
        End While
        target = temp
        s.Clear()
    End Sub

    Public Sub resizeArray(a As List(Of EdgeTable), size As Integer)
        'resizing array and assign the NULL value to each index
        For i As Integer = 0 To size
            a.Add(Nothing)
        Next
    End Sub

    Public Sub displaySET(temp As List(Of EdgeTable))
        'to check the content of SET
        For i = 0 To temp.Count - 1
            If Not (temp(i) Is Nothing) Then
                Dim node As EdgeTable = temp(i)
                Dim str As String
                While Not (node Is Nothing)
                    str = ""
                    str = node.ymin.ToString + " " + node.ymax.ToString + " " + node.xmin.ToString + " " + node.dx.ToString
                    MsgBox(str)
                    node = node.nxt
                End While

            End If
        Next
    End Sub

    Public Sub displayAET(temp As EdgeTable)
        'to check the content of SET
        If Not (temp Is Nothing) Then
            Dim node As EdgeTable = temp
            Dim str As String
            While Not (node Is Nothing)
                str = ""
                str = str + node.ymin.ToString + " " + node.ymax.ToString + " " + node.xmin.ToString + " " + node.dx.ToString
                node = node.nxt
                MsgBox(str)
            End While
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

    Public Function CountEdge(head As EdgeTable)
        Dim i As Integer = 0
        Dim node As EdgeTable = head
        While Not (node Is Nothing)
            i = i + 1
            node = node.nxt
        End While
        Return i
    End Function

End Module
