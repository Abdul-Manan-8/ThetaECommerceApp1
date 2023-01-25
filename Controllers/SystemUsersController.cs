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
using Microsoft.AspNetCore.Http;

namespace ThetaECommerceApp.Controllers
{
    public class SystemUsersController : Controller
    {
        private readonly theta_ecommerce_dbContext _context;
        private readonly IWebHostEnvironment _he;
        public SystemUsersController(theta_ecommerce_dbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: SystemUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.SystemUsers.ToListAsync());
        }

        // GET: SystemUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SystemUsers == null)
            {
                return NotFound();
            }

            var systemUser = await _context.SystemUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemUser == null)
            {
                return NotFound();
            }

            return View(systemUser);
        }

        // GET: SystemUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SystemUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Type,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData")] SystemUser systemUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemUser);
        }

        // GET: SystemUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SystemUsers == null)
            {
                return NotFound();
            }

            var systemUser = await _context.SystemUsers.FindAsync(id);
            if (systemUser == null)
            {
                return NotFound();
            }
            return View(systemUser);
        }

        // POST: SystemUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Type,Status,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,MetaData")] SystemUser systemUser)
        {
            if (id != systemUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemUserExists(systemUser.Id))
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
            return View(systemUser);
        }

        // GET: SystemUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SystemUsers == null)
            {
                return NotFound();
            }

            var systemUser = await _context.SystemUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemUser == null)
            {
                return NotFound();
            }

            return View(systemUser);
        }

        // POST: SystemUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SystemUsers == null)
            {
                return Problem("Entity set 'theta_ecommerce_dbContext.SystemUsers'  is null.");
            }
            var systemUser = await _context.SystemUsers.FindAsync(id);
            if (systemUser != null)
            {
                _context.SystemUsers.Remove(systemUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SystemUserExists(int id)
        {
            return _context.SystemUsers.Any(e => e.Id == id);
        }




        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }













        //[HttpGet]
        //public IActionResult RetrievePassword()
        //{
        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> RetrievePassword(string username, [Bind("Id,UserName,Password")] SystemUser systemUser)
        {
            if (username != systemUser.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _context.SystemUsers.FindAsync(username);
                if (user != null)
                {
                    MailMessage MailObj = new MailMessage();

                    MailObj.Subject = "Forget Password for <b>" + user.UserName + "</b> from ECommerce webapplication";
                    MailObj.Body = "Greeting Mr." + user.UserName + ",<br><br>" +
                        "Your Retrieve password request is accepted. here is your password " + user.Password + ". Do not share this password to anyone. feel free to contact us if you need more help.<br><br>" +
                        "Have a nice day.<br><br>Regards,<br> <span style='color:green;'>ECommerse Team</span>";
                    if (user.Type == 4)
                    {
                        var email = (_context.Customers.Where(a => a.SystemUserId == user.Id).FirstOrDefault());
                        MailObj.To.Add(email.Email);
                    }
                    if (user.Type == 3)
                    {
                        var email = (_context.Sellers.Where(a => a.SystemUserId == user.Id).FirstOrDefault());
                        MailObj.To.Add(email.Email);
                    }
                    MailObj.Bcc.Add("abdul124manan@gmai.com");

                    MailObj.From = new MailAddress("students@thetademos.com", "ECommerse Team");


                    SmtpClient SmtpObj = new SmtpClient();
                    SmtpObj.Port = 465;
                    SmtpObj.Host = "mail.thetademos.com";
                    SmtpObj.Credentials = new NetworkCredential("students@thetademos.com", "P@kist@n@@123");

                    SmtpObj.EnableSsl = true;


                    try
                    {
                        SmtpObj.Send(MailObj);
                    }
                    catch (Exception ex)
                    {
                        return Problem();
                    }
                }

            }
            return View(systemUser);

        }




        //[HttpGet]
        //public IActionResult ResetPassword()
        //{
        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(string username, [Bind("Id,UserName,Password")] SystemUser systemUser)
        {
            if (username != systemUser.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _context.SystemUsers.FindAsync(username);
                user.RecoveryCode = Guid.NewGuid().ToString();
                if (user != null)
                {
                    MailMessage MailObj = new MailMessage();

                    MailObj.Subject = "Reset Password for <b>" + user.UserName + "</b> from ECommerce webapplication";
                    MailObj.Body = "Greeting Mr." + user.UserName + ",<br><br>" +
                        "Your Reset password request is accepted. Click the link " + /*<a href= "_he/SystemUser/ResetPassword?user.RecoveryCode"/>*/ " to change password, this link is temporary. Do not share this to anyone. feel free to contact us if you need more help.<br><br>" +
                        "Have a nice day.<br><br>Regards,<br> <span style='color:green;'>ECommerse Team</span>";
                    if (user.Type == 4)
                    {
                        var email = (_context.Customers.Where(a => a.SystemUserId == user.Id).FirstOrDefault());
                        MailObj.To.Add(email.Email);
                    }
                    if(user.Type == 3)
                    {
                        var email = (_context.Sellers.Where(a => a.SystemUserId == user.Id).FirstOrDefault());
                        MailObj.To.Add(email.Email);
                    }

                    MailObj.Bcc.Add("abdul124manan@gmai.com");

                    MailObj.From = new MailAddress("students@thetademos.com", "ECommerse Team");


                    SmtpClient SmtpObj = new SmtpClient();
                    SmtpObj.Port = 465;
                    SmtpObj.Host = "mail.thetademos.com";
                    SmtpObj.Credentials = new NetworkCredential("students@thetademos.com", "P@kist@n@@123");

                    SmtpObj.EnableSsl = true;


                    try
                    {
                        SmtpObj.Send(MailObj);

                    }
                    catch (Exception ex)
                    {
                        return Problem();
                    }
                }

            }
            return View(systemUser);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(SystemUser U)
        {
            SystemUser User = _context.SystemUsers.Where(a => a.UserName == U.UserName && a.Password == U.Password).FirstOrDefault();

            if (User == null)
            {
                ViewBag.Error = "Invalid user name or password";
                return View();
            }



            //Success Login
            HttpContext.Session.SetString("UserName", User.UserName);
            HttpContext.Session.SetString("Password", User.Id.ToString());
            HttpContext.Session.SetString("Type", User.Type.Value.ToString());
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }














    }
}
