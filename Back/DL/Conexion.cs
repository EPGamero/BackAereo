using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DL
{
    public class Conexion
    {
        public static string ConnectionString()
        {
            string CadenaConexion = "Data Source=LAPTOP-DO9MIAN5;Initial Catalog=AereoMexico;Integrated Security=True";
            return CadenaConexion;
        }
        public static SqlCommand CreateCommand(string Query, SqlConnection context)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand(Query,context);
            return cmd;
        }
        public static int ExecuteCommand(SqlCommand cmd)
        {
            int RowsAffected = cmd.ExecuteNonQuery();
            return RowsAffected;
        }
        public static DataTable ExecuteCommandSelect(SqlCommand cmd)
        {
            DataTable table = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(table);
            return table;
        }
    }
}
