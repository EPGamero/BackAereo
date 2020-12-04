using ML;
using ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    public class AreoMéxicoController : ApiController
    {
        /// <summary>
        /// Loggin
        /// </summary>
        /// <returns>datos.Messagge => loggin success</returns>
        /// <response code="200">Success</response>
        /// <response code="400">input values incorrects</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Fail loggin process</response>
        [HttpGet]
        [Route("api/loggin")]
        public HttpResponseMessage Loggin(Users user)
        {
            var Response = new MethodResponse<Users>() { Code = 400, Messagge = "Válida tus datos.", Result = null };
            try
            {
                if (user.UserName != "" && user.UserName != null && user.Password != "" && user.Password != null)
                {
                    var datos = BL.User.Loggin(user);

                    if (datos.Code != 200)
                    {
                        if (datos.Code == 401)
                        {
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, datos.Messagge);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, datos.Messagge);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, datos.Messagge);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Response.Messagge);
                }
            }
            catch (Exception Ex)
            {
                Response.Code = Convert.ToInt32(HttpStatusCode.InternalServerError);
                Response.Messagge = Ex.ToString();
                Response.Result = null;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Response.Messagge);
            }
        }

        /// <summary>
        /// get Flights by Dates
        /// </summary>
        /// <returns>datos.Messagge => loggin success</returns>
        /// <response code="200">Success</response>
        /// <response code="400">input values incorrects</response>
        /// <response code="500">Fail loggin process</response>
        [HttpGet]
        [Route("api/flights/{startDate}/{endDate}")]
        public HttpResponseMessage GetFlightsByDates(string startDate, string endDate)
        {
            var Response = new MethodResponse<List<Flights>>() { Code = 400, Messagge = "Válida tus datos.", Result = new List<Flights>() };
            try
            {
                if (startDate != "" && startDate != null && endDate != "" && endDate != null)
                {
                    var datos = BL.Flight.GetFlights(startDate, endDate);
                    if (datos.Code != 200)
                    {
                        if (datos.Code == 400)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, datos.Messagge);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, datos.Messagge);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, datos.Result);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Response.Messagge);
                }
            }
            catch (Exception Ex)
            {
                Response.Code = Convert.ToInt32(HttpStatusCode.InternalServerError);
                Response.Messagge = Ex.ToString();
                Response.Result = null;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Response.Messagge);
            }
        }

        /// <summary>
        /// get Flights by Dates
        /// </summary>
        /// <returns>datos.Messagge => loggin success</returns>
        /// <response code="200">Success</response>
        /// <response code="400">input values incorrects</response>
        /// <response code="500">Fail loggin process</response>
        [HttpPost]
        [Route("api/reservations")]
        public HttpResponseMessage CreateReservations(List<Reservations> reservations)
        {
            var Response = new MethodResponse<List<Flights>>() { Code = 400, Messagge = "Válida tus datos.", Result = new List<Flights>() };
            try
            {
                if (reservations.Count>0)
                {
                    var datos = BL.Reservation.AddReservations(reservations);
                    if (datos.Code != 200)
                    {
                        if (datos.Code == 400)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, datos.Result);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, datos.Messagge);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, datos.Result);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Response.Messagge);
                }
            }
            catch (Exception Ex)
            {
                Response.Code = Convert.ToInt32(HttpStatusCode.InternalServerError);
                Response.Messagge = Ex.ToString();
                Response.Result = null;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Response.Messagge);
            }
        }

        /// <summary>
        /// add new passenger
        /// </summary>
        /// <returns>datos => post success</returns>
        /// <response code="200">Success</response>
        /// <response code="400">input values incorrects</response>
        /// <response code="500">Fail post process</response>
        [HttpPost]
        [Route("api/passenger")]
        public HttpResponseMessage CreateReservations(Passengers passenger)
        {
            var Response = new MethodResponse<Passengers>() { Code = 400, Messagge = "Válida tus datos.", Result = null };
            try
            {
                if (passenger.Name!="" && passenger.Name!=null && passenger.LastName!="" && passenger.LastName!=null)
                {
                    var datos = BL.Passenger.AddPassenger(passenger);
                    if (datos.Code != 200)
                    {
                        if (datos.Code == 400)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, datos.Messagge);
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, datos.Messagge);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, datos.Result);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Response.Messagge);
                }
            }
            catch (Exception Ex)
            {
                Response.Code = Convert.ToInt32(HttpStatusCode.InternalServerError);
                Response.Messagge = Ex.ToString();
                Response.Result = null;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Response.Messagge);
            }
        }

    }
}
