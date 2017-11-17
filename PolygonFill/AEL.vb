Public Class AEL
    'created by handy
    Public head As EdgeTable
        Public length As Integer

        Public Sub New()
            head = Nothing
            length = 0
        End Sub

        Public Sub add(ymin As Integer, ymax As Integer, xmin As Integer, dx As Integer, dy As Integer, normalize As Integer, Optional carry As Integer = 0)
            Dim node As EdgeTable = New EdgeTable(ymin, ymax, xmin, dx, dy, normalize, carry),
            currentNode As EdgeTable = Me.head

            ' an empty list
            If currentNode Is Nothing Then
                Me.head = node
                Me.length += 1

                Return
            End If

            If node.xmin < Me.head.xmin OrElse (node.xmin = Me.head.xmin AndAlso node.dx / node.dy < Me.head.dx / Me.head.dy) Then ' if the new node is the smallest one
                node.nxt = currentNode
                Me.head = node
                Me.length += 1

                Return
            End If

            ' a non-empty list
            Dim temp As New EdgeTable
            While currentNode IsNot Nothing
                If node.xmin > currentNode.xmin Then ' sort it ascendingly based on xOfYMin
                    temp = currentNode
                ElseIf node.xmin = currentNode.xmin Then ' if xOfYMin is same
                    If node.dx / node.dy > currentNode.dx / currentNode.dy Then ' sort it ascendingly based on dx/dy
                        temp = currentNode
                    End If
                End If
                currentNode = currentNode.nxt
            End While

            node.nxt = temp.nxt
            temp.nxt = node
            Me.length += 1
        End Sub

        Public Sub remove(position As Integer)
        Dim currentNode As EdgeTable = Me.head
        Dim length As Integer = Me.length,
            counter As Integer = 0,
            previous As EdgeTable = Nothing

            ' an invalid position
            If position < 0 OrElse position > length - 1 Then
                MsgBox("Out of index in removing node")
            End If

            ' the first node is removed
            If position = 0 Then
                Me.head = currentNode.nxt
                currentNode = Nothing
                Me.length += 1

                Return
            End If

            ' any other node is removed
            While counter < position
                previous = currentNode
                currentNode = currentNode.nxt
                counter += 1
            End While

            previous.nxt = currentNode.nxt
            currentNode = Nothing
            Me.length -= 1
        End Sub
    End Class


