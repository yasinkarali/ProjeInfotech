using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using YazilimKursClient.Areas.Admin.Models;
using YazilimKursClient.Extensions;
using static System.Net.Mime.MediaTypeNames;


namespace YazilimKursClient.Areas.Admin.Controllers
{
	[Authorize(Roles = "Student,Teacher")]
	[Area("Admin")]
	public class CourseController : Controller
	{
		[HttpGet]
		public IActionResult CourseView(int id)
		{
			// api ya git course detayını çek sayfada göster
			return View();
		}
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
			var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value; // Giriş yapan kullanıcının ID'sini alın;

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
				// TODO : admin girişi yapılırsa tüm öğretmenleri göster
				TeacherList = rootTeachers.Data.Where(o => o.Id == Convert.ToInt32(userId)).Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList()
			};
			return View(courseModel);
		}

		[Authorize(Roles = "Teacher")]

		[HttpPost]
		public async Task<IActionResult> Create(AddCourseModel addCourseModel, IFormFile image)

		{

			var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value; // Giriş yapan kullanıcının ID'sini alın;

			if (ModelState.IsValid && image != null)
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
				addCourseModel.TeacherList = rootTeachers.Data.Where(o => o.Id == Convert.ToInt32(userId)).Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList();
			};
			ViewBag.ImageErrorMessage = image == null ? "Resim seçiniz" : null;
			return View(addCourseModel);
		}

		[Authorize(Roles = "Teacher")]
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value; // Giriş yapan kullanıcının ID'sini alın;


			using (var httpClient = new HttpClient())
			{
				// İlk olarak kursun kime ait olduğunu kontrol edin
				var courseResponse = await httpClient.GetAsync($"http://localhost:5502/api/courses/{id}");
				if (!courseResponse.IsSuccessStatusCode)
				{
					// Kurs bulunamadıysa veya başka bir hata oluştuysa, uygun bir işlem yapın
					return View("Error");
				}

				var course = await courseResponse.Content.ReadFromJsonAsync<CourseModel>();

				// Eğer öğrenci rolünde ise ve kurs onun değilse, silme işlemi yapılmaz
				if (User.IsInRole("Student") && course.StudentId.ToString() != userId)
				{
					return Forbid(); // Yetkisiz işlem uyarısı verilir
				}

				// Kurs silme işlemi
				HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:5502/api/courses/{id}");

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					// Hata yönetimi yapabilirsiniz
					return View("Error");
				}
			}
		}

		[Authorize(Roles = "Teacher")]
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{

			EditCourseModel courseModel = new EditCourseModel();

			var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value; // Giriş yapan kullanıcının ID'sini alın;

			var rootTeachers = new Root<List<TeacherModel>>();
			using (var httpClient = new HttpClient())
			{
				rootTeachers = await httpClient.GetFromJsonAsync<Root<List<TeacherModel>>>($"http://localhost:5502/api/Teacher");

				if (rootTeachers.IsSucceeded == false)
				{
					return null;
				}



				var courseResponse = await httpClient.GetAsync($"http://localhost:5502/api/courses/{id}");
				if (!courseResponse.IsSuccessStatusCode)
				{
					// Kurs bulunamadıysa veya başka bir hata oluştuysa, uygun bir işlem yapın
					return View("Error");
				}

				string contentRepsonse = await courseResponse.Content.ReadAsStringAsync();

				var editModel = JsonConvert.DeserializeObject<Root<EditCourseModel>>(contentRepsonse);


				courseModel = editModel.Data;

			}

			// TODO : admin girişi yapılırsa tüm öğretmenleri göster
			courseModel.TeacherList = rootTeachers.Data.Where(o => o.Id == Convert.ToInt32(userId)).Select(x => new SelectListItem
			{
				Text = x.Name,
				Value = x.Id.ToString()
			}).ToList();

			return View(courseModel);


		}

		[Authorize(Roles = "Teacher")]
		[HttpPost]
		public async Task<IActionResult> Edit(EditCourseModel model, IFormFile image)
		{
			var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value; // Giriş yapan kullanıcının ID'sini alın;

			if (ModelState.IsValid && image != null)
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


					model.ImageUrl = imageUrl;
					// kurs ekle
					var serializeModel = System.Text.Json.JsonSerializer.Serialize(model);
					var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
					var result = await httpClient.PutAsync("http://localhost:5502/api/Courses", stringContent);
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
				model.TeacherList = rootTeachers.Data.Where(o => o.Id == Convert.ToInt32(userId)).Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString()
				}).ToList();
			};
			ViewBag.ImageErrorMessage = image == null ? "Resim seçiniz" : null;
			return View(model);


		}


	}
}
