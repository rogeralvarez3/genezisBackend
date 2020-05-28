using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class Rlisclientezona : System.Web.UI.Page
    {
        reporte miclase = new reporte();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            }catch(Exception ex)
            {
                String error = ex.Message;
            }
            if (IsPostBack == false)
            {
                miclase.llenarcombo(txtzona, "fzona ", "text");
                txtzona.Items.Insert(0,"--Selecciones una Zona--");
                txtzona.Items.Insert(1,"TODAS LAS ZONAS");

            }
        }

        protected void cmdaceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtzona.SelectedItem.Text=="TODAS LAS ZONAS")
                {
                    miclase.reportes("vcliente", "reportes/cliente/lisclientezona.rdlc", "zona,nombre,iden,telefono,dir", "vcliente", visor);
                }
                else
                {
                miclase.reportes("vcliente", "reportes/cliente/lisclientezona.rdlc", "zona,nombre,iden,telefono,dir", "vcliente", visor, "zona='" + txtzona.SelectedItem.Text + "'");
                }
                
            }
            catch(Exception ex)
            {
                String error = ex.Message;
            }
        }
    }
}