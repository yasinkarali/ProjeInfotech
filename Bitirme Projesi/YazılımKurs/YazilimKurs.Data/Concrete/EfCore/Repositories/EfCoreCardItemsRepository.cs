using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Concrete.EfCore.Repositories
{
    public class EfCoreCardItemsRepository:EfCoreGenericRepository<CardItem>,ICardItemRepository
    {
        public EfCoreCardItemsRepository(YazilimKursDbContext yazilimKursDbContext) : base(yazilimKursDbContext)
        {
            
        }
        private YazilimKursDbContext Context
        {
            get { return _dbContext as YazilimKursDbContext; }
        }


    }
}
