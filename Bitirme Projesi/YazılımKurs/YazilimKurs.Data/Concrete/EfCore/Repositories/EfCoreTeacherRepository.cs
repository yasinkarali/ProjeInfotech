using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Concrete.EfCore.Repositories
{
    public class EfCoreTeacherRepository : EfCoreGenericRepository<Teacher>, ITeacherRepository
    {
        public EfCoreTeacherRepository(YazilimKursDbContext yazilimKursDbContext):base(yazilimKursDbContext)
        {
            
        }
        private YazilimKursDbContext Context1 { get { return _dbContext as YazilimKursDbContext; } }
        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            Context1.Teachers.Add(teacher);
            await Context1.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher> GetTeacherByUsernameAsync(string username)
        {
            var teacher = await Context1.Teachers.FirstOrDefaultAsync(x => x.Username == username);
            return teacher;
        }
        public async Task<Teacher> GetTeacherByNameAsync(string name)
        {
            var teacher = await Context1.Teachers.FirstOrDefaultAsync(x => x.Name == name);
            return teacher;
        }

    }
}