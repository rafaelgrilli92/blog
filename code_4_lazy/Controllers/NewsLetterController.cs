using code_4_lazy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace code_4_lazy.Controllers
{
    public class NewsLetterController : Controller
    {
        private InscritoDAO InscritoDAO;
        public NewsLetterController(InscritoDAO InscritoDAO)
        {
            this.InscritoDAO = InscritoDAO;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InscreverUsuario(string Email)
        {
            Inscrito inscrito = new Inscrito();
            inscrito.Email = Email;
            InscritoDAO.InserirInscrito(inscrito);

            //Enviar email para usuario
            string html_mensagem = System.IO.File.ReadAllText(Server.MapPath("~/Content/layouts/layout_inscricao_newsletter.html"), System.Text.Encoding.GetEncoding("iso-8859-1"));
            EmailHelper.enviarEmail("Inscrição Newsletter", html_mensagem, Email);

            Session["inscritoComSucesso"] = "OK";
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}