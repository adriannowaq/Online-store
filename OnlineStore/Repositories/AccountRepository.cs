using OnlineStore.Data;
using System.Threading.Tasks;
using System.Linq;
using OnlineStore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace OnlineStore.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext dbContext;
        private readonly Sha256Helper sha256Helper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountRepository(AppDbContext dbContext,
                                 Sha256Helper sha256Helper,
                                 IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.sha256Helper = sha256Helper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            var hashedPassword = sha256Helper.Hash(password);
            var account = await dbContext.Users
                .Where(u => u.Email.Equals(email) && u.Password.Equals(hashedPassword)).FirstOrDefaultAsync();

            if (account == null)
                return false;

            var accountClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.ToString())
            };
            var accountClaimsIdentity = new ClaimsIdentity(accountClaims);
            var principal = new ClaimsPrincipal(accountClaimsIdentity);

            await httpContextAccessor.HttpContext.SignInAsync(principal);

            return true;
        }

        public Task<bool> CheckEmailExistsAsync(string email)
        {
            return dbContext.Users.Where(u => u.Email.Equals(email)).AnyAsync();
        }

        public async Task<bool> CreateAccountAsync(string email, string password, UserRole role = UserRole.None)
        {
            password = sha256Helper.Hash(password);

            if (!await CheckEmailExistsAsync(email))
            {
                await dbContext.Users.AddAsync(new User
                {
                    Email = email,
                    Password = password,
                    Role = role
                });
                await dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
