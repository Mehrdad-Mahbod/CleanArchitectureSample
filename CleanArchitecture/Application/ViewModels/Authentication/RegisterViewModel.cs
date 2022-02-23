using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.ViewModels
{
   public class RegisterViewModel
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Family { get; set; }

        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(250)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(250)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
    }

    public enum CheckUser
    {
        UserNameAndEmailNotValid,
        EmailNotValid,
        UserNameNotValid,
        OK
    }
}
