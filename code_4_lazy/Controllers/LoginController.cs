using code_4_lazy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_4_lazy.Controllers
{
    public class LoginController : Controller
    {
        private UsuarioDAO UsuarioDAO;
        public LoginController(PostDAO PostDAO, CategoriaDAO CategoriaDAO, UsuarioDAO UsuarioDAO) 
        {
            this.UsuarioDAO = UsuarioDAO;
        }

        [Route("Admin")]
        public ActionResult Index()
        {
             if (Session["usuario"] != null)
             {
                 return RedirectToAction("Posts", "Admin");
             }
             else
             {
                 return View();
             }
            
        }

        [HttpPost]
        public ActionResult Autenticar(string Login, string Senha)
        {
            Usuario usuario = UsuarioDAO.AutenticarUsuario(Login, Senha);
            if (usuario != null)
            {
                Session["usuario"] = usuario;
                return RedirectToAction("Posts", "Admin");
            }
            else
            {
                ModelState.AddModelError("loginInvalido", "Usuário e/ou Senha incorretos");
                return View("Index");
            }
        }

        public ActionResult Sair()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}