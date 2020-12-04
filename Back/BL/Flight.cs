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
    public class Flight
    {
        public static MethodResponse<List<Flights>> GetFlights(string startDate, string endDate)
        {
            MethodResponse<List<Flights>> response = new MethodResponse<List<Flights>>() { Code = 200, Messagge = "Mapeo exitoso", Result = new List<Flights>() };
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    string Query = "sp_GetFlightsByDate";
                    SqlCommand cmd = DL.Conexion.CreateCommand(Query, context);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable table = DL.Conexion.ExecuteCommandSelect(cmd);
                    if (table.Rows.Count > 0)
                    {
                        foreach(DataRow row in table.Rows)
                        {
                            Flights flight = new Flights();
                            flight.NoFlight = Convert.ToString(row[0]);
                            flight.OCountry = new Countries();
                            flight.OCountry.Country = Convert.ToString(row[1]);
                            flight.DCountry = new Countries();
                            flight.DCountry.Country = Convert.ToString(row[2]);
                            flight.DepartureDate= Convert.ToString(row[3]);
                            response.Result.Add(flight);
                        }
                        return response;
                    }
                    else
                    {
                        response.Code = 400;
                        response.Messagge = "No hay registros de estas fechas";
                        response.Result = new List<Flights>();
                        return response;
                    }
                }
            }
            catch (Exception Ex)
            {
                response.Code = 500;
                response.Messagge = Ex.ToString();
                response.Result = new List<Flights>();
                return response;
            }
        }
    }
}
