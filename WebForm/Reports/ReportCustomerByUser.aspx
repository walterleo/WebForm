<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportCustomerByUser.aspx.cs" Inherits="WebForm.Reports.ReportCustomerByUser" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" CssClass="text-danger"></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div>
            <asp:Label ID="lblUserId" runat="server" Text="User ID:"></asp:Label>
            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
            <asp:Button ID="btnRun" runat="server" Text="Run" OnClick="btnRun_Click" />
        </div>
        <asp:Label ID="lblMessage1" runat="server" ForeColor="Red" Visible="False"></asp:Label>

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="False">
            <LocalReport ReportPath="Reports\ReportCustomerByUser.rdlc"></LocalReport>
        </rsweb:ReportViewer>
    </form>
</body>
</html>
