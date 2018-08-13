using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web
{
    public class RoleItem : BaseItem
    {
        public string RoleName { get; set; }


        public RoleItem Clone()
        {
            RoleItem item = new RoleItem();
            item.Id = this.Id;
            item.RoleName = this.RoleName;
           
            return item;
        }

    }
}