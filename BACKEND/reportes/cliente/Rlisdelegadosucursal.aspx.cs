using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class Rlisdelegadosucursal : System.Web.UI.Page
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
                miclase.llenarcombo(txtsucursal, "fsucursal ", "text");
                txtsucursal.Items.Insert(0,"--Selecciones una sucursal--");
                txtsucursal.Items.Insert(1,"TODAS LAS SUCURSALES");

            }
        }

        protected void cmdaceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtsucursal.SelectedItem.Text=="TODAS LAS SUCURSALES")
                {
                    miclase.reportes("vcliente", "reportes/cliente/lisdelegadossucursal.rdlc", "sucursal,nombre,iden,telefono,dir", "vcliente", visor,"esdelegado='SI'");
                }
                else
                {
                miclase.reportes("vcliente", "reportes/cliente/lisdelegadossucursal.rdlc", "sucursal,nombre,iden,telefono,dir", "vcliente", visor, "sucursal='" + txtsucursal.SelectedItem.Text + "' AND esdelegado='SI'");
                }
                
            }
            catch(Exception ex)
            {
                String error = ex.Message;
            }
        }
    }
}