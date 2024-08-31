using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using YazilimKursClient.Areas.Admin.Models;
using YazilimKursClient.Extensions;


namespace YazilimKursClient.Areas.Admin.Controllers
{
    [Authorize(Roles = "Student,Teacher")]
    [Area("Admin")]
    public class CourseController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rootCourses = new Root<List<CourseModel>>();
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:5502/api/courses"))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();
                    rootCourses = System.Text.Json.JsonSerializer.Deserialize<Root<List<CourseModel>>>(contentRepsonse);
                }
            }
            return View(rootCourses.Data);
        }
		[Authorize(Roles = "Teacher")]
		[HttpGet]
        public async Task<IActionResult> Create()
        {

            var rootTeachers = new Root<List<TeacherModel>>();
            using (var httpClient = new HttpClient())
            {
                rootTeachers = await httpClient.GetFromJsonAsync<Root<List<TeacherModel>>>("http://localhost:5502/api/Teacher");

                if (rootTeachers.IsSucceeded == false)
                {
                    return null;
                }


                //using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:5502/api/Teacher"))
                //{
                //    if (!httpResponseMessage.IsSuccessStatusCode)
                //    {
                //        return null;
                //    }
                //    string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();

                //    rootTeachers = JsonSerializer.Deserialize<Root<List<TeacherModel>>>(contentRepsonse);
                //}

            }

            AddCourseModel courseModel = new AddCourseModel
            {
                TeacherList = rootTeachers.Data.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(courseModel);
        }

		[Authorize(Roles = "Teacher")]

		[HttpPost]

        public async Task<IActionResult> Create(AddCourseModel addCourseModel,IFormFile image)

        {
            if (ModelState.IsValid && image!=null)
            {
                using (var httpClient = new HttpClient())
                {
                    var imageUrl = "";
                    using (var stream = image.OpenReadStream())
                    {
                        var imageContent = new MultipartFormDataContent();
                        byte[] bytes = stream.ToByteArray();
                        imageContent.Add(new ByteArrayContent(bytes), "file", image.FileName);
                        HttpResponseMessage responseMessage = await httpClient.PostAsync("http://localhost:5502/api/Courses/addimage", imageContent);
                        var responseString = await responseMessage.Content.ReadAsStringAsync();
                        //var response = JsonSerializer.Deserialize<Root<ImageModel>>(responseString);
                        var response = JsonConvert.DeserializeObject<Root<ImageModel>>(responseString);
                        if (response.IsSucceeded)
                        {
                            imageUrl = response.Data.ImageUrl;
                        }
                        else
                        {
                            Console.Write(response.Error);
                        }
                    }


                    addCourseModel.ImageUrl = imageUrl;
                    // kurs ekle
                    var serializeModel = System.Text.Json.JsonSerializer.Serialize(addCourseModel);
                    var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
                    var result = await httpClient.PostAsync("http://localhost:5502/api/Courses", stringContent);
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }



                }
               
            }
            var rootTeachers = new Root<List<TeacherModel>>();
            using (var httpClient = new HttpClient())
            {
                rootTeachers = await httpClient.GetFromJsonAsync<Root<List<TeacherModel>>>("http://localhost:5502/api/Teacher");

                if (rootTeachers.IsSucceeded == false)
                {
                    return null;
                }
                addCourseModel.TeacherList = rootTeachers.Data.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
            };
            ViewBag.ImageErrorMessage = image == null ? "Resim seçiniz" : null;
            return View(addCourseModel);
        }

         


    }
}
