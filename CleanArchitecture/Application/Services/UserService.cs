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
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public CheckUser CheckUser(string username, string email)
        {
            bool userNameValid = _userRepository.IsExistUserName(username);
            bool emailValid = _userRepository.IsExistEmail(email.Trim().ToLower());

           
                
             if (userNameValid&&emailValid)
                return ViewModels.CheckUser.UserNameAndEmailNotValid;
            else if (emailValid)
                return ViewModels.CheckUser.EmailNotValid;
            else if (userNameValid)
                 return ViewModels.CheckUser.UserNameNotValid;

            return ViewModels.CheckUser.OK;
        }

        public int RegisterUser(User user)
        {
           _userRepository.AddUser(user);
            _userRepository.Save();
            return user.ID;
        }

        public bool IsExistUser(string email, string password)
        {
            return _userRepository.IsExistUser(email.Trim().ToLower(), PasswordHelper.EncodePasswordMd5(password));
        }
    }
}
