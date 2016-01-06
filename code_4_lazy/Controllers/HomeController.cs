using code_4_lazy.Filters;
using code_4_lazy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_4_lazy.Controllers
{
    public class HomeController : Controller
    {
         private PostDAO PostDAO;
         private CategoriaDAO CategoriaDAO;
         public HomeController(PostDAO PostDAO, CategoriaDAO CategoriaDAO) { this.PostDAO = PostDAO; this.CategoriaDAO = CategoriaDAO; }

        [Route("")]
        public ActionResult Index()
        {
            ViewBag.Posts = PostDAO.ListarUltimosPostsPublicados(6);
            return View();
        }
    }
}