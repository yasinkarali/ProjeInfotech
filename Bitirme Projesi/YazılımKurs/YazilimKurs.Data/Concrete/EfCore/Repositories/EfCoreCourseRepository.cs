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
        public EfCoreCourseRepository(YazilimKursDbContext yazilimKursDbContext) : base(yazilimKursDbContext)
        {

        }

        private YazilimKursDbContext Context { get { return _dbContext as YazilimKursDbContext; } }
        public async Task<List<Course>> GetActiveCoursesAsync()
        {
            List<Course> courses =
            await Context
            .Courses
            .Where(c => c.IsActive == true)
            .ToListAsync();
            return courses;
        }

        public async Task<List<Course>> GetActiveCoursesWithCourseStudentsAsync()
        {
            List<Course> courses =
            await Context
            .Courses
            .Include(c => c.CourseStudents)
            .Where(c => c.IsActive == true)
            .ToListAsync();
            return courses;
        }

        public async Task<List<Course>> GetCoursesByTeacherIdAsync(int id)
        {
            List<Course> courses = await Context.Courses.Where(x => x.TeacherId == id).ToListAsync();
            return courses;

        }

        public async Task<List<Course>> GetCoursesWithTeacherNameAsync()
        {
            List<Course> courses = await Context
                .Courses
                .Include(x => x.Teacher)
                .ToListAsync();
            return courses;
        }


        public async Task<List<Course>> GetHomeCourseAsync()
        {
            List<Course> courses = await Context.Courses.Where(c => c.IsHome && c.IsActive).ToListAsync();
            return courses;
        }

        public async Task<bool> UpdateCourseAsync(int id, decimal price, string name, string description, string imageUrl, bool isActive)
        {

            var course = await Context.Courses.FirstOrDefaultAsync(o => o.Id == id);

            if (course == null)
            {
                return false;
            }

            course.Price = price;
            course.Name = name;
            course.Description = description;
            course.ImageUrl = imageUrl;
            course.IsActive = isActive;


            return await Context.SaveChangesAsync() > 0;
        }
    }
}
