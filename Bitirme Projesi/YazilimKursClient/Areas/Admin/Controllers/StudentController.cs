using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using YazilimKursClient.Areas.Admin.Models;

namespace YazilimKursClient.Areas.Admin.Controllers
{
    [Authorize(Roles = "Student")]
    [Area("Admin")]
	public class StudentController : Controller
	{
     
		[HttpGet]
		public IActionResult ListUserCourse()
		{
			//var studentId = HttpContext.Session.GetString("studentId");

			//var studentName = HttpContext.Session.GetString("username");

            var model = new List<CourseModel>();


            return View(model);
		}
	}
}
