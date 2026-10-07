using Microsoft.AspNetCore.Mvc;

using Eustaquio_Midterm_Store.Data;

using Eustaquio_Midterm_Store.Models;



namespace Eustaquio_Midterm_Store.Controllers

{

    public class CartController : Controller

    {

        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db) { _db = db; }

        // READ: shows all cart items 
        public IActionResult Index()
        {
            return View(_db.CartItems.ToList());
        }

        // UPDATE: changes an item's quantity
        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var product = _db.CartItems.Find(id);
            if (product != null)
            {
                product.Quantity = Math.Max(1, quantity);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // DELETE: remove the product from cart
        [HttpPost]
        public IActionResult Remove(int id)
        {
            var product = _db.CartItems.Find(id);
            if (product != null)
            {
                _db.CartItems.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}