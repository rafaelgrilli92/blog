using code_4_lazy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_4_lazy.Controllers
{
    public class MenuController : Controller
    {
        private PostDAO PostDAO;
        private CategoriaDAO CategoriaDAO;
        public MenuController(PostDAO PostDAO, CategoriaDAO CategoriaDAO) { this.PostDAO = PostDAO; this.CategoriaDAO = CategoriaDAO; }

        [ChildActionOnly]
        public ActionResult MenuDireito()
        {
            ViewBag.Posts = PostDAO.ListarUltimosPostsPublicados(5);
            ViewBag.Categorias = CategoriaDAO.ListarCategorias();
            return PartialView("_MenuLateralDireito");
        }

        [ChildActionOnly]
        public ActionResult MenuSuperior()
        {
            ViewBag.Categorias = CategoriaDAO.ListarCategorias();
            return PartialView("_MenuSuperior");
        }
    }
}