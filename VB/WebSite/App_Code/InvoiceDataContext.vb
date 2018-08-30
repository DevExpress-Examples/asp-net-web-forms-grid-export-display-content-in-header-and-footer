Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Web
Imports System.Linq

<DataObject(True)> _
Public Class InvoiceDataContext
    Private Property Invoices() As List(Of Invoice)
        Get
            If HttpContext.Current.Session("Invoices") Is Nothing Then
                Dim list As New List(Of Invoice)()

                list.Add(New Invoice(0, "Invoice1", 10.0D))
                list.Add(New Invoice(1, "Invoice2", 15.0D))
                list.Add(New Invoice(2, "Invoice3", 20.0D))
                HttpContext.Current.Session("Invoices") = list
                Return list
            Else
                Return CType(HttpContext.Current.Session("Invoices"), List(Of Invoice))
            End If
        End Get
        Set(ByVal value As List(Of Invoice))
            HttpContext.Current.Session("Invoices") = value
        End Set
    End Property

    <DataObjectMethod(DataObjectMethodType.Select, True)> _
    Public Function GetInvoices() As List(Of Invoice)
        Return Invoices
    End Function
End Class