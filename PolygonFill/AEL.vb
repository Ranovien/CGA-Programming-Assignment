﻿Public Class AEL
    'created by handy
    'edited by kevin
    Public head As EdgeTable
    Public length As Integer
    Private s As New Stack(Of EdgeTable)

    Public Sub New()
        head = Nothing
        length = 0
        s.Clear()
    End Sub

    Public Sub add(tempdata As EdgeTable)
        Dim node As EdgeTable = New EdgeTable(tempdata)
        Dim currentNode As EdgeTable = Me.head

        ' an empty list
        If currentNode Is Nothing Then
            Me.head = node
            Me.length = 1
        Else
            While currentNode IsNot Nothing
                If node.xmin < currentNode.xmin OrElse (node.xmin = currentNode.xmin AndAlso node.dx / node.dy < currentNode.dx / currentNode.dy) Then
                    s.Push(node)
                    If currentNode.nxt IsNot Nothing Then
                        s.Push(currentNode)
                        Exit While
                    End If
                Else
                    s.Push(currentNode)
                    If currentNode.nxt Is Nothing Then
                        s.Push(node)
                        Exit While
                    End If
                End If
                currentNode = currentNode.nxt
            End While
            refillAET()
        End If
    End Sub

    Public Sub remove(position As Integer)
        Dim currentNode As EdgeTable = Me.head
        Dim length As Integer = Me.length,
            counter As Integer = 0,
            previous As EdgeTable = Nothing

        ' an invalid position


        ' the first node is removed
        If position = 1 Then
            Me.head = currentNode.nxt
            Me.length -= 1
        Else
            ' any other node is removed
            For i As Integer = 1 To position - 1
                s.Push(currentNode)
                currentNode = currentNode.nxt
            Next
            If currentNode.nxt IsNot Nothing Then
                s.Push(currentNode.nxt)
            End If
            refillAET()
        End If
    End Sub

    Public Sub update()
        'by created by kevin
        If length > 0 Then
            Dim currentNode As EdgeTable = Me.head
            While currentNode IsNot Nothing
                'Update the data
                currentNode.carry = currentNode.carry + currentNode.dx
                If (currentNode.dx < 0) Then
                    While (-(currentNode.carry + currentNode.carry) >= currentNode.dy)
                        currentNode.carry = currentNode.carry + currentNode.dy
                        currentNode.xmin = currentNode.xmin - 1
                    End While
                Else
                    While ((currentNode.carry + currentNode.carry) >= currentNode.dy)
                        currentNode.carry = currentNode.carry - currentNode.dy
                        currentNode.xmin = currentNode.xmin + 1
                    End While
                End If
                s.Push(currentNode)
                currentNode = currentNode.nxt
            End While
            refillAET()
        End If
    End Sub

    Public Sub sorted()
        Dim currentNode As EdgeTable = Me.head
        Me.head = Nothing
        While currentNode IsNot Nothing
            Me.add(currentNode)
            currentNode = currentNode.nxt
        End While
    End Sub

    Private Sub refillAET()
        Me.head = Nothing
        Dim prevtemp As New EdgeTable
        Dim temp As EdgeTable = s.Pop()
        While s.Count() > 0
            prevtemp = s.Pop()
            prevtemp.nxt = temp
            temp = prevtemp
        End While
        Me.head = temp
        length = CountAET()
        s.Clear()
    End Sub

    Public Function CountAET()
        Dim i As Integer = 0
        Dim node As EdgeTable = Me.head
        While Not (node Is Nothing)
            i = i + 1
            node = node.nxt
        End While
        Return i
    End Function
End Class


