using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YazilimKurs.Entity.Abstract
{
    public interface ICommonEntity
    {

        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}