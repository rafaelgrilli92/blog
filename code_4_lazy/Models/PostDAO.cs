using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class PostDAO
    {
        private ISession session;

        public PostDAO(ISession session)
        {
            this.session = session;
        }

        public void InserirAlterarPost(Post post)
        {
            ITransaction tx = session.BeginTransaction();
            session.Merge(post);
            tx.Commit();
        }

        public Post ObterPostPorId(int id)
        {
            return session.Get<Post>(id);
        }

        public void DeletarPost(Post post)
        {
            ITransaction tx = session.BeginTransaction();
            session.Delete(post);
            tx.Commit();
        }

        public IList<Post> ListarPosts()
        {
            //Hibernate Query Language
            string hql = "select P from Post P order by P.DataPublicacao desc";
            IQuery query = session.CreateQuery(hql);
            return query.List<Post>();
        }

        public IList<Post> ListarPostsPublicados()
        {
            //Hibernate Query Language
            string hql = "select P from Post P WHERE P.Publicado = true order by P.DataPublicacao desc";
            IQuery query = session.CreateQuery(hql);
            return query.List<Post>();
        }

        public IList<Post> ListarUltimosPostsPublicados(int qtdePosts)
        {
            //Hibernate Query Language
            string hql = "select P from Post P WHERE P.Publicado = true order by P.DataPublicacao desc";
            IQuery query = session.CreateQuery(hql).SetMaxResults(qtdePosts);
            return query.List<Post>();
        }

        public IList<Post> listarPostsPublicadosPorCategoria(string NomeCategoria)
        {
            //Hibernate Query Language
            string hql = @"select P from Post P 
                           WHERE P.Publicado = true 
                           and P.Categoria.NomeCategoria = :NomeCategoria
                           order by P.DataPublicacao desc";
            IQuery query = session.CreateQuery(hql);
            query.SetParameter("NomeCategoria", NomeCategoria);
            return query.List<Post>();
        }

        public IList<Post> listarPostsPublicadosPorBusca(string str)
        {
            //Hibernate Query Language
            string hql = @"select P from Post P 
                           WHERE P.Publicado = true 
                           and (P.Categoria.NomeCategoria like :str
                                OR P.Titulo like :str
                                OR P.Descricao like :str
                                OR P.Conteudo like :str
                               )
                           order by P.DataPublicacao desc";
            IQuery query = session.CreateQuery(hql);
            query.SetParameter("str", "%" + str + "%");
            return query.List<Post>();
        }
    }


}