
using Microsoft.AspNetCore.Mvc;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;

namespace YazilimKurs.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
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
            var response = await _courseService.GetAllAsync();
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
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
            var response = await _courseService.GetCoursesWithTeacherIdAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


    }
}