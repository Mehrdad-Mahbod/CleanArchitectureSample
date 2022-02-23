using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data.Repository;
using System.Reflection;

namespace Infrastructure.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {

            //.بایستی در این قسمت معرفی کرد AutoMapper برای استفاده از 
            service.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //Application Layer
            service.AddScoped<IUserService, UserService>();
            //Infra Data Layer
            service.AddScoped<IUserRepository, UserRepository>();

            //Application Layer
            service.AddScoped<IMenuService, MenuService>();
            //Infra Data Layer
            service.AddScoped<IMenuRepository, MenuRepository>();


        }
    }
}
