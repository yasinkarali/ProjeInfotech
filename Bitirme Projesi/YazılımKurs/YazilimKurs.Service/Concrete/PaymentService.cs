using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using YazilimKurs.Data.Abstract;
using YazilimKurs.Service.Abstract;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Concrete
{
	public class PaymentService : IPaymentService
	{
		private readonly ICartRepository _cardRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderItemRepository _orderItemRepository;

		public PaymentService(ICartRepository cardRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
		{
			_cardRepository = cardRepository;
			_orderRepository = orderRepository;
			_orderItemRepository = orderItemRepository;
		}

		public async Task<Response<bool>> Pay(PaymentRequestDto requestDto)
		{

			var cart = await _cardRepository.GetCardByUserIdAsync(requestDto.UserId);

			if (cart == null)
			{
				return Response<bool>.Fail("Sepet bulunamadı!", 404);
			}

			string totalPrice = (cart.CardItems.Sum(o => o.Course.Price * o.Quantity)).ToString().Replace(",", ".");


			Options options = new Options();
			options.ApiKey = "sandbox-ifayq5wOB5l9J0sN8xWu7MyjduTmPIry";
			options.SecretKey = "0LyH9iVOUdGG4oJ004WrGOEiG7v23xhr";
			options.BaseUrl = "https://sandbox-api.iyzipay.com/";

			CreatePaymentRequest request = new CreatePaymentRequest();
			request.Locale = Locale.TR.ToString();
			request.ConversationId = "123456789";
			request.Price = totalPrice.ToString();
			request.PaidPrice = totalPrice.ToString();
			request.Currency = Currency.TRY.ToString();
			request.Installment = 1;
			request.BasketId = cart.Id.ToString();
			request.PaymentChannel = PaymentChannel.WEB.ToString();
			request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

			PaymentCard paymentCard = new PaymentCard();
			paymentCard.CardHolderName = requestDto.NameSurname;
			paymentCard.CardNumber = requestDto.CardNumber;
			paymentCard.ExpireMonth = requestDto.Month;
			paymentCard.ExpireYear = requestDto.Year;
			paymentCard.Cvc = requestDto.Cvv;
			paymentCard.RegisterCard = 0;
			request.PaymentCard = paymentCard;

			Buyer buyer = new Buyer();
			buyer.Id = "BY789";
			buyer.Name = "John";
			buyer.Surname = "Doe";
			buyer.GsmNumber = "+905350000000";
			buyer.Email = "email@email.com";
			buyer.IdentityNumber = "74300864791";
			buyer.LastLoginDate = "2015-10-05 12:43:35";
			buyer.RegistrationDate = "2013-04-21 15:12:09";
			buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
			buyer.Ip = "85.34.78.112";
			buyer.City = "Istanbul";
			buyer.Country = "Turkey";
			buyer.ZipCode = "34732";
			request.Buyer = buyer;
			
			Address shippingAddress = new Address();
			shippingAddress.ContactName = "Jane Doe";
			shippingAddress.City = "Istanbul";
			shippingAddress.Country = "Turkey";
			shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
			shippingAddress.ZipCode = "34742";
			request.ShippingAddress = shippingAddress;

			Address billingAddress = new Address();
			billingAddress.ContactName = "Jane Doe";
			billingAddress.City = "Istanbul";
			billingAddress.Country = "Turkey";
			billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
			billingAddress.ZipCode = "34742";
			request.BillingAddress = billingAddress;

			List<BasketItem> basketItems = new List<BasketItem>();
			foreach (var item in cart.CardItems)
			{
				BasketItem basketItem = new BasketItem();
				basketItem.Id = item.Course.Id.ToString();
				basketItem.Name = item.Course.Name;
				basketItem.Category1 = "kurs";
				basketItem.Category2 = "";
				basketItem.ItemType = BasketItemType.VIRTUAL.ToString();
				basketItem.Price = item.Course.Price.ToString().Replace(",", ".");
				basketItems.Add(basketItem);
			}
			request.BasketItems = basketItems;


			Payment payment = Payment.Create(request, options);

			if (payment.Status == Status.SUCCESS.ToString()) // ödeme başarılı
			{
				// sipariş oluştur
				var order = await _orderRepository.CreateAsync(new Entity.Concrete.Order
				{
					UserId = requestDto.UserId,
					OrderDate = DateTime.Now,
				});

				if (order.Id == 0)
				{
					return Response<bool>.Fail("Sipariş oluşturulamadı! İade için başvurun", 404);
				}


				foreach (var item in cart.CardItems)
				{
					Entity.Concrete.OrderItem orderItem = new Entity.Concrete.OrderItem
					{
						OrderId = order.Id,
						CourseId = item.CourseId,
						Quantity = item.Quantity,
						Price = item.Course.Price
					};

					await _orderItemRepository.CreateAsync(orderItem);

				}

				// sepeti temizle 
				await _cardRepository.DeleteAsync(cart);

				return Response<bool>.Success(true, StatusCodes.Status200OK);

			}
			else // ödeme başarısız
			{
				return Response<bool>.Fail(payment.ErrorMessage ?? "Ödeme Başarısız! Lütfen tekrar deneyin", 404);
			}
		}

	}
}
