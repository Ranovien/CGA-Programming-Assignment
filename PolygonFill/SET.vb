Public Class EdgeTable
    Public ymin As Integer
    Public ymax As Integer
    Public xmin As Integer
    Public dx As Integer
    Public dy As Integer
    Public carry As Integer
    Public normalize As Integer
    Public nxt As EdgeTable

    Public Sub New()
        carry = Nothing
        normalize = Nothing
        nxt = Nothing
    End Sub
End Class
