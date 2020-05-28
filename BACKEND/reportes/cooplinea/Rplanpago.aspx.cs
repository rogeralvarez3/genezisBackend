using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.cooplinea
{
    public partial class Rplanpago : System.Web.UI.Page
    {
        reporte miclase = new reporte();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (IsPostBack == false)
            {
                   
                   
                    String codigo = servicioweb.cod.ToString();
                    String nombre = servicioweb.nom.ToString();
                   
                    miclase.reportes("cplanpago", "reportes/cooplinea/planpago.rdlc", "fecha,numcuota,principal,mantvalor,interes,total,saldofinal,dias", "cplanpago", visor, "codigo='" + codigo + "'");
                    List<ReportParameter> parametros = new List<ReportParameter>();
                    parametros.Add(new ReportParameter("codigo", codigo));
                    parametros.Add(new ReportParameter("nombre", nombre));
                    visor.LocalReport.SetParameters(parametros);
                   
                 
            }
            }catch(Exception ex)
            {
                String error = ex.Message;
            }
            
        }

        
    }
}