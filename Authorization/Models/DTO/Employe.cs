namespace Authorization.Models.DTO
{
    public class Employe
    {
        public string? UserLogin { get; set; } 
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? RoleName { get; set; }
        public bool? IsBlocked { get; set; }
    }
}
