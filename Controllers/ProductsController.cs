using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThetaECommerceApp.Models;

namespace ThetaECommerceApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly theta_ecommerce_dbContext _context;
        private readonly IWebHostEnvironment _he;

        public ProductsController(theta_ecommerce_dbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SellerId,Image,Quantity,CategoryId,Price,ShortDescription,LongDescription,DeliveryDays,DeliveryCharges,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData,SeoData")] Product product, IList<IFormFile> Img)
        {


            string Final = "";
            foreach (var pics in Img)
            {
                string FinalFilePathVirtual = "/data/product/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(pics.FileName);

                using (FileStream FS = new FileStream(_he.WebRootPath + FinalFilePathVirtual, FileMode.Create))
                {
                    pics.CopyTo(FS);
                }
                Final = Final + ',' + FinalFilePathVirtual;
            }

            if (ModelState.IsValid)
            {
                if (Final.StartsWith(','))
                {
                    Final = Final.Remove(0, 1);
                }
                product.Image = Final;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SellerId,Image,Quantity,CategoryId,Price,ShortDescription,LongDescription,DeliveryDays,DeliveryCharges,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData,SeoData")] Product product, IList<IFormFile> Img)
        {


            string Final = "";
            foreach (var pics in Img)
            {
                string FinalFilePathVirtual = "/data/product/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(pics.FileName);

                using (FileStream FS = new FileStream(_he.WebRootPath + FinalFilePathVirtual, FileMode.Create))
                {
                    pics.CopyTo(FS);
                }
                Final = Final + ',' + FinalFilePathVirtual;
            }
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.Image = Final;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'theta_ecommerce_dbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                var File = product.Image;
                var spltimg = product.Image.Split(',');
                foreach (var img in spltimg)
                {
                    string CatPath = _he.WebRootPath + img;
                    FileInfo file = new FileInfo(CatPath);

                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }




        public string GetRC(int id, int cid)
        {
            //System.Threading.Thread.Sleep(6000);
            return "<h1 class='alert alert-danger'>Hello I am coming form GetRI Action in Products Controller.</h1>";
        }




        public void Count(int id)
        {
            //System.Threading.Thread.Sleep(6000);
            var cnt = _context.Products.Find(id);
            cnt.NoOfView = cnt.NoOfView + 1;
            _context.Products.Update(cnt);
            _context.SaveChangesAsync();
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
