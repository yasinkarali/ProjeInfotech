using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Abstract
{
    public interface ICourseRepository:IGenericRepository<Course>
    {
        Task<List<Course>> GetActiveCoursesAsync();
        Task<List<Course>> GetActiveCoursesWithCourseStudentsAsync();

        Task<List<Course>> GetCoursesByTeacherIdAsync(int id);
        Task<List<Course>> GetCoursesWithTeacherNameAsync();
        Task<List<Course>> GetHomeCourseAsync();
        Task<bool> UpdateCourseAsync(int id, decimal price, string name, string description, string imageUrl, bool isActive);

    }
}