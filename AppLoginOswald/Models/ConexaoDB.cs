using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

namespace AppLoginOswald.Models
{
    public class ConexaoDB : IDisposable
    {
        private readonly MySqlConnection conexao;

        public ConexaoDB()
        {
            conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            conexao.Open();
        }

        

        public void ExecutaComando(string strQuery)
        {
            var vComando = new MySqlCommand(strQuery, conexao);
             vComando.ExecuteNonQuery();
        }

        public MySqlDataReader RetornaComando(string strQuery)
        {
            var vComando = new MySqlCommand(strQuery, conexao);
            
            return vComando.ExecuteReader();
        }

        public string RetornaData(string strQuery)
        {
            string result;
            var vComando = new MySqlCommand(strQuery, conexao);

            result = (string)vComando.ExecuteScalar();
            if(result == null)
            {
                result = "";
            }
            return result;
        }

        public void Dispose()
        {
            if(conexao.State == ConnectionState.Open)
            conexao.Close();
        }

    }
}