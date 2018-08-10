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
        int AddRoleItem(RoleItem item);
        List<RoleItem> GetRoleItems();


        //User

        int AddUserItem(UserItem item);
        UserItem GetUserItem(string username);


    }
}
