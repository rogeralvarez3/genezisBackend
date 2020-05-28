<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rpia.aspx.cs" Inherits="BACKEND.reportes.ahorro.rpia" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>







<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>PIA</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="position:absolute;width:60%;height:100%;top:0%;left:20%">
           
           
            
            <rsweb:ReportViewer ID="visor" runat="server" style="position:absolute;height:100%;width:100%;top:8%;box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)">
             
            </rsweb:ReportViewer>
           
          
        </div>
    </form>
</body>
</html>
