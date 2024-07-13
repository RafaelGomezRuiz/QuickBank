using Microsoft.AspNetCore.Identity;
using QuickBank.Core.Application.Enums;
using QuickBank.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuickBank.Infrastructure.Persistence.Seeds.Users
{
    public class DefaultBasicUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new()
            {
                Id = "f294u-ewrdm-woj93-hj3dn-8937w",
                UserName = "user",
                Email = "user@email.com",
                FirstName = "user",
                LastName = "user",
                IdCard = "402-402-4002",
                Status = (int)EUserStatus.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            ApplicationUser defaultUser2 = new()
            {
                Id = "n8d7x-qsj3p-lf9b2-hu6zr-4579y",
                UserName = "user2",
                Email = "user2@email.com",
                FirstName = "user2",
                LastName = "user2",
                Status = (int)EUserStatus.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);

            if (user == null)
            {
                // Si el usuario no existe, créalo
                await userManager.CreateAsync(defaultUser, "123Pa$$Word!");
                await userManager.AddToRoleAsync(defaultUser, ERoles.BASIC.ToString());
            }

            var user2 = await userManager.FindByEmailAsync(defaultUser2.Email);

            if (user2 == null)
            {
                // Si el usuario no existe, créalo
                await userManager.CreateAsync(defaultUser2, "123Pa$$Word!");
                await userManager.AddToRoleAsync(defaultUser2, ERoles.BASIC.ToString());
            }
        }
    }
}
