using System;

namespace BookShop_MVC.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}