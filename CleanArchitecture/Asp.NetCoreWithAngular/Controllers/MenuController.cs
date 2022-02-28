using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Asp.NetCoreWithAngular.Controllers
{
    [EnableCors("CorsPolicy")] //وقتی از پورت 5000 خوانده می شود باید کامنت نباشد
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {

        private IMenuService IMenuService;

        public MenuController(IMenuService IMenuService)
        {
            this.IMenuService = IMenuService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("GetAllUserMenusWithUserIdAndRoleId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllUserMenusWithUserIdAndRoleId([FromBody] UserRoleViewModel UserRoleViewModel)
         {
            /*var id = User.Claims.ToList()[0].Value;

            var loggedInUser = HttpContext.User;
            var claym = loggedInUser.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Name);*/

            List<MenuViewModel> MenuList;
            try
            {
                //Menu menu = new Menu();
                //MenuList =  menu.SelectAllUserRoleMenu(this.DbMenus, UserRole);
                MenuList = await this.IMenuService.GetAllUserMenusWithUserIdAndRoleId(UserRoleViewModel);
                return Json(MenuList);
            }
            catch (SqlException Ex)
            {
                return Json(Ex.Message);
            }
        }
    }
}