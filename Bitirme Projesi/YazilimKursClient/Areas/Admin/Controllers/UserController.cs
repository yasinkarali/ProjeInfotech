using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YazilimKursClient.Areas.Admin.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


namespace YazilimKursClient.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			await HttpContext.SignOutAsync();
			return RedirectToAction("Login");
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{

			if (ModelState.IsValid)
			{
				using (var httpClient = new HttpClient())
				{
					var rootStudent = new Root<StudentModel>();

					using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5502/api/Students/LoginStudent?username={model.Username}&password={model.Password}"))
					{
						if (httpResponseMessage.IsSuccessStatusCode == true)
						{
							string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();
							rootStudent = JsonConvert.DeserializeObject<Root<StudentModel>>(contentRepsonse);

							// login
							HttpContext.Session.SetString("username", rootStudent.Data.Username);
							//HttpContext.Session.SetString("imageUrl", student.ImageUrl);
							HttpContext.Session.SetString("studentId", rootStudent.Data.Id.ToString());
							//ViewBag.s = rootStudent.Data.Name;
							var claims = new List<Claim>
				{
                    new Claim(ClaimTypes.NameIdentifier, rootStudent.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name, rootStudent.Data.Username),
					new Claim(ClaimTypes.Role, "Student"),
				};
							var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
							var authProperties = new AuthenticationProperties();
							await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

							return Redirect("/admin/course"); // TODO : öğrenci giriş yaptı
						}

						ModelState.AddModelError("", rootStudent.Error ?? "Öğrenci için kullanıcı adı veya şifre hatalı");

					}


					var rootTeacher = new Root<TeacherModel>();
					using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5502/api/Teacher/LoginTeacher?username={model.Username}&password={model.Password}"))
					{
						if (httpResponseMessage.IsSuccessStatusCode == true)
						{
							string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();
							rootTeacher = JsonConvert.DeserializeObject<Root<TeacherModel>>(contentRepsonse);

							// login 
							//ViewBag.t = rootTeacher.Data.Name;
							HttpContext.Session.SetString("username", rootTeacher.Data.Username);
							//HttpContext.Session.SetString("teacherImageUrl", teacher.TeacherImageUrl);
							HttpContext.Session.SetString("teacherId", rootTeacher.Data.Id.ToString());
							var claims = new List<Claim>
				{
					 new Claim(ClaimTypes.NameIdentifier, rootTeacher.Data.Id.ToString()),
					new Claim(ClaimTypes.Name, rootTeacher.Data.Username),
					new Claim(ClaimTypes.Role, "Teacher"),
				};
							var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
							var authProperties = new AuthenticationProperties();
							await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

							return Redirect("/admin/course");  // teacher giriş yaptı
						}

						ModelState.AddModelError("", rootTeacher.Error ?? "Öğretmen için kullanıcı adı veya şifre hatalı");

					}
				}
			}


			return View(model);
		}


		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Role == "Student")
				{
					// öğrenci ekle
					var serializeModel = System.Text.Json.JsonSerializer.Serialize(new
					{
						Name = model.Name,
						Surname = model.Surname,
						Username = model.Username,
						Password = model.Password,
					});

					using (var httpClient = new HttpClient())
					{
						var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
						var result = await httpClient.PostAsync("http://localhost:5502/api/Student", stringContent);

						if (result.IsSuccessStatusCode == true)
						{
							return Redirect("/admin/student/listusercourse"); // öğrenci sayfası
						}

						ModelState.AddModelError("", "Başarısız işlem!");

					}
				
				}
				else if (model.Role == "Teacher")
				{
					// öğretmen ekle
					var serializeModel = System.Text.Json.JsonSerializer.Serialize(new
					{
						Name = model.Name,
						Surname = model.Surname,
						Username = model.Username,
						Password = model.Password,
					});

					using (var httpClient = new HttpClient())
					{
						var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
						var result = await httpClient.PostAsync("http://localhost:5502/api/Teacher", stringContent);

						if (result.IsSuccessStatusCode == true)
						{
							return Redirect("/admin/teacher/teachercourse"); // öğretmen sayfası
						}

						ModelState.AddModelError("", "Başarısız işlem!");

					}
				}
			}


			return View(model);
		}

	}
}
