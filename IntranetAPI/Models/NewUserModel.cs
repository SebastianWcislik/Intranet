namespace IntranetAPI.Models
{
    public class NewUserModel
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Surname { get; set; }
        public int Priviliges { get; set; }
    }
}
