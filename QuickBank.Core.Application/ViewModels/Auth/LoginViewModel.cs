using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Core.Application.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe colocar su email")]
        [DataType(DataType.Text)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Debe colocar una contrasenia")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
