using Microsoft.AspNetCore.Identity;
using QuickBank.Core.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Infraestructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? IdCard { get; set; }
        public int Status { get; set; }

    }
}
