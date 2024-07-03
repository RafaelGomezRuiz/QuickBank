using Microsoft.AspNetCore.Identity;
using QuickBank.Core.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Infraestructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SUPERADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.BASIC.ToString()));
        }
    }
}
