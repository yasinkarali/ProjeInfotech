using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class AddToCardDto
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public int Quantity { get; set; }
    }
}
