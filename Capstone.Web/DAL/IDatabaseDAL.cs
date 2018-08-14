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
        List<Users> GetAllUsers();
        Users GetUser(string username, string password);


    }
}
