using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Abstract
{
    public interface ICartRepository:IGenericRepository<Card>
    {
        Task<Card> GetCardByUserIdAsync(string userId);
    }
}
