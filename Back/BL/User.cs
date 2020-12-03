using ML;
using ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace BL
{
    public class User
    {
        public static MethodResponse<Users> Loggin(Users user)
        {
            MethodResponse<Users> response = new MethodResponse<Users> { Code = 200, Messagge = "Bienvenido", Result = null };
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string Query = "sp_Loggin";
                    SqlCommand cmd = DL.Conexion.CreateCommand(Query, context);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable table = DL.Conexion.ExecuteCommandSelect(cmd);
                    if (table.Rows.Count > 0)
                    {
                        return response;
                    }
                    else
                    {
                        response.Code = 401;
                        response.Messagge = "Usuario o contraseña incorrectos";
                        response.Result = null;
                        return response;
                    }
                }
            }
            catch (Exception Ex)
            {
                response.Code = 500;
                response.Messagge = Ex.ToString();
                response.Result = null;
                return response;
            }
        }
    }
}
