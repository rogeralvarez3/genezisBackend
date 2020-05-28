using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace BACKEND
{
    /// <summary>
    /// Descripción breve de serviciocliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class serviciocliente : System.Web.Services.WebService
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
        public String Guardar2(String tabla, String campos, String valores)
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

        public String GuardarPdf(String nombre, String doc)
        {
            doc = doc.Split(',')[1];
            String result = "Archivo guardado correctamente";
            String path = System.Web.HttpContext.Current.Server.MapPath("~/") + "\\escrituras\\" + nombre;
            if (File.Exists(path)) { File.Delete(path); }
            try
            {
                File.WriteAllBytes(path, Convert.FromBase64String(doc.Replace(@"\ n", "")));
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
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

                    if (tabla == "fclientexxx")
                    {
                        DateTime fecha = DateTime.Parse(row[1].ToString());
                        String fechaa = fecha.ToString("yyyy-MM-dd");
                        DateTime fecha2 = DateTime.Parse(row[5].ToString());
                        String fechana = fecha2.ToString("yyyy-MM-dd");


                        fila.Add("id", row[0].ToString());
                        fila.Add("fecha", fechaa);
                        fila.Add("nombre", row[2].ToString());
                        fila.Add("iden", row[3].ToString());
                        fila.Add("dir", row[4].ToString());
                        fila.Add("fechana", fechana);
                        fila.Add("edad", row[6].ToString());
                        fila.Add("sexo", row[7].ToString());
                        fila.Add("escolaridad", row[8].ToString());
                        fila.Add("profesion", row[9].ToString());
                        fila.Add("telefono", row[10].ToString());
                        fila.Add("ocupacion", row[11].ToString());
                        fila.Add("hijos", row[12].ToString());
                        fila.Add("estadocivil", row[13].ToString());
                        fila.Add("nacionalidad", row[14].ToString());
                        fila.Add("ciudad", row[15].ToString());
                        fila.Add("sucursal", row[16].ToString());
                        fila.Add("zona", row[17].ToString());
                        fila.Add("correo", row[18].ToString());
                        fila.Add("lugartrabajo", row[19].ToString());
                        fila.Add("dirtrabajo", row[20].ToString());
                        fila.Add("teletrabajo", row[21].ToString());
                        fila.Add("conyugue", row[22].ToString());
                        fila.Add("teleconyugue", row[23].ToString());
                        fila.Add("referente", row[24].ToString());
                        fila.Add("telereferente", row[25].ToString());
                        fila.Add("dirreferente", row[26].ToString());
                        fila.Add("promotor", row[27].ToString());
                        fila.Add("esdelegado", row[28].ToString());
                        fila.Add("inscrito", row[29].ToString());
                        if (row[30].ToString() == "")
                        {
                            fila.Add("foto", "../img/avatar.png");
                        }
                        else
                        {
                            fila.Add("foto", row[30].ToString());
                        }

                        fila.Add("retiro", row[31].ToString());
                        fila.Add("fecharetiro", row[32].ToString());
                        fila.Add("fechadelegado", row[33].ToString());
                    }
                    else if (tabla == "finstitucionxxx")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("fecha", row[1].ToString());
                        fila.Add("nombre", row[2].ToString());
                        fila.Add("ruc", row[3].ToString());
                        fila.Add("dir", row[4].ToString());
                        fila.Add("telefono", row[5].ToString());
                        fila.Add("fechana", row[6].ToString());
                        fila.Add("edad", row[7].ToString());
                        fila.Add("ocupacion", row[8].ToString());
                        fila.Add("pais", row[9].ToString());
                        fila.Add("ciudad", row[10].ToString());
                        fila.Add("sucursal", row[11].ToString());
                        fila.Add("zona", row[12].ToString());
                        fila.Add("promotor", row[13].ToString());
                        fila.Add("correo", row[14].ToString());
                        fila.Add("rep1", row[15].ToString());
                        fila.Add("idenrep1", row[16].ToString());
                        fila.Add("dirrep1", row[17].ToString());
                        fila.Add("telerep1", row[18].ToString());
                        fila.Add("cargorep1", row[19].ToString());
                        fila.Add("rep2", row[20].ToString());
                        fila.Add("idenrep2", row[21].ToString());
                        fila.Add("dirrep2", row[22].ToString());
                        fila.Add("telerep2", row[23].ToString());
                        fila.Add("cargorep2", row[24].ToString());
                        fila.Add("retiro", row[25].ToString());
                        fila.Add("fecharetiro", row[26].ToString());
                        fila.Add("inscrito", row[27].ToString());
                    }
                    else if (tabla == "vinstitucion")
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
                js.MaxJsonLength = 999999999;
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


                    if (tabla == "fescolaridad")
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
                        fila.Add("codcliente", row[34].ToString());
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
            if (tabla == "fcargos")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else if (tabla == "festudios")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else if (tabla == "movescrituras")
            {
                sql = "SELECT * FROM " + tabla + " WHERE escritura='" + condicion + "'";
            }
            else if (tabla == "vcréditos")
            {
                condicion = condicion.Replace(",", " or id=");
                sql = "SELECT * FROM " + tabla + " WHERE id=" + condicion + "";
            }
            else if (tabla == "vcargos")
            {
                sql = "SELECT * FROM " + tabla + " WHERE idcliente='" + condicion + "'";
            }
            else if (tabla == "vestudios")
            {
                sql = "SELECT * FROM " + tabla + " WHERE idcliente='" + condicion + "'";
            }
            else if (tabla == "vcliente")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else if (tabla == "vapertura")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcliente='" + condicion + "'";
            }
            else if (tabla == "vdesembolso")
            {
                sql = "SELECT * FROM " + tabla + " WHERE codcliente='" + condicion + "'";
            }
            else if (tabla == "vinstitucion")
            {
                sql = "SELECT * FROM " + tabla + " WHERE id='" + condicion + "'";
            }
            else
            {
                sql = "SELECT * FROM " + tabla + " WHERE value='" + condicion + "'";
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
                    if (tabla == "fzona")
                    {

                        fila.Add("posicion", row[3].ToString());
                        fila.Add("circun", row[4].ToString());
                    }
                    else if (tabla == "fcargosxx")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("idcliente", row[1].ToString());
                        fila.Add("comite", row[2].ToString());
                        fila.Add("cargo", row[3].ToString());
                        fila.Add("fechainicial", row[4].ToString());
                        fila.Add("fechafinal", row[5].ToString());
                        fila.Add("actual", row[6].ToString());
                    }
                    else if (tabla == "festudiosxx")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("idcliente", row[1].ToString());
                        fila.Add("tipoestudio", row[2].ToString());
                        fila.Add("nomestudio", row[3].ToString());
                        fila.Add("temas", row[4].ToString());
                        fila.Add("fechainicio", row[5].ToString());
                        fila.Add("fechafinal", row[6].ToString());
                    }
                    else if (tabla == "vcargos")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("idcliente", row[1].ToString());
                        fila.Add("comite", row[2].ToString());
                        fila.Add("cargo", row[3].ToString());
                        fila.Add("fechainicial", row[4].ToString());
                        fila.Add("fechafinal", row[5].ToString());
                        fila.Add("actual", row[6].ToString());
                    }
                    else if (tabla == "vestudios")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("idcliente", row[1].ToString());
                        fila.Add("tipoestudio", row[2].ToString());
                        fila.Add("nomestudio", row[3].ToString());
                        fila.Add("temas", row[4].ToString());
                        fila.Add("fechainicio", row[5].ToString());
                        fila.Add("fechafinal", row[6].ToString());
                    }
                    else if (tabla == "vcliente")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("fecha", row[1].ToString());
                        fila.Add("nombre", row[2].ToString());
                        fila.Add("iden", row[3].ToString());
                        fila.Add("dir", row[4].ToString());
                        fila.Add("fechana", row[5].ToString());
                        fila.Add("edad", row[6].ToString());
                        fila.Add("escolaridad", row[7].ToString());
                        fila.Add("profesion", row[8].ToString());
                        fila.Add("telefono", row[9].ToString());
                        fila.Add("ocupacion", row[10].ToString());
                        fila.Add("hijos", row[11].ToString());
                        fila.Add("pais", row[12].ToString());
                        fila.Add("ciudad", row[13].ToString());
                        fila.Add("sucursal", row[14].ToString());
                        fila.Add("zona", row[15].ToString());
                        fila.Add("correo", row[16].ToString());
                        fila.Add("referente", row[17].ToString());
                        fila.Add("telereferente", row[18].ToString());
                        fila.Add("promotor", row[19].ToString());
                        fila.Add("esdelegado", row[20].ToString());
                        fila.Add("inscrito", row[21].ToString());
                        fila.Add("estadocivil", row[22].ToString());
                        fila.Add("circun", row[23].ToString());

                    }
                    else if (tabla == "vinstitucion")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("fecha", row[1].ToString());
                        fila.Add("nombre", row[2].ToString());
                        fila.Add("ruc", row[3].ToString());
                        fila.Add("dir", row[4].ToString());
                        fila.Add("telefono", row[5].ToString());
                        fila.Add("fechana", row[6].ToString());
                        fila.Add("edad", row[7].ToString());
                        fila.Add("ocupacion", row[8].ToString());
                        fila.Add("pais", row[9].ToString());
                        fila.Add("ciudad", row[10].ToString());
                        fila.Add("sucursal", row[11].ToString());
                        fila.Add("zona", row[12].ToString());
                        fila.Add("posicion", row[13].ToString());
                        fila.Add("promotor", row[14].ToString());
                        fila.Add("correo", row[15].ToString());
                        fila.Add("rep1", row[16].ToString());
                        fila.Add("idenrep1", row[17].ToString());
                        fila.Add("dirrep1", row[18].ToString());
                        fila.Add("telerep1", row[19].ToString());
                        fila.Add("cargorep1", row[20].ToString());
                        fila.Add("rep2", row[21].ToString());
                        fila.Add("idenrep2", row[22].ToString());
                        fila.Add("dirrep2", row[23].ToString());
                        fila.Add("telerep2", row[24].ToString());
                        fila.Add("cargorep2", row[25].ToString());
                        fila.Add("circun", row[28].ToString());
                        fila.Add("inscrito", row[29].ToString());

                    }
                    else if (tabla == "vapertura")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("tipocuenta", row[2].ToString());
                        fila.Add("codcuenta", row[3].ToString());
                        fila.Add("documentos", row[4].ToString());
                    }
                    else if (tabla == "vdesembolso")
                    {
                        fila.Add("id", row[0].ToString());
                        fila.Add("codcredito", row[2].ToString());
                        fila.Add("sector", row[3].ToString());
                        fila.Add("documentos", row[4].ToString());
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
                resultado = e.Message;

            }
            return resultado;


        }

        [WebMethod]
        public String metodoestudios(String idcliente)
        {
            String result = "";
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            DataTable t = new DataTable(), t2 = new DataTable(), t3 = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter();
            List<Dictionary<String, Object>> dResult = new List<Dictionary<string, object>>();
            Dictionary<String, Object> item = new Dictionary<string, object>();
            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            String sql = "SELECT * FROM vestudios where idcliente=" + idcliente;
            cmd.Connection = con;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
                adp.SelectCommand = cmd;
                adp.Fill(t);
                foreach (DataRow fila in t.Rows)
                {
                    item = new Dictionary<String, Object>();
                    item.Add("id", fila["id"]);
                    item.Add("nomestudio", fila["nomestudio"]);
                    item.Add("tipoestudio", fila["tipoestudio"]);
                    item.Add("temas", fila["temas"]);
                    dResult.Add(item);
                }

                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                result = js.Serialize(dResult);
                con.Close();
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
        [WebMethod]
        public String ArchivosExpediente(String cliente, String tipo, String numeroDocumento)
        {
            String result = "";
            String ruta = System.Web.HttpContext.Current.Server.MapPath("~/expedientes") + "\\" + cliente;

            if (Directory.Exists(ruta))
            {
                ruta += "\\" + tipo;
                if (Directory.Exists(ruta))
                {
                    ruta += "\\" + numeroDocumento;
                    if (Directory.Exists(ruta))
                    {
                        DirectoryInfo di = new DirectoryInfo(ruta);
                        foreach (FileInfo archivo in di.GetFiles())
                        {
                            result += (Char)34 + archivo.Name + (Char)34 + ",";
                        }
                    }

                }

            }
            result = "[" + result.Substring(0, result.Length - 1) + "]";
            return result;
        }
        public static string GetFileExtension(String base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }
        private string tipoDoc(string tipo)
        {
            if (tipo == "ahorro")
            {
                return "1";
            }else if (tipo == "créditos")
            {
                return "2";
            }else
            {
                return "3";
            }
        }
        [WebMethod]
        public String GuardarImagen(String socio, String tipo, String numeroDocumento, String nombreDocumento, String imagen)
        {
            String result = "";
            imagen = imagen.Split(',')[1];
            String extensión = GetFileExtension(imagen);
            String imgPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "\\expedientes";
            if (!Directory.Exists(imgPath + "\\" + socio))
            {
                Directory.CreateDirectory(imgPath + "\\" + socio);
            }
            if (!Directory.Exists(imgPath + "\\" + socio + "\\" + tipo))
            {
                Directory.CreateDirectory(imgPath + "\\" + socio + "\\" + tipo);
            }
            if (!Directory.Exists(imgPath + "\\" + socio + "\\" + tipo + "\\" + numeroDocumento))
            {
                Directory.CreateDirectory(imgPath + "\\" + socio + "\\" + tipo + "\\" + numeroDocumento);
            }
            try
            {
                String ruta = imgPath + "\\" + socio + "\\" + tipo + "\\" + numeroDocumento;
                DirectoryInfo di = new DirectoryInfo(ruta);
                foreach (FileInfo archivo in di.GetFiles())
                {
                    if (Path.GetFileNameWithoutExtension(archivo.FullName) == nombreDocumento)
                    {
                        File.Delete(archivo.FullName);
                    }
                }

                File.WriteAllBytes(imgPath + "\\" + socio + "\\" + tipo + "\\" + numeroDocumento + "\\" + nombreDocumento + "." + extensión, Convert.FromBase64String(imagen));
                String iddoc = Dlookup("fdocexpediente", "value", "`text`='" + nombreDocumento + "' and tipo = "+tipoDoc(tipo));
                String tabla = "", colCodigo = "";
                if (tipo == "créditos" || tipo == "asesoría")
                {
                    tabla = "cdesembolso";
                    colCodigo = "`codcredito`";
                }
                else if (tipo == "ahorro")
                {
                    tabla = "aapertura";
                    colCodigo = "`codcuenta`";
                }
                String documentos = Dlookup(tabla, "documentos", condición: colCodigo + "='" + numeroDocumento+"'");
                String[] docs = documentos.Split(',');
                if (docs[0] != "")
                {
                    if (Array.IndexOf(docs, iddoc) == -1) { Array.Resize(ref docs, docs.Length + 1); docs.SetValue(iddoc, docs.Length - 1); documentos = String.Join(",", docs); }
                }
                else { documentos = iddoc; }
                result = Update(tabla, "`documentos`='" + documentos + "'", colCodigo + "='" + numeroDocumento + "' and `codcliente`='" + socio+"'");


            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }
        [WebMethod]
        public String cargardocumentos(String tipo)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            DataTable t = new DataTable();
            MySqlDataAdapter adp = new MySqlDataAdapter();
            String resultado = "";
            con = conexion.conexion.obtnerconexion();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            String sql = "";
            if (tipo == "AHORRO")
            {
                sql = "SELECT * FROM fdocexpediente WHERE tipo='1'";
            }
            else if (tipo == "CREDITO")
            {
                sql = "SELECT * FROM fdocexpediente WHERE tipo='2'";
            }
            else if (tipo == "ASESORIA")
            {
                sql = "SELECT * FROM fdocexpediente WHERE tipo='3'";
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
                    fila.Add("value", row[0].ToString());
                    fila.Add("text", row[1].ToString());

                    filas.Add(fila);
                }



                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

                resultado = js.Serialize(filas);
                con.Close();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return resultado;
        }


        public String Dlookup(String tabla, String campo, String condición)
        {
            String result = "";
            MySqlConnection con = new MySqlConnection();
            con = conexion.conexion.obtnerconexion();
            MySqlCommand cmd = con.CreateCommand();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (condición.Trim().Length > 0) { condición = " where " + condición; }
            cmd.CommandText = "select " + campo + " from " + tabla + condición;
            result = Nz(cmd.ExecuteScalar(),"");
            return result;
        }
        [WebMethod]
        public String Update(String tabla, String campos_valores, String condición)
        {
            String result = "";
            MySqlConnection con = new MySqlConnection();
            con = conexion.conexion.obtnerconexion();
            MySqlCommand cmd = con.CreateCommand();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "update " + tabla + " set " + campos_valores + " where " + condición;
            try
            {
                cmd.ExecuteNonQuery();
                result = "Guardado correctamente";
            }
            catch (Exception ex)
            {

                result = ex.Message;
            }
            return result;
        }
        [WebMethod]

        public String zipFile(String idCliente)
        {
            String path = HttpContext.Current.Server.MapPath("~/") + "expedientes\\" + idCliente;
            String filePath = path + "\\..\\temp_" + idCliente + ".zip";
            File.Delete(filePath);
            ZipFile.CreateFromDirectory(path, filePath);
            String result = "expedientes/temp_" + idCliente + ".zip";
            return result;
        }
        private String Nz(Object valor, String siEsNulo)
        {
            if (valor == null) { return siEsNulo; } else { return valor.ToString(); }
        }
    }
}
