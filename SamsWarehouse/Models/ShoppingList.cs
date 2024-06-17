using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamsWarehouse.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public DateTime FinalisedDate { get; set; } = DateTime.Now;

        [DataType(DataType.Currency)]
        public decimal Total { get; set; } = 0;

        public virtual List<ShoppingListItem> ListItems { get; set; } = new List<ShoppingListItem>();
    }
}
