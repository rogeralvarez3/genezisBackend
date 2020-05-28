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
    /// Descripción breve de servicioweb
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class servicioweb : System.Web.Services.WebService
    {
        public static String cod = "";
        public static String nom = "";
        public static String fechainicial = "";
        public static String fechafinal = "";
        public static String todos = "";
        [WebMethod]
       
        public String Guardar(String tabla, String campos, String valores)
        {
            Object resultado = "";
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            String sql2 = ";select last_insert_id()";
            if (campos.IndexOf("`id`") >= 0) { sql2 = ""; }
            cmd.Connection = con;
            cmd.CommandText = "REPLACE INTO " + tabla + "(" + campos + ")" + " VALUES " + "(" + valores + ")" + sql2;

            try
            {
                resultado = cmd.ExecuteScalar();
                if (resultado != null) { resultado = "id:" + resultado; } else { resultado = "id:"; }
                con.Close();
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado.ToString();

        }
        [WebMethod]
        public String buscarcuentas(String condicion)
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

            sql = "SELECT * FROM vapertura WHERE codcliente='" + condicion + "'";

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

            sql = "SELECT * FROM vdesembolso WHERE codcliente='" + condicion + "'";

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
        public String buscar(String condicion, String tabla)
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
            if (tabla == "fcliente")
            {
                sql = "SELECT * FROM fcliente WHERE codigoc='" + condicion + "'";
            }
            else if (tabla == "vapertura")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcuenta='" + condicion + "'";
            }
            else if (tabla == "vdesembolso")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcredito='" + condicion + "'";
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
        public String llenarcombo(String tabla)
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
            if (tabla == "atipocuenta")
            {
                sql = "SELECT * FROM " + tabla + "";
            }
            else if (tabla == "fzona")
            {
                sql = "SELECT * FROM fzona ";
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
        public String calcular(String plazo, String modalidad, String fecha, String monto, String tasa, String moneda)
        {
            DataTable T = new DataTable();
            String resultado = "";
            try
            {

                T.Columns.Clear();
                T.Clear();

                T.Columns.Add("Número", typeof(int));
                T.Columns.Add("Fecha", typeof(System.DateTime));
                T.Columns.Add("Principal", typeof(double));
                T.Columns.Add("MValor", typeof(double));
                T.Columns.Add("Intereses", typeof(double));
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
                        drow[5] = dias;

                    }
                    else
                    {
                        DateTime fecha1 = Convert.ToDateTime(myDate);
                        DateTime fecha2 = Convert.ToDateTime(NewDate);
                        TimeSpan tspam = fecha2 - fecha1;
                        int dias = tspam.Days;
                        drow[5] = dias;


                        //saca los días transcurridos entre un mes y el otro
                    }



                }

                double tasamvalor = 5;
                double ultimomv = 0;
                for (int a = 0; a < T.Rows.Count; a++)
                {
                    if (T.Rows[a][3] == DBNull.Value)
                    {
                        ultimomv = 0;
                      
                    }
                    else
                    {
                        ultimomv += Convert.ToDouble(T.Rows[a][3].ToString());
                        
                    }


                    if (moneda == "1")
                    {
                       T.Rows[a][3] = (((varSaldo * Convert.ToDouble(tasamvalor / 100)) * Convert.ToInt32(T.Rows[a][5]))) / 365;

                    }
                    else
                    {
                        T.Rows[a][3] = 0;
                    }

                    T.Rows[a][4] = (((varSaldo + Convert.ToDouble(T.Rows[a][3])) * ((Convert.ToDouble(tasa)) / 100)) * Convert.ToDouble(T.Rows[a][5])) / 365;
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

            catch (Exception e)
            {
                String error = e.Message;
            }
            return resultado;
        }
        [WebMethod]
        public String calcularcredito(String plazo, String modalidad, String fecha, String monto, String tasa)
        {
            DataTable T = new DataTable();
            String resultado = "";
            try
            {

                T.Columns.Clear();
                T.Clear();

                T.Columns.Add("Número", typeof(int));
                T.Columns.Add("Fecha", typeof(System.DateTime));
                T.Columns.Add("Principal", typeof(double));
                T.Columns.Add("MValor", typeof(double));
                T.Columns.Add("Intereses", typeof(double));
                T.Columns.Add("Cuota", typeof(double));
                T.Columns.Add("Saldo", typeof(double));
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

                    drow[2] = string.Format("{0:0.00}", Convert.ToDouble(monto) / Npagos);
                    //para llenar la columna 'Abono al principal'
                    if (i == 0 & i == 1)
                    {
                        DateTime fecha1 = Convert.ToDateTime(myDate);
                        DateTime fecha2 = Convert.ToDateTime(NewDate);
                        TimeSpan tspam = fecha2 - fecha1;
                        int dias = tspam.Days;
                        drow[7] = dias;

                    }
                    else
                    {
                        DateTime fecha1 = Convert.ToDateTime(myDate);
                        DateTime fecha2 = Convert.ToDateTime(NewDate);
                        TimeSpan tspam = fecha2 - fecha1;
                        int dias = tspam.Days;
                        drow[7] = dias;


                        //saca los días transcurridos entre un mes y el otro
                    }

                }
                Double tasamvalor = 5;

                for (int a = 0; a < T.Rows.Count; a++)
                {
                    
                    double ultimomv = 0;
                 


                    if (T.Rows[a][3] == DBNull.Value)
                    {
                        ultimomv = 0;
                           }
                    else
                    {
                        ultimomv += Convert.ToDouble(T.Rows[a][3].ToString());
                     
                    }

                

                    double mvalor = ((((varSaldo *(tasamvalor/100))) * Convert.ToDouble(T.Rows[a][7])))/ 360;
                    T.Rows[a][3] = mvalor.ToString("N2");
                    double interes = (((varSaldo + Convert.ToDouble(T.Rows[a][3])) * ((Convert.ToDouble(tasa)) / 100)) * Convert.ToDouble(T.Rows[a][7])) / 360;
                    T.Rows[a][4] = interes.ToString("N2");
                    double cuota=Convert.ToDouble(T.Rows[a][2]) + Convert.ToDouble(T.Rows[a][3]) + Convert.ToDouble(T.Rows[a][4]);
                    T.Rows[a][5] = cuota.ToString("N2");
                    varSaldo = (Convert.ToDouble(varSaldo) - Convert.ToDouble(T.Rows[a][2]));
                    T.Rows[a][6] = varSaldo.ToString("N2");

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

            catch (Exception e)
            {
                String error = e.Message;
            }
            return resultado;

        }

        private Boolean logged()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["codigo"] != null) { return true; } else { return false; }
        }
      
        [WebMethod(EnableSession = true)]

        public String Login(String user, String pass)
        {
            HttpContext context = HttpContext.Current;
         
            String result="";
            if (context.Session["codigo"] != null)
            {
                if (user == context.Session["codigo"].ToString())
                {
                    return context.Session["nombre"].ToString();
                }
            }
            MySqlConnection cn = conexion.conexion.obtnerconexion();
            if (cn.State == ConnectionState.Closed) { cn.Open(); }
            MySqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "select concat(nombre,';',IFNULL(foto,'')) from fcliente where codigoc='" + user + "' and pass='" + pass + "'";
            try
            {
                result = Nz(cmd.ExecuteScalar(), "");
            }
            catch (Exception ex)
            {
                String error = ex.Message;
               
            }
           
            if (result.Length > 0)
            {

                context.Session["nombre"] = result.Split(';')[0].ToString();
                context.Session["codigo"] = user;

            }
            else
            {
                result = "error;Usuario o contraseña incorrecta.";
            }
            return result;

        }

        private String Nz(Object valor, String siEsNulo)
        {
            if (valor == null) { return siEsNulo; } else { return valor.ToString(); }
        }
        [WebMethod]
        public String codigo(String codigo,String nombre)
        {
            try
            {
          cod = codigo;
            nom = nombre;
            }catch(Exception ex)
            {
                String error = ex.Message;
            }
            
            return "";
        }
        [WebMethod]
        public String codigoahorro(String codigo, String nombre,String fechai,String fechaf,string todo)
        {
            try
            {
            cod = codigo;
            nom = nombre;
            fechainicial = fechai;
            fechafinal = fechaf;
            todos = todo;
            }catch(Exception ex)
            {
                String error = ex.Message;
            }
            
            return "";
        }
    }
}
