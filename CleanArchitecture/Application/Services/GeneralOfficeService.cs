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
            await Task.Run(() =>
            {
                try
                {
                    GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);
                    this.IGeneralOfficeRepository.Insert(GeneralOffice);
                    //this.IGeneralOfficeRepository.SaveAsync();
                    GeneralOfficeViewModel = this.IMapper.Map<GeneralOfficeViewModel>(GeneralOffice);
                    TCS.SetResult(GeneralOfficeViewModel);
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                }
            });
            return TCS.Task.Result;
        }

        public async Task<GeneralOfficeViewModel> EditAsync(GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            TaskCompletionSource<GeneralOfficeViewModel> TCS = new TaskCompletionSource<GeneralOfficeViewModel>();
            await Task.Run(() =>
            {
                try
                {
                    GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);
                    this.IGeneralOfficeRepository.Update(GeneralOffice);
                    GeneralOfficeViewModel = this.IMapper.Map<GeneralOfficeViewModel>(GeneralOffice);
                    TCS.SetResult(GeneralOfficeViewModel);
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                }
            });
            return TCS.Task.Result;

        }

        public Task<GeneralOfficeViewModel> GetAsync(GeneralOfficeViewModel GeneralOffice)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GeneralOfficeViewModel>> GetListAsync(GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);

            List<GeneralOffice> GeneralOfficeList =await this.IGeneralOfficeRepository.SelectList(GeneralOffice);

            return this.IMapper.Map<List<GeneralOfficeViewModel>>(GeneralOfficeList);



        }

        public async Task<int> RemoveAsync(GeneralOfficeViewModel GeneralOfficeViewModel)
        {
            TaskCompletionSource<int> TCS = new TaskCompletionSource<int>();
            await Task.Run(() =>
            {
                try
                {
                    GeneralOffice GeneralOffice = this.IMapper.Map<GeneralOffice>(GeneralOfficeViewModel);
                    int Row = this.IGeneralOfficeRepository.Delete(GeneralOffice).Result;
                    //GeneralOfficeViewModel = this.IMapper.Map<GeneralOfficeViewModel>(GeneralOffice);
                    TCS.SetResult(Row);
                }
                catch (Exception Ex)
                {
                    TCS.SetException(Ex);
                }
            });
            return TCS.Task.Result;
        }
    }
}
