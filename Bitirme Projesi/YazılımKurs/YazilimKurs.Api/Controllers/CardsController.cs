using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Service.Concrete;
using YazilimKurs.Shared.Dtos;

namespace YazilimKurs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ICardItemService _cardItemService;

        public CardsController(ICardService cardService, ICardItemService cardItemService)
        {
            _cardService = cardService;
            _cardItemService = cardItemService;
        }

        [HttpGet("initialize/{userId}")]
        public async Task<IActionResult> Initialize(string userId)
        {
            var response = await _cardService.InitializeCardAsync(userId);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }
        [HttpGet("getcard/{userId}")]
        public async Task<IActionResult> GetCardByUserId(string userId)
        {
            var response = await _cardService.GetCardByUserIdAsync(userId);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCard(AddToCardDto addToCardDto)
        {
            var response = await _cardItemService.AddToCardAsync(addToCardDto);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }
        [HttpGet("deleteitem/{itemId}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            var response = await _cardItemService.DeleteItemFromCardAsync(itemId);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }
        [HttpGet("clear/{userId}")]
        public async Task<IActionResult> ClearCard(string userId)
        {
            var response = await _cardItemService.ClearCardAsync(userId);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }

        [HttpPost("changequantity")]
        public async Task<IActionResult> ChangeQuantity(int cardItemId, int quantity)
        {
            var response = await _cardItemService.ChangeQuantityAsync(cardItemId, quantity);
            if (!response.IsSucceeded)
            {
                return NotFound(JsonSerializer.Serialize(response));
            }
            return Ok(JsonSerializer.Serialize(response));
        }
    }
}
