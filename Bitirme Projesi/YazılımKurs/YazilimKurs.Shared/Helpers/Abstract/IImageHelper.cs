using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Shared.Dtos;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Shared.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<Response<ImageDto>> Upload(IFormFile file,string directoryName);
    }
}
