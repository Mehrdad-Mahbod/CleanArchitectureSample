using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Menu : BaseEntity
    {
        public byte? ParentId { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public string Icon { get; set; }
        public bool IsSelective { get; set; }
        //[ForeignKey("MenuId"), Required]
        public virtual ICollection<UserMenu> UserMenus { get; set; }
        //[ForeignKey("MenuId"), Required]
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
        public Menu()
        {
            UserMenus = new List<UserMenu>();
            RoleMenus = new List<RoleMenu>();
        }
        public Menu(Menu Menu)
        {
            ID = Menu.ID;
            ParentId = Menu.ParentId;
            Name = Menu.Name;
            Component = Menu.Component;
            Icon = Menu.Icon;
            IsSelective = Menu.IsSelective;
            UserMenus = Menu.UserMenus;
            RoleMenus = Menu.RoleMenus;
        }
        //public List<Menu> SelectAllMenu(IGenericRepository<Menu> DbMenus)
        //{
        //    List<Menu> Menus;
        //    try
        //    {
        //        Menus = DbMenus.GetAll(a => a.IsSelective == true).ToList();
        //        return Menus;
        //    }
        //    catch (SqlException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //    catch (SystemException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //}

        //public /*async Task<List<Menu>>*/ List<Menu> SelectAllUserRoleMenu(IGenericRepository<Menu> DbMenus, IdentityUserRole<int> _UserRole)
        //{
        //    List<Menu> MenuLists = new List<Menu>();
        //    try
        //    {

        //        MenuLists = DbMenus.GetAll(a => a.UserMenus.Any(b => b.UserId == _UserRole.UserId), null, "UserMenus")
        //            .Union
        //            (
        //             DbMenus.GetAll(aa => aa.RoleMenus.Any(bb => bb.RoleId == _UserRole.RoleId), null, "RoleMenus")
        //            )
        //            .OrderBy(a => a.ID)
        //            .ThenBy(a => a.ParentId)
        //            .ToList();

        //        return MenuLists;
        //    }
        //    catch (SqlException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //    catch (DbException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //}
        //public List<Menu> SelectAllMenusAssignedToTheUser()
        //{
        //    List<Menu> MenusList = new List<Menu>();
        //    try
        //    {
        //        //using (ModelsDbContext modelsDbContext = new ModelsDbContext())
        //        //{
        //        //    MenusList = (from M in modelsDbContext.Menus
        //        //                 join Um in modelsDbContext.UserMenus on M.ID equals Um.MenuId
        //        //                 where Um.UserId == this.UserMenuList.ToArray()[0].UserId
        //        //                 select M).ToList();
        //        //}
        //        return MenusList;
        //    }
        //    catch (SqlException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //    catch (DbException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //}
    }
}