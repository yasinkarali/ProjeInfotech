using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public string TeacherName { get; set; }
        public string ImageUrl { get; set; }

    }
}