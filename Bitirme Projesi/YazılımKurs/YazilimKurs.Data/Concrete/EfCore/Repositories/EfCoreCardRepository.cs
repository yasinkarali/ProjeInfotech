using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Concrete.EfCore.Repositories
{
    public class EfCoreCardRepository : EfCoreGenericRepository<Card>, ICardRepository
    {
        public EfCoreCardRepository(YazilimKursDbContext yazilimKursDbContext):base(yazilimKursDbContext)
        {
            
        }
        private YazilimKursDbContext Context
        {
            get { return _dbContext as YazilimKursDbContext; }
        }
        public async Task<Card> GetCardByUserIdAsync(string userId)
        {
           return await Context
                .Cards
                .Where (c => c.UserId == userId)
                .Include (c => c.CardItems)
                .ThenInclude(y=>y.Course)
                .FirstOrDefaultAsync ();
        }
    }
}
