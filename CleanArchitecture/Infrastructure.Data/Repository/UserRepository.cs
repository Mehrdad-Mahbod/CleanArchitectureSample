using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;


namespace Infrastructure.Data.Repository
{
   public class UserRepository:IUserRepository
   {
       private ApplicationDbContext AppDbContext;

       public UserRepository(ApplicationDbContext AppDbContext)
       {
           this.AppDbContext = AppDbContext;
       }

       public bool IsExistUser(string email, string password)
       {
           return AppDbContext.Users.Any(u => u.Email == email && u.Password == password);
       }

       public void AddUser(User user)
        {
            AppDbContext.Add(user);
        }

        public bool IsExistUserName(string userName)
        {
            return AppDbContext.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return AppDbContext.Users.Any(u => u.Email == email);
        }

       public void Save()
       {
           AppDbContext.SaveChanges();
       }
   }
}
