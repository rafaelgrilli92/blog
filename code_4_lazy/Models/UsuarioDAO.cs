using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class UsuarioDAO
    {
          private ISession session;

          public UsuarioDAO(ISession session)
        {
            this.session = session;
        }

        public void InserirAlterarUsuario(Usuario usuario)
        {
            ITransaction tx = session.BeginTransaction();
            session.Merge(usuario);
            tx.Commit();
        }

        public Usuario ObterUsuarioPorId(int id)
        {
            return session.Get<Usuario>(id);
        }

        public void DeletarUsuario(Usuario usuario)
        {
            ITransaction tx = session.BeginTransaction();
            session.Delete(usuario);
            tx.Commit();
        }

        public IList<Usuario> ListarUsuario()
        {
            string hql = "select U from Usuario U";
            IQuery query = session.CreateQuery(hql);
            return query.List<Usuario>();
        }

        public Usuario AutenticarUsuario(string Login, string Senha)
        {
            CryptographyManager crypt = new CryptographyManager();
            Senha = crypt.Encrypt(Senha);

            string hql = "select U from Usuario U where U.Login = :Login AND U.Senha = :Senha";
            IQuery query = session.CreateQuery(hql);
            query.SetParameter("Login", Login);
            query.SetParameter("Senha", Senha);
            return query.UniqueResult<Usuario>();
        }
    }
}