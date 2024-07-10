using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Abstract
{
    public interface IStudentService
    {
        Task<Response<StudentDto>> AddAsync(AddStudentDto addStudentDto);
        Task<Response<StudentDto>> GetStudentByUsernameAsync(string username);
        Task<Response<StudentDto>> UpdateAsync(EditStudentDto editStudentDto);

        Task<Response<List<StudentDto>>> GetAllAsync();
        Task<Response<StudentDto>> GetByIdAsync(int id);
        Task<Response<NoContent>> DeleteAsync(int id);

    }
}