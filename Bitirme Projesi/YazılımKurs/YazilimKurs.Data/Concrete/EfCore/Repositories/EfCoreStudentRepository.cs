using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Concrete.EfCore.Repositories
{
    public class EfCoreStudentRepository : EfCoreGenericRepository<Student>, IStudentRepository
    {
        public EfCoreStudentRepository(YazilimKursDbContext yazilimKursDbContext):base(yazilimKursDbContext)
        {
            
        }
        private YazilimKursDbContext Context
        {
            get 
            {
                return _dbContext as YazilimKursDbContext; 
            }
        }
        public async Task<Student> AddStudentAsync(Student student)
        {
            Context.Students.Add(student);
            await Context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> GetStudentByUsernameAsync(string username)
        {
         var student = await  Context.Students.FirstOrDefaultAsync(x=>x.Username == username);
         return student ;
        }

    }
}