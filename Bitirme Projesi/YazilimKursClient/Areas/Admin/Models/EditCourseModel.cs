using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YazilimKursClient.Areas.Admin.Models
{
    public class EditCourseModel
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [DisplayName("Kurs Adı")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "Bu alanın uzunluğu 3 karakterden kısa olamaz")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [MinLength(5, ErrorMessage = "Bu alanın uzunluğu 5 karakterden kısa olamaz")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        [DisplayName("Fiyat")]
        [Required(ErrorMessage = "Bu alan boş bırakılamaz")]
        [Range(minimum: 0, maximum: 10000, ErrorMessage = "Bu alana 0-10000 aralığı dışında bir değer girilemez")]
        public decimal? Price { get; set; }

        [JsonPropertyName("teacherId")]
        public int TeacherId { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("isHome")]   
        public bool IsHome { get; set; }
        public List<SelectListItem> TeacherList { get; set; }
    }
}
