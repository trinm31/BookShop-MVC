using System.Collections.Generic;

namespace BookShop_MVC.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCarts { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}