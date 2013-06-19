using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using DomiCheckEmail.Models;

namespace DomiCheckEmail.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var modelo = new HomeModel();

            return View(modelo);
        }

        public JsonResult EnviarMail(string servidor, string puerto, string usuario, string clave, string destinatario)
        {
            if(servidor == null || servidor.Equals(string.Empty))
            {
                return Json(new { success = false, log = "El campo servidor es obligatorio" }, "application/json");
            }
            if (puerto == null || puerto.Equals(string.Empty))
            {
                return Json(new { success = false, log = "El campo puerto es obligatorio" }, "application/json");
            }
            if (usuario == null || usuario.Equals(string.Empty))
            {
                return Json(new { success = false, log = "El campo usuario es obligatorio" }, "application/json");
            }
            if (clave == null || clave.Equals(string.Empty))
            {
                return Json(new { success = false, log = "El campo clave es obligatorio" }, "application/json");
            }
            if (destinatario == null || destinatario.Equals(string.Empty))
            {
                return Json(new { success = false, log = "El campo destinatario es obligatorio" }, "application/json");
            }

            try
            {

                var fromAddress = new MailAddress(usuario);
                var toAddress = new MailAddress(destinatario);
                var fromPassword = clave;
                const string subject = "Prueba DomiCheckEmail";
                const string body = "Esto es una prueba de funcionamiento de correo.";

                var smtp = new SmtpClient
                               {
                                   Host = servidor,
                                   Port = int.Parse(puerto),
                                   EnableSsl = false,
                                   DeliveryMethod = SmtpDeliveryMethod.Network,
                                   UseDefaultCredentials = false,
                                   Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                               };
                using (var message = new MailMessage(fromAddress, toAddress)
                                         {
                                             Subject = subject,
                                             Body = body
                                         })
                {
                    smtp.Send(message);
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = true, log = ex.Message }, "application/json");
            }

            return Json(new { success = true, log = "Mensaje enviado satisfactoriamente" }, "application/json");
        }
    }
}
