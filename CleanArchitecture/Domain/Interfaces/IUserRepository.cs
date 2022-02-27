using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        Task<User> Insert(User User);
        void Save();
    }
}
