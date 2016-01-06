using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class InscritoDAO
    {
        private ISession session;

        public InscritoDAO(ISession session)
        {
            this.session = session;
        }

        public void InserirInscrito(Inscrito inscrito)
        {
            ITransaction tx = session.BeginTransaction();
            session.Merge(inscrito);
            tx.Commit();
        }
    }
}