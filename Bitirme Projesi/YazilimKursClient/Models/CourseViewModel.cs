using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YazilimKursClient.Models
{
    public class CourseViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [DisplayName("Kurs Adý")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [DisplayName("Açýklama")]

        public string Description { get; set; }

        [JsonPropertyName("price")]
        [DisplayName("Fiyat")]

        public double Price { get; set; }

        [JsonPropertyName("teacherName")]
        [DisplayName("Öðretmen Adý")]

        public string TeacherName { get; set; }

        [JsonPropertyName("imageUrl")]
        [DisplayName("Resim")]

        public string ImageUrl { get; set; }
    }
}