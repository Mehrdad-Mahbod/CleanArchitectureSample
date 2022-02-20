using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Domain.Models
{

    public class Role : BaseEntity
    {
        [ForeignKey("RoleId"), Required]
        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }



    }
}
