using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace code_4_lazy
{
    public class ConnectionFactory
    {
        public static IDbConnection CriaConexao()
        {
            var stringConexao = ConfigurationManager.ConnectionStrings["conn_rgrilli"];
            IDbConnection conexao = new MySqlConnection();
            conexao.ConnectionString = stringConexao.ConnectionString;
            conexao.Open();
            return conexao;
        }
    }
}