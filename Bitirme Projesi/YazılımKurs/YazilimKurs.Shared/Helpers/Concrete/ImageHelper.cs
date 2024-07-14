using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazilimKurs.Shared.Helpers.Abstract;
using YazilimKurs.Shared.ResponseDtos;

namespace YazilimKurs.Shared.Helpers.Concrete
{
    public class ImageHelper : IUploadHelper
    {
        private string _imagesFolder;
        private readonly string[] permittedExtension = { ".png", ".jpg", ".jpeg" };
        private readonly string[] permittedMimeTypes = { "image/png", "image/jpg", "image/jpeg" };
        public ImageHelper(IWebHostEnvironment env)
        {
            _imagesFolder = Path.Combine(env.WebRootPath, "images");
        }
        public async Task<Response<string>> Upload(IFormFile file)
        {
            if (file == null || file.Length==0)
            {
                return Response<string>.Fail("Resim dosyasında sorun var!", 401);
            }
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || permittedExtension.Contains(extension))
            {
                return Response<string>.Fail("Resim formatı hatalı.png,jpg veya jpeg gönderiniz",401);
            }
            if (!permittedMimeTypes.Contains(file.ContentType))
            {
                return Response<string>.Fail("Lütfen resim dosyası içeriğini kontrol ediniz", 401);
            }
        }
    }
}
