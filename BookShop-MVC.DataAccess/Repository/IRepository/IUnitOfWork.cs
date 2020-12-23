using System;

namespace BookShop_MVC.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ISP_Call SP_Call { get; }
        ICompanyRepository Company { get; }
        IApplicationUserRepository ApplcationUser { get; }
        void Save();
    }
}