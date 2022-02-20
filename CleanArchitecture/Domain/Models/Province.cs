using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;


namespace Domain.Models
{
    public class Province : BaseEntity
    {
        public string Name { get; set; }
        public string PrePhoneNumber { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public Province() : base()
        {
            IsDeleted = false;
        }
        public Province(short ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        //public async Task<List<Province>> SelectAsync(IGenericRepository<Province> IgrProvince)
        //{
        //    List<Province> ProvincesList = new List<Province>();

        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            ProvincesList = IgrProvince.GetAll(null,null,"").ToList();
        //        });

        //        return ProvincesList;
        //    }
        //    catch (SqlException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //    catch (DbException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }

        //}

        //public async Task<List<Province>> SelectWithClauseAsync(IGenericRepository<Province> IgrProvince)
        //{
        //    List<Province> ProvincesList = new List<Province>();

        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            ProvincesList = IgrProvince.GetAll(a=>a.Name.Contains(this.Name), null, "").ToList();
        //        });

        //        return ProvincesList;
        //    }
        //    catch (SqlException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }
        //    catch (DbException /*Ex*/)
        //    {
        //        //throw Ex;
        //        throw;
        //    }

        //}
    }
}
