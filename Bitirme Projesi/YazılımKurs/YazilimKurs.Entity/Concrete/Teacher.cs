using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Entity.Abstract;

namespace YazilimKurs.Entity.Concrete
{
    public class Teacher : IBaseEntity , ICommonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public List<Course> Courses { get; set; }
    }

}