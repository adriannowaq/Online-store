using OnlineStore.Models.Account.Admin;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(AddProductModel productDetails);
    }
}
