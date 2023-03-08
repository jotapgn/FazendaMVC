using FazendaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FazendaMVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Fazenda"); ;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}