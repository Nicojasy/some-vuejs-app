using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SomeVueJSApp.Models
{
    public class UserDto
    {
        [Required]
        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [Required]
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [Required]
        [JsonPropertyName("patronymic")]
        public string Patronymic { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }
    }
}
