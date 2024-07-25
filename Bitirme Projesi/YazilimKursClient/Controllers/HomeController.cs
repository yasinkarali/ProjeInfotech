using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YazilimKursClient.Models;
using YazilimKursClient.Repository;

namespace YazilimKursClient.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var courseList = DataRepository.GetCourses();
        return View(courseList);
    }
}
