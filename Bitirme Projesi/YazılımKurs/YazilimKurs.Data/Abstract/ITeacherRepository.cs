using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Abstract
{
    public interface ITeacherRepository:IGenericRepository<Teacher>
    {
        Task<Teacher> GetTeacherByUsernameAsync(string username);
        Task<Teacher> AddTeacherAsync(Teacher teacher);
        Task<Teacher> GetTeacherByNameAsync(string name);
    }
}