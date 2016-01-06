using code_4_lazy.Filters;
using code_4_lazy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_4_lazy.Controllers
{
    [AutorizacaoFilterAttribute]
    public class AdminController : Controller
    {
        private PostDAO PostDAO;
        private CategoriaDAO CategoriaDAO;
        private UsuarioDAO UsuarioDAO;
        public AdminController(PostDAO PostDAO, CategoriaDAO CategoriaDAO, UsuarioDAO UsuarioDAO)
        {
            this.PostDAO = PostDAO;
            this.CategoriaDAO = CategoriaDAO;
            this.UsuarioDAO = UsuarioDAO;
        }

        #region POSTS
        [Route("Admin/Posts")]
        public ActionResult Posts()
        {
            ViewBag.Posts = PostDAO.ListarPosts();
            return View();
        }

        [Route("Admin/Posts/Dados")]
        public ActionResult Dados(int? id)
        {
            Post post = new Post();
            if (id != null)
            {
                post = PostDAO.ObterPostPorId((int)id);
            }

            ViewBag.Categorias = CategoriaDAO.ListarCategorias();
            return View(post);
        }

        [Route("Admin/Posts/AdicionarAlterar")]
        [HttpPost]
        public ActionResult AdicionarAlterar(Post post, HttpPostedFileBase ArquivoImagem)
        {
            if (ModelState.IsValid)
            {
                //if (ArquivoImagem.ContentLength > 0)
                //{
                //    var fileName = Path.GetFileName(ArquivoImagem.FileName);
                //    var path = Path.Combine(Server.MapPath("~/Content/img/img_posts"), fileName);
                //    ArquivoImagem.SaveAs(path);
                //}

                PostDAO.InserirAlterarPost(post);
                return RedirectToAction("Posts");
            }
            else
            {
                ViewBag.Categorias = CategoriaDAO.ListarCategorias();
                return View("Dados", post);
            }

        }
        #endregion

        #region CATEGORIAS
        [Route("Admin/Categorias")]
        public ActionResult Categorias()
        {
            ViewBag.Categorias = CategoriaDAO.ListarCategorias();
            return View();
        }

        [Route("Admin/Categorias/AdicionarAlterar")]
        [HttpPost]
        public ActionResult AdicionarAlterar(int? Id, string NomeCategoria)
        {
            if (ModelState.IsValid)
            {
                Categoria categoria = new Categoria();
                if (Id != null) { categoria.Id = (int)Id; }
                categoria.NomeCategoria = NomeCategoria;

                CategoriaDAO.InserirAlterarCategoria(categoria);
                return RedirectToAction("Categorias");
            }
            else
            {
                return View("Categorias");
            }
        }

        [Route("Admin/Categorias/Excluir")]
        [HttpGet]
        public ActionResult Excluir(int? Id)
        {
            if (Id != null)
            {
                Categoria categoria = CategoriaDAO.ObterCategoriaPorId((int)Id);
                CategoriaDAO.ExcluirCategoria(categoria);
            }
            return RedirectToAction("Categorias");
        }
        #endregion

    }
}