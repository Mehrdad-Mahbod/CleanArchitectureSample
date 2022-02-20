using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserMenu : BaseEntity
    {
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public int RegisteredUserId { get; set; }
        //[NotMapped]
        [JsonIgnore]
        public Menu Menu { get; set; }
        public UserMenu()
        {

        }
        public UserMenu(UserMenu UserMenu)
        {
            UserId = UserMenu.UserId;
            MenuId = UserMenu.MenuId;
            RegisteredUserId = UserMenu.RegisteredUserId;
            AddedDate = UserMenu.AddedDate;
            IsDeleted = UserMenu.IsDeleted;
            Menu = UserMenu.Menu;
        }
    }
}
