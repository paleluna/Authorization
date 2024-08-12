namespace Authorization.Models.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }

        public DTO.Employe? Employe { get; set; }
    }
}
