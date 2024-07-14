using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Shared.Helpers.Abstract
{
    public interface IUploadHelper
    {
        Task<Response<string>> Upload(IFormFile file);
    }
}
