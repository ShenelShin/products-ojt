using Microsoft.AspNetCore.Mvc;
using SimpApplication.Models;

namespace SimpApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context; 
        public ProductController(AppDbContext context)
        {
            _context = context; 
        }

        
        public IActionResult Index()
        {
            var products = _context.Products.ToList(); 
            return View(products);
        }
    }
}