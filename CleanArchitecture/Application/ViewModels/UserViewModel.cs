using Domain.Models;
using System.Collections.Generic;


namespace Application.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public byte Gender { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Password { get; set; }
        public int CityId { get; set; }
        public int? ParentId { get; set; }
        public byte[] Image { get; set; }
        public string ImgBase64 { get; set; }
        public bool LockoutEnabled { get; set; }
        public string SecurityStamp { get; set; }
        public bool RememberMe { get; set; }
        public virtual ICollection<UserMenu> UserMenus { get; set; }
        //[ForeignKey("UserId"), Required]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
