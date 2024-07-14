using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YazilimKurs.Entity.Abstract;

namespace YazilimKurs.Entity.Concrete
{
    public class Course : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int TeacherId { get; set; }
        public string ImageUrl { get; set; }

        public Teacher Teacher { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
       
      

    }
}