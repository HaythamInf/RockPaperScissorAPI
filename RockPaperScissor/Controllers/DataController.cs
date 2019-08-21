using RockPaperScissor.BL;
using RockPaperScissor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RockPaperScissor.Controllers
{
    public class DataController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/GetMoves")]
        public HttpResponseMessage ObtenerMovimientos()
        {
            Respuesta respuesta = new Respuesta();
            var message = Request.CreateResponse(HttpStatusCode.Accepted, "OK");
            respuesta.ResponseCode = HttpStatusCode.OK.ToString();
            try
            {
                ConexionSQL sql = new ConexionSQL();
                DataSet registros = sql.ConsumirProcedimientoAlmacenado("GetMoves");
                DataTable dt = Funciones.DataSet_To_DataTable(registros);
                respuesta.Message = Funciones.DataTableToJSON(dt);
                message = Request.CreateResponse(HttpStatusCode.OK, respuesta);
                return message;
            }
            catch (Exception ex) //Casos contrarios a la actualización exitosa
            {
                switch (ex.Message)
                {
                    default:
                        respuesta.ResponseCode = "500 " + HttpStatusCode.InternalServerError.ToString();
                        respuesta.Message = ex.ToString();
                        message = Request.CreateResponse(HttpStatusCode.InternalServerError, respuesta);
                        break;
                }

                return message;
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/GetScore")]
        public HttpResponseMessage ObtenerPunajeHistorico()
        {
            Respuesta respuesta = new Respuesta();
            var message = Request.CreateResponse(HttpStatusCode.Accepted, "OK");
            respuesta.ResponseCode = HttpStatusCode.OK.ToString();
            try
            {
                ConexionSQL sql = new ConexionSQL();
                DataSet registros = sql.ConsumirProcedimientoAlmacenado("GetScore");
                DataTable dt = Funciones.DataSet_To_DataTable(registros);
                respuesta.Message = Funciones.DataTableToJSON(dt);
                message = Request.CreateResponse(HttpStatusCode.OK, respuesta);
                return message;
            }
            catch (Exception ex) //Casos contrarios a la actualización exitosa
            {
                switch (ex.Message)
                {
                    default:
                        respuesta.ResponseCode = "500 " + HttpStatusCode.InternalServerError.ToString();
                        respuesta.Message = ex.ToString();
                        message = Request.CreateResponse(HttpStatusCode.InternalServerError, respuesta);
                        break;
                }

                return message;
            }
        }
    }
}
