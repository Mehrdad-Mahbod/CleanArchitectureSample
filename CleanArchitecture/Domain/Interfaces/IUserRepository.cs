using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        bool IsExistUser(string Email, string Password);
        bool IsExistUserName(string UserName);
        bool IsExistEmail(string Email);
        bool IsExistPhoneNumber(string Name,string Family ,string PhoneNumber);
        User SelectUserNameWithPassword(string UserName, string Password);        
        void AddUser(User user);
        void Save();
    }
}
