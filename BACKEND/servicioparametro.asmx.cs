using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BACKEND
{
    /// <summary>
    /// Descripción breve de servicioparametro
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
 [System.Web.Script.Services.ScriptService]
    public class servicioparametro : System.Web.Services.WebService
    {
        [WebMethod]
        public void Guardar(String tabla, String campos, String valores)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd.Connection = con;
            cmd.CommandText = "REPLACE INTO " + tabla + "(" + campos + ")" + " VALUES " + "(" + valores + ")";

            try
            {
                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception e)
            {
                String error = e.Message;
            }

        }


        [WebMethod]

        public void Eliminar(String tabla, String condicion)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();

            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM " + tabla + " WHERE value='" + condicion + "'";
            try
            {

                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

                String error = e.Message;
            }


        }
        [WebMethod]
        public String buscardatos(String tabla)
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

            String resultado = "";
            String sql = "";

            sql = "SELECT * FROM " + tabla;

            cmd.Connection = con;
            cmd.CommandText = sql;

            try
            {
                cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(t);
                List<Dictionary<Object, String>> filas = new List<Dictionary<Object, String>>();
                Dictionary<Object, String> fila;

                foreach (DataRow row in t.Rows)
                {
                    fila = new Dictionary<object, string>();

                    if (tabla == "fpais")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "fciudad")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "fsucursal")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("prefijo", row[2].ToString());

                    }
                    else if (tabla == "fzona")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("sucursal", row[2].ToString());
                        fila.Add("posicion", row[3].ToString());
                        fila.Add("circun", row[4].ToString());

                    }
                    else if (tabla == "fescolaridad")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "fprofesion")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "fpromotor")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("telefono", row[2].ToString());
                        fila.Add("sucursal", row[3].ToString());

                    }
                    else if (tabla == "fcomites")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "fcargo")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "ftipoestudio")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "fnomestudio")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("tipoestudio", row[2].ToString());

                    }
                    else if (tabla == "ftemas")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("nomestudio", row[2].ToString());

                    }
                    else if (tabla == "fdocexpediente")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("tipo", row[2].ToString());

                    }
                    else if (tabla == "atipocuenta")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                       

                    }
                    else if (tabla == "csector")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        

                    }
                    else if (tabla == "cactividad")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("sector", row[2].ToString());


                    }
                    else if (tabla == "cproducto")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("actividad", row[2].ToString());


                    }
                    else if (tabla == "ctipoescritura")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }
                    else if (tabla == "ctipopropiedad")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }


                    filas.Add(fila);


                   
                }
 System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

                    resultado = js.Serialize(filas);


                con.Close();
            }
            catch (Exception e)
            {
                String error = e.Message;

            }
            return resultado;

        }

        [WebMethod]

        public String combos(String tabla)
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

            String resultado = "";
            String sql = "";

            sql = "SELECT * FROM " + tabla;

            cmd.Connection = con;
            cmd.CommandText = sql;

            try
            {

                cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(t);
                List<Dictionary<Object, String>> filas = new List<Dictionary<Object, String>>();
                Dictionary<Object, String> fila;
                foreach (DataRow row in t.Rows)
                {
                    fila = new Dictionary<object, string>();


                    if (tabla == "fcircun")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fpais")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fprofesion")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fpromotor")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("telefono", row[2].ToString());
                        fila.Add("sucursal", row[3].ToString());
                    }
                    else if (tabla == "fsucursal")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fzona")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("sucursal", row[2].ToString());
                        fila.Add("posicion", row[3].ToString());
                        fila.Add("circun", row[4].ToString());

                    }
                    else if (tabla == "fciudad")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fcomites")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fcargo")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fcliente")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[2].ToString());
                    }
                    else if (tabla == "ftipoestudio")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                    }
                    else if (tabla == "fnomestudio")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("tipoestudio", row[2].ToString());
                    }
                    else if (tabla == "ftemas")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("nomestudio", row[2].ToString());
                    }
                    else if (tabla == "csector")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        
                    }
                    else if (tabla == "cactividad")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());

                    }



                    filas.Add(fila);


                   

                }
 System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

                    resultado = js.Serialize(filas);

                con.Close();
            }
            catch (Exception e)
            {
                String error = e.Message;

            }
            return resultado;

        }

    }
}
