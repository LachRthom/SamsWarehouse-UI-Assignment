namespace SamsWarehouse.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual List<ShoppingListItem> ListItems { get; set; }
    }
}
