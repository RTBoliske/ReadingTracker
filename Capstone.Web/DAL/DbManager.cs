using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;

namespace Capstone.Web
{
    public class DbManager
    {

        public static void PopulateDatabase(IDatabaseSvc db)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                RoleItem stdRole = new RoleItem()
                {
                    RoleName = "Standard",
                    
                };
                stdRole.Id = db.AddRoleItem(stdRole);

                // Add Product 1
                RoleItem adminRole = new RoleItem()
                {
                    RoleName = "Admin",

                };
                adminRole.Id = db.AddRoleItem(adminRole);

                string saltString = "abcdefgh";
                byte[] salt = ASCIIEncoding.ASCII.GetBytes(saltString);
                var passwordHash = HashPasswordWithPBKDF2("password");
               var split = passwordHash.Split('|');
                //Add Standard User
                UserItem userItem = new UserItem()
                {
                    FirstName = "Christopher",
                    LastName = "Rupp",
                    UserName = "christopherjrupp",
                    Password = split[1].Trim(),
                    Salt = split[0].Trim(),
                    RoleId = stdRole.Id
                };
                db.AddUserItem(userItem);


                userItem = new UserItem()
                {
                    FirstName = "Christopher",
                    LastName = "Rupp",
                    UserName = "admin",
                    Password = split[1].Trim(),
                    Salt = split[0].Trim(),
                    RoleId = adminRole.Id
                };
                db.AddUserItem(userItem);

                scope.Complete();
               
                
            }
        }

        public static bool VerifyPassword(string password, string hash, string salt, int saltSize = 10, int workFactor = 10000)
        {
            bool result = false;
            
           

           // var passwordHash = HashPasswordWithPBKDF2(password, saltBytes, workFactor);
           // var split = passwordHash.Split('|');
            //result = split[1].Trim() == hash;
            return result;
        }

        public static string HashPasswordWithPBKDF2(string password, string saltStr = "abcd1234", int workFactor = 10000)
        {
            byte[] salt = ASCIIEncoding.ASCII.GetBytes(saltStr);

            // Creates the crypto service provider and provides the salt - usually used to check for a password match
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, workFactor);

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);      //gets the hased password

            return Convert.ToBase64String(salt) + " | " + Convert.ToBase64String(hash);
        }


    }
}