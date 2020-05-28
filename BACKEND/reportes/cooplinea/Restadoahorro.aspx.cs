using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.cooplinea
{
    public partial class Restadoahorro : System.Web.UI.Page
    {
        reporte miclase = new reporte();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (IsPostBack == false)
            {

                    DataTable t = new DataTable();
                    String codigo = "10000007826";//servicioweb.cod.ToString();
                    String nombre = "FRANKLIN BENARD CASTRO";//servicioweb.nom.ToString();
                    String fechai = "2018-12-25"; //servicioweb.fechainicial.ToString();
                    String fechaf = "2019-09-24";//servicioweb.fechafinal.ToString();
                    String todo = "False"; //servicioweb.todos.ToString();
                    String saldoa ;
                    List<ReportParameter> parametros = new List<ReportParameter>();
                    if (todo == "True")
                    {
                    miclase.reportes("movahorro", "reportes/cooplinea/estadoahorro.rdlc", " fecha,cuenta,transanccion,depositos,retiros,numerooperacion", "movahorro", visor, "cuenta='" + codigo + "' ORDER BY fecha, transanccion");

                    }
                    else
                    {
                        miclase.reportes("movahorro", "reportes/cooplinea/estadoahorro.rdlc", " fecha,cuenta,transanccion,depositos,retiros,numerooperacion", "movahorro", visor, "cuenta='" + codigo + "' AND fecha>='" + fechai + "' AND fecha<='" + fechaf +"' ORDER BY fecha, transanccion");

                    }
                   // t = miclase.saldoa("movahorro", " SUM(depositos)-SUM(retiros) as saldoi", " fecha<'" + fechai + "' AND cuenta='" + codigo +"'");
                    saldoa = t.Rows[0][0].ToString();
                   
                    parametros.Add(new ReportParameter("codigo", codigo));
                    parametros.Add(new ReportParameter("nombre", nombre));
                    parametros.Add(new ReportParameter("saldoa",saldoa));
                    visor.LocalReport.SetParameters(parametros);
                  
                   
                 
            }
            }catch(Exception ex)
            {
                String error = ex.Message;
            }
            
        }

        
    }
}