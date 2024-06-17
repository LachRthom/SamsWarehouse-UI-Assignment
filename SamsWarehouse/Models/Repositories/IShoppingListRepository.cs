using System.Collections.Generic;

namespace SamsWarehouse.Models.Repositories
{
    public interface IShoppingListRepository
    {
        IEnumerable<ShoppingList> GetAllShoppingLists(int userId);
        ShoppingList GetShoppingListById(int id);
        void AddShoppingList(ShoppingList shoppingList);
        void UpdateShoppingList(ShoppingList shoppingList);
        void DeleteShoppingList(int id);
        void Save();
    }
}
