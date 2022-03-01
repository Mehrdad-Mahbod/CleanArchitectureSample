using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class GeneralOfficeRepository : IGeneralOfficeRepository
    {
        private ApplicationDbContext ApplicationDbContext;

        public GeneralOfficeRepository(ApplicationDbContext ApplicationDbContext )
        {
            this.ApplicationDbContext = ApplicationDbContext;
        }

        public async Task<GeneralOffice> InsertAsync(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<GeneralOffice> TCS = new TaskCompletionSource<GeneralOffice>();
            await Task.Run(() =>
            {
                using (IDbContextTransaction Transaction = this.ApplicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        string TSql = "DECLARE @MaxID AS Int;SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.GeneralOffices);" +
                        "DBCC CHECKIDENT(GeneralOffices, RESEED,@MaxID);";

                        int Row = this.ApplicationDbContext.Database.ExecuteSqlRaw(TSql);

                        this.ApplicationDbContext.Add(GeneralOffice);
                        this.ApplicationDbContext.SaveChangesAsync().Wait();

                        Transaction.Commit();

                        TCS.SetResult(GeneralOffice);
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
                }
            });
            return TCS.Task.Result;
        }

        public async Task<GeneralOffice> UpdateAsync(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<GeneralOffice> TCS = new TaskCompletionSource<GeneralOffice>();
            await Task.Run(() =>
            {
                try
                {
                    this.ApplicationDbContext.Entry(GeneralOffice).State = EntityState.Modified;

                    this.ApplicationDbContext.Update(GeneralOffice);
                    this.ApplicationDbContext.SaveChangesAsync().Wait();

                    TCS.SetResult(GeneralOffice);
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

        public Task<GeneralOffice> Select(GeneralOffice GeneralOffice)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GeneralOffice>> SelectListAsync(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<List<GeneralOffice>> TCS = new TaskCompletionSource<List<GeneralOffice>>();
            List<GeneralOffice> GeneralOfficeList = new List<GeneralOffice>();
            await Task.Run(() =>
            {
                try
                {
                    GeneralOfficeList = this.ApplicationDbContext.GeneralOffices.Select(A => A).ToList();
                    TCS.SetResult(GeneralOfficeList);
                }
                catch (SqlException Ex)
                {
                    TCS.SetException(Ex);
                }
            });

            return TCS.Task.Result;
        }

        public async Task<int> DeleteAsync(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<int> TCS = new TaskCompletionSource<int>();
            int Row = 0;
            await Task.Run(() =>
            {
                try
                {
                    this.ApplicationDbContext.Entry(GeneralOffice).State = EntityState.Modified;

                    GeneralOffice.IsDeleted = true;

                    this.ApplicationDbContext.Update(GeneralOffice);
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
