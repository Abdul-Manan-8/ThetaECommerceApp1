using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ThetaECommerceApp.Models;

namespace ThetaECommerceApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly theta_ecommerce_dbContext _context;
        private readonly IWebHostEnvironment _he;

        public CustomersController(theta_ecommerce_dbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string UserName, string Password, [Bind("Id,Name,Image,City,PhoneNumber,Address,Dob,SystemUserId,Gender,Email,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData")] Customer customer, IFormFile Img)
        {
            string FinalFilePathVirtual = "/data/customer/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);

            using (FileStream FS = new FileStream(_he.WebRootPath + FinalFilePathVirtual, FileMode.Create))
            {
                Img.CopyTo(FS);
            }

            if (ModelState.IsValid)
            {
                SystemUser U = new SystemUser();
                U.UserName = UserName;
                U.Password = Password;
                U.Type = 4;
                _context.SystemUsers.Add(U);
                await _context.SaveChangesAsync();
                var GetU = _context.SystemUsers.Where(u => u.UserName == UserName).FirstOrDefault();

                customer.Image = FinalFilePathVirtual;
                customer.SystemUserId = GetU.Id;
                _context.Add(customer);
                await _context.SaveChangesAsync();


                //Welcome email
                if (!string.IsNullOrEmpty(customer.Email))
                {
                    MailMessage MailObj = new MailMessage();

                    MailObj.Subject = "Welcome <b>" + customer.Name + "</b> to our ECommerce webapplication";
                    MailObj.Body = "Welcome " + customer.Name + ",<br><br>" +
                        "Thank you for signing up on our system. feel free to contact us if you need any help.<br><br>" +
                        "Regards,<br> <span style='color:green;'>ECommerse Team</span>";
                    MailObj.To.Add(customer.Email);
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
                    //catch(Exception ex)
                    //{

                    //}
                }

            }
            return RedirectToAction("RegisterSuccess", "HomeController");
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image,City,PhoneNumber,Address,Dob,SystemUserId,Gender,Email,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData")] Customer customer, IFormFile Img)
        {
            string FinalFilePathVirtual = "/data/customer/pics/" + Guid.NewGuid().ToString() + Path.GetExtension(Img.FileName);

            using (FileStream FS = new FileStream(_he.WebRootPath + FinalFilePathVirtual, FileMode.Create))
            {
                Img.CopyTo(FS);
            }

            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.Image = FinalFilePathVirtual;
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'theta_ecommerce_dbContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {

                var File = customer.Image;
                string CatPath = _he.WebRootPath + File;
                FileInfo file = new FileInfo(CatPath);

                if (file.Exists)
                {
                    file.Delete();
                }

                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
