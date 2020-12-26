using BookShop_MVC.DataAccess.Data;
using BookShop_MVC.DataAccess.Repository.IRepository;
using BookShop_MVC.Models;

namespace BookShop_MVC.DataAccess.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public void Dispose()
        {
           _db.Dispose();
        }

        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }
        public ISP_Call SP_Call { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; }
        public IOrderHeaderRepository OrderHeader { get; }
        public IShoppingCartRepository ShoppingCart { get; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}