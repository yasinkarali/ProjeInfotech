using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazilimKurs.Entity.Concrete
{
    public class CardItem
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int CardId { get; set; }
        public Card card { get; set; }
        public int Quantity { get; set; }
    }
}
