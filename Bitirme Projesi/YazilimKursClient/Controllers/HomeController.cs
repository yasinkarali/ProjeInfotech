using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YazilimKursClient.Models;

namespace YazilimKursClient.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
