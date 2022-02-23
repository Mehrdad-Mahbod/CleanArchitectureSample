using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class MenuRepository : IMenuRepository
    {


        public ApplicationDbContext AppDbContext;

        public MenuRepository(ApplicationDbContext AppDbContext)
        {
            this.AppDbContext = AppDbContext;
        }



        public async Task<List<Menu>> SelectAllUserMenusWithUserIdAndRoleId(UserRole UserRole)
        {

            TaskCompletionSource<List<Menu>> TCS = new TaskCompletionSource<List<Menu>>();
            List<Menu> MenuLists = new List<Menu>();
            await Task.Run(() =>
            {
                try
                {


                    MenuLists = (from M in AppDbContext.Menus
                                 join Um in AppDbContext.UserMenus on M.ID equals Um.MenuId
                                 where Um.UserId == UserRole.UserId
                                 select M)
                                 .Union
                                 (from M in AppDbContext.Menus
                                  join Rm in AppDbContext.RoleMenus on M.ID equals Rm.MenuId
                                  where Rm.RoleId == UserRole.RoleId
                                  select M
                                  ).ToList();

                    TCS.SetResult(MenuLists);



                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                }
            });
            return TCS.Task.Result;
        }
    }
}
