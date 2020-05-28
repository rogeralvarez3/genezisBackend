using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BACKEND
{
    /// <summary>
    /// Descripción breve de servicioahorro
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]

    public class servicioahorro : System.Web.Services.WebService
    {
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        public static String codcliente = "";
        public static DataTable t2;
        public static String monto2 = "";
       
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
                cmd.CommandText = "UPDATE " + tabla + " SET " + sql + " WHERE  id=" + id;
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
        public void Guardarbene(String tabla, String datos, String cuenta)

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
                cmd.CommandText = "SELECT codcuenta FROM abeneficiarios ";
            cmd.ExecuteNonQuery();
            adp.SelectCommand = cmd;
            adp.Fill(t);
            for(int i = 0; i < t.Rows.Count; i++)
            {
                if (t.Rows[i][0].ToString()==cuenta)
                {
               cmd.CommandText = "DELETE FROM " + tabla + " WHERE codcuenta='" + cuenta + "'";
               cmd.ExecuteNonQuery();
                    break;
                }
                
            }
           
            foreach (var misdatos in data)
            {
                String campos = "";
                String valores = "";
                    if (misdatos.Keys.Contains("codcuenta")==false)
                    {
                     misdatos.Add("codcuenta", cuenta);
                    }
                       
                        var key = misdatos.Keys;
                        var value = misdatos.Values;
                        foreach (var res in key)
                        {
                           
                            if (res.ToString() != "id")
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
            cmd.CommandText = "DELETE FROM " + tabla + " WHERE id='" + condicion + "'";


            try
            {

                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM abeneficiarios WHERE codcuenta='" + condicion + "'";
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {

                String error = e.Message;
            }


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
        public String buscarcampo(String tabla, String condicion)
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
            if (tabla == "vapertura")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcliente='" + condicion + "'";
            }
            else if (tabla == "aapertura")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else if (tabla == "atipocuenta")
            {
                sql = "SELECT * FROM " + tabla + " WHERE value='" + condicion + "'";
            }
            else if (tabla == "abeneficiarios")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcuenta='" + condicion + "'";
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



                    if (tabla == "vapertura")
                    {

                        fila.Add("id", row[0].ToString());
                        fila.Add("tipocuenta", row[2].ToString());
                        fila.Add("codcuenta", row[3].ToString());
                        fila.Add("saldo", row[5].ToString());

                    }
                    else if (tabla == "aapertura")
                    {
                        String fechamenor = "";
                        String fechaa = "";
                        String fechaaV = "";
                        if (DBNull.Value.Equals(row[5]))
                        {
                            fechaa = "1900-01-01";
                        }
                        else
                        {
                            DateTime fechas = DateTime.Parse(row[5].ToString());
                            fechaa = fechas.ToString("yyyy-MM-dd");
                        }

                        if (DBNull.Value.Equals(row[13]))
                        {
                            fechaaV = "1900-01-01";
                        }
                        else
                        {
                            DateTime fechaV = DateTime.Parse(row[13].ToString());
                            fechaaV = fechaV.ToString("yyyy-MM-dd");
                        }


                        if (DBNull.Value.Equals(row[26]))
                        {
                            fechamenor = "1900-01-01";
                        }
                        else
                        {
                            DateTime fechaname = DateTime.Parse(row[26].ToString());
                            fechamenor = fechaname.ToString("yyyy-MM-dd");
                        }


                        fila.Add("id", row[0].ToString());
                        fila.Add("codcliente", row[1].ToString());
                        fila.Add("tipocuenta", row[2].ToString());
                        fila.Add("fechaa", fechaa);
                        fila.Add("tasa", row[7].ToString());
                        fila.Add("depositoxmes", row[8].ToString());
                        fila.Add("mancumunada", row[9].ToString());
                        fila.Add("titular", row[10].ToString());
                        fila.Add("codmancu", row[11].ToString());
                        fila.Add("origenfondo", row[12].ToString());
                        fila.Add("fechaven", fechaaV);
                        fila.Add("plazo", row[24].ToString());
                        fila.Add("depinicial", row[30].ToString());
                        fila.Add("nombremenor", row[25].ToString());
                        fila.Add("fechanamenor", fechamenor);
                        fila.Add("edad", row[27].ToString());
                        fila.Add("parentezcomenor", row[28].ToString());
                        fila.Add("sexomenor", row[29].ToString());
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
            if (resultado == "")
            {
                return resultado = "nada";
            }
            else
            {
                return resultado;
            }


        }
        [WebMethod]
        public String buscarcampo2(String tabla, String condicion)
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
            if (tabla == "vapertura")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else if (tabla == "aapertura")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else if (tabla == "atipocuenta")
            {
                sql = "SELECT * FROM " + tabla + " WHERE value='" + condicion + "'";
            }
            else if (tabla == "abeneficiarios")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcuenta='" + condicion + "'";
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
            if (resultado == "")
            {
                return resultado = "nada";
            }
            else
            {
                return resultado;
            }


        }
        [WebMethod]
        public String contrato(String condicion)
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
            String sql = "";

            sql = "SELECT * FROM vapertura WHERE id='" + condicion + "'";

            cmd.Connection = con;
            cmd.CommandText = sql;

            try
            {

                cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(t);

                String ruta = HttpContext.Current.Server.MapPath("~/reportes/ahorro/contrato.txt");
                String resultado = "";
                String archivo = System.IO.File.ReadAllText(ruta);

                if (t.Rows.Count > 0)
                {
                    foreach (DataColumn c in t.Columns)
                    {
                        String valor = t.Rows[0][c].ToString();
                        if (c.DataType == typeof(DateTime) && valor != "")
                        {
                            DateTime fecha = DateTime.Parse(valor);
                            valor = fecha.ToLongDateString();
                        }
                        archivo = archivo.Replace("{{" + c.ColumnName + "}}", valor);

                    }
                }
                char ch= (char)13;
                String[] lista = archivo.Split(ch);
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<String> lst = new List<String>();
               
                int i = 0;
                foreach (String fila in lista)
                {
                    lst.Add(fila.ToString());
                   
                    i = +1;
                }
               
                resultado = js.Serialize(lst);
                return resultado;

            }
            catch (Exception ex)
            {

                String error = ex.Message;
                return error;
            }



        }
        [WebMethod]
        public String cod(String codigo)
        {
            try
            {
                codcliente = codigo;
              
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }

            return "";
        }
        [WebMethod]
        public String calcular(String plazo, String modalidad, String fecha, String monto, String tasa, String moneda)
        {
            DataTable T = new DataTable();
            String resultado = "";
            try
            {
                monto2 = monto;
                T.Columns.Clear();
                T.Clear();

                T.Columns.Add("Número", typeof(int));
                T.Columns.Add("Fecha", typeof(System.DateTime));
                T.Columns.Add("Principal", typeof(double));
                T.Columns.Add("MValor", typeof(double));
                T.Columns.Add("Intereses", typeof(double));
                T.Columns.Add("Total", typeof(double));
                T.Columns.Add("Días", typeof(int));

                int Npagos = Convert.ToInt32(plazo);
                if (modalidad.ToLower() == "diario")
                {
                    Npagos = Convert.ToInt32((Convert.ToDateTime(fecha) - Convert.ToDateTime(fecha).AddMonths(Convert.ToInt32(plazo))).TotalDays);
                    //Npagos = DateDiff(DateInterval.Day,Convert.ToDateTime(fecha), DateAdd(DateInterval.Month,Convert.ToInt32(plazo), Convert.ToDateTime(fecha)));
                }
                else if (modalidad.ToLower() == "semanal")
                {
                    Npagos = Npagos * 4;
                }
                else if (modalidad.ToLower() == "quincenal")
                {
                    Npagos = Npagos * 2;
                }
                else if (modalidad.ToLower() == "bimensual")
                {
                    Npagos = Npagos / 2;
                }
                else if (modalidad.ToLower() == "trimestral")
                {
                    if (Npagos % 3 > 0 | Npagos < 3)
                    {
                        // texto.Text = "El plazo no coincide con la forma de pagos";

                        return resultado;
                    }
                    Npagos = Npagos / 3;
                }
                else if (modalidad.ToLower() == "cada cuatro meses")
                {
                    if (Npagos % 4 > 0 | Npagos < 4)
                    {
                        // texto.Text = "El plazo no coincide con la forma de pagos";
                        return resultado;
                    }
                    Npagos = Npagos / 4;
                }
                else if (modalidad.ToLower() == "semestral")
                {
                    if (Npagos % 6 > 0 | Npagos < 6)
                    {
                        //texto.Text = "El plazo no coincide con la forma de pagos";
                        return resultado;
                    }
                    Npagos = Npagos / 6;
                }
                else if (modalidad.ToLower() == "anual")
                {
                    if (Npagos % 12 > 0 | Npagos < 12)
                    {
                        //  texto.Text = "El plazo no coincide con la forma de pagos";
                        return resultado;
                    }
                    Npagos = Npagos / 12;
                }
                else if (modalidad.ToLower() == "al vencimiento")
                {
                    Npagos = Npagos / Npagos;
                }
                double varSaldo = Convert.ToDouble(monto);
                DateTime NewDate = Convert.ToDateTime(fecha);
                for (int i = 0; i <= Npagos - 1; i++)
                {
                    DataRow drow = T.Rows.Add();
                    drow[0] = i + 1;
                    //para llenar la columna 'Número'
                    System.DateTime myDate = NewDate;
                    if (modalidad.ToLower() == "diario")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddDays(1 * (i + 1));
                        // NewDate = DateAdd(DateInterval.Day, 1 * (i + 1), Convert.ToDateTime(fecha));
                    }
                    else if (modalidad.ToLower() == "semanal")
                    {

                        NewDate = Convert.ToDateTime(fecha).AddDays(7 * (i + 1));
                    }
                    else if (modalidad.ToLower() == "quincenal")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddDays(15 * (i + 1));
                    }
                    else if (modalidad.ToLower() == "bimensual")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddMonths(2 * (i + 1));
                    }
                    else if (modalidad.ToLower() == "trimestral")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddMonths(3 * (i + 1));
                    }
                    else if (modalidad.ToLower() == "cada 4 meses")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddMonths(4 * (i + 1));
                    }
                    else if (modalidad.ToLower() == "semestral")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddMonths(6 * (i + 1));
                    }
                    else if (modalidad.ToLower() == "anual")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddMonths(12 * (i + 1));
                    }
                    else if (modalidad.ToLower() == "al vencimiento")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddMonths(Convert.ToInt32(plazo));

                    }
                    else if (modalidad.ToLower() == "mensual")
                    {
                        NewDate = Convert.ToDateTime(fecha).AddMonths(1 * (i + 1));
                    }

                    //para llenar la columna 'Fecha'
                    drow[1] = NewDate;

                    drow[2] = Convert.ToDouble(monto);
                    //para llenar la columna 'Abono al principal'
                    if (i == 0 & i == 1)
                    {
                        DateTime fecha1 = Convert.ToDateTime(myDate);
                        DateTime fecha2 = Convert.ToDateTime(NewDate);
                        TimeSpan tspam = fecha2 - fecha1;
                        int dias = tspam.Days;
                        drow[6] = dias;

                    }
                    else
                    {
                        DateTime fecha1 = Convert.ToDateTime(myDate);
                        DateTime fecha2 = Convert.ToDateTime(NewDate);
                        TimeSpan tspam = fecha2 - fecha1;
                        int dias = tspam.Days;
                        drow[6] = dias;


                        //saca los días transcurridos entre un mes y el otro
                    }



                }

                double tasamvalor = 3;
                double ultimomv = 0;
               
                for (int a = 0; a < T.Rows.Count; a++)
                {

                    if (moneda == "1")
                    {
                        T.Rows[a][3] =Math.Round((((varSaldo * Convert.ToDouble(tasamvalor / 100)) * Convert.ToInt32(T.Rows[a][6]))) / 365,2) ;

                    }
                    else
                    {
                        T.Rows[a][3] = 0;
                    }

                    T.Rows[a][4] =Math.Round((((varSaldo + ultimomv) * ((Convert.ToDouble(tasa)) / 100)) * Convert.ToDouble(T.Rows[a][6])) / 365,2) ;
                    T.Rows[a][5] =Math.Round(Convert.ToDouble(T.Rows[a][2]) + Convert.ToDouble(T.Rows[a][3]) + Convert.ToDouble(T.Rows[a][4]),2);
                    if (T.Rows[a][3] == DBNull.Value)
                    {
                        ultimomv = 0;
                        
                    }
                    else
                    {
                        ultimomv += Convert.ToDouble(T.Rows[a][3].ToString());

                  
                }

                  
                    List<Dictionary<Object, String>> filas = new List<Dictionary<Object, String>>();
                Dictionary<Object, String> fila;
                foreach (DataRow row in T.Rows)
                {
                    fila = new Dictionary<object, string>();

                    foreach (DataColumn col in T.Columns)
                    {
                        String valor = row[col.ColumnName].ToString();
                        if (col.DataType == typeof(DateTime) && valor != "")
                        {
                            DateTime fechass = DateTime.Parse(valor);
                            valor = fechass.ToString("dd/MM/yyyy");
                        }
                        fila.Add(col.ColumnName, valor);
                    }
                    filas.Add(fila);
                }
                    
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                resultado = js.Serialize(filas);
                   

                }
                t2 = T;

            }
            catch (Exception e)
            {
                String error = e.Message;
            }
            return resultado;
        }
    }
}
