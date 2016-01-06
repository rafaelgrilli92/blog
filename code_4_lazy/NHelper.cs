using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace code_4_lazy
{
    public class NHelper
    {
        private static ISessionFactory factory = CriaSessionFactory();

        private static ISessionFactory CriaSessionFactory()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();
            ISessionFactory factory = Fluently.Configure(cfg).Mappings(x => { x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()); }).BuildSessionFactory();
            return factory;
        }

        public static ISession AbreSession()
        {
            return factory.OpenSession();
        }
    }
}