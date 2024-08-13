namespace Authorization.Models.DTO
{
    public class Role
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public int AppId { get; set; }
        public string? Description { get; set; }
        public App? App { get; set; }
    }
}
