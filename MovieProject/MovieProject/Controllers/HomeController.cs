using Microsoft.AspNetCore.Mvc;
using MovieProject.Models;
using MovieProject.Repository;
using System.Diagnostics;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

    public IActionResult Index()
        {
            return View();  
        }
    }
}