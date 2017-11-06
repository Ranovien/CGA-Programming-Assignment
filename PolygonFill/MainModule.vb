Module MainModule
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
End Module
