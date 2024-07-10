using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class EditTeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}