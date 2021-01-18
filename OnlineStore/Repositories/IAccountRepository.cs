using OnlineStore.Data;
using OnlineStore.Models.Account;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> SignInAsync(string email, string password);
        Task SignOutAsync();
        Task<bool> CheckEmailExistsAsync(string email);
        Task<bool> CreateAccountAsync(string email, string password, UserRole role = UserRole.None);
        Task<Address> FindDeliveryAddressAsync(int userId);
        Task<List<Address>> FindUserAddressesAsync(int userId);
        Task AddOrUpdateAddressAsync(int userId, Address address, AddressType addressType);
    }
}
