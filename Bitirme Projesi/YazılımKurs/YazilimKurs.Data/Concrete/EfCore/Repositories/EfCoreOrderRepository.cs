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
    public class EfCoreOrderRepository : EfCoreGenericRepository<Order>, IOrderRepository
    {
        public EfCoreOrderRepository(YazilimKursDbContext yazilimKursDbContext) : base(yazilimKursDbContext)
        {

        }
        private YazilimKursDbContext Context
        {
            get { return _dbContext as YazilimKursDbContext; }
        }

        public async Task<List<Order>> GetAllOrdersAsync(string userId = null)
        {
            if (userId == null)
            {
                return await Context
                    .Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(y => y.Course)
                    .ToListAsync();
            }
            return await Context
                .Orders
                .Where(x => x.UserId == userId)
                .Include(x => x.OrderItems)
                .ThenInclude(y => y.Course)
                .ToListAsync();
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await Context
             .Orders
             .Where(x => x.Id == orderId)
             .Include(x => x.OrderItems)
             .ThenInclude(y => y.Course)
             .FirstOrDefaultAsync();
        }
    }
}
