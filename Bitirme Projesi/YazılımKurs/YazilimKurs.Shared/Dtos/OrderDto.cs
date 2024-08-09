using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
