using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GeneralOfficeService : IGeneralOfficeService
    {

        private IGeneralOfficeRepository IGeneralOfficeRepository;
        private IMapper IMapper;


        public GeneralOfficeService(IGeneralOfficeRepository IGeneralOfficeRepository, IMapper IMapper)
        {
            this.IGeneralOfficeRepository = IGeneralOfficeRepository;
            this.IMapper = IMapper;
        }

        public async Task<GeneralOfficeViewModel> AddAsync(GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            TaskCompletionSource<GeneralOfficeViewModel> TCS = new TaskCompletionSource<GeneralOfficeViewModel>();
            try
            {
                GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);
                GeneralOffice = await this.IGeneralOfficeRepository.InsertAsync(GeneralOffice);
                //this.IGeneralOfficeRepository.SaveAsync();
                GeneralOfficeViewModel = this.IMapper.Map<GeneralOfficeViewModel>(GeneralOffice);
                TCS.SetResult(GeneralOfficeViewModel);
            }
            catch (Exception Ex)
            {
                TCS.SetException(Ex);
            }
            return TCS.Task.Result;
        }

        public async Task<GeneralOfficeViewModel> EditAsync(GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            TaskCompletionSource<GeneralOfficeViewModel> TCS = new TaskCompletionSource<GeneralOfficeViewModel>();
            try
            {
                GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);
                GeneralOffice = await this.IGeneralOfficeRepository.UpdateAsync(GeneralOffice);
                GeneralOfficeViewModel = this.IMapper.Map<GeneralOfficeViewModel>(GeneralOffice);
                TCS.SetResult(GeneralOfficeViewModel);
            }
            catch (Exception Ex)
            {
                TCS.SetException(Ex);
            }
            return TCS.Task.Result;

        }

        public Task<GeneralOfficeViewModel> GetAsync(GeneralOfficeViewModel GeneralOffice)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GeneralOfficeViewModel>> GetListAsync(GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            TaskCompletionSource<List<GeneralOfficeViewModel>> TCS = new TaskCompletionSource<List<GeneralOfficeViewModel>>();
            try
            {
                GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);
                List<GeneralOffice> GeneralOfficeList = await this.IGeneralOfficeRepository.SelectListAsync(GeneralOffice);
                TCS.SetResult(this.IMapper.Map<List<GeneralOfficeViewModel>>(GeneralOfficeList));
            }
            catch (Exception Ex)
            {
                TCS.SetException(Ex);
            }
            return TCS.Task.Result;
        }

        public async Task<int> RemoveAsync(GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            TaskCompletionSource<int> TCS = new TaskCompletionSource<int>();
            try
            {
                GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);
                int Row = await this.IGeneralOfficeRepository.DeleteAsync(GeneralOffice);
                //GeneralOfficeViewModel = this.IMapper.Map<GeneralOfficeViewModel>(GeneralOffice);
                TCS.SetResult(Row);
            }
            catch (Exception Ex)
            {
                TCS.SetException(Ex);
            }
            return TCS.Task.Result;
        }
    }
}
