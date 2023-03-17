using ECommerceWebsite.Data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.ProductTypes).Include(f => f.TagNames).ToList());
        }

        //POST index action method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount,decimal? largeAmount)
        {
            var products=_db.Products.Include(c=>c.ProductTypes).Include(c=>c.TagNames)
                .Where(c=>c.Price>=lowAmount&& c.Price<=largeAmount).ToList(); 

            if(lowAmount==null&& largeAmount==null)
            {
                products=_db.Products.Include(c=>c.ProductTypes).Include(c=>c.TagNames).ToList();   
            }
            return View(products);
        }

        //Get create method
        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["specialTagId"] = new SelectList(_db.TagNames.ToList(), "Id", "TagName");
            return View();
        }

        //post create method
        [HttpPost]

        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct=_db.Products.FirstOrDefault(c=>c.Name== products.Name);
                if(searchProduct!=null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["specialTagId"] = new SelectList(_db.TagNames.ToList(), "Id", "TagName");
                    return View(products);
                }
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "~/Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "~/Images/No Image.jpg ";
                }


                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
                return View(products);
            
        }


        //Get Edit Action Method
        public IActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["specialTagId"] = new SelectList(_db.TagNames.ToList(), "Id", "TagName");
            if(id==null)
            {
                return NotFound();  
            }
            var product=_db.Products.Include(c=>c.ProductTypes)
                .Include(c=>c.TagNames).FirstOrDefault(c=>c.Id==id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //post edit Action Method
        [HttpPost]
        public async Task<IActionResult> Edit(Products products,IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "~/Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "~/Images/No Image.jpg ";
                }


                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(products);


        }

        //Get Details Action method
        public IActionResult Details(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var product=_db.Products.Include(c=>c.ProductTypes).Include(c=>c.TagNames).FirstOrDefault(c=> c.Id==id);

            if(product==null)
            {
                return NotFound();
            }
            return View(product);
        }

        //get delete Action Method

        public IActionResult Delete(int ?id)
        {
            if (id==null)
            {
                return NotFound();

            }
            var product=_db.Products.Include(c=>c.ProductTypes).Include(c=>c.TagNames)
                .Where(c=>c.Id==id).FirstOrDefault();  
            if(product==null)
            {
                return NotFound();
            }
            return View(product);
        }
        //post delete action method
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult>DeleteConfirm(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var product=_db.Products.FirstOrDefault(c=>c.Id==id);   
            if(product==null)
            {
                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
