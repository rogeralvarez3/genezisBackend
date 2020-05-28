using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class proyeccionahorro : System.Web.UI.Page
    {
        reporte miclase =new reporte();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (IsPostBack == false)
                {
                  DataTable t = new DataTable();
                t = servicioahorro.t2;
                    String monto = servicioahorro.monto2;

                miclase.reportes2("proyeccionahorro", "reportes/ahorro/proyeccionahorro.rdlc", t, visor);

                    ReportParameter[] parameters = new ReportParameter[1];
                    parameters[0] = new ReportParameter("monto", monto.ToString());
                    this.visor.LocalReport.SetParameters(parameters);
                    this.visor.LocalReport.Refresh();
                }

               
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }

        }
    }
}