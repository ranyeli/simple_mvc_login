using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLogInSystem.Core.Interfaces;

namespace SimpleLogInSystem.Web.Models
{
    public class UserModel: IUserModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name ="Email Address:")]
        public string Email { get; set; }
        [Required]
        [StringLength(200, MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name ="Password:")]
        public string Password { get; set; }
    }
}
