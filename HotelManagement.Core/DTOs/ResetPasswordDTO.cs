using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage ="UserName is Required")]
        public string Username { get; set; }       

        [Required(ErrorMessage = "New password is Required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Comfirm Password is Required")]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        public string ConfirmNewPassword { get; set; }

        public string Token { get; set; }
    }
}
