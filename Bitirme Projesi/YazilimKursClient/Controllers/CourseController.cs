using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YazilimKursClient.Models;

namespace YazilimKursClient.Controllers
{
    public class CourseController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var rootCourses = new Root<List<CourseViewModel>>();
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:5502/api/courses/GetActiveCoursesWithCourseStudents"))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();
                    //rootCourses = System.Text.Json.JsonSerializer.Deserialize<Root<List<CourseViewModel>>>(contentRepsonse);

                    rootCourses = JsonConvert.DeserializeObject<Root<List<CourseViewModel>>>(contentRepsonse);
                }
            }
            return View(rootCourses.Data);
        }


    }
}
