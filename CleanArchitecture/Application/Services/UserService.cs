using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;
using Application.Security;
using Application.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class UserService:IUserService
    {
        private IUserRepository UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        public CheckUser CheckUser(RegisterViewModel RegisterViewModel)
        {
            /*bool UserNameValid = UserRepository.IsExistUserName(RegisterViewModel.UserName);
            bool EmailValid = UserRepository.IsExistEmail(RegisterViewModel.Email.Trim().ToLower());*/

            bool PhoneNumberValid = UserRepository.IsExistPhoneNumber(RegisterViewModel.Name, RegisterViewModel.Family, RegisterViewModel.PhoneNumber);
            /*
            if (UserNameValid && EmailValid)
            {
                return ViewModels.CheckUser.UserNameAndEmailNotValid;
            }
            else if (EmailValid)
            {
                return ViewModels.CheckUser.EmailNotValid;
            }
            else if (UserNameValid)
            {
                return ViewModels.CheckUser.UserNameNotValid;
            }
            */

            if (PhoneNumberValid)
            {
                return ViewModels.CheckUser.UserNameNotValid;
            }

            return ViewModels.CheckUser.OK;
        }

        public int RegisterUser(User User)
        {
            UserRepository.AddUser(User);
            UserRepository.Save();
            return User.ID;
        }

        public bool IsExistUser(string email, string password)
        {
            return UserRepository.IsExistUser(email.Trim().ToLower(), PasswordHelper.EncodePasswordMd5(password));
        }
        public User SelectUserNameWithPassword(LoginViewModel LoginViewModel)
        {            
            return UserRepository.SelectUserNameWithPassword(LoginViewModel.UserName, PasswordHelper.EncodePasswordMd5(LoginViewModel.Password));
        }
    }
}
