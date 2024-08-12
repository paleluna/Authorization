namespace Authorization.Models.DTO.Set
{
    public class User
    { 
        public int Id { get; set; }
        public List<Get.Role> Roles { get; set; }
    }

    public class SimpleUser
    {
        public string AccId { get; set; }
        public List<Set.SimpleRole> Roles { get; set; }
    }
}
