using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Authorization.Models.DTO
{
    public class RefreshTokenModel
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken {  get; set; }
        public DateTime RefreshTokenExpirationExpiration { get; set; }
        public bool? isActive { get; set; }
        public bool? isExpired { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
