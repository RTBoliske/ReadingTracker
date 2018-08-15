using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web
{
   public interface IDatabaseSvc
    {
        //Role

        //User
        List<User> GetAllUsers();
        User GetUser(string username, string password);
        User CreateUser(User newUser);
        int CreateFamily(Family newFamily);


    }
}
