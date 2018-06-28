using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication.Domain;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class FriendRepository
    {
                                   
        string stringDeConexao = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saulo\source\repos\AniversarioAmigos\WebApplication\App_Data\Database1.mdf;Integrated Security = True";

        public List<FriendViewModel> GetAllFriends()
        {
            List<FriendViewModel> lst = new List<FriendViewModel>();
            FriendViewModel aux = new FriendViewModel();
            DataTable dt = new DataTable();
            Conexao.Conexao banco = new Conexao.Conexao();
            banco.string_conexao = stringDeConexao;

            if (banco.conectar())
            {
                banco.Executar("select * " +
                                "from Friends", true);

                dt = banco.GetData("Friends");

                banco.desconectar();
            }

            foreach (DataRow dr in dt.Rows)
            {
                aux = new FriendViewModel();
                aux.FriendId = (int)dr["FriendId"];
                aux.FirstName = dr["FirstName"].ToString();
                aux.LastName = dr["LastName"].ToString();
                aux.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());

                lst.Add(aux);
            }
            return lst;

        }

        public List<FriendViewModel> GetOneFriendByName(string name)
        {
            List<FriendViewModel> lista = new List<FriendViewModel>();
            FriendViewModel auxiliar = new FriendViewModel();
            DataTable dataTable = new DataTable();
            Conexao.Conexao conect = new Conexao.Conexao();
            conect.string_conexao = stringDeConexao;

            if (conect.conectar())
            {
                conect.Executar("select * " +
                                "from Friends " +
                                "where " +
                                "FirstName like '%" + name + "%'", true);

                dataTable = conect.GetData("Friends");

                conect.desconectar();
            }

            foreach (DataRow dr in dataTable.Rows)
            {

                auxiliar.FriendId = (int)dr["FriendId"];
                auxiliar.FirstName = dr["FirstName"].ToString();
                auxiliar.LastName = dr["LastName"].ToString();
                auxiliar.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());

                lista.Add(auxiliar);
            }
            return lista;
        }

        public bool DeleteAFriendById(int id)
        {
            bool res = false;
            Conexao.Conexao connect = new Conexao.Conexao();
            connect.string_conexao = stringDeConexao;

            if (connect.conectar())
            {
                res = connect.Executar(string.Format(
                                "delete Pessoa " +
                                " where id = {0}",
                                id.ToString()), false);

                connect.desconectar();
            }

            return res;
        }

        public bool UpdateAFriend(FriendViewModel friendViewModel)
        {
            bool res = false;
            Conexao.Conexao Connect = new Conexao.Conexao();
            Connect.string_conexao = stringDeConexao;

            if (Connect.conectar())
            {
                res = Connect.Executar(string.Format("update Friends " +
                                " set FirstName = '{0}', " +
                                " LastName = '{1}'," +
                                " BirthDate = '{2}'" +
                                " where FriendId = {3}",
                                friendViewModel.FirstName,
                                friendViewModel.LastName,
                                friendViewModel.BirthDate,
                                friendViewModel.FriendId), false);

                Connect.desconectar();
            }

            return res;
        }

        public FriendViewModel GetOneFriendById(int id)
        {
            List<FriendViewModel> lista = new List<FriendViewModel>();
            FriendViewModel auxiliar = new FriendViewModel();
            DataTable dataTable = new DataTable();
            Conexao.Conexao conect = new Conexao.Conexao();
            conect.string_conexao = stringDeConexao;

            if (conect.conectar())
            {
                conect.Executar("select * " +
                                "from Friends " +
                                "where " +
                                "FriendId = " + id.ToString(), true);

                dataTable = conect.GetData("Friends");

                conect.desconectar();
            }

            foreach (DataRow dr in dataTable.Rows)
            {
                auxiliar.FriendId = (int)dr["FriendId"];
                auxiliar.FirstName = dr["FirstName"].ToString();
                auxiliar.LastName = dr["LastName"].ToString();
                auxiliar.BirthDate = DateTime.Parse(dr["BirthDate"].ToString());

                lista.Add(auxiliar);
            }
            return lista.Count > 0 ? lista[0] : null;
        }

        public IEnumerable<Friend> GetAllTodaysBirthdays()
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=c:\users\saulo\source\repos\WebApplication\WebApplication\App_Data\Database1.mdf;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                var commandText = "Select * from Friends where BirthDate = GETDATE()";
                var selectCommand = new SqlCommand(commandText, connection);

                var friends = new List<Friend>();

                try
                {
                    connection.Open();
                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            var friend = new Friend();
                            friend.FriendId = (int)reader["FriendId"];
                            friend.FirstName = reader["FirstName"].ToString();
                            friend.LastName = reader["LastName"].ToString();
                            friend.BirthDate = (DateTime)reader["BirthDate"];

                            friends.Add(friend);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                return friends;
            }
        }

        public bool InsertFriends(FriendViewModel newFriend)
        {
            bool result = false;
            string dataformatada = string.Format(newFriend.BirthDate.ToString("yyyy-MM-dd"));
            string sql = "insert into dbo.Friends " +
                   " ( FirstName, LastName, BirthDate )" +
                   " values " +
                   " ('"+ newFriend.FirstName + "','" + newFriend.LastName + "','" + dataformatada + "') ";

            Conexao.Conexao BD = new Conexao.Conexao();
            BD.string_conexao = stringDeConexao;

            BD.conectar();

            result = BD.Executar(sql, false);

            BD.desconectar();

            return result;


        }
    }
}