using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IGeneralOfficeRepository
    {
        public Task<GeneralOffice> InsertAsync(GeneralOffice GeneralOffice);
        public Task<GeneralOffice> UpdateAsync(GeneralOffice GeneralOffice);
        public Task<List<GeneralOffice>> SelectList(GeneralOffice GeneralOffice);
        public Task<GeneralOffice> Select(GeneralOffice GeneralOffice);
        public Task<int> Delete(GeneralOffice GeneralOffice);
    }
}
