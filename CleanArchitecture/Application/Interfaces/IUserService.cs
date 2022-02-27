using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        CheckUser CheckUser(RegisterViewModel RegisterViewModel);
        Task<UserViewModel> AddUserAsync(UserViewModel User);
        bool IsExistUser(string email, string password);
        User GetUserNameWithPassword(LoginViewModel LoginViewModel);
    }
}
