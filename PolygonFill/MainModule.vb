Module MainModule

    Dim edgetable As List(Of EdgeTable)
    'The SET table
    Dim temp As EdgeTable
    'Temporary store the part of edgetable data
    Dim AET As EdgeTable
    'The AEL 

    Public Sub FillPolygon(a As Tpolygon, ByRef g As Graphics, pen As Pen)
        'Fill the edge table
        FillSET(a)
        'Set new AET
        'AET = New EdgeTable
        AET = Nothing
        'Tranverse the AET
        'MsgBox("boom")
        ProcessAET(g, pen)
    End Sub

    Public Sub FillSET(a As Tpolygon)
        'filling the SET if the polygon is valid (doesn't cross)
        If a.isAbleToFIlled Then
            edgetable = New List(Of EdgeTable)
            'get the min and max to know how index that is needed
            Dim min As Integer = getMinimumY(a.vertices)
            Dim max As Integer = getMaximumY(a.Vertices)
            Dim size As Integer = max - min + 1
            resizeArray(edgetable, size)
            'resize the array for the iteration in AEL, may cause problem
            Dim d As Integer
            'the increment
            For i As Integer = 0 To a.size
                d = i + 1
                If i = a.size Then
                    'if it's the last index, make the line with last point and start point
                    d = 0
                End If
                If Not (a.vertices(i).Y = a.vertices(d).Y) Then
                    'If it Is Not horizontal line Then fill all data 
                    temp = New EdgeTable
                    temp.normalize = min
                    temp.ymin = If(a.vertices(i).Y <= a.vertices(d).Y, a.vertices(i).Y, a.vertices(d).Y)
                    temp.ymax = If(a.vertices(i).Y >= a.vertices(d).Y, a.vertices(i).Y, a.vertices(d).Y)
                    temp.xmin = If(a.vertices(i).Y <= a.vertices(d).Y, a.vertices(i).X, a.vertices(d).X)
                    temp.dx = a.vertices(d).X - a.vertices(i).X
                    temp.dy = a.vertices(d).Y - a.vertices(i).Y
                    temp.carry = 0
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
            'displaySET(edgetable) 'to check the result
        End If
    End Sub

    Public Sub ProcessAET(ByRef g As Graphics, pen As Pen)
        'Loop from index 0 to Max
        For i As Integer = 0 To edgetable.Count - 1

            'delete the single expired
            CheckSingleExpired(i)

            'insert the new edges (sorted)
            InsertEdgesToAET(i)
            'draw lines (don't forget about the normalization)
            drawlines(i, g, pen)
            'delete the double expired
            CheckDoubleExpired()
            'update 
            updateAET()
            'sort
            sortAET()
        Next
    End Sub

    Public Sub updateAET()
        'create new container
        If (CountAET() > 0) Then
            Dim temp As EdgeTable = Nothing
            Dim data As EdgeTable = AET
            While Not (data Is Nothing)
                'Update the data
                data.carry = data.carry + data.dx
                If (data.dx < 0) Then
                    While (-(data.carry + data.carry) >= data.dy)
                        data.carry = data.carry + data.dy
                        data.xmin = data.xmin - 1
                    End While
                Else
                    While ((data.carry + data.carry) >= data.dy)
                        data.carry = data.carry - data.dy
                        data.xmin = data.xmin + 1
                    End While
                End If
                sortedInsertion(temp, data)
                data = data.nxt
            End While
        End If
        AET = temp
        'replace AET with new one
    End Sub

    Public Sub sortAET()
        Dim temp As EdgeTable = Nothing
        'create new container
        If Not (AET Is Nothing) Then
            Dim data As EdgeTable = AET
            While Not (data Is Nothing)
                sortedInsertion(temp, data)
                data = data.nxt
            End While
        End If
        AET = temp
        'replace AET with new one
    End Sub

    Public Sub drawlines(y As Integer, ByRef g As Graphics, pen As Pen)
        If (CountAET() >= 2) Then
            Dim data As EdgeTable = AET
            Dim data2 As EdgeTable = AET.nxt
            While Not (data Is Nothing Or data2 Is Nothing)
                g.DrawLine(pen, data.xmin, y + data.normalize, data2.xmin, y + data2.normalize)
                data = data.nxt.nxt
                If Not (data Is Nothing) Then
                    data2 = data.nxt
                End If
            End While
        End If
    End Sub

    Public Sub InsertEdgesToAET(y As Integer)
        'Check the scanline, if there is new edge, insert it
        If Not (edgetable(y) Is Nothing) Then
            Dim temp As EdgeTable = AET
            Dim data As EdgeTable = edgetable(y)
            While Not (data Is Nothing)
                'insert the new edge
                sortedInsertion(temp, data)
                data = data.nxt
            End While
            AET = temp
        End If

    End Sub

    Public Sub CheckSingleExpired(y As Integer)
        Dim temp As New EdgeTable
        'create new container
        If (CountAET() >= 1) Then
            Dim data As EdgeTable = AET
            While Not (data Is Nothing)
                If data.ymin = y Then
                    'delete node by ignoring it
                    data = data.nxt
                Else
                    sortedInsertion(temp, data)
                    data = data.nxt
                End If
            End While
        End If

        AET = temp
        'replace AET with new one
    End Sub

    Public Sub CheckDoubleExpired()
        'create new container
        Dim temp As New EdgeTable
        If (CountAET() > 1) Then
            Dim data As EdgeTable = AET
            Dim data2 As EdgeTable = AET.nxt
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
        AET = temp
        'replace AET with new one
    End Sub

    Public Sub sortedInsertion(ByRef target As EdgeTable, temp As EdgeTable)
        If target Is Nothing Then
            'if there is no data in that index
            'assign the temp to that index 
            target = temp
        Else
            Dim node As EdgeTable = target
            ' node as the current node that will be used to tranverse the SET
            While (True)
                If node.xmin < temp.xmin Then
                    'if xmin value of current node is less than the temp data
                    If node.nxt Is Nothing Then
                        'assign it, it's the last place
                        target.nxt = temp
                        Exit While
                    Else
                        'it's not the last place, tranverse the SET
                        node = node.nxt
                    End If
                ElseIf node.xmin > temp.xmin Then
                    'if xmin value of current node is more than the temp data
                    Dim store As EdgeTable = node
                    'store the current node's data
                    target = temp
                    'replace the current node with the temp data
                    If node.nxt Is Nothing Then
                        'if it's the last, just assign the stored data to the last
                        target.nxt = store
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
                        target = temp
                        'replace the current node with the temp data
                        If node.nxt Is Nothing Then
                            'if it's the last, just assign the stored data to the last
                            target.nxt = store
                            Exit While
                        Else
                            'if it's not the last
                            'tranverse the SET, but use the stored data as the temp data
                            temp = store
                            node = node.nxt
                        End If
                        Exit While
                    Else
                        'if the value of current node is same or less than the temp data
                        If node.nxt Is Nothing Then
                            'If it's the last, just assign the stored data to the last
                            target.nxt = temp
                            Exit While
                        Else
                            'if it's not the last
                            'tranverse the SET
                            node = node.nxt
                        End If
                    End If
                End If
            End While
        End If
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

    Public Function CountAET()
        Dim i = 0
        Dim node = AET
        While Not (node Is Nothing)
            i = i + 1
            node = node.nxt
        End While
        Return i
    End Function
End Module
