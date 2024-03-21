using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Model;

namespace TodoList.Code
{
    public class RoleHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleHandler(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task CreateRole(string roleName)
        {
            if (!await RoleExists(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        public async Task<bool> AddRoleToUser(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> RemoveRoleFromUser(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                return result.Succeeded;
            }
            return false;
        }

        public async Task<bool> addUserToDefaultRole(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Count == 0)
            {
                IdentityResult addRoleResult;

                bool result = await IsRolesNull();
                if (result)
                    addRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
                else
                    addRoleResult = await _userManager.AddToRoleAsync(user, "User");

                return addRoleResult.Succeeded;
            }
            return true;
        }

        public async Task<bool> IsRolesNull()
        {
            var result = await _roleManager.Roles.ToListAsync();
            
            if (result == null || result.Count() <= 0)
            {
                await CreateRole("Admin");
                await CreateRole("User");
                return true;
            }
            return false;
        }
    }
}
