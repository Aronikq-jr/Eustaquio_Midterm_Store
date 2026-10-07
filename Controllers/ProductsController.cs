using Microsoft.AspNetCore.Mvc;

using Eustaquio_Midterm_Store.Data;

using Eustaquio_Midterm_Store.Models;

 

namespace Eustaquio_Midterm_Store.Controllers

{

    public class ProductsController : Controller

    {

        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db) { _db = db; }

 

        // shows the list

        public IActionResult Index(string searchString)
        {
            var products = _db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
            products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewData["searchString"] = searchString;
            return View(products.ToList());
        }

 

        // shows the empty add-form

        public IActionResult Create()

        {

            return View();

        }

        // saves a new product
        [HttpPost]
        public IActionResult Create(Product product)

        {

            _db.Products.Add(product);

            _db.SaveChanges();

            return RedirectToAction("Index");

        }
                public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }

        // EDIT - save the changes
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE - remove the product
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult AddCart(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index");

            var item = _db.CartItems.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                _db.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            _db.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }

    }

}