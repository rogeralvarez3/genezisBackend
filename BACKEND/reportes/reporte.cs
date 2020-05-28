using Microsoft.Reporting.WebForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BACKEND.reportes
{
    public class reporte
    {
        public void reportes(String data, String nombrereporte, String campos, String tabla, ReportViewer visor, [Optional] string condicion)
        {
            DataTable t = new DataTable();
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            ReportDataSource datos_reporte = new ReportDataSource();
            LocalReport reporte = new LocalReport();
            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if ((condicion != null))
            {
                condicion = (" where " + condicion);
            }
            cmd.Connection = con;
            cmd.CommandText = "SELECT " + campos + " FROM " + tabla + condicion;
            try
            {
                cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                t.Clear();
                da.Fill(t);
                datos_reporte.Name = data;
                datos_reporte.Value = t;
                reporte = visor.LocalReport;
                reporte.DataSources.Clear();
                reporte.ReportPath = nombrereporte;
                //reporte.ReportEmbeddedResource =nombrereporte;
                reporte.DataSources.Add(datos_reporte);


                // visor .LocalReport.EnableExternalImages = true;
                visor.LocalReport.Refresh();




            }
            catch (Exception e)
            {
                String error = e.Message;
            }

        }
        public void reportes2(String data, String nombrereporte, DataTable tabla, ReportViewer visor)
        {

            try { 
          
            ReportDataSource datos_reporte = new ReportDataSource();
            LocalReport reporte = new LocalReport();
                datos_reporte.Name = data;
                datos_reporte.Value = tabla;
                reporte = visor.LocalReport;
                reporte.DataSources.Clear();
                reporte.ReportPath = nombrereporte;
                //reporte.ReportEmbeddedResource =nombrereporte;
                reporte.DataSources.Add(datos_reporte);


                // visor .LocalReport.EnableExternalImages = true;
                visor.LocalReport.Refresh();




            }
            catch (Exception e)
            {
                String error = e.Message;
            }

        }
        public Boolean llenarcombo(DropDownList combo, String tabla, String campo, [Optional] String condicion)
        {

            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            DataTable t = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter();

            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (condicion != null)
            {
                condicion = " where " + condicion;
            }
            cmd.Connection = con;
            cmd.CommandText = ("SELECT value," + campo + " FROM " + tabla + condicion);
            adp.SelectCommand = cmd;
            try
            {
                cmd.ExecuteScalar();
                adp.Fill(t);
                combo.DataSource = t;
                combo.DataTextField = campo;
                combo.DataValueField = "value";
                combo.DataBind();
                con.Close();
            }
            catch (Exception e)

            {
                String error = e.Message;

            }

            return true;
        }


        public DataTable datos(String tabla, String campo, [Optional] String condicion)
        {

            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            DataTable t = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter();

            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (condicion != null)
            {
                condicion = " where " + condicion;
            }
            cmd.Connection = con;
            cmd.CommandText = ("SELECT " + campo + " FROM " + tabla + condicion);
            adp.SelectCommand = cmd;
            try
            {
                cmd.ExecuteScalar();
                adp.Fill(t);
                
                con.Close();
            }
            catch (Exception e)

            {
                String error = e.Message;

            }

            return t;
        }

    }
    }
