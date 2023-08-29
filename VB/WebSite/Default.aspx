<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v22.2, Version=22.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                KeyFieldName="Id">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Id" ReadOnly="True" VisibleIndex="0">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Price" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                DataObjectTypeName="Invoice" SelectMethod="GetInvoices"
                TypeName="InvoiceDataContext"></asp:ObjectDataSource>
            <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1">
            </dx:ASPxGridViewExporter>
            <asp:Button ID="b1" runat="server" Text="Export to PDF" OnClick="pdf_Click" />
            <asp:Button ID="b2" runat="server" Text="Export to RTF" OnClick="rtf_Click" />
            <asp:Button ID="b3" runat="server" Text="Export to XLS" OnClick="xls_Click" />
        </div>
    </form>
</body>
</html>