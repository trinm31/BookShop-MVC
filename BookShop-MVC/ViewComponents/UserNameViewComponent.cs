using System.Security.Claims;
using System.Threading.Tasks;
using BookShop_MVC.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookShop_MVC.ViewComponents
{
    public class UserNameViewComponent: ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserNameViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claims.Value);
            return View(userFromDb);
        }
    }
}