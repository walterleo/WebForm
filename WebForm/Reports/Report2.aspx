<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report2.aspx.cs" Inherits="WebForm.Reports.Report2" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server"> 
                <LocalReport ReportPath="Reports\Report1.rdlc">
                  
                </LocalReport>
            </rsweb:ReportViewer>
            
        </div>
    </form>
</body>
</html>
