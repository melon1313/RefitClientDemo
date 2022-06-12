using System.Text.Json.Serialization;

namespace RefitClientDemo.Repositories.Models.Dtos
{
    public class ClubUserDto
    {
        [JsonPropertyName("RemoteClubUserId")]
        public string ClubUserId { get; set; }

        [JsonPropertyName("RemoteClubId")]
        public int ClubId { get; set; }

        [JsonPropertyName("RemoteClubUserName")]
        public string ClubUserName { get; set; }
    }
}
