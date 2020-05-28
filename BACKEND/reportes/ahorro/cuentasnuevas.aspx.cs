using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class cuentasnuevas : System.Web.UI.Page
    {
        reporte miclase = new reporte();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            }

        protected void cmdlisto_Click(object sender, EventArgs e)
        {
            try
            {
                miclase.reportes("vapertura", "reportes/ahorro/cuentasnuevas.rdlc", "nombre,telefono,text,codigo,saldoi,fechaa", " detalleahorro ", visor, "fechaa>='" + txtfechas1.Text + "'  AND fechaa<='" + txtfechas2.Text + "'");
              

                
               
            }
            catch(Exception ex)
            {
                String error = ex.Message;
            }
        }
    }
}