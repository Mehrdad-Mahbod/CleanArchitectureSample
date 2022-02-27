using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Security;
using Application.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using AutoMapper;

namespace Asp.NetCoreWithAngular.Controllers
{
    [EnableCors("CorsPolicy")] //وقتی از پورت 5000 خوانده می شود باید کامنت نباشد
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private IUserService IUserService;
        private IConfiguration IConfiguration;
        private IMapper IMapper;

        public AuthenticationController(IUserService UserService, IConfiguration Configuration, IMapper IMapper)
        {
            this.IUserService = UserService;
            this.IConfiguration = Configuration;
            this.IMapper = IMapper;
        }


        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel RegisterViewModel)
        {
            ModelState.Remove("Email");
            ModelState.Remove("UserName");
            ModelState.Remove("Password");
            ModelState.Remove("RePassword");
            if (ModelState.IsValid)
            {

                CheckUser CheckUser = IUserService.CheckUser(RegisterViewModel);
                if (CheckUser != CheckUser.OK)
                {
                    ViewBag.Check = CheckUser;
                    return View(RegisterViewModel);
                }

                User User = new User()
                {
                    //Email = RegisterViewModel.Email.Trim().ToLower(),
                    UserName = RegisterViewModel.PhoneNumber,
                    Name = RegisterViewModel.Name,
                    Family = RegisterViewModel.Family,
                    PhoneNumber = RegisterViewModel.PhoneNumber,
                    Password = PasswordHelper.EncodePasswordMd5("123"),
                    CityId = 6,
                    UserRoles = new List<UserRole>() { new UserRole() { RoleId = 1 } }
                };

                UserViewModel UserViewModel = IMapper.Map<UserViewModel>(User);

                UserViewModel = await IUserService.AddUserAsync(UserViewModel);

                //return Ok(TogList);
                return Json("کاربر مورد نظر در سیستم درج گردید!");
            }
            else
            {
                return BadRequest("اطلاعات ناقص می باشد!!");
            }
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login(string ReturnUrl="/")
        {
            ViewBag.Return = ReturnUrl;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginViewModel LoginViewModel, string ReturnUrl)
        {
            ModelState.Remove("Email");
            ModelState.Remove("PhoneNumber");            
            if (ModelState.IsValid)
            {
                User U = IUserService.GetUserNameWithPassword(LoginViewModel);

                if (U != null)
                {
                    var Claims = new[]
                    {
                        new Claim("UserID", U.ID.ToString()),
                        new Claim("RoleID", U.UserRoles.ToList()[0].RoleId.ToString()),
                        //new Claim(ClaimTypes.Name,U.Email),
                        //new Claim(ClaimTypes.NameIdentifier,LoginViewModel.Email)
                        new Claim(JwtRegisteredClaimNames.UniqueName , U.UserName/*UserInfo.ID.ToString()*/),
                        new Claim(JwtRegisteredClaimNames.FamilyName ,U.Name + " " + U.Family),
                        //new Claim(JwtRegisteredClaimNames.Email ,UserInfo.Email),
                        new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString())
                    };

                    var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var Principal = new ClaimsPrincipal(Identity);
                    var Properties = new AuthenticationProperties()
                    {
                        IsPersistent = LoginViewModel.RememberMe
                    };

                    HttpContext.SignInAsync(Principal, Properties);
                    /*return Redirect(ReturnUrl);*/

                    /****************************************/
                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IConfiguration["Jwt:MehrdadSecurityKey"]));
                    var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

                    var Expiration = DateTime.UtcNow.AddDays(7);

                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer: IConfiguration["Jwt:Value"],
                        audience: IConfiguration["Jwt:Value"],
                        claims: Claims,
                        expires: Expiration,
                        signingCredentials: creds);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = Expiration
                    });
                }
                else
                {
                    return BadRequest("چنین کاربری در سیستم موجود نمی باشد!!");
                }
            }
            else
            {
                //return View(LoginViewModel);
                return BadRequest();
            }
        }

        #endregion

        [Route("Logout")]
        public IActionResult Logout()
        {
            /*HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");*/
            try
            {
                //await SignInManager.SignOutAsync();
                string[] Message = new string[2];
                string IsLoginText = string.Empty;
                if (User?.Identity.IsAuthenticated == true)
                {
                    IsLoginText = "لاگین بود";
                }
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //Message[0] = IsLoginText + UserManager.GetUserId(HttpContext.User);
                Message[0] = IsLoginText + HttpContext.User.Identity;
                Message[1] = "شما از سیستم خارج شدید";
                return Ok(Message);
            }
            catch (SystemException Ex)
            {
                return BadRequest(Ex.Message);
            }

        }
    }
}