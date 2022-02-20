using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class User:BaseEntity
    {

        public byte Gender { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }

        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }


        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
        
        
        [Required]
        [MaxLength(250)]
        public string Password { get; set; }

        public int CityId { get; set; }
        public int? ParentId { get; set; }
        public byte[] Image { get; set; }
        [NotMapped]
        public string ImgBase64 { get; set; }
        [NotMapped]
        public bool RememberMe { get; set; }
        [ForeignKey("UserId")/*, Required*/]
        public virtual ICollection<UserMenu> UserMenus { get; set; }

        //[ForeignKey("UserId"), Required]
        public virtual ICollection<UserRole> UserRoles { get; set; }



    }
}
