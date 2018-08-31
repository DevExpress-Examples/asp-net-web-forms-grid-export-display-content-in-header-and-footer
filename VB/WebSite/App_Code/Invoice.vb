Imports System
Imports System.Collections.Generic

<Serializable> _
Public Class Invoice
    Implements IEquatable(Of Invoice)

    Public Property Id() As Integer
    Public Property Description() As String
    Public Property Price() As Decimal

    Public Sub New(ByVal id As Integer, ByVal description As String, ByVal price As Decimal)
        Me.Id = id
        Me.Description = description
        Me.Price = price
    End Sub

    Public Sub New()
    End Sub

    Public Function Equals(ByVal other As Invoice) As Boolean Implements IEquatable(Of Invoice).Equals
        Return Me.Id.Equals(other.Id)
    End Function
End Class