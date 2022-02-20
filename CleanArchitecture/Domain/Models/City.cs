using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Models
{
    public class City : BaseEntity
    {
        [NotMapped]
        public virtual Province Province { get; set; }
        [ForeignKey("ProvinceId")]
        public short ProvinceId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public City()
        {
            IsDeleted = false;
            //this.Province = new Province();
        }
        public City(City City)
        {
            ID = City.ID;
            ProvinceId = City.ProvinceId;
            Name = City.Name;
        }
        //public async Task<List<City>> SelectCityWithProvinceId(IGenericRepository<City> IgrCity)
        //{
        //	List<City> CitiesList = new List<City>();
        //	try
        //	{
        //		await Task.Run(() =>
        //		  {
        //			  CitiesList = IgrCity.GetAll(a => a.ProvinceId == this.ProvinceId && a.Name.Contains(this.Name)  , null, "Province").ToList();
        //		  });
        //		return CitiesList;
        //	}
        //	catch (SqlException/*Ex*/)
        //	{
        //		throw;
        //	}
        //}
    }
}
