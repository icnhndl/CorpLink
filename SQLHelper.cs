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
            string query =
                $"INSERT INTO users(login, password_hash) " +
                $"VALUES({login}, {passwordHash});";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public bool IsUserExists(string login)
        {
            string query =
                $"SELECT COUNT(*) FROM users " +
                $"WHERE login = {login};";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return (int)cmd.ExecuteScalar() == 1;
            }

        }

        public bool AuthorizeUser(string login, string password)
        {
            string passwordHash = Encoder.Encode(password);
            string query =
                $"SELECT COUNT(*) FROM users " +
                $"WHERE login = {login} AND password_hash = {passwordHash};";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                return (int)cmd.ExecuteScalar() == 1;
            }
        }

        public List<UserDetails> GetContacts(string login)
        {
            string query =
                $"SELECT * FROM contacts c " +
                $"WHERE owner_id = {login} " +
                $"JOIN user_details ud " +
                $"ON c.user_id = ud.user_id " +
                $"JOIN departments d " +
                $"ON ud.department_id = d.id;";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                var contacts_data = cmd.ExecuteReader();

                List<UserDetails> userDetails = new();

                while (contacts_data.Read())
                {
                    userDetails.Add(new UserDetails
                    {
                        Surname = (string)contacts_data["surname"],
                        Name = (string)contacts_data["name"],
                        PatName = (string)contacts_data["pat_name"],
                        OfficeNumber = (int)contacts_data["office_num"],
                        Photo = (byte[])contacts_data["photo"],
                        FaxNumber = (string)contacts_data["fax_num"],
                        PersonalNumber = (string)contacts_data["personal_num"],
                        DepartmentName = (string)contacts_data["department_name"]
                    });

                }
                return userDetails;
            }
        }



    }
}
