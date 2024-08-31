using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Abstract
{
    public interface ITeacherService
    {
        Task<Response<TeacherDto>> AddAsync(AddTeacherDto addTeacherDto);
        Task<Response<TeacherDto>> GetTeacherByUsernameAsync(string username);
        Task<Response<TeacherDto>> GetTeacherByUsernameAndPasswordAsync(string username, string password);
        Task<Response<TeacherDto>> GetTeacherByNameAsync(string name);


        Task<Response<TeacherDto>> UpdateAsync(EditTeacherDto editTeacherDto);

        Task<Response<List<TeacherDto>>> GetAllAsync();
        Task<Response<TeacherDto>> GetByIdAsync(int id);
        Task<Response<NoContent>> DeleteAsync(int id);
    }
}