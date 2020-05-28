using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class rpia : System.Web.UI.Page
    {
        reporte miclase = new reporte();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
 if (IsPostBack == false)
            {
             DataTable t = new DataTable();
            String codigo = servicioahorro.codcliente.ToString();
            t = miclase.datos("vcliente ", " fecha,nombre,iden,fechana,dir,zona,telefono,correo,pais,ciudad,conyugue,hijos,escolaridad,ocupacion,estadocivil,profesion,lugartrabajo,dirtrabajo,teletrabajo,referente,dirreferente,telereferente ", " id='" + codigo + "'");

                    //  saldoa = t.Rows[0][0].ToString();
                    miclase.reportes("vapertura", "reportes/ahorro/pia.rdlc", "text,codigo,saldoi,moneda,fechaa", " vapertura ", visor,"codcliente='" + codigo +"'");
                    ReportParameter[] parameters = new ReportParameter[22];
                    parameters[0] = new ReportParameter("fechaingreso", t.Rows[0][0].ToString());
                    parameters[1] = new ReportParameter("nombre", t.Rows[0][1].ToString());
                    parameters[2] = new ReportParameter("cedula", t.Rows[0][2].ToString());
                    parameters[3] = new ReportParameter("fechana", t.Rows[0][3].ToString());
                    parameters[4] = new ReportParameter("dir", t.Rows[0][4].ToString());
                    parameters[5] = new ReportParameter("zona", t.Rows[0][5].ToString());
                    parameters[6] = new ReportParameter("telefono", t.Rows[0][6].ToString());
                    parameters[7] = new ReportParameter("correo", t.Rows[0][7].ToString());
                    parameters[8] = new ReportParameter("nacionalidad", t.Rows[0][8].ToString());
                    parameters[9] = new ReportParameter("ciudad", t.Rows[0][9].ToString());
                    parameters[10] = new ReportParameter("nomconyugue", t.Rows[0][10].ToString());
                    parameters[11] = new ReportParameter("hijos", t.Rows[0][11].ToString());
                    parameters[12] = new ReportParameter("escolaridad", t.Rows[0][12].ToString());
                    parameters[13] = new ReportParameter("ocupacion", t.Rows[0][13].ToString());
                    parameters[14] = new ReportParameter("estadocivil", t.Rows[0][14].ToString());
                    parameters[15] = new ReportParameter("profesion", t.Rows[0][15].ToString());
                    parameters[16] = new ReportParameter("lugartrabajo", t.Rows[0][16].ToString());
                    parameters[17] = new ReportParameter("dirtrabajo", t.Rows[0][17].ToString());
                    parameters[18] = new ReportParameter("teletrabajo", t.Rows[0][18].ToString());
                    parameters[19] = new ReportParameter("referente", t.Rows[0][19].ToString());
                    parameters[20] = new ReportParameter("dirreferente", t.Rows[0][20].ToString());
                    parameters[21] = new ReportParameter("telereferente", t.Rows[0][21].ToString());
                    //parameters[19] = new ReportParameter("ingresom", t.Rows[0][2].ToString());
                    this.visor.LocalReport.SetParameters(parameters);
                    this.visor.LocalReport.Refresh();
                }
       
            }catch(Exception ex)
            {
                String error = ex.Message;
            }
           
        }
    }
}