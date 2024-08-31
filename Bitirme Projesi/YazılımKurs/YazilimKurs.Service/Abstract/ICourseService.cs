using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Entity.Concrete;
using YazilimKurs.Shared;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;



namespace YazilimKurs.Service.Abstract
{
    public interface ICourseService
    {
        Task<Response<CourseDto>> AddAsync(AddCourseDto addCourseDto); 
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<List<CourseDto>>> GetActiveCoursesAsync();
        Task<Response<List<CourseDto>>> GetCourseStudentsByStudentIdAsync(int id);

        Task<Response<List<CourseDto>>> GetHomeCoursesAsync();
        Task<Response<CourseDto>> UpdateAsync(EditCourseDto editCourseDto);
        Task<Response<CourseDto>> GetByIdAsync(int id);
        Task<Response<NoContent>> DeleteAsync(int id);

        Task<Response<List<CourseDto>>> GetCoursesByTeacherIdAsync(int id);
        Task<Response<List<CourseDto>>> GetAllWithTeacherNameAsync();






    }
}