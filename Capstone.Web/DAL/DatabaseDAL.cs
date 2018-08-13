using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class DatabaseDAL : IDatabaseSvc
    {
        private string _connectionString;

        public DatabaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Users> GetAllUsers()
        {
            List<Users> allUsers = new List<Users>();

            string usersSearchSql = @"SELECT * FROM Users;";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(usersSearchSql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        allUsers.Add(MapRowToUsers(reader));
                    }
                }

                return allUsers;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        private Users MapRowToUsers(SqlDataReader reader)
        {
            return new Users()
            {
                ID = Convert.ToInt32(reader["ID"]),
                FirstName = Convert.ToString(reader["First_name"]),
                LastName = Convert.ToString(reader["Last_name"]),
                FamilyID = Convert.ToInt32(reader["FamilyID"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"]),
                Salt = Convert.ToString(reader["Salt"]),
                RoleID = Convert.ToInt32(reader["RoleID"]),
            };
        }
    }
}