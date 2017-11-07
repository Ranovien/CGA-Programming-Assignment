Public Class EdgeTable
    Public ymin As Integer
    Public ymax As Integer
    Public xmin As Integer
    Public dx As Integer
    Public dy As Integer
    Public carry As Integer
    Public normalize As Integer
    Public Nxt As EdgeTable

    Public Sub New()
        carry = 0
        Nxt = Nothing
    End Sub
End Class
