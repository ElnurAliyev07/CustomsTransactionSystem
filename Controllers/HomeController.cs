using CustomsTransactionSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomsTransactionSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Custom");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
