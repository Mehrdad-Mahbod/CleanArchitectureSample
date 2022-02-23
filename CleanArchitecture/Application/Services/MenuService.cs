using Application.Interfaces;
using Application.ViewModels;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MenuService : IMenuService
    {

        private IMenuRepository IMenuRepository;
        private IMapper IMapper;

        public MenuService(IMenuRepository IMenuRepository, IMapper IMapper)
        {
            this.IMenuRepository = IMenuRepository;
            this.IMapper = IMapper;

        }

        public async Task<List<MenuViewModel>> GetAllUserMenusWithUserIdAndRoleId(UserRoleViewModel UserRoleViewModel)
        {


            UserRole UserRole = IMapper.Map<UserRole>(UserRoleViewModel);

            List<Menu> MenuList = await this.IMenuRepository.SelectAllUserMenusWithUserIdAndRoleId(UserRole);

            List<MenuViewModel> MenuViewModelList = IMapper.Map<List<MenuViewModel>>(MenuList);

            return MenuViewModelList;


        }
    }
}
