using System.Text.Json.Serialization;

namespace YazilimKursClient.Areas.Admin.Models
{
    public class CourseModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Price")]
        public decimal Price { get; set; }

        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }

        [JsonPropertyName("TeacherName")]
        public string TeacherName { get; set; }
		[JsonPropertyName("TeacherId")]
		public int TeacherId { get; set; }

		[JsonPropertyName("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("IsHome")]
        public bool IsHome { get; set; }
    }
}
