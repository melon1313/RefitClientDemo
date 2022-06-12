using System.Text.Json.Serialization;

namespace RefitClientDemo.Repositories.Models.Dtos
{
    public class ClubDto
    {
        [JsonPropertyName("RemoteClubId")]
        public int Id { get; set; }

        [JsonPropertyName("RemoteClubName")]
        public string Name { get; set; }

        [JsonPropertyName("RemoteClubDescription")]
        public string Description { get; set; }
    }
}
