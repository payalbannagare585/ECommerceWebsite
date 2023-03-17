using ECommerceWebsite.Data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductTypesController : Controller
	{
		private ApplicationDbContext _db;

		public ProductTypesController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			//var data=db.ProductTypes.ToList();
			 return View(_db.ProductTypes.ToList());
		}

		//create get action method
 
		public IActionResult Create()
		{
			return View();
		}
		//create post Action Method

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductTypes productTypes)
		{
			if (ModelState.IsValid)
			{
				_db.ProductTypes.Add(productTypes);
				await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved...";
				return RedirectToAction(actionName:nameof(Index));
			}
			return View(productTypes);
		}


        public IActionResult Edit(int? id)
        {
			if(id == null)
			{
				return NotFound();	
			}
			var productType=_db.ProductTypes.Find(id);
			if(productType == null)
			{
				return NotFound();
			}
            return View(productType);
        }
        //Edit post Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
               
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(productTypes);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        //Details post Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            
                return RedirectToAction(actionName: nameof(Index));
            
           
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
        //Delete post Action Method

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            
            var productType= _db.ProductTypes.Find(id);
            if(productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(productType);
        }

    }
}

