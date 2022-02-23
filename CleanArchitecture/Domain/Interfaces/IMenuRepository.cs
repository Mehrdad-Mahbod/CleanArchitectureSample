using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IMenuRepository
    {
        public Task<List<Menu>> SelectAllUserMenusWithUserIdAndRoleId(UserRole UserRole);
    }
}
