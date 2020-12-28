using System.Threading.Tasks;
using BookShop_MVC.Models;

namespace BookShop_MVC.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepositoryAsync<Category>
    {
        Task Update(Category category);
    }
}