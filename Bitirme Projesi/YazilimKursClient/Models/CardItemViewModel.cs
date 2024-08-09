using System.Text.Json.Serialization;

namespace YazilimKursClient.Models
{
    public class CardItemViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("CourseId")]
        public int CourseId { get; set; }

        [JsonPropertyName("Course")]
        public CourseViewModel Course { get; set; }

        [JsonPropertyName("CardId")]
        public int CardId { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
    }
}

