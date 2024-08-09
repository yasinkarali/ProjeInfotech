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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IImageHelper _imageHelper;

        public TeacherController(ITeacherService teacherService, IImageHelper imageHelper)
        {
            _teacherService = teacherService;
            _imageHelper = imageHelper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddTeacherDto addTeacherDto)
        {
            var response = await _teacherService.AddAsync(addTeacherDto);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _teacherService.GetAllAsync();
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetTeacherByUsername(string username)
        {
            var response = await _teacherService.GetTeacherByUsernameAsync(username);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetTeacherByName(string name)
        {
            var response = await _teacherService.GetTeacherByNameAsync(name);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var response = await _teacherService.GetByIdAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _teacherService.DeleteAsync(id);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(EditTeacherDto editTeacherDto)
        {
            var response = await _teacherService.UpdateAsync(editTeacherDto);
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok();
        }
        [HttpPost("addimage")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            var response = await _imageHelper.Upload(file,"teachers");
            if (!response.IsSucceeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}