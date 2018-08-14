﻿using System;
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

        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>();

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
        public User GetUser(string username, string password)
        {
            User user = new User();

            string sql = @"SELECT TOP 1 * FROM Users WHERE Username = @username AND password = @password";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
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
            catch (SqlException ex)
            {
                throw;
            }

            return user;
        }
        //public Users InsertNewUser(Users user)
        //{
        //    Users user = new Users();

        //    string sql = @"INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
        //                   VALUES (, 'Binegar', 3, 'jbinegar', '1234asdf', 'jbinegar', 2);";

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddWithValue("@username", username);
        //            cmd.Parameters.AddWithValue("@password", password);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                user = new Users
        //                {
        //                    ID = Convert.ToInt32(reader["ID"]),
        //                    FirstName = Convert.ToString(reader["First_name"]),
        //                    LastName = Convert.ToString(reader["Last_name"]),
        //                    FamilyID = Convert.ToInt32(reader["FamilyID"]),
        //                    Username = Convert.ToString(reader["Username"]),
        //                    Password = Convert.ToString(reader["Password"]),
        //                    Salt = Convert.ToString(reader["Salt"]),
        //                    RoleID = Convert.ToInt32(reader["RoleID"]),
        //                };
        //            }

        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }

        //    return user;
        //}
        private User MapRowToUsers(SqlDataReader reader)
        {
            return new User()
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