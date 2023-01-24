using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThetaECommerceApp.Models;

namespace ThetaECommerceApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly theta_ecommerce_dbContext _context;
        private readonly IWebHostEnvironment _he;

        public CategoriesController(theta_ecommerce_dbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int cid)
        {
            if (cid != null)
            {
                return View(await _context.Categories.Where(u => u.Id == cid).ToListAsync());
            }
            else
            {
                return View(await _context.Categories.ToListAsync());
            }
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Image,Description,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,SeoData,MetaData")] Category category, IFormFile Img)
        {
            string FinalFilePathVirtual = "/data/category/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);

            using (FileStream FS = new FileStream(_he.WebRootPath + FinalFilePathVirtual, FileMode.Create))
            {
                Img.CopyTo(FS);
            }



            if (ModelState.IsValid)
            {
                category.Image = FinalFilePathVirtual;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,Description,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,SeoData,MetaData")] Category category, IFormFile Img)
        {
            string FinalPath = "/data/category/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);
            using (FileStream FS = new FileStream(_he.WebRootPath + FinalPath, FileMode.Create))
            {
                Img.CopyTo(FS);
            }






            //var cat = await _context.Categories.FindAsync(id);
            //if (cat != null)
            //{
            //    var File = category.Image;
            //    string CatPath = _he.WebRootPath + File;
            //    FileInfo file = new FileInfo(CatPath);

            //    if (file.Exists)
            //    {
            //        file.Delete();
            //    }
            //}








            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.Image = FinalPath;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'theta_ecommerce_dbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                var File = category.Image;
                string CatPath = _he.WebRootPath + File;
                FileInfo file = new FileInfo(CatPath);

                if (file.Exists)
                {
                    file.Delete();
                }
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }




        [HttpPost]
        public bool deleteproduct(int id)
        {
            try
            {
                var obj = _context.Products.Find(id);

                _context.Products.Remove(obj);
                _context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }










    }
}
