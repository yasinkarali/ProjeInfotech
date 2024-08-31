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
    public class CardService : ICardService
    {
        private readonly ICartRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(ICartRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }
        public async Task<Response<CardDto>> GetCardByUserIdAsync(string userId)
        {
            Card card = await _cardRepository.GetCardByUserIdAsync(userId);
            return Response<CardDto>.Success(_mapper.Map<CardDto>(card), 200);
        }

        public async Task<Response<NoContent>> InitializeCardAsync(string userId)
        {
            Card card = new Card { UserId = userId };
            await _cardRepository.CreateAsync(card);
            return Response<NoContent>.Success(201);
        }
    }
}
