using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SamsWarehouse.Models;
using SamsWarehouse.Models.Repositories;
using SamsWarehouse.Models.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SamsWarehouse.Controllers
{
    [Authorize]
    public class ShoppingListController : Controller
    {
        private readonly IShoppingListRepository _shoppingListRepository;
        private readonly IShoppingListItemRepository _shoppingListItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly WarehouseDBContext _context;

        public ShoppingListController(
            IShoppingListRepository shoppingListRepository,
            IShoppingListItemRepository shoppingListItemRepository,
            IProductRepository productRepository,
            WarehouseDBContext context)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListItemRepository = shoppingListItemRepository;
            _productRepository = productRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var shoppingLists = _shoppingListRepository.GetAllShoppingLists(userId.Value);
            return View(shoppingLists);
        }

        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            ViewBag.UserId = userId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ShoppingList shoppingList)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            shoppingList.UserId = userId.Value;

            if (ModelState.IsValid)
            {
                try
                {
                    var query = _context.ShoppingLists
                                        .Where(s => s.UserId == shoppingList.UserId)
                                        .ToQueryString();
                    Debug.WriteLine($"SQL Query: {query}");

                    _shoppingListRepository.AddShoppingList(shoppingList);
                    _shoppingListRepository.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error creating shopping list: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Debug.WriteLine("Inner exception: " + ex.InnerException.Message);
                    }
                }
            }
            else
            {
                Debug.WriteLine("ModelState is invalid. Errors:");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Debug.WriteLine($"{key}: {error.ErrorMessage}");
                    }
                }
            }

            return View(shoppingList);
        }

        public IActionResult Edit(int id)
        {
            var shoppingList = _shoppingListRepository.GetShoppingListById(id);
            if (shoppingList == null)
            {
                return NotFound();
            }

            var viewModel = new ShoppingListViewModel
            {
                ShoppingList = shoppingList,
                Products = _productRepository.GetAllProducts(),
                ShoppingListItem = new ShoppingListItem()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddItem(ShoppingListViewModel viewModel)
        {
            Console.WriteLine("AddItem method called.");
            Debug.WriteLine("AddItem method called.");

            Console.WriteLine($"Adding Item to ShoppingList ID: {viewModel.ShoppingList.Id}");
            Console.WriteLine($"Product ID: {viewModel.ShoppingListItem.ProductId}");
            Console.WriteLine($"Quantity: {viewModel.ShoppingListItem.Quantity}");
            Debug.WriteLine($"Adding Item to ShoppingList ID: {viewModel.ShoppingList.Id}");
            Debug.WriteLine($"Product ID: {viewModel.ShoppingListItem.ProductId}");
            Debug.WriteLine($"Quantity: {viewModel.ShoppingListItem.Quantity}");

            if (ModelState.IsValid)
            {
                var shoppingList = _shoppingListRepository.GetShoppingListById(viewModel.ShoppingList.Id);
                if (shoppingList != null)
                {
                    var product = _productRepository.GetProductById(viewModel.ShoppingListItem.ProductId);
                    viewModel.ShoppingListItem.ShoppingListId = shoppingList.Id;
                    viewModel.ShoppingListItem.TotalPrice = viewModel.ShoppingListItem.Quantity * product.UnitPrice;
                    _shoppingListItemRepository.AddShoppingListItem(viewModel.ShoppingListItem);
                    shoppingList.Total += viewModel.ShoppingListItem.TotalPrice;
                    _shoppingListRepository.Save();
                    _shoppingListItemRepository.Save();

                    Console.WriteLine("Item added successfully.");
                    Debug.WriteLine("Item added successfully.");
                }
                return RedirectToAction(nameof(Edit), new { id = viewModel.ShoppingList.Id });
            }

            Console.WriteLine("ModelState is invalid. Errors:");
            Debug.WriteLine("ModelState is invalid. Errors:");
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                foreach (var error in state.Errors)
                {
                    Console.WriteLine($"{key}: {error.ErrorMessage}");
                    Debug.WriteLine($"{key}: {error.ErrorMessage}");
                }
            }

            viewModel.ShoppingList = _shoppingListRepository.GetShoppingListById(viewModel.ShoppingList.Id);
            viewModel.Products = _productRepository.GetAllProducts();
            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveItem(int shoppingListId, int itemId)
        {
            var shoppingList = _shoppingListRepository.GetShoppingListById(shoppingListId);
            var item = _shoppingListItemRepository.GetShoppingListItemById(itemId);
            if (shoppingList != null && item != null)
            {
                shoppingList.Total -= item.TotalPrice;
                _shoppingListItemRepository.RemoveShoppingListItem(itemId);
                _shoppingListRepository.Save();
                _shoppingListItemRepository.Save();
            }
            return RedirectToAction(nameof(Edit), new { id = shoppingListId });
        }
    }
}
