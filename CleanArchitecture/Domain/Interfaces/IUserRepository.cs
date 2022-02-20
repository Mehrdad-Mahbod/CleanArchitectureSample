using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Interfaces
{
   public interface IUserRepository
   {
       bool IsExistUser(string email, string password);


       void AddUser(User user);
       bool IsExistUserName(string userName);
       bool IsExistEmail(string email);
       void Save();
   }
}
