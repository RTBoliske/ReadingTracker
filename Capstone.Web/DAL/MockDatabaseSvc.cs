using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class MockDatabaseSvc : IDatabaseSvc
    {
        private static Dictionary<int, RoleItem> _roleItems = new Dictionary<int, RoleItem>();
        private static Dictionary<string,UserItem> _userItems = new Dictionary<string, UserItem>();
        private static int _roleId = 1;
        private static int _userId = 1;

        public MockDatabaseSvc()
        {
            if (_roleItems.Count == 0 && _userItems.Count == 0)
            {
                DbManager.PopulateDatabase(this);
            }
        }


        public int AddUserItem(UserItem item)
        {
            item.Id = _userId++;
            _userItems.Add(item.UserName, item);
            return item.Id;
        }

        public int AddRoleItem(RoleItem item)
        {
            item.Id = _roleId++;
            _roleItems.Add(item.Id, item);
            return item.Id;
        }


        public List<RoleItem> GetRoleItems()
        {
            List<RoleItem> items = new List<RoleItem>();
            foreach (var item in _roleItems)
            {
                items.Add(item.Value.Clone());
            }
            return items;
        }

        public UserItem GetUserItem(string username)
        {
            UserItem item = null;

            if (_userItems.ContainsKey(username))
            {
                item = _userItems[username];
            }
            else
            {
                throw new Exception("Item does not exist.");
            }

            return item.Clone();
        }
    }
}