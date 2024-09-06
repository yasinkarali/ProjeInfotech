using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazilimKurs.Shared.Dtos
{
    public class PaymentRequestDto
    {
        public string UserId { get; set; }
        public string NameSurname { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Cvv { get; set; }
    }
}
