using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class CardItemDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CourseId { get; set; }
        public CourseDto course { get; set; }
        public int CardId { get; set; }
       
        [JsonIgnore]
        public CardDto card { get; set; }
        public int Quantity { get; set; }
    }
}
