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

        public Task<GeneralOffice> Insert(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<GeneralOffice> TCS = new TaskCompletionSource<GeneralOffice>();

            using (IDbContextTransaction Transaction = this.ApplicationDbContext.Database.BeginTransaction())
            {
                /*await Task.Run(() =>
                {
                });*/

                try
                {
                    string TSql = "DECLARE @MaxID AS Int;SET @MaxID=(SELECT ISNULL(MAX(ID),0) FROM dbo.GeneralOffices);" +
                    "DBCC CHECKIDENT(GeneralOffices, RESEED,@MaxID);";

                    int Row = this.ApplicationDbContext.Database.ExecuteSqlRaw(TSql);

                    this.ApplicationDbContext.Add(GeneralOffice);
                    this.ApplicationDbContext.SaveChanges();

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
                return TCS.Task;
            }                             
        }

        public Task<GeneralOffice> Update(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<GeneralOffice> TCS = new TaskCompletionSource<GeneralOffice>();

            /*await Task.Run(() =>
            {
            });*/

            try
            {
                this.ApplicationDbContext.Entry(GeneralOffice).State = EntityState.Modified;

                this.ApplicationDbContext.Update(GeneralOffice);
                this.ApplicationDbContext.SaveChanges();

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
            return TCS.Task;

        }

        public Task<GeneralOffice> Select(GeneralOffice GeneralOffice)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GeneralOffice>> SelectList(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<List<GeneralOffice>> TCS = new TaskCompletionSource<List<GeneralOffice>>();
            List<GeneralOffice> GeneralOfficeList = new List<GeneralOffice>();
            try
            {
                await Task.Run(() =>
                {
                    GeneralOfficeList = this.ApplicationDbContext.GeneralOffices.Select(A => A).ToList();
                    TCS.SetResult(GeneralOfficeList);
                });
            }
            catch(SqlException Ex)
            {
                TCS.SetException(Ex);
            }

            return TCS.Task.Result;
        }

        public Task<int> Delete(GeneralOffice GeneralOffice)
        {
            TaskCompletionSource<int> TCS = new TaskCompletionSource<int>();
            int Row = 0;
            try
            {
                this.ApplicationDbContext.Entry(GeneralOffice).State = EntityState.Modified;

                GeneralOffice.IsDeleted = true;

                this.ApplicationDbContext.Update(GeneralOffice);
                Row = this.ApplicationDbContext.SaveChanges();

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
            return TCS.Task;
        }
    }
}
