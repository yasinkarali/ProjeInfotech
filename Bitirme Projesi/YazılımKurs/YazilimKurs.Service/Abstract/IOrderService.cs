using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Abstract
{
    public interface IOrderService
    {
        Task<Response<NoContent>> CreateAsync(OrderDto orderDto);
        Task<Response<OrderDto>> GetOrderAsync(int orderId);
        Task<Response<List<OrderDto>>> GetAllOrdersAsync(string? userId = null);
    }
}
