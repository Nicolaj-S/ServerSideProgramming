using Microsoft.AspNetCore.Identity;
using TodoList.Data;

namespace TodoList.Code
{
    public class RoleHandler
    {
        public async Task createRole(string user, string role,IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();

            var userRoleCheck = await roleManager.RoleExistsAsync(role);
            if (!userRoleCheck)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            ApplicationUser identityUser = await userManager.FindByEmailAsync(user);
            await userManager.AddToRoleAsync(identityUser,role);

        }
    }
}
