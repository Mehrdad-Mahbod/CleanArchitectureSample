using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;
using Domain.Models;
using Infrastructure.Data.Context;
using Domain.Interfaces.GenericRepository;

namespace Infrastructure.Data.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext ApplicationDbContext;
        public DbSet<T> Entities;
        public GenericRepository(ApplicationDbContext ApplicationDbContext)
        {
            this.ApplicationDbContext = ApplicationDbContext;
            Entities = ApplicationDbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> SelectListAsyncGenericRepository(Expression<Func<T, bool>> Where = null, Func<IQueryable<T>, IOrderedQueryable<T>> Orderby = null, string Includes = "")
        {
            TaskCompletionSource<IQueryable<T>> TCS = new TaskCompletionSource<IQueryable<T>>();
            IQueryable<T> Query = Entities;            
            List<GeneralOffice> GeneralOfficeList = new List<GeneralOffice>();
            await Task.Run(() =>
            {
                try
                {
                    if (Where != null)
                    {
                        Query = Query.Where(Where);
                    }
                    if (Orderby != null)
                    {
                        Query = Orderby(Query);
                    }
                    if (Includes != "")
                    {
                        foreach (string include in Includes.Split(','))
                        {
                            Query = Query.Include(include);
                        }
                    }
                    TCS.SetResult(Query);
                }
                catch (SqlException Ex)
                {
                    TCS.SetException(Ex);
                }
                catch (DbUpdateException Ex)
                {
                    TCS.SetException(Ex);
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                }
            });
            return TCS.Task.Result;
        }
        public async Task<T> SelectAsyncGenericRepository(T Entity)
        {
            TaskCompletionSource<T> TCS = new TaskCompletionSource<T>();
            await Task.Run(() =>
            {
                try
                {
                    T Query  = Entities.SingleOrDefault(S => S.ID == Entity.ID);
                    TCS.SetResult(Query);
                }
                catch (SqlException Ex)
                {
                    TCS.SetException(Ex);
                }
                catch (DbUpdateException Ex)
                {
                    TCS.SetException(Ex);
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                }
            });
            return TCS.Task.Result;
        }
        public async Task<T> InsertAsyncGenericRepository(T Entity)
        {
            /*using (System.Transactions.TransactionScope Scope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeAsyncFlowOption.Enabled))
            {
                Entities.Add(Entity);
                Context.SaveChanges();
                Scope.Complete();
            }*/

            TaskCompletionSource<T> TCS = new TaskCompletionSource<T>();
            await Task.Run(() =>
            {
                using (IDbContextTransaction Transaction = this.ApplicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Entities.Add(Entity);
                        /*string TableName = Entities.EntityType.GetTableName();
                        string TSql = "DECLARE @MaxID AS Int;";
                        if (TableName == "TypeOfGoodses")
                        {
                            TSql += "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo." + TableName + ");DBCC CHECKIDENT(" + TableName + ", RESEED,@MaxID);";
                        }
                        var x = this.ApplicationDbContext.Database.ExecuteSqlRaw(TSql);*/

                        ApplicationDbContext.SaveChangesAsync().Wait();
                        Transaction.Commit();
                        TCS.SetResult(Entity);
                    }
                    catch (SqlException Ex)
                    {
                        Transaction.Rollback();
                        TCS.SetException(Ex);
                    }
                    catch (DbUpdateException Ex)
                    {
                        Transaction.Rollback();
                        TCS.SetException(Ex);
                    }
                    catch (Exception Ex)
                    {
                        Transaction.Rollback();
                        TCS.SetException(Ex);
                    }
                }
            });
            return TCS.Task.Result;
        }
        public async Task<List<T>> InsertListAsyncGenericRepository(List<T> EntityList)
        {
            TaskCompletionSource<List<T>> TCS = new TaskCompletionSource<List<T>>();
            await Task.Run(() =>
            {
                using (IDbContextTransaction Transaction = this.ApplicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Entities.AddRange(EntityList);
                        /*string TableName = Entities.EntityType.GetTableName();
                        string TSql = "DECLARE @MaxID AS Int;";

                        if (TableName == "TypeOfGoodses")
                        {
                            TSql += "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo." + TableName + ");DBCC CHECKIDENT(" + TableName + ", RESEED,@MaxID);";
                        }
                        var x = this.ApplicationDbContext.Database.ExecuteSqlRaw(TSql);*/

                        ApplicationDbContext.SaveChangesAsync().Wait();
                        Transaction.Commit();
                        TCS.SetResult(EntityList);
                    }
                    catch (SqlException Ex)
                    {
                        Transaction.Rollback();
                        TCS.SetException(Ex);
                    }
                    catch (DbUpdateException Ex)
                    {
                        Transaction.Rollback();
                        TCS.SetException(Ex);
                    }
                    catch (Exception Ex)
                    {
                        Transaction.Rollback();
                        TCS.SetException(Ex);
                    }
                }
            });
            return TCS.Task.Result;
        }
        public async Task<T> UpdateAsyncGenericRepository(T Entity)
        {
            #region Old Code
            /*
            try
            {
                if (Entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }
                if (Entity.ID == 0)
                {
                    throw new Exception($"{nameof(Update)} مقدار کلید اصلی این فقره اطلاعات صفر می باشد");
                }

                string TableName = Entities.EntityType.GetTableName();
                string TSql = "DECLARE @MaxID AS Int;";

                if (TableName == "Goodses")
                {
                    TSql += "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo." + TableName + ");DBCC CHECKIDENT(" + TableName + ", RESEED,@MaxID);" +
                        "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.GoodsesFeature);DBCC CHECKIDENT(GoodsesFeature, RESEED,@MaxID);" +
                        "SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.ImagesGoodses);DBCC CHECKIDENT(ImagesGoodses, RESEED,@MaxID)";
                }
                var x = this.ApplicationDbContext.Database.ExecuteSqlRaw(TSql);


                Entities.Update(Entity);
                //Context.Entry(Entity).State = EntityState.Modified;
                foreach (var Item in ApplicationDbContext.Entry(Entity).Properties)
                {
                    if (!Item.Metadata.IsKey())
                    {
                        string Name = Item.Metadata.Name;
                        if (Item.Metadata.IsForeignKey())
                        {
                            if (int.Parse(Item.CurrentValue.ToString()) == 0)
                            {
                                Item.IsModified = false;
                            }
                            else
                            {
                                Item.IsModified = true;
                            }
                        }
                        else
                        {
                            if (Item.CurrentValue == null)
                            {
                                Item.IsModified = false;
                            }
                            else if (Item.Metadata.Name != "IsDeleted")
                            {
                                if (Item.CurrentValue.ToString() == "0")
                                {
                                    Item.IsModified = false;
                                }
                            }
                            else
                            {
                                Item.IsModified = true;
                            }
                        }
                    }
                }
                ApplicationDbContext.SaveChanges();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            */

            #endregion Old Code
            TaskCompletionSource<T> TCS = new TaskCompletionSource<T>();
            await Task.Run(() =>
            {
                try
                {
                    this.ApplicationDbContext.Entry(Entity).State = EntityState.Modified;
                    this.ApplicationDbContext.Update(Entity);
                    this.ApplicationDbContext.SaveChangesAsync().Wait();
                    TCS.SetResult(Entity);
                }
                catch (SqlException Ex)
                {
                    TCS.SetException(Ex);
                    throw;
                }
                catch (DbUpdateException Ex)
                {
                    TCS.SetException(Ex);
                    throw;
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                    throw;
                }
            });
            return TCS.Task.Result;
        }
        public async Task<int> DeleteAsyncGenericRepository(T Entity)
        {
            #region Old Code
            /*
            try
            {
                if (Entity == null)
                {
                    throw new ArgumentNullException("Entity");
                }
                //Entities.Remove(Entity);
                Entities.Add(Entity);
                ApplicationDbContext.Entry(Entity).State = EntityState.Modified;
                ApplicationDbContext.SaveChanges();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
            */
            #endregion Old Code
            TaskCompletionSource<int> TCS = new TaskCompletionSource<int>();
            int Row = 0;
            await Task.Run(() =>
            {
                try
                {
                    this.ApplicationDbContext.Entry(Entity).State = EntityState.Modified;
                    Entity.IsDeleted = true;
                    this.ApplicationDbContext.Update(Entity);
                    Row = this.ApplicationDbContext.SaveChangesAsync().GetAwaiter().GetResult();
                    TCS.SetResult(Row);
                }
                catch (SqlException Ex)
                {
                    TCS.SetException(Ex);
                    throw;
                }
                catch (DbUpdateException Ex)
                {
                    TCS.SetException(Ex);
                    throw;
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                    throw;
                }
            });
            return TCS.Task.Result;
        }

    }
}
