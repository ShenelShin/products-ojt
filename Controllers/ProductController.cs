using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPortal.Models;
using ProductPortal.Models.Entities;
using ProductPortal.Web.Data;

namespace ProductPortal.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var product = new Product
            {
                // Do NOT set ProductID here! Let the database generate it.
                ProductName = viewModel.ProductName,
                ProductType = viewModel.ProductType,
                Price = viewModel.Price
            };

            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            ViewBag.Message = "Product added successfully!";
            return View(new ProductViewModel());



        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var products = await dbContext.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductType = product.ProductType,
                Price = product.Price
            };

            return View(viewModel);
        }



    }
    
}
