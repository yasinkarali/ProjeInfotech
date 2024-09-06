using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;

namespace YazilimKurs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> NewOrder(OrderDto orderDto)
        {
            var response = await _orderService.CreateAsync(orderDto);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonConvert.SerializeObject(response));
            }
            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var response = await _orderService.GetOrderAsync(orderId);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonConvert.SerializeObject(response));
            }
            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpGet("getall/{userId?}")]
        public async Task<IActionResult> GetOrder(string userId = null)
        {
            var response = await _orderService.GetAllOrdersAsync(userId);

			var jsonResponse = JsonConvert.SerializeObject(response, Formatting.Indented, new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});

			if (!response.IsSucceeded)
			{
				return NotFound(jsonResponse);
			}
			return Ok(jsonResponse);
		}
    }
}
