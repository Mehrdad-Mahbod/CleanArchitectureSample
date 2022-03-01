using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces.GenericRepository;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IGeneralOfficeRepository: IGenericRepository<GeneralOffice>
    {
        public Task<GeneralOffice> InsertAsync(GeneralOffice GeneralOffice);
        public Task<GeneralOffice> UpdateAsync(GeneralOffice GeneralOffice);
        public Task<List<GeneralOffice>> SelectListAsync(GeneralOffice GeneralOffice);
        public Task<GeneralOffice> SelectAsync(GeneralOffice GeneralOffice);
        public Task<int> DeleteAsync(GeneralOffice GeneralOffice);
    }
}
