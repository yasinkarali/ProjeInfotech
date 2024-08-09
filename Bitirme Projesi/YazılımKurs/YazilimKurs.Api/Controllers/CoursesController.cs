
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.Helpers.Abstract;

namespace YazilimKurs.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IImageHelper _imageHelper;
        public CoursesController(ICourseService courseService, IImageHelper imageHelper)
        {
            _courseService = courseService;
            _imageHelper = imageHelper;
        }

        [HttpPost]

        public async Task<IActionResult> Create(AddCourseDto addCourseDto)
        {
            var response = await _courseService.AddAsync(addCourseDto);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllWithTeacherNameAsync();
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var response = await _courseService.GetByIdAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("active")]

        public async Task<IActionResult> GetActiveCourses()
        {
            var response = await _courseService.GetActiveCoursesAsync();
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("home")]

        public async Task<IActionResult> GetHomeCourses()
        {
            var response = await _courseService.GetHomeCoursesAsync();
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _courseService.DeleteAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(EditCourseDto editCourseDto)
        {
            var response = await _courseService.UpdateAsync(editCourseDto);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }



        [HttpGet("GetCoursesWithTeacherId/{id}")]

        public async Task<IActionResult> GetCoursesWithTeacherIdAsync(int id)
        {
            var response = await _courseService.GetCoursesByTeacherIdAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        [HttpPost("addimage")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            var response = await _imageHelper.Upload(file,"courses");
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


    }
}