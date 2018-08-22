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

        //User
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
        public User GetUserByUsername(string username)
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
        public User GetUserByID(int id)
        {
            User user = new User();

            string sql = @"SELECT TOP 1 * FROM Users WHERE ID = @id";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);

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

        //Family
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

        //Books
        public Book GetMostCurrentBook(int userID)
        {

            string sql = @"SELECT TOP 1 * FROM Book WHERE Title = @title AND ISBN = @ISBN";

            Book book = new Book();

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
            string sql = @"INSERT INTO Book (FamilyID, Title, Author, ISBN) VALUES (@FamilyID, @Title, @Author, @ISBN);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    //cmd.Parameters.AddWithValue("@ID", book.ID);
                    cmd.Parameters.AddWithValue("@FamilyID", book.FamilyID);
                    cmd.Parameters.AddWithValue("@Title", book.Title);
                    cmd.Parameters.AddWithValue("@Author", book.Author);
                    cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                    var bookID = (int)cmd.ExecuteScalar();
                    
                    return book;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public List<Book> GetAllBooksByFamilyID(int familyID)
        {
            List<Book> bookList = new List<Book>();
            string sql = @"SELECT Book.Title As Title,
                                  Book.Author As Author,
                                  Book.ISBN As ISBN
                                  From Book
                                  Where Book.FamilyID = @FamilyID;";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@FamilyID", familyID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Book book = new Book
                        {
                            //ID = Convert.ToInt32(reader["ID"]),
                            Title = Convert.ToString(reader["Title"]),
                            Author = Convert.ToString(reader["Author"]),
                            ISBN = Convert.ToString(reader["ISBN"]),
                            //FamilyID = Convert.ToInt32(reader["FamilyID"]),
                        };
                        bookList.Add(book);
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }
            
            return bookList;
        }
        
        public List<Book> GetActiveBooks(int userID)
        {
            List<Book> bookList = new List<Book>();
            string sql = @"SELECT Book.ID AS ID, 
                            Book.FamilyID AS FamilyID, 
                            Book.Title AS Title, 
                            Book.Author AS Author, 
                            Book.ISBN AS ISBN 
                            FROM 
                            Book 
							JOIN ReadingLog ON ReadingLog.BookID = Book.ID 
                            JOIN Users ON Users.ID = ReadingLog.UserID 
                            JOIN Family ON Family.ID = Users.FamilyID
                            WHERE 
                            Users.ID = @UserID
                            AND ReadingLog.Status = 'Active';";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            FamilyID = Convert.ToInt32(reader["FamilyID"]),
                            Title = Convert.ToString(reader["Title"]),
                            Author = Convert.ToString(reader["Author"]),
                            ISBN = Convert.ToString(reader["ISBN"]),
                        };
                        bookList.Add(book);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return bookList;
        }


        public List<Book> GetInactiveBooks(int userID)
        {
            List<Book> bookList = new List<Book>();
            string sql = @"SELECT Book.ID AS ID, 
                            Book.FamilyID AS FamilyID, 
                            Book.Title AS Title, 
                            Book.Author AS Author, 
                            Book.ISBN AS ISBN 
                            FROM 
                            Book 
							JOIN ReadingLog ON ReadingLog.BookID = Book.ID 
                            JOIN Users ON Users.ID = ReadingLog.UserID 
                            JOIN Family ON Family.ID = Users.FamilyID
                            WHERE 
                            Users.ID = @UserID
                            AND ReadingLog.Status = 'Active';";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            FamilyID = Convert.ToInt32(reader["FamilyID"]),
                            Title = Convert.ToString(reader["Title"]),
                            Author = Convert.ToString(reader["Author"]),
                            ISBN = Convert.ToString(reader["ISBN"]),
                        };
                        bookList.Add(book);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return bookList;
        }

        //Reading Logs
        public List<ReadingLog> GetReadingLog(int userID)
        {
            string sql = @"SELECT ReadingLog.ID AS ID, ReadingLog.BookID AS BookID, Book.Title AS Title, Users.ID AS UserID, Family.ID AS FamilyID, ReadingLog.Minutes_read AS Minutes_read, ReadingLog.Type AS Type,
                           ReadingLog.Status AS Status, ReadingLog.Date AS Date FROM ReadingLog JOIN BOOK ON Book.ID = ReadingLog.BookID JOIN Users ON Users.ID = ReadingLog.UserID JOIN Family ON Users.FamilyID = Family.ID
						   WHERE Users.ID = @UserID;";
            List<ReadingLog> logs = new List<ReadingLog>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ReadingLog log = new ReadingLog
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            BookID = Convert.ToInt32(reader["BookID"]),
                            Title = Convert.ToString(reader["Title"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            FamilyID = Convert.ToInt32(reader["FamilyID"]),
                            MinutesRead = Convert.ToInt32(reader["Minutes_read"]),
                            Type = Convert.ToString(reader["Type"]),
                            Status = Convert.ToString(reader["Status"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                        };
                        logs.Add(log);
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return logs;
        } //need to test
        public ReadingLog CreateReadingLog(ReadingLog log)
        {
            string sql = @"INSERT INTO ReadingLog (BookID, UserID, Minutes_read, Status, Type, Date)
                           VALUES (@BookID, @UserID, @Minutes_read, @Status, @Type, @Date);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@BookID", log.BookID);
                    cmd.Parameters.AddWithValue("@UserID", log.UserID);
                    cmd.Parameters.AddWithValue("@Minutes_read", log.MinutesRead);
                    cmd.Parameters.AddWithValue("@Status", log.Status);
                    cmd.Parameters.AddWithValue("@Type", log.Type);
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
        
        //Prizes
        public List<Prize> GetPrizes (int familyID)
        {
            List<Prize> prizeList = new List<Prize>();

            string sql = @"SELECT * FROM Prize WHERE FamilyID = @familyID";

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
                        Prize prize = new Prize
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            UserType = Convert.ToInt32(reader["UserType"]),
                            Milestone = Convert.ToInt32(reader["Goal"]),
                            MaxNumPrizes = Convert.ToInt32(reader["MaxNumPrize"]),
                            isActive = Convert.ToBoolean(reader["isActive"]),
                            StartDate = Convert.ToDateTime(reader["StartDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            FamilyID = Convert.ToInt32(reader["FamilyID"]),
                        };
                        prizeList.Add(prize);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return prizeList;
        }
        public Prize AddPrize (Prize prize)
        {
            string sql = @"INSERT INTO Prize (UserType, Goal, MaxNumPrize, isActive, StartDate, EndDate, FamilyID) 
                           VALUES (@UserType, @Goal, @MaxNumPrize, @isActive, @StartDate, @EndDate, @FamilyID);
                           SELECT CAST(SCOPE_IDENTITY() as int);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@UserType", prize.UserType);
                    cmd.Parameters.AddWithValue("@Goal", prize.Milestone);
                    cmd.Parameters.AddWithValue("@MaxNumPrize", prize.MaxNumPrizes);
                    cmd.Parameters.AddWithValue("@isActive", prize.isActive);
                    cmd.Parameters.AddWithValue("@StartDate", prize.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", prize.EndDate);
                    cmd.Parameters.AddWithValue("@FamilyID", prize.FamilyID);
                    var prizeID = (int)cmd.ExecuteScalar();

                    return prize;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public List<PrizeProgress> GetPrizesByUser (User user)  //not passing back in new USERID for children
        {
            string sql = @"select p.id AS ID, p.UserType AS UserType, p.MaxNumPrize AS MaxNumPrize, p.Goal AS Goal, r.UserID AS UserID, sum(r.minutes_read) AS Minutes_read, (sum(cast(r.minutes_read as real)) / cast(goal as real)) * 100.0 as percentComplete
                            from prize p
                            left join ReadingLog r on p.FamilyID = @familyID
	                               and @todayDate between p.StartDate and p.EndDate
                            where p.isActive = 1
                            and r.UserID = @userID
                            and p.UserType = @userType
                            group by p.id, p.Goal, r.UserID, p.UserType, p.MaxNumPrize
                            order by p.id;";
            //string sql = @"SELECT Prize.ID AS ID, Prize.UserType AS UserType, Prize.Goal AS Goal, 
            //               Prize.MaxNumPrize AS MaxNumPrize, Prize.isActive AS isActive,
            //               Prize.StartDate AS StartDate, Prize.EndDate AS EndDate FROM Prize 
            //               JOIN Family ON Prize.FamilyID = Family.ID JOIN Users ON Users.FamilyID = Family.ID 
            //               WHERE Family.ID = @familyID AND @todayDate > Prize.StartDate AND @todayDate < Prize.EndDate 
            //               AND isActive = 1;";

            List <PrizeProgress> prizeList = new List<PrizeProgress>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@familyID", user.FamilyID);
                    cmd.Parameters.AddWithValue("@todayDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@userID", user.ID);
                    cmd.Parameters.AddWithValue("@userType", user.RoleID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PrizeProgress prize = new PrizeProgress
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            UserType = Convert.ToInt32(reader["UserType"]),
                            MaxNumPrizes = Convert.ToInt32(reader["MaxNumPrize"]),
                            Milestone = Convert.ToInt32(reader["Goal"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            MinutesRead = Convert.ToInt32(reader["Minutes_read"]),
                            PercentProgress = Convert.ToDecimal(reader["percentComplete"]),
                        };
                        prizeList.Add(prize);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return prizeList;
        }


        //Mappers
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