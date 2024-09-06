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
	public class CardItemService : ICardItemService
	{
		private readonly ICartItemRepository _cardItemRepository;
		private readonly ICartRepository _cardRepository;
		private readonly IMapper _mapper;

		public CardItemService(ICartItemRepository cardItemRepository, IMapper mapper, ICartRepository cardRepository)
		{
			_cardItemRepository = cardItemRepository;
			_mapper = mapper;
			_cardRepository = cardRepository;
		}
        public async Task<Response<List<CardItemDto>>> GetCardItemsByUserIdAsync(string userId)
        {
            Card card = await _cardRepository.GetCardByUserIdAsync(userId);
			if (card == null)
			{
                return Response<List<CardItemDto>>.Fail("Bir hata oluştu", 400);
            }

            var cartItems = _mapper.Map<List<CardItemDto>>(card.CardItems);

            return Response<List<CardItemDto>>.Success(cartItems, 200);
        }
        public async Task<Response<NoContent>> AddToCardAsync(AddToCardDto addToCardDto)
		{
			var card = await _cardRepository.GetCardByUserIdAsync(addToCardDto.UserId);
			if (card == null)
			{
				card = new Card { UserId = addToCardDto.UserId, CreatedDate = DateTime.Now };
				await _cardRepository.CreateAsync(card);

				//return Response<NoContent>.Fail("Bir hata oluştu", 400);
			}

			if (card.CardItems.Any(o => o.CourseId == addToCardDto.CourseId) == false)
			{
				card.CardItems.Add(new CardItem
				{
					CourseId = addToCardDto.CourseId,
					CardId = card.Id,
					Quantity = addToCardDto.Quantity
				});
			}


			//int index = card.CardItems.FindIndex(x => x.CourseId == addToCardDto.CourseId);
			//if (index < 0) //Eklenmeye çalışılan kitap sepette daha önceden yoksa
			//{
			//	card.CardItems.Add(new CardItem
			//	{
			//		CourseId = addToCardDto.CourseId,
			//		CardId = card.Id,
			//		Quantity = addToCardDto.Quantity
			//	});
			//}
			//else//Eklenmeye çalışılan kitap sepette daha önceden varsa
			//{
			//	//card.CardItems[index].Quantity += addToCardDto.Quantity;
			//	return Response<NoContent>.Fail("Aynı kurs birden fazla satın alınamaz!", 400);
			//}

			await _cardRepository.UpdateAsync(card);
			return Response<NoContent>.Success(204);
		}

		public async Task<Response<NoContent>> ChangeQuantityAsync(int cardItemId, int quantity)
		{
			CardItem cardItem = await _cardItemRepository.GetByIdAsync(cardItemId);
			cardItem.Quantity = quantity;
			await _cardItemRepository.UpdateAsync(cardItem);
			return Response<NoContent>.Success(204);
		}

		public async Task<Response<NoContent>> ClearCardAsync(string userId)
		{
			Card card = await _cardRepository.GetCardByUserIdAsync(userId);
			card.CardItems = new List<CardItem>();
			await _cardRepository.UpdateAsync(card);
			return Response<NoContent>.Success(200);
		}

		public async Task<Response<NoContent>> DeleteItemFromCardAsync(int cardItemId)
		{
			CardItem cardItem = await _cardItemRepository.GetByIdAsync(cardItemId);
			await _cardItemRepository.DeleteAsync(cardItem);
			return Response<NoContent>.Success(200);
		}
	}
}
