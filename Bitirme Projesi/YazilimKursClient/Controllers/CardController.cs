using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using YazilimKursClient.Models;

namespace YazilimKursClient.Controllers
{

	[Authorize(Roles = "Student")]
	public class CardController : Controller
	{

		public async Task<IActionResult> Index()
		{
			var studentId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

			var rootCard = new Root<List<CardItemViewModel>>();
			using (var httpClient = new HttpClient())
			{
                //using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5502/api/Cards/getcard/{studentId}"))
                //{
                //	if (!httpResponseMessage.IsSuccessStatusCode)
                //	{
                //		return null;
                //	}

                //                string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();

                //	rootCard = JsonConvert.DeserializeObject<Root<CardViewModel>>(contentRepsonse);


                using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5502/api/Cards/getcarditems/{studentId}"))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
						//return null;
						return View();
                    }

                    string contentRepsonse = await httpResponseMessage.Content.ReadAsStringAsync();

                    rootCard = JsonConvert.DeserializeObject<Root<List<CardItemViewModel>>>(contentRepsonse);



                }
            }
			return View(rootCard.Data);
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int courseId)
		{
			var studentId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;

			var serializeModel = System.Text.Json.JsonSerializer.Serialize(new
			{
				UserId = studentId,
				CourseId = courseId,
				Quantity = 1
			});

			using (var httpClient = new HttpClient())
			{
				var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
				var result = await httpClient.PostAsync("http://localhost:5502/api/cards", stringContent);

				if (result.IsSuccessStatusCode == true)
				{
					return RedirectToAction("Index", "Card");
				}
			}


			return RedirectToAction("Index", "Course");


		
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value; // Giriş yapan kullanıcının ID'sini alın;


			using (var httpClient = new HttpClient())
			{

				HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5502/api/cards/deleteitem/{id}");

				if (response.IsSuccessStatusCode)
				{
				
				}
				else
				{
				
				}

				return RedirectToAction("index","card");
			}
		}
	}
}
