using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SamsWarehouse.Models.Repositories
{
    public class ShoppingListRepository : IShoppingListRepository
    {
        private readonly WarehouseDBContext _context;

        public ShoppingListRepository(WarehouseDBContext context)
        {
            _context = context;
        }

        public IEnumerable<ShoppingList> GetAllShoppingLists(int userId)
        {
            return _context.ShoppingLists
                           .Where(s => s.UserId == userId)
                           .Include(s => s.ListItems) 
                           .ToList();
        }

        public ShoppingList GetShoppingListById(int id)
        {
            return _context.ShoppingLists
                           .Include(s => s.ListItems) 
                           .FirstOrDefault(s => s.Id == id);
        }

        public void AddShoppingList(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Add(shoppingList);
        }

        public void UpdateShoppingList(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Update(shoppingList);
        }

        public void DeleteShoppingList(int id)
        {
            var shoppingList = GetShoppingListById(id);
            if (shoppingList != null)
            {
                _context.ShoppingLists.Remove(shoppingList);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
