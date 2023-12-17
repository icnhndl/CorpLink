using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
namespace CorpLink
{
    internal class SQLHelper
    {
        private MySqlConnection connection;

        public SQLHelper(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }
        public void RegisterUser(string login, string password)
        {
            string passwordHash = Encoder.Encode(password);
            string query = $"INSERT INTO users(login, password_hash) VALUES({login}, {passwordHash});";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public bool IsUserExists(string login)
        {
            string query = $"SELECT COUNT(*) FROM users WHERE login = {login};";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return (int)cmd.ExecuteScalar() == 1;
            }

        }

        public bool AuthorizeUser(string login, string password)
        {
            string passwordHash = Encoder.Encode(password);
            string query = $"SELECT COUNT(*) FROM users WHERE login = {login} AND password_hash = {passwordHash};";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return (int)cmd.ExecuteScalar() == 1;
            }
        }

        public object GetContacts(string login)
        {
            string query = $"SELECT * FROM ;";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return (int)cmd.ExecuteScalar() == 1;
            }
        }
    }
}
