using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomiCheckEmail.Models
{
    public class HomeModel
    {
        public string Servidor { get; set; }

        public string Usuario { get; set; }

        public string Clave { get; set; }

        public string Destinatario { get; set; }

        public string Logs { get; set; }

        public string Puerto { get; set; }
    }
}