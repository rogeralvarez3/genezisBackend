<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="saldosdeahorro.aspx.cs" Inherits="BACKEND.reportes.ahorro.saldosdeahorro" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms"   TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <link href="../../complementos/bootstrap.min.css" rel="stylesheet" />
    <script src="../../complementos/jquery-3.3.1.min.js"></script>
    <script src="../../complementos/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="position: absolute; width: 60%; height: 100%; top: 0%; left: 19%">
            <rsweb:ReportViewer ID="visor" runat="server" Style="position: absolute; height: 100%; width: 100%; top: 0%; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)">
            </rsweb:ReportViewer>

        </div>
        
            <div class="modal-dialog modal-dialog-centered" role="document" style="margin-right:1%;width:19%;margin-top:0%">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Filtrar</h5>
                        
                    </div>
                    <div class="modal-body">
                       
                    </div>
                    <div class="modal-footer">
                       
                        <asp:Button ID="cmdlisto" runat="server" Text="Listo" CssClass="btn btn-primary" OnClick="cmdlisto_Click" />
                        
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
