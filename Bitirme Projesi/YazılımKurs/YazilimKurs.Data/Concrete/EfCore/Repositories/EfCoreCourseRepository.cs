using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Concrete.EfCore.Repositories
{
    public class EfCoreCourseRepository : EfCoreGenericRepository<Course>, ICourseRepository
    {
        public EfCoreCourseRepository(YazilimKursDbContext yazilimKursDbContext):base(yazilimKursDbContext) 
        {
            
        }

        private YazilimKursDbContext Context { get{return _dbContext as YazilimKursDbContext; } }
        public async Task<List<Course>> GetActiveCoursesAsync()
        {
           List<Course> courses = 
           await Context
           .Courses
           .Where(c => c.IsActive==true)
           .ToListAsync();
           return courses;
        }

        public async Task<List<Course>> GetCoursesWithTeacherIdAsync(int id)
        {
            List<Course> courses = await  Context.Courses.Where(x => x.TeacherId==id).ToListAsync();
            return courses;
            
        }
    }
}
