using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Abstract
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
      
        Task<Student> GetStudentByUsernameAsync(string username);
        Task<Student> AddStudentAsync(Student student);

    }
}