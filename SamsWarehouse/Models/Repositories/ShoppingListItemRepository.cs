using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SamsWarehouse.Models.Repositories
{
    public class ShoppingListItemRepository : IShoppingListItemRepository
    {
        private readonly WarehouseDBContext _context;

        public ShoppingListItemRepository(WarehouseDBContext context)
        {
            _context = context;
        }

        public ShoppingListItem GetShoppingListItemById(int id)
        {
            return _context.ShoppingListItems
                           .FirstOrDefault(li => li.Id == id);
        }

        public void AddShoppingListItem(ShoppingListItem shoppingListItem)
        {
            _context.ShoppingListItems.Add(shoppingListItem);
        }

        public void RemoveShoppingListItem(int id)
        {
            var shoppingListItem = GetShoppingListItemById(id);
            if (shoppingListItem != null)
            {
                _context.ShoppingListItems.Remove(shoppingListItem);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
