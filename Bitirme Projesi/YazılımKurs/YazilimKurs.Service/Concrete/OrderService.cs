using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Entity.Concrete;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<Response<NoContent>> CreateAsync(OrderDto orderDto)
        {
            await _orderRepository.CreateAsync(_mapper.Map<Order>(orderDto));
            return Response<NoContent>.Success(201);
        }

        public async Task<Response<List<OrderDto>>> GetAllOrdersAsync(string? userId = null)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(userId);
            return Response<List<OrderDto>>.Success(_mapper.Map<List<OrderDto>>(orders), 200);
        }

        public async Task<Response<OrderDto>> GetOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            return Response<OrderDto>.Success(_mapper.Map<OrderDto>(order), 200);
        }
    }
}
