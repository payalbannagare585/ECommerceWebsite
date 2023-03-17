using ECommerceWebsite.Data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ECommerceWebsite.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.ProductTypes)
                .Include(c=>c.TagNames).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Get product details action method

        public IActionResult Detail(int id)
        {
            if(id==null)
            {
                return NotFound();

            }
            var product=_db.Products.Include(c=>c.ProductTypes).FirstOrDefault(c=>c.Id==id);
            if(product==null)
            {
                return NotFound();
            }
            return View(product);
        } 
    }
}