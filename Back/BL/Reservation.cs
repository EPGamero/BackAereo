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
    public class Reservation
    {
        public static MethodResponse<List<ReservationsResponse>> AddReservations(List<Reservations> reservations)
        {
            MethodResponse<List<ReservationsResponse>> response = new MethodResponse<List<ReservationsResponse>>();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ConnectionString()))
                {
                    List<ReservationsResponse> StatusList = new List<ReservationsResponse>();

                    foreach (var reservation in reservations)
                    {
                        string QueryValite = "sp_ValiteReservation";
                        SqlCommand cmd = DL.Conexion.CreateCommand(QueryValite, context);
                        cmd.Parameters.AddWithValue("@NoFlight", reservation.Flight.NoFlight);
                        cmd.Parameters.AddWithValue("@idPassenger", reservation.Passenger.idPassenger);
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable table = DL.Conexion.ExecuteCommandSelect(cmd);
                        context.Close();
        
                        if (table.Rows.Count == 0)
                        {
                            string QueryAdd = "sp_AddReservations";
                            SqlCommand cmdd = DL.Conexion.CreateCommand(QueryAdd, context);
                            cmdd.Parameters.AddWithValue("@NoFlight", reservation.Flight.NoFlight);
                            cmdd.Parameters.AddWithValue("@idPassenger", reservation.Passenger.idPassenger);
                            cmdd.CommandType = CommandType.StoredProcedure;
                            int RowsAffecteed = DL.Conexion.ExecuteCommand(cmdd);
                            if (RowsAffecteed > 0)
                            {
                                ReservationsResponse estatus = new ReservationsResponse();
                                estatus.Estatus = "Reservación realizada con éxito con Número de vuelo: " + reservation.Flight.NoFlight + " , Pasajero: " + reservation.Passenger.idPassenger;
                                StatusList.Add(estatus);

                                response.Code = 200;
                                response.Messagge = "Reservación realizada con éxito";
                                response.Result = StatusList;
                            }
                            else
                            {
                                ReservationsResponse estatus = new ReservationsResponse();
                                estatus.Estatus = "Hubo un error al realizar la reservación con Número de vuelo: " + reservation.Flight.NoFlight + " , Pasajero: " + reservation.Passenger.idPassenger;
                                StatusList.Add(estatus);

                                response.Code = 400;
                                response.Messagge = "Hubo un error al realizar la reservación";
                                response.Result = StatusList;
                            }
                        }
                        else
                        {
                            ReservationsResponse estatus = new ReservationsResponse();
                            estatus.Estatus = "Ya hay una reservación con estos datos Número de vuelo: " + reservation.Flight.NoFlight + " , Pasajero: " + reservation.Passenger.idPassenger;
                            StatusList.Add(estatus);
                            response.Code = 400;
                            response.Messagge = "Ya hay una reservación con estos datos:";
                            response.Result = StatusList;
                        }
                    }
                    return response;
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
