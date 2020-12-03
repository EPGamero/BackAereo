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
    public class UsersController : ApiController
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
        public HttpResponseMessage GetLocations(Users user)
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
                Response.Code =Convert.ToInt32(HttpStatusCode.InternalServerError);
                Response.Messagge = Ex.ToString();
                Response.Result = null;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Response.Messagge);
            }
        }

    }
}
