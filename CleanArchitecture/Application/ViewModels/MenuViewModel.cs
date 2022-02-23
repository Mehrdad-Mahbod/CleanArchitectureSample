using Domain.Models;
using System.Collections.Generic;


namespace Application.ViewModels
{
    public class MenuViewModel
    {
        public int ID { get; set; }

        public byte? ParentId { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public string Icon { get; set; }
        public bool IsSelective { get; set; }
        //[ForeignKey("MenuId"), Required]
        public virtual ICollection<UserMenu> UserMenus { get; set; }
        //[ForeignKey("MenuId"), Required]
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
