using BookShop_MVC.Models;

namespace BookShop_MVC.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository: IRepository<CoverType>
    {
        void Update(CoverType coverType);
    }
}