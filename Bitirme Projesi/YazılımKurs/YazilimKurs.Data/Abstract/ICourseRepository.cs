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

        Task<List<Course>> GetCoursesByTeacherIdAsync(int id);
        Task<List<Course>> GetCoursesWithTeacherNameAsync();
        Task<List<Course>> GetHomeCourseAsync();


    }
}