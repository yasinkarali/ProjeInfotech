using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderDto Order { get; set; }
        public int CourseId { get; set; }
        public CourseDto Course { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
