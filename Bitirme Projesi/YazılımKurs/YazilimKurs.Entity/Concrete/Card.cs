using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazilimKurs.Entity.Concrete
{
    public class Card
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public List<CardItem> CardItems { get; set; }
    }
}
