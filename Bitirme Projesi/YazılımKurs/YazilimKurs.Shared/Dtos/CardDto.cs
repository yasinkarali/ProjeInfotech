using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public List<CardItemDto> CartItems { get; set; }
    }
}
