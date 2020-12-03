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
            string CadenaConexion = "";
            return CadenaConexion;
        }
        public static SqlCommand CreateCommand(string Query, SqlConnection context)
        {
            context.Open();
            SqlCommand cmd = new SqlCommand();
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
