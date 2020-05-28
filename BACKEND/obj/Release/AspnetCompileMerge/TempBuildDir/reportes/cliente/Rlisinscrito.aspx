<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rlisinscrito.aspx.cs" Inherits="BACKEND.reportes.ahorro.Rlisinscrito" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>COOPEFACSA R.L</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="position:absolute;width:80%;height:100%;top:0%;left:10%">
            <div style="position:absolute;height:7%;width:100%;top:0%;background-color:#0094ff;box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);color:white;">
               <img src="../../img/LOGO-2.png" style="position:absolute;height:100%;width:5%;"/>
                <div style="position:absolute;height:100%;width:80%;left:20%">
                    <asp:DropDownList ID="txtins" runat="server" ToolTip="Elije una opción" AutoPostBack="True" style="position:absolute;height:100%;background-color:#0094ff;border:none;color:white;">
                        <asp:ListItem Selected="True">--Elije una opción--</asp:ListItem>
                        <asp:ListItem>INSCRITOS</asp:ListItem>
                        <asp:ListItem>NO INSCRITOS</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="txtsucursal" runat="server" ToolTip="SUCURSAL" AutoPostBack="True" style="position:absolute;height:100%;background-color:#0094ff;border:none;color:white;left:15%;"></asp:DropDownList>
                    <asp:Button ID="cmdaceptar" runat="server" Text="Aceptar" ForeColor="white" BackColor="#006699" style="position:absolute;height:100%;right:0%;cursor:pointer;" BorderStyle="None" OnClick="cmdaceptar_Click"  />
                </div>
            </div>
            <rsweb:ReportViewer ID="visor" runat="server" style="position:absolute;height:100%;width:100%;top:8%;box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
