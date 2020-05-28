using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class Rlisclientecircun : System.Web.UI.Page
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
                miclase.llenarcombo(txtcircun, "fcircun ", "text");
                txtcircun.Items.Insert(0,"--Selecciones una circunscripción--");
                txtcircun.Items.Insert(1,"TODAS LAS CIRCUNSCRIPCIONES");

            }
        }

        protected void cmdaceptar_Click(object sender, EventArgs e)
        {
            try
            {
            
                if(txtcircun.SelectedItem.Text=="TODAS LAS CIRCUNSCRIPCIONES")
                {
                    miclase.reportes("vcliente", "reportes/cliente/lisclientecircun.rdlc", "circun,nombre,iden,telefono,dir", "vcliente", visor);
                }
                else
                {
                miclase.reportes("vcliente", "reportes/cliente/lisclientecircun.rdlc", "circun,nombre,iden,telefono,dir", "vcliente", visor, "circun='" + txtcircun.SelectedItem.Text + "'");
                }
                
            }
            catch(Exception ex)
            {
                String error = ex.Message;
            }
        }
    }
}