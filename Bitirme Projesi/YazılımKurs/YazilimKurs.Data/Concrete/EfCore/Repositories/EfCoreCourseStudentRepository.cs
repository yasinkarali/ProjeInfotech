using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Concrete.EfCore.Repositories
{
    public class EfCoreCourseStudentRepository : EfCoreGenericRepository<CourseStudent>, ICourseStudentRepository
    {
        public EfCoreCourseStudentRepository(YazilimKursDbContext yazilimKursDbContext) : base(yazilimKursDbContext)
        {
            
        }
        private YazilimKursDbContext Context
        {
            get { return _dbContext as YazilimKursDbContext; }
        }

        public async Task<List<CourseStudent>> GetCourseStudentsWithCoursesByStudentIdAsync(int studentId)
        {
            return await Context.CoursesStudents.Where(o => o.StudentId == studentId)
                .Include(o => o.Course)
                .ToListAsync();
        }
        public async Task<List<CourseStudent>> GetCourseStudentsByStudentIdAsync(int studentId)
        {
            return await Context.CoursesStudents.Where(o => o.StudentId == studentId)
                .ToListAsync();
        }

    }
}
