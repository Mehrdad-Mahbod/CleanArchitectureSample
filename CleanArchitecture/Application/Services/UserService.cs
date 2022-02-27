using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Security;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class UserService:IUserService
    {
        private IUserRepository IUserRepository;
        private IMapper IMapper;

        public UserService(IUserRepository UserRepository , IMapper IMapper)
        {
            this.IUserRepository = UserRepository;
            this.IMapper = IMapper;
        }

        public CheckUser CheckUser(RegisterViewModel RegisterViewModel)
        {
            /*bool UserNameValid = UserRepository.IsExistUserName(RegisterViewModel.UserName);
            bool EmailValid = UserRepository.IsExistEmail(RegisterViewModel.Email.Trim().ToLower());*/

            bool PhoneNumberValid = IUserRepository.IsExistPhoneNumber(RegisterViewModel.Name, RegisterViewModel.Family, RegisterViewModel.PhoneNumber);
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

        public async Task<UserViewModel> AddUserAsync(UserViewModel UserViewModel)
        {
            TaskCompletionSource<UserViewModel> TCS = new TaskCompletionSource<UserViewModel>();
            await Task.Run(() =>
            {
                try
                {
                    User User = this.IMapper.Map<User>(UserViewModel);
                    IUserRepository.Insert(User);
                    //IUserRepository.Save();

                    UserViewModel = this.IMapper.Map<UserViewModel>(User);
                    TCS.SetResult(UserViewModel);
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                }
            });
            return TCS.Task.Result;


        }

        public bool IsExistUser(string email, string password)
        {
            return IUserRepository.IsExistUser(email.Trim().ToLower(), PasswordHelper.EncodePasswordMd5(password));
        }
        public User GetUserNameWithPassword(LoginViewModel LoginViewModel)
        {            
            return IUserRepository.SelectUserNameWithPassword(LoginViewModel.UserName, PasswordHelper.EncodePasswordMd5(LoginViewModel.Password));
        }

    }
}
