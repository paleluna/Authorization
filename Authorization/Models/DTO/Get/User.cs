namespace Authorization.Models.DTO.Get
{
    public class User
    {
        public string AccId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }  
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public bool IsBlocked { get; set; }
        public string? AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
