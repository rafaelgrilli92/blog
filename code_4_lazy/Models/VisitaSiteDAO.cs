using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class VisitaSiteDAO
    {
        public void InserirVisita(VisitaSite visita)
        {
            using (ISession session = NHelper.AbreSession())
            {
                ITransaction tx = session.BeginTransaction();
                session.Save(visita);
                tx.Commit();
            }
        }

        public int ObterQtdeVisitantesSite()
        {
            using (ISession session = NHelper.AbreSession())
            {
                //Hibernate Query Language
                string hql = "select count(*) from VisitaSite V";
                IQuery query = session.CreateQuery(hql);
                return Convert.ToInt32(query.UniqueResult());
            }
        }
    }
}