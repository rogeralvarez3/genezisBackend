using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes.ahorro
{
    public partial class Rlisinscrito : System.Web.UI.Page
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
                ReportParameter[] parameters = new ReportParameter[1];

                if (txtins.SelectedItem.Text == "INSCRITOS")
                {
                   
                    if (txtsucursal.SelectedItem.Text=="TODAS LAS SUCURSALES")
                {
                    miclase.reportes("vcliente", "reportes/cliente/lisinscritos.rdlc", "sucursal,nombre,iden,telefono,dir", "vcliente", visor,"inscrito='SI'");
                       
                    }
                else
                {
                miclase.reportes("vcliente", "reportes/cliente/lisinscritos.rdlc", "sucursal,nombre,iden,telefono,dir", "vcliente", visor, "sucursal='" + txtsucursal.SelectedItem.Text + "' AND inscrito='SI'");
                     
                    }
                    parameters[0] = new ReportParameter("ins", "INSCRITO");
                    this.visor.LocalReport.SetParameters(parameters);
                    this.visor.LocalReport.Refresh();
                }

                else if(txtins.SelectedItem.Text=="NO INSCRITOS")
                {
                    parameters[0] = new ReportParameter("ins", "NO INSCRITO");
                    this.visor.LocalReport.SetParameters(parameters);
                    this.visor.LocalReport.Refresh();
                    if (txtsucursal.SelectedItem.Text == "TODAS LAS SUCURSALES")
                    {
                        miclase.reportes("vcliente", "reportes/cliente/lisinscritos.rdlc", "sucursal,nombre,iden,telefono,dir", "vcliente", visor, "inscrito='NO'");
                    }
                    else
                    {
                        miclase.reportes("vcliente", "reportes/cliente/lisinscritos.rdlc", "sucursal,nombre,iden,telefono,dir", "vcliente", visor, "sucursal='" + txtsucursal.SelectedItem.Text + "' AND inscrito='NO'");
                    }
                }
            }
            catch(Exception ex)
            {
                String error = ex.Message;
            }
        }
    }
}