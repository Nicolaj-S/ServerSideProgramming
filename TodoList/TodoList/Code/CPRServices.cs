using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Data;
using TodoList.Model;

namespace TodoList.Code
{
    public class CPRServices
    {
        private readonly TodoContext _context;

        public CPRServices(TodoContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserCPR(Cpr cprRecord)
        {
            var exists = await CheckEmailAsync(cprRecord.User);
            if (!exists)
            {
                cprRecord.Cpr1 = HashContent.hash(cprRecord.Cpr1);
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
            var hashedCpr = HashContent.hash(Cpr);
            return await _context.Cprs.AnyAsync(c => c.Cpr1 == hashedCpr && c.User == email);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
