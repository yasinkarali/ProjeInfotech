using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Abstract
{
    public interface ICardItemService
    {
        Task<Response<NoContent>> AddToCardAsync(AddToCardDto addToCardDto);
        Task<Response<NoContent>> ClearCardAsync(string userId);
        Task<Response<NoContent>> DeleteItemFromCardAsync(int cardItemId);
        Task<Response<NoContent>> ChangeQuantityAsync(int cardItemId, int quantity);
    }
}
