using System;
using System.Collections.Generic;
using System.Text;
using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IUserService
    {
        CheckUser CheckUser(RegisterViewModel RegisterViewModel);
        int RegisterUser(User user);
        bool IsExistUser(string email, string password);
        User SelectUserNameWithPassword(LoginViewModel LoginViewModel);
    }
}
