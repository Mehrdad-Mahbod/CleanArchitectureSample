using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
   public class LoginViewModel
    {
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(11)]
        [EmailAddress]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(250)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool  RememberMe { get; set; }
    }
}
