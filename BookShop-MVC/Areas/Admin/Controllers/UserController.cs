using System;
using System.Linq;
using System.Threading.Tasks;
using BookShop_MVC.DataAccess.Data;
using BookShop_MVC.DataAccess.Repository.IRepository;
using BookShop_MVC.Models;
using BookShop_MVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public UserController(ApplicationDbContext db, IUnitOfWork unitOfWork,UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }
        
        #region Api Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //var userList = _db.ApplicationUsers.Include(u => u.company).ToList();
            var userList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "company");
            
            //var roles = _roleManager.Roles.ToList();
            //var userRole = _db.UserRoles.ToList();
            //var roles = _db.Roles.ToList();
            
            foreach (var user in userList)
            {
                 // var roleId = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                 // user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                var usertemp = await _userManager.FindByIdAsync(user.Id);
                var roleTemp = await _userManager.GetRolesAsync(usertemp);
                user.Role = roleTemp.FirstOrDefault();
                if (user.company == null)
                {
                    user.company = new Company()
                    {
                        Name = ""
                    };
                    
                }
            }
            return Json(new {data = userList});
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var objFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            if(objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, we will unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful." });
        }
        #endregion

    }
}