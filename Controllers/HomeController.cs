using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThetaECommerceApp.Models;

namespace ThetaECommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly theta_ecommerce_dbContext _context;
        private readonly IWebHostEnvironment _he;

        public HomeController(ILogger<HomeController> logger, theta_ecommerce_dbContext context, IWebHostEnvironment he)
        {
            _logger = logger;
            _context = context;
            _he = he;
        }

        public IActionResult Index()
        {
            //ViewBag.CatList = _context.Categories.ToList();
            ViewBag.TopProduct = _context.Products.Take(4).ToList();
            return View();
        }

        public IActionResult AboutUs()
        {
            //return View();
            return RedirectToAction("Create", "CustomerController");
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult RegisterSuccess()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}