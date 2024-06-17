using System.Collections.Generic;

namespace SamsWarehouse.Models.ViewModels
{
    public class ShoppingListViewModel
    {
        public ShoppingList ShoppingList { get; set; }
        public ShoppingListItem ShoppingListItem { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
