using code_4_lazy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_4_lazy.Controllers
{
    public class PostController : Controller
    {
        private PostDAO PostDAO;
        public PostController(PostDAO PostDAO) { this.PostDAO = PostDAO; }

        [Route("Post/{postId}/{year}/{month}/{titulo}")]
        public ActionResult Index(int postId, string year, string month, string titulo)
        {
            Post post = PostDAO.ObterPostPorId(postId);

            //Verificar se a sessão ja visualizou o post
            if (Session["PostVisualizado"] == null || (Session["PostVisualizado"] != null && !Session["PostVisualizado"].ToString().Contains("<" + postId + ">")))
            { 
                Session["PostVisualizado"] += "<" + postId + ">";
                post.Visualizacoes++;
                PostDAO.InserirAlterarPost(post);
            }

            return View(post);
        }


        [HttpPost]
        public ActionResult Like(int postId)
        {
            Post post = PostDAO.ObterPostPorId(postId);
            post.Likes++;
            PostDAO.InserirAlterarPost(post);

            Session["Liked"] += "<" + postId.ToString() + ">";

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [Route("Posts")]
        public ActionResult PostsCategoria(string categoria)
        {
            ViewBag.Titulo = categoria;
            IList<Post> posts = PostDAO.listarPostsPublicadosPorCategoria(categoria);
            return View("ListarPosts", posts);
        }

        [Route("Busca")]
        public ActionResult BuscaPosts(string str)
        {
            ViewBag.Titulo = str;
            IList<Post> posts = PostDAO.listarPostsPublicadosPorBusca(str);
            return View("ListarPosts", posts);
        }

    }
}

