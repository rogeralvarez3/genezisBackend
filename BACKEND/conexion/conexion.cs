using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BACKEND.conexion
{
    public class conexion
    {
        public static MySqlConnection obtnerconexion()
        {
            MySqlConnection con = new MySqlConnection();
            try
            {
           // port=3312;
            con.ConnectionString = "Server =192.168.1.66;port=3312; Database = genezis;Uid =root; Pwd =; Convert Zero Datetime=True; ";
            con.Open();
            }catch(Exception ex)
            {
                String error = ex.Message;
            }
           
            return con;
        }

    }
}
