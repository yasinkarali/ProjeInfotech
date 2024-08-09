using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Abstract
{
    public interface ICardService
    {
        Task<Response<NoContent>> InitializeCardAsync(string userId);
        Task<Response<CardDto>> GetCardByUserIdAsync(string userId);
    }
}
