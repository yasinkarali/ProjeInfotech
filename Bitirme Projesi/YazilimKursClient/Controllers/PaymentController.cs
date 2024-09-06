using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using YazilimKursClient.Models;

namespace YazilimKursClient.Controllers
{
	public class PaymentController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(CreditCardModel model)
		{
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            model.UserId = userId;

            var serializeModel = System.Text.Json.JsonSerializer.Serialize(model);

            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("http://localhost:5502/api/payments", stringContent);

                if (result.IsSuccessStatusCode == true)
                {
                    TempData["Message"] = "Ödeme başarılı";
                    return RedirectToAction("Result", "Home");
                }

                string contentRepsonse = await result.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<Root<bool>>(contentRepsonse);

                ModelState.AddModelError("", res.Error ?? "Ödeme başarısız!");
            }

         


            return View(model);


		}
	}
}
