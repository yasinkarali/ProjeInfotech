using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.Helpers.Abstract;

namespace YazilimKurs.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IImageHelper _imageHelper;

        public StudentsController(IStudentService studentService, IImageHelper imageHelper)
        {
            _studentService = studentService;
            _imageHelper = imageHelper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddStudentDto addStudentDto)
        {
             var response = await _studentService.AddAsync(addStudentDto);
             if (!response.IsSucceeded)
             {
                return NotFound(response); 
             }
             return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _studentService.GetAllAsync();
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);  
        }
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetStudentByUsername(string username)
        {
            var response = await _studentService.GetStudentByUsernameAsync(username);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var response = await _studentService.GetByIdAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _studentService.DeleteAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(EditStudentDto editStudentDto)
        {
            var response = await _studentService.UpdateAsync(editStudentDto);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok();
        }
        [HttpPost("addimage")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            var response = await _imageHelper.Upload(file,"students");
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}