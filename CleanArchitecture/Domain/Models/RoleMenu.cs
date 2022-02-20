using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class RoleMenu : BaseEntity
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        //[NotMapped]
        //[JsonIgnore]
        public Menu Menu { get; set; }
        public RoleMenu()
        {

        }
        public RoleMenu(RoleMenu RoleMenu)
        {
            RoleId = RoleMenu.RoleId;
            MenuId = RoleMenu.MenuId;
            Menu = RoleMenu.Menu;
        }
    }
}
