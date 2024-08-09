using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Entity.Concrete;

namespace YazilimKurs.Data.Abstract
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<List<Order>> GetAllOrdersAsync(string userId = null);
        Task<Order> GetOrderAsync(int orderId);
    }
}
