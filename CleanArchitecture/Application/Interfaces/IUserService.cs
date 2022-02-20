using System;
using System.Collections.Generic;
using System.Text;
using Application.ViewModels;
using Domain.Models;

namespace Application.Interfaces
{
   public interface IUserService
   {
       CheckUser CheckUser(string username, string email);
       int RegisterUser(User user);
       bool IsExistUser(string email, string password);
   }
}
