using Microsoft.AspNetCore.Identity;
using QuickBank.Core.Application.Enums;

namespace QuickBank.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(ERoles.SUPERADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERoles.ADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERoles.BASIC.ToString()));
        }
    }
}
