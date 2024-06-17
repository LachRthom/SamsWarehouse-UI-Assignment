using System.Collections.Generic;

namespace SamsWarehouse.Models.Repositories
{
    public interface IShoppingListItemRepository
    {
        ShoppingListItem GetShoppingListItemById(int id);
        void AddShoppingListItem(ShoppingListItem shoppingListItem);
        void RemoveShoppingListItem(int id);
        void Save();
    }
}
