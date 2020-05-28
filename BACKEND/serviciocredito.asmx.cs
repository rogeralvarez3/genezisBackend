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
    /// Descripción breve de serviciocredito
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
 [System.Web.Script.Services.ScriptService]
    public class serviciocredito : System.Web.Services.WebService
    {
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        [WebMethod]
        public string Guardar(String tabla, String datos)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd.Connection = con;

            Dictionary<String, String> data = js.Deserialize<Dictionary<String, String>>(datos);
            String campos = "";
            String valores = "";
            String id = data["id"];
            foreach (var misdatos in data)
            {
                if (misdatos.Key != "id")
                {
                    campos += misdatos.Key + ",";
                    valores += "'" + misdatos.Value + "'" + ",";

                }

            }
            campos = campos.Substring(0, campos.Length - 1);
            valores = valores.Substring(0, valores.Length - 1);
            if (id == "0")
            {
                cmd.CommandText = "INSERT INTO " + tabla + "(" + campos + ")" + " VALUES " + "(" + valores + ");select last_insert_id() from " + tabla;
            }
            else
            {
                String sql = "";

                foreach (var misdatos in data)
                {
                    if (misdatos.Key != "id")
                    {
                        sql = sql + misdatos.Key + "=" + "'" + misdatos.Value + "'" + ",";
                    }

                }
                sql = sql.Substring(0, sql.Length - 1);
                cmd.CommandText = "UPDATE " + tabla + " SET " + sql + " WHERE  id='" + id +"'";
            }

            try
            {
                String result = cmd.ExecuteScalar().ToString();
                con.Close();

                return result;
            }
            catch (Exception e)
            {
                String error = "Error:" + e.Message;
                return error;
            }

        }
        [WebMethod]
        public void Guardarfiadores(String tabla, String datos, String codcredito)

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

            cmd.Connection = con;
            List<Dictionary<String, String>> data = js.Deserialize<List<Dictionary<String, String>>>(datos);
            try
            {
                cmd.CommandText = "SELECT codcredito FROM cfiadores ";
                cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(t);
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    if (t.Rows[i][0].ToString() == codcredito)
                    {
                        cmd.CommandText = "DELETE FROM " + tabla + " WHERE codcredito='" + codcredito + "'";
                        cmd.ExecuteNonQuery();
                        break;
                    }

                }

                foreach (var misdatos in data)
                {
                    String campos = "";
                    String valores = "";
                    if (misdatos.Keys.Contains("codcredito") == false)
                    {
                        misdatos.Add("codcredito", codcredito);
                    }

                    var key = misdatos.Keys;
                    var value = misdatos.Values;
                    foreach (var res in key)
                    {

                        if (res.ToString() != "nombre")
                        {
                            campos += res.ToString() + ",";
                            valores += "'" + misdatos[res.ToString()] + "'" + ",";
                        }
                    }
                    campos = campos.Substring(0, campos.Length - 1);
                    valores = valores.Substring(0, valores.Length - 1);
                    cmd.CommandText = "INSERT INTO " + tabla + "(" + campos + ")" + " VALUES " + "(" + valores + ")";
                    cmd.ExecuteNonQuery();




                }



                con.Close();

            }
            catch (Exception e)
            {
                String error = "Error:" + e.Message;

            }

        }
        [WebMethod]
        public void Guardargarantia(String tabla, String datos, String codcredito)

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

            cmd.Connection = con;
            List<Dictionary<String, String>> data = js.Deserialize<List<Dictionary<String, String>>>(datos);
            try
            {
                cmd.CommandText = "SELECT codcredito FROM cgarantias ";
                cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(t);
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    if (t.Rows[i][0].ToString() == codcredito)
                    {
                        cmd.CommandText = "DELETE FROM " + tabla + " WHERE codcredito='" + codcredito + "'";
                        cmd.ExecuteNonQuery();
                        break;
                    }

                }

                foreach (var misdatos in data)
                {
                    String campos = "";
                    String valores = "";
                    if (misdatos.Keys.Contains("codcredito") == false)
                    {
                        misdatos.Add("codcredito", codcredito);
                    }

                    var key = misdatos.Keys;
                    var value = misdatos.Values;
                    foreach (var res in key)
                    {

                        if (res.ToString() != "nombre" )
                        {
                            campos += res.ToString() + ",";
                            valores += "'" + misdatos[res.ToString()] + "'" + ",";
                        }
                    }
                    campos = campos.Substring(0, campos.Length - 1);
                    valores = valores.Substring(0, valores.Length - 1);
                    cmd.CommandText = "INSERT INTO " + tabla + "(" + campos + ")" + " VALUES " + "(" + valores + ")";
                    cmd.ExecuteNonQuery();




                }



                con.Close();

            }
            catch (Exception e)
            {
                String error = "Error:" + e.Message;

            }

        }
        [WebMethod]
        public void Guardarestado(String tabla, String datos, String codcredito)

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

            cmd.Connection = con;
            cmd.CommandText = "SELECT codcredito FROM " + tabla;
            cmd.ExecuteNonQuery();
            adp.SelectCommand = cmd;
            adp.Fill(t);
            for (int i = 0; i < t.Rows.Count; i++)
            {
                if (t.Rows[i][0].ToString() == codcredito)
                {
                    cmd.CommandText = "DELETE FROM " + tabla + " WHERE codcredito='" + codcredito + "'";
                    cmd.ExecuteNonQuery();
                    break;
                }

            }
          

            Dictionary<String, String> data = js.Deserialize<Dictionary<String, String>>(datos);
                String campos = "";
                String valores = "";
                String id = data["id"];

                foreach (var misdatos in data)
                {
                
                if (misdatos.Key != "id")
                    {

                        campos += misdatos.Key + ",";
                        valores += "'" + misdatos.Value + "'" + ",";

                    }
               

            }
                campos = campos.Substring(0, campos.Length - 1);
                valores = valores.Substring(0, valores.Length - 1);
                
                    cmd.CommandText = "INSERT INTO " + tabla + "(" + campos + ")" + " VALUES " + "(" + valores + ")";
                
               

                try
                {
                    String result = cmd.ExecuteScalar().ToString();
                    con.Close();

                    
                }
                catch (Exception e)
                {
                    String error = "Error:" + e.Message;
                   
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


                    if (tabla == "atipocuenta")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                        fila.Add("tasa", row[2].ToString());
                        fila.Add("aplazo", row[4].ToString());
                        fila.Add("esfutura", row[9].ToString());
                        fila.Add("plazo", row[13].ToString());
                    }
                    else if (tabla == "fcliente")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[2].ToString());
                        
                        
                    }
                    else if (tabla == "ctipogarantia")
                    {
                        fila.Add("value", row[0].ToString());
                        fila.Add("text", row[1].ToString());
                       


                    }
                    else
                    {
                        foreach (DataColumn col in t.Columns)
                        {
                            String valor = row[col.ColumnName].ToString();
                            if (col.DataType == typeof(DateTime) && valor != "")
                            {
                                DateTime fecha = DateTime.Parse(valor);
                                valor = fecha.ToString("yyyy-MM-dd");
                            }
                            fila.Add(col.ColumnName, valor);
                        }
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
        public String buscardatos2(String tabla, String condicion)
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
            if (tabla == "vdesembolso")
            {
           sql = "SELECT * FROM " + tabla + " WHERE codcliente='" + condicion + "'";
            }else if (tabla == "cdesembolso")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";

            }else if (tabla == "vfiadores")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcredito='" + condicion + "'";
            }
            else if (tabla == "vgarantias")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcredito='" + condicion + "'";
            }
            else if (tabla == "cproducto")
            {
                sql = "SELECT * FROM " + tabla + " WHERE value='" + condicion + "'";
            }
            else if (tabla == "fcliente")
            {
                sql = "SELECT categoria,edad FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else if (tabla == "cestadoresultado")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcredito='" + condicion + "'";
            }
            else if (tabla == "cbalance")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcredito='" + condicion + "'";
            }
            else if (tabla == "vcomites")
            {
                sql = "SELECT * FROM " + tabla + " WHERE miembro1='" + condicion + "'";
            }
            else if (tabla == "detallefiador")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcredito='" + condicion + "'";
            }
            else if (tabla == "detallegarantia")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcredito='" + condicion + "'";
            }
            else if (tabla == "ctipogarantia")
            {
                sql = "SELECT sincomite FROM " + tabla + " WHERE value='" + condicion + "'";
            }
            else
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            

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

                    String fechasolicitud = "";
                    String fechaspago = "";
                    if (tabla == "cdesembolso")
                    {
                        if (DBNull.Value.Equals(row[28]))
                        {
                            fechasolicitud = "1900-01-01";
                        }
                        else
                        {
                            DateTime fechas = DateTime.Parse(row[28].ToString());
                            fechasolicitud = fechas.ToString("yyyy-MM-dd");
                        }
                        if (DBNull.Value.Equals(row[33]))
                        {
                            fechaspago = "1900-01-01";
                        }
                        else
                        {
                            DateTime fechas2 = DateTime.Parse(row[33].ToString());
                            fechaspago = fechas2.ToString("yyyy-MM-dd");
                        }

                        fila.Add("id", row[0].ToString());
                        fila.Add("codcliente", row[1].ToString());
                        fila.Add("fechasolicitud", fechasolicitud);
                        fila.Add("montosolicitado", row[29].ToString());
                        fila.Add("sector", row[3].ToString());
                        fila.Add("actividad", row[4].ToString());
                        fila.Add("producto", row[5].ToString());
                        fila.Add("plazo", row[8].ToString());
                        fila.Add("formapago", row[9].ToString());
                        fila.Add("utilizacion", row[30].ToString());
                        fila.Add("esconparcial", row[32].ToString());
                        fila.Add("diapago", fechaspago);
                    }
                    else if (tabla == "fcliente")
                    {
                        fila.Add("categoria", row[0].ToString());
                        fila.Add("edad", row[1].ToString());

                    }
                    else if (tabla == "vfiadores")
                    {
                        fila.Add("codcliente", row[0].ToString());
                        fila.Add("nombre", row[1].ToString());
                    }
                    else
                    {
                        foreach (DataColumn col in t.Columns)
                        {
                            String valor = row[col.ColumnName].ToString();
                            if (col.DataType == typeof(DateTime) && valor != "")
                            {
                                DateTime fecha = DateTime.Parse(valor);
                                valor = fecha.ToString("yyyy-MM-dd");
                            }
                            fila.Add(col.ColumnName, valor);
                        }
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
        public String buscarcreditos(String condicion)
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
           
                sql = "SELECT saldo FROM cdesembolso " + " WHERE codcliente='" + condicion + "' and saldo>'0'";
            
           


            cmd.Connection = con;
            cmd.CommandText = sql;

            try
            {

                cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(t);
                int ncredito = 0;

                if (t.Rows.Count < -1)
                {
                    ncredito = 0;
                }
                List<Dictionary<Object, String>> filas = new List<Dictionary<Object, String>>();
                Dictionary<Object, String> fila;

                fila = new Dictionary<object, string>();
                ncredito = t.Rows.Count;
                double sumasaldo =0;
                for(int i=0; i < t.Rows.Count; ++i)
                {
                    sumasaldo +=Convert.ToDouble(t.Rows[i][0].ToString());
                }
                foreach (DataRow row in t.Rows)
                {
                                           
                    //    foreach (DataColumn col in t.Columns)
                    //    {
                    //        String valor = row[col.ColumnName].ToString();
                    //        if (col.DataType == typeof(DateTime) && valor != "")
                    //        {
                    //            DateTime fecha = DateTime.Parse(valor);
                    //            valor = fecha.ToString("yyyy-MM-dd");
                    //        }
                    //        fila.Add(col.ColumnName, valor);

                    //}

                }
                fila.Add("ncredito", ncredito.ToString());
                fila.Add("saldo", sumasaldo.ToString());

                filas.Add(fila);

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
        public String buscarcreditoporcomite(String montoinicial,String montofinal)
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

            sql = "SELECT * FROM vdesembolso WHERE montosolicitado>='" + montoinicial + "' AND   montosolicitado<='" + montofinal +"' AND aprovado='2' AND sincomite='2'";




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

                        foreach (DataColumn col in t.Columns)
                        {
                            String valor = row[col.ColumnName].ToString();
                            if (col.DataType == typeof(DateTime) && valor != "")
                            {
                                DateTime fecha = DateTime.Parse(valor);
                                valor = fecha.ToString("dd/MM/yyyy");
                            }
                            fila.Add(col.ColumnName, valor);
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
