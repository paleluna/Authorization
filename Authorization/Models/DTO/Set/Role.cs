namespace Authorization.Models.DTO.Set
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class SimpleRole
    {
        public int RoleId { get; set; }
        public int AppId { get; set; }
    }
}
