using OnlineStore.Data;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CreateAccountAsync(string email, string password, UserRole role = UserRole.None);
    }
}
