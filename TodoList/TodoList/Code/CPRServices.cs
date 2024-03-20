using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Model;

namespace TodoList.Code
{
    public class CPRServices
    {
        private readonly TodoContext _context;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CPRServices(TodoContext context, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateUserCPR(Cpr cprRecord)
        {
            var exists = await CheckEmailAsync(cprRecord.User);
            if (!exists)
            {
                cprRecord.Cpr1 = (string?)HashContent.SequentialHash(cprRecord.Cpr1, "string");
                _context.Cprs.Add(cprRecord);
                return await Save();
            }
            return false;
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            return await _context.Cprs.AnyAsync(c => c.User == email);
        }

        public async Task<bool> CheckAccountAsync(string email, string Cpr)
        {
            var hashedCpr = HashContent.SequentialHash(Cpr, "string");
            return await _context.Cprs.AnyAsync(c => c.Cpr1 == hashedCpr && c.User == email);
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<List<IdentityUserRole<string>>> GetUserRoleMapping()
        {
            return await _dbContext.UserRoles.ToListAsync();
        }


        public async Task<List<Cpr>> GetAllUserWithCpr()
        {
            return await _context.Cprs.ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
