<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ProiectING.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:Button ID="refreshBtn" runat="server" Text="Refresh" OnClick="refreshBtn_Click" />
        <br />
        <asp:Table ID="Table1" runat="server" style="float: left">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Currency</asp:TableHeaderCell>
                <asp:TableHeaderCell>Value</asp:TableHeaderCell>
                <asp:TableHeaderCell>Direction</asp:TableHeaderCell>
                <asp:TableHeaderCell>Difference</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </form>
</body>
</html>
