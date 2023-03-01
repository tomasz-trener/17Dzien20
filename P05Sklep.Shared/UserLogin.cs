using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P05Sklep.Shared
{
    public class UserLogin
    {
        [Required]
        [EmailAddress(ErrorMessage ="Zly adres email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20,ErrorMessage ="Niepoprawna długość")]
       // [CustomValidation()]
        public string Password { get; set; } = string.Empty;
    }
}
