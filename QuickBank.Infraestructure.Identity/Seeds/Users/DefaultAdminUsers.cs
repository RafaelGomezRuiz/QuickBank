using Microsoft.AspNetCore.Identity;
using QuickBank.Core.Application.Enums;
using QuickBank.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBank.Infrastructure.Persistence.Seeds.Users
{
    public class DefaultAdminUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new()
            {
                Id = "g5k2l-vxztp-yub64-qm7fr-1298z",
                UserName = "admin",
                Email = "admin@email.com",
                FirstName = "admin",
                LastName = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                // Si el usuario no existe, créalo
                await userManager.CreateAsync(defaultUser, "123Pa$$Word!");
                await userManager.AddToRoleAsync(defaultUser, Roles.ADMIN.ToString());
            }
        }
    }
}
