using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YazilimKursClient.Models;
using YazilimKursClient.Repository;

namespace YazilimKursClient.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        var rootCourses = new Root<List<CourseViewModel>>();
        using (var httpClient = new HttpClient())
        {
            using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:5502/api/Courses"))
            {
                if (!httpResponseMessage.IsSuccessStatusCode) 
                {
                    return null;
                }
                string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();
                rootCourses = JsonSerializer.Deserialize<Root<List<CourseViewModel>>>(contentRepsonse);
            }
        }
        return View(rootCourses.Data);
    }
}
