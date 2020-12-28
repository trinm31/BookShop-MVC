using System.Linq;
using System.Threading.Tasks;
using BookShop_MVC.DataAccess.Data;
using BookShop_MVC.DataAccess.Repository.IRepository;
using BookShop_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop_MVC.DataAccess.Repository
{
    public class CategoryRepository : RepositoryAsync<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task Update(Category category)
        {
            var objFromDb =await _db.Categories.FirstOrDefaultAsync(s => s.Id == category.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
            }
        }
    }
}