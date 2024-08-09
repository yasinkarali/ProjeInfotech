﻿using System.Text.Json.Serialization;

namespace YazilimKursClient.Areas.Admin.Models
{
    public class TeacherModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
