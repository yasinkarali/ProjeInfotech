using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YazilimKursClient.Areas.Admin.Models;

namespace YazilimKursClient.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Student")]
	public class OrderController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

			var rootCourses = new Root<List<OrderViewModel>>();
			using (var httpClient = new HttpClient())
			{
				using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5502/api/orders/getall/{userId}"))
				{
					if (!httpResponseMessage.IsSuccessStatusCode)
					{
						return null;
					}
					string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();
					rootCourses = System.Text.Json.JsonSerializer.Deserialize<Root<List<OrderViewModel>>>(contentRepsonse);
				}
			}
			return View(rootCourses.Data);
		}
	}
}
