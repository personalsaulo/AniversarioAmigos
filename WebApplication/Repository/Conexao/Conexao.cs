using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication.Repository.Conexao
{
    public class Conexao
    {
        private SqlConnection conn;
        private SqlCommand Comando;
        private SqlDataReader dr;
        public string string_conexao { get; set; }

        public bool conectar()
        {
            conn = new SqlConnection();
            conn.ConnectionString = string_conexao;
            conn.Open();
            return conn.State == System.Data.ConnectionState.Open;
        }

        public bool desconectar()
        {
            if (Comando != null)
                Comando.Dispose();
            Comando = null;
            conn.Close();
            conn.Dispose();
            conn = null;
            return true;
        }

        public bool Executar(string sql, bool select)
        {
            Comando = conn.CreateCommand();
            Comando.CommandText = sql;
            Comando.CommandType = System.Data.CommandType.Text;

            if (select)
            {
                dr = Comando.ExecuteReader();
                return dr != null;
            }
            else
            {
                return Comando.ExecuteNonQuery() > 0;
            }

        }

        public DataTable GetData(string nome_tabela)
        {
            DataTable dt = new DataTable(nome_tabela);
            dt.Load(dr);
            return dt;
        }
    }
}