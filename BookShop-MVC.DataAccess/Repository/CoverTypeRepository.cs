using System;
using System.Collections.Generic;
using System.Linq;
using BookShop_MVC.DataAccess.Data;
using BookShop_MVC.DataAccess.Repository.IRepository;
using BookShop_MVC.Models;

namespace BookShop_MVC.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>,ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverType coverType)
        {
            var objFromDb = _db.CoverTypes.FirstOrDefault(s => s.Id == coverType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = coverType.Name;
            }
        }
    }
}