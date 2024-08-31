using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using YazilimKursClient.Models;

namespace YazilimKursClient.Controllers
{

    //[Authorize(Roles = "Student")]
    public class CardController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var rootCard = new Root<CardViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:5502/api/Cards/getcard/3"))
                {
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                    rootCard = JsonSerializer.Deserialize<Root<CardViewModel>>(contentResponse);
                }
            }
            return View(rootCard.Data);
        }
    }
}
