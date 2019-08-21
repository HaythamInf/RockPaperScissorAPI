using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockPaperScissor.Models
{
    public class Respuesta
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }

        public Respuesta()
        {
            this.ResponseCode = string.Empty;
            this.Message = string.Empty;
        }
    }
}