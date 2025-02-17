﻿namespace SamsWarehouse.Models
{
    public class User
    {
        public int UserId { get; set; } 
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<ShoppingList> ShoppingLists { get; set; }
    }
}
