using System;
using System.Data.SqlClient;
using System.Transactions;
using Capstone.Web.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Web.Tests.Integration
{
    [TestClass]
    public class DatabaseDALTests
    {
        private TransactionScope _tran;      //<-- used to begin a transaction during initialize and rollback during cleanup
        private string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=dbfamilyreader;Trusted_Connection=Yes;";
        private int _familyID;                 //<-- used to hold the city id of the row created for our test

        // Set up the database before each test        
        [TestInitialize]
        public void Initialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            _tran = new TransactionScope();

            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd;

                conn.Open();

                //Insert a Dummy Record for Country                
                //cmd = new SqlCommand("INSERT INTO Family (Family_name) VALUES ('TestingTestTest');", conn);
                //cmd.ExecuteNonQuery();

                //Insert a Dummy Record for City that belongs to 'ABC Country'
                //If we want to the new id of the record inserted we can use
                // SELECT CAST(SCOPE_IDENTITY() as int) as a work-around
                // This will get the newest identity value generated for the record most recently inserted
                cmd = new SqlCommand("INSERT INTO Family (Family_name) VALUES ('TestingTestTest'); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
                _familyID = (int)cmd.ExecuteScalar();
            }
        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }

        [TestMethod]
        public void CheckFamilyID()
        {
            DatabaseDAL readerDAL = new DatabaseDAL(_connectionString);

            Family family = new Family();
            family.FamilyName = "Testingtesttest2";
            int familyID = readerDAL.CreateFamily(family);

            Assert.AreNotEqual(0, familyID);

        }

        [TestMethod]
        public void TestRegistration()
        {
            DatabaseDAL readerDAL = new DatabaseDAL(_connectionString);

            Family family = new Family();
            family.FamilyName = "Hostetter";

            int familyID = readerDAL.CreateFamily(family);

            User user = new User();
            user.FirstName = "Ann";
            user.LastName = "Hostetter";
            user.Password = "1234asdf";
            user.Username = "ahostetter";
            user.FamilyID = familyID;

            User newUser = readerDAL.CreateUser(user);

            Assert.AreEqual(user.FamilyID, familyID);
        }

        [TestMethod]
        public void TestGetFamilyID()
        {
            DatabaseDAL readerDAL = new DatabaseDAL(_connectionString);

            Family family = new Family();
            family.FamilyName = "Testing1234";
            family.ID = readerDAL.CreateFamily(family);

            User user = new User();
            user.FirstName = "Jose";
            user.LastName = "Martino";
            user.Password = "1234asdf";

            PasswordHash ph = new PasswordHash(user.Password);

            user.Salt = ph.Salt;
            user.Password = ph.Hash;
            user.Username = "jmartino";
            user.FamilyID = family.ID;
            user.FamilyName = family.FamilyName;


            User newUser = readerDAL.CreateUser(user);

            user = readerDAL.GetUser(newUser.Username, user.Password);

            Assert.AreEqual(user.FamilyID, family.ID);
        }
    }
}
