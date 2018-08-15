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
    }
}
