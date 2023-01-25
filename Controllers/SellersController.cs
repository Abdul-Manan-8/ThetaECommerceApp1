using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThetaECommerceApp.Models;

namespace ThetaECommerceApp.Controllers
{
    public class SellersController : Controller
    {
        private readonly theta_ecommerce_dbContext _context;
        private readonly IWebHostEnvironment _he;

        public SellersController(theta_ecommerce_dbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Sellers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sellers.ToListAsync());
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string UserName, string Password, [Bind("Id,Name,Image,CompanyName,WebsiteUrl,Cnic,City,ShortDescription,LongDescription,Email,Gender,PhoneNumber,Address,Dob,SystemUserId,Type,MartialStatus,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData,SeoData")] Seller seller, IFormFile Img)
        {
            string FinalFilePathVirtual = "/data/seller/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);

            using (FileStream FS = new FileStream(_he.WebRootPath + FinalFilePathVirtual, FileMode.Create))
            {
                Img.CopyTo(FS);
            }

            if (ModelState.IsValid)
            {

                SystemUser U = new SystemUser();
                U.UserName = UserName;
                U.Password = Password;
                U.Type = 3;
                _context.SystemUsers.Add(U);
                await _context.SaveChangesAsync();
                var GetU = _context.SystemUsers.Where(u => u.UserName == UserName).FirstOrDefault();

                seller.Image = FinalFilePathVirtual;
                seller.SystemUserId = GetU.Id;
                _context.Add(seller);
                await _context.SaveChangesAsync();


                //Welcome email
                if (!string.IsNullOrEmpty(seller.Email))
                {
                    MailMessage MailObj = new MailMessage();

                    MailObj.Subject = "Welcome <b>" + seller.Name + "</b> to our ECommerce webapplication";
                    MailObj.Body = "Welcome " + seller.Name + ",<br><br>" +
                        "Thank you for signing up on our system. feel free to contact us if you need any help.<br><br>" +
                        "Regards,<br> <span style='color:green;'>ECommerse Team</span>";
                    MailObj.To.Add(seller.Email);
                    MailObj.CC.Add("abdul124manan@gmai.com");
                    MailObj.Bcc.Add("natiqbutt2018@gmail.com");

                    MailObj.From = new MailAddress("students@thetademos.com", "ECommerse Team");

                    MailObj.Attachments.Add(new Attachment(_he.WebRootPath + FinalFilePathVirtual));

                    SmtpClient SmtpObj = new SmtpClient();
                    SmtpObj.Port = 465;
                    SmtpObj.Host = "mail.thetademos.com";
                    SmtpObj.Credentials = new NetworkCredential("students@thetademos.com", "P@kist@n@@123");
                    SmtpObj.EnableSsl = true;

                    SmtpObj.Send(MailObj);

                    //For exception handling
                    //try
                    //{
                    //    SmtpObj.Send(MailObj);
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                }
            }
            return RedirectToAction("RegisterSuccess", "Home");
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,CompanyName,WebsiteUrl,Cnic,City,ShortDescription,LongDescription,Email,Gender,PhoneNumber,Address,Dob,SystemUserId,Type,MartialStatus,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData,SeoData")] Seller seller, IFormFile Img)
        {
            string FinalFilePathVirtual = "/data/seller/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);

            using (FileStream FS = new FileStream(_he.WebRootPath + FinalFilePathVirtual, FileMode.Create))
            {
                Img.CopyTo(FS);
            }

            if (id != seller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    seller.Image = FinalFilePathVirtual;
                    _context.Update(seller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellerExists(seller.Id))
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
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sellers == null)
            {
                return NotFound();
            }

            var seller = await _context.Sellers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sellers == null)
            {
                return Problem("Entity set 'theta_ecommerce_dbContext.Sellers'  is null.");
            }
            var seller = await _context.Sellers.FindAsync(id);
            if (seller != null)
            {
                var File = seller.Image;
                string CatPath = _he.WebRootPath + File;
                FileInfo file = new FileInfo(CatPath);

                if (file.Exists)
                {
                    file.Delete();
                }
                _context.Sellers.Remove(seller);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.Id == id);
        }
    }
}
