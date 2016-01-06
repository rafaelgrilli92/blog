using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class CategoriaDAO
    {
        private ISession session;

        public CategoriaDAO(ISession session)
        {
            this.session = session;
        }

        public Categoria ObterCategoriaPorId(int id)
        {
            return session.Get<Categoria>(id);
        }

        public void InserirAlterarCategoria(Categoria categoria)
        {
            ITransaction tx = session.BeginTransaction();
            session.Merge(categoria);
            tx.Commit();
        }

        public void ExcluirCategoria(Categoria categoria)
        {
            ITransaction tx = session.BeginTransaction();
            session.Delete(categoria);
            tx.Commit();
        }

        public IList<Categoria> ListarCategorias()
        {
            //Hibernate Query Language
            string hql = "select C from Categoria C order by C.NomeCategoria";
            IQuery query = session.CreateQuery(hql);
            return query.List<Categoria>();
        }
    }
}