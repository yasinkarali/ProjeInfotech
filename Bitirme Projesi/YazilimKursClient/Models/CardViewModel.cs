using System.Text.Json.Serialization;

namespace YazilimKursClient.Models
{
    public class CardViewModel
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("UserId")]
        public string UserId { get; set; }

        [JsonPropertyName("CardItems")]
        public List<CardItemViewModel> CardItems { get; set; }
    }
}
