
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Service.Abstract
{
    public interface IPaymentService
    {
        Task<Response<bool>> Pay(PaymentRequestDto requestDto);
    }
}
