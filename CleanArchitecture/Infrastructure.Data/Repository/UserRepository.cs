using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
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
            string TSQL = "DECLARE @MaxID AS Int;" +
                "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.Users);DBCC CHECKIDENT(Users, RESEED,@MaxID);" +
                "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.UserRoles);DBCC CHECKIDENT(UserRoles, RESEED,@MaxID);";
            int Row = AppDbContext.Database.ExecuteSqlRaw(TSQL);
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

        public bool IsExistPhoneNumber(string Name, string Family, string PhoneNumber)
        {
            return AppDbContext.Users.Any(U => U.Name == Name && U.Family == Family && U.PhoneNumber == PhoneNumber);
        }

        public User SelectUserNameWithPassword(string UserName, string Password)
        {
            /*return AppDbContext.Users
                .Where(U => U.UserName == UserName && U.Password == Password)
                .Select(U => U)
                .FirstOrDefault();*/


            /*User User = (from U in AppDbContext.Users
                         join Ur in AppDbContext.UserRoles on U.ID equals Ur.UserId
                         group Ur by U into G
                         where G.Key.UserName == UserName && G.Key.Password == Password
                         select new User()
                         {
                             ID = G.Key.ID,
                             Name = G.Key.Name,
                             Family = G.Key.Family,
                             UserName = G.Key.UserName,
                             PhoneNumber = G.Key.PhoneNumber,
                             UserRoles = G.Select(A => A).ToList()
                         })
                         .FirstOrDefault();*/
            
            User User1 = (from U in AppDbContext.Users
                          where U.UserName == UserName && U.Password == Password
                          select new User()
                          {
                              ID = U.ID,
                              Name = U.Name,
                              Family = U.Family,
                              UserName = U.UserName,
                              PhoneNumber = U.PhoneNumber,
                              UserRoles = (from Ur in AppDbContext.UserRoles where Ur.UserId == U.ID select Ur).ToList()
                          })
                          .FirstOrDefault();
            
            User User2 = AppDbContext.Users
                .Include(A => A.UserRoles)
                .Where(A => A.UserName == UserName && A.Password == Password)
                .Select(A => A)
                .FirstOrDefault();

            return User2;
        }

        public void Save()
        {
            AppDbContext.SaveChanges();
        }


    }
}
