using ECommerceWebsite.Data;
using ECommerceWebsite.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebsite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagNamesController : Controller
    {
      
           private ApplicationDbContext _db;

            public TagNamesController(ApplicationDbContext db)
            {
                _db = db;
            }
            public IActionResult Index()
            {

            return View(_db.TagNames.ToList());
            }
        //create get action method

        public IActionResult Create()
        {
            return View();
        }
        //create post Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagNames tagNames)
        {
            if (ModelState.IsValid)
            {
                _db.TagNames.Add(tagNames);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(tagNames);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tagNames = _db.TagNames.Find(id);
            if (tagNames == null)
            {
                return NotFound();
            }
            return View(tagNames);
        }
        //Edit post Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TagNames tagNames)
        {
            if (ModelState.IsValid)
            {
                _db.Update(tagNames);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(tagNames);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tagNames = _db.TagNames.Find(id);
            if (tagNames == null)
            {
                return NotFound();
            }
            return View(tagNames);
        }
        //Details post Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(TagNames tagNames)
        {

            return RedirectToAction(actionName: nameof(Index));


        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tagNames = _db.TagNames.Find(id);
            if (tagNames == null)
            {
                return NotFound();
            }
            return View(tagNames);
        }
        //Delete post Action Method

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var tagNames = _db.TagNames.Find(id);
            if (tagNames == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.Remove(tagNames);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(tagNames);
        }

    }
}

