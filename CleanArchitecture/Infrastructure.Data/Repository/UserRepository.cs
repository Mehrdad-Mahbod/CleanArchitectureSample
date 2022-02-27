using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext ApplicationDbContext;

        public UserRepository(ApplicationDbContext ApplicationDbContext)
        {
            this.ApplicationDbContext = ApplicationDbContext;
        }

        public bool IsExistUser(string email, string password)
        {
            return ApplicationDbContext.Users.Any(u => u.Email == email && u.Password == password);
        }

        public Task<User> Insert(User User)
        {
            TaskCompletionSource<User> TCS = new TaskCompletionSource<User>();
            using (IDbContextTransaction Transaction = this.ApplicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    string TSQL = "DECLARE @MaxID AS Int;" +
                        "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.Users);DBCC CHECKIDENT(Users, RESEED,@MaxID);" +
                        "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.UserRoles);DBCC CHECKIDENT(UserRoles, RESEED,@MaxID);";
                    
                    int Row = ApplicationDbContext.Database.ExecuteSqlRaw(TSQL);
                    ApplicationDbContext.Add(User);

                    this.ApplicationDbContext.SaveChanges();

                    Transaction.Commit();

                    TCS.SetResult(User);
                }
                catch (SqlException Ex)
                {
                    TCS.SetException(Ex);
                    Transaction.Rollback();
                    throw;
                }
                catch (DbUpdateException Ex)
                {
                    TCS.SetException(Ex);
                    Transaction.Rollback();
                    throw;
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                    Transaction.Rollback();
                    throw;
                }
                return TCS.Task;
            }
        }

        public bool IsExistUserName(string userName)
        {
            return ApplicationDbContext.Users.Any(u => u.UserName == userName);
        }

        public bool IsExistEmail(string email)
        {
            return ApplicationDbContext.Users.Any(u => u.Email == email);
        }

        public bool IsExistPhoneNumber(string Name, string Family, string PhoneNumber)
        {
            return ApplicationDbContext.Users.Any(U => U.Name == Name && U.Family == Family && U.PhoneNumber == PhoneNumber);
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
            
            User User1 = (from U in ApplicationDbContext.Users
                          where U.UserName == UserName && U.Password == Password
                          select new User()
                          {
                              ID = U.ID,
                              Name = U.Name,
                              Family = U.Family,
                              UserName = U.UserName,
                              PhoneNumber = U.PhoneNumber,
                              UserRoles = (from Ur in ApplicationDbContext.UserRoles where Ur.UserId == U.ID select Ur).ToList()
                          })
                          .FirstOrDefault();
            
            User User2 = ApplicationDbContext.Users
                .Include(A => A.UserRoles)
                .Where(A => A.UserName == UserName && A.Password == Password)
                .Select(A => A)
                .FirstOrDefault();

            return User2;
        }

        public void Save()
        {
            ApplicationDbContext.SaveChanges();
        }


    }
}
