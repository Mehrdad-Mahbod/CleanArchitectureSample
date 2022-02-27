using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGeneralOfficeService
    {
        public Task<GeneralOfficeViewModel> AddAsync(GeneralOfficeViewModel GeneralOffice);
        public Task<GeneralOfficeViewModel> EditAsync(GeneralOfficeViewModel GeneralOffice);
        public Task<List<GeneralOfficeViewModel>> GetListAsync(GeneralOfficeViewModel GeneralOffice);
        public Task<GeneralOfficeViewModel> GetAsync(GeneralOfficeViewModel GeneralOffice);
        public Task<int> RemoveAsync(GeneralOfficeViewModel GeneralOffice);

    }
}
