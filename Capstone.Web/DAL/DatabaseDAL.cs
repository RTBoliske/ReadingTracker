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
        public User GetUser(string username)
        {
            User user = new User();

            string sql = @"SELECT TOP 1 * FROM Users WHERE Username = @username";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    //cmd.Parameters.AddWithValue("@password", password);

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
        public User CreateUser(User newUser)
        {
            string sql = @"INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
                           VALUES (@firstName, @lastName, @familyID, @username, @password, @salt, @roleID);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@familyName", newUser.FamilyName);
                    cmd.Parameters.AddWithValue("@firstName", newUser.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", newUser.LastName);
                    cmd.Parameters.AddWithValue("@familyID", newUser.FamilyID);
                    cmd.Parameters.AddWithValue("@username", newUser.Username);
                    cmd.Parameters.AddWithValue("@password", newUser.Password);
                    cmd.Parameters.AddWithValue("@salt", newUser.Salt);
                    cmd.Parameters.AddWithValue("@roleID", 2);
                    var userID = (int)cmd.ExecuteScalar();

                    User user = new User();

                    user.ID = userID;
                    user.FirstName = newUser.FirstName;
                    user.LastName = newUser.LastName;
                    user.Username = newUser.Username;
                    user.Password = newUser.Password;
                    user.FamilyName = newUser.FamilyName;
                    user.FamilyID = newUser.FamilyID;
                    user.Salt = newUser.Salt;
                    user.RoleID = newUser.RoleID;

                    return user;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public User CreateFamilyMember(User newUser)
        {
            string sql = @"INSERT INTO Users (First_name, Last_name, FamilyID, Username, Password, Salt, RoleID) 
                           VALUES (@firstName, @lastName, @familyID, @username, @password, @salt, @roleID);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@familyName", newUser.FamilyName);
                    cmd.Parameters.AddWithValue("@firstName", newUser.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", newUser.LastName);
                    cmd.Parameters.AddWithValue("@familyID", newUser.FamilyID);
                    cmd.Parameters.AddWithValue("@username", newUser.Username);
                    cmd.Parameters.AddWithValue("@password", newUser.Password);
                    cmd.Parameters.AddWithValue("@salt", newUser.Salt);
                    cmd.Parameters.AddWithValue("@roleID", newUser.RoleID);
                    var userID = (int)cmd.ExecuteScalar();

                    User user = new User();

                    user.ID = userID;
                    user.FirstName = newUser.FirstName;
                    user.LastName = newUser.LastName;
                    user.Username = newUser.Username;
                    user.Password = newUser.Password;
                    user.FamilyName = newUser.FamilyName;
                    user.FamilyID = newUser.FamilyID;
                    user.Salt = newUser.Salt;
                    user.RoleID = newUser.RoleID;

                    return user;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public string GetFamilyFromFamilyID(int familyID)
        {
            Family family = new Family();

            string sql = @"SELECT TOP 1 * FROM Family WHERE ID = @familyID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@familyID", familyID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        family = new Family
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            FamilyName = Convert.ToString(reader["Family_name"]),
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return family.FamilyName;
        }
        public User GetUserByFamily(int familyID)
        {
            User user = new User();

            string sql = @"SELECT First_name, Last_name FROM Users WHERE FamilyID = @familyID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@familyID", familyID);

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
        //public User GetUserFromFamilyID(int familyID)
        //{
        //    User user = new User();

        //    string sql = @"SELECT TOP 1 * FROM Family WHERE ID = @familyID";

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(_connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddWithValue("@familyID", familyID);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                user = new User
        //                {
        //                    ID = Convert.ToInt32(reader["ID"]),
        //                    FamilyName = Convert.ToString(reader["Family_name"]),
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
        public List<User> GetAllUsersFromFamilyID(int familyID)
        {
            List<User> userList = new List<User>();

            string sql = @"SELECT * FROM Users WHERE FamilyID = @familyID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@familyID", familyID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User
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
                        userList.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return userList;
        }
        public int CreateFamily(Family newFamily)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Family (Family_name) VALUES (@familyName); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                    cmd.Parameters.AddWithValue("@familyName", newFamily.FamilyName);
                    var familyID = (int)cmd.ExecuteScalar();

                    return familyID;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Book GetBook(Book book)
        {

            string sql = @"SELECT TOP 1 * FROM Book WHERE Title = @title AND ISBN = @ISBN";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", book.Title);
                    cmd.Parameters.AddWithValue("@ISBN", book.ISBN);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        book = new Book
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            FamilyID = Convert.ToInt32(reader["FamilyID"]),
                            Title = Convert.ToString(reader["Title"]),
                            Author = Convert.ToString(reader["Author"]),
                            ISBN = Convert.ToString(reader["ISBN"]),
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return book;
        }
        public Book CreateBook(Book book)
        {
            string sql = @"INSERT INTO Book (ID, FamilyID, UserID, Title, ISBN, Type) VALUES (@ID, @UserName, @FamilyID, @Title, @ISBN, @Type);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@ID", book.ID);
                    cmd.Parameters.AddWithValue("@UserName", book.UserID);
                    cmd.Parameters.AddWithValue("@FamilyID", book.FamilyID);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                    cmd.Parameters.AddWithValue("@Type", book.Type);
                    var bookID = (int)cmd.ExecuteScalar();
                    
                    return book;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public ReadingLog GetReadingLog(ReadingLog log)
        {
            string sql = @"SELECT * FROM ReadingLog WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", log.UserID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        log = new ReadingLog
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            BookID = Convert.ToInt32(reader["BookID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            FamilyID = Convert.ToInt32(reader["FamilyID"]),
                            MinutesRead = Convert.ToInt32(reader["Minutes_read"]),
                            Complete = Convert.ToBoolean(reader["Complete"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                        };
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return log;
        } //need to test
        public ReadingLog CreateReadingLog(ReadingLog log)
        {
            string sql = @"INSERT INTO ReadingLog ReadingLog (BookID, UserID, FamilyID, Minutes_read, Complete, Date)
                           VALUES (@BookID, @UserID, @FamilyID, @Minutes_read, @Complete, @Date);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@BookID", log.BookID);
                    cmd.Parameters.AddWithValue("@UserID", log.UserID);
                    cmd.Parameters.AddWithValue("@FamilyID", log.FamilyID);
                    cmd.Parameters.AddWithValue("@MInutes_read", log.MinutesRead);
                    cmd.Parameters.AddWithValue("@Complete", log.Complete);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    var bookID = (int)cmd.ExecuteScalar();

                    return log;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        } //need to test

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