using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ML;
using ML.Entities;

namespace BL
{
    public class Passenger
    {
        public static MethodResponse<Passengers> AddPassenger(Passengers passenger)
        {
            MethodResponse<Passengers> response = new MethodResponse<Passengers>() { Code = 200, Messagge = "Pasajero Agregado", Result = null };
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string Query = "sp_AddPassenger";
                    SqlCommand cmd = DL.Conexion.CreateCommand(Query, context);
                    cmd.Parameters.AddWithValue("@Name", passenger.Name);
                    cmd.Parameters.AddWithValue("@LastName", passenger.LastName);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int RowsAffected = DL.Conexion.ExecuteCommand(cmd);
                    if (RowsAffected > 0)
                    {
                        response.Result = passenger;
                        return response;
                    }
                    else
                    {
                        response.Code = 400;
                        response.Messagge = "Pasajero no agregado";
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
