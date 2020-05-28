using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class saldosdeahorro : System.Web.UI.Page
    {
        reporte miclase = new reporte();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void cmdlisto_Click(object sender, EventArgs e)
        {
            try
            {
                miclase.reportes("vapertura", "reportes/ahorro/saldosdeahorro.rdlc", "nombre,text,codigo,fechaa,saldo,mvalor,interes,sexo", " detalleahorro ", visor);




            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
        }
    }

}