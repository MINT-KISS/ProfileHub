namespace ProfileHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Surname { get; set; }
        public required string Name { get; set; } 
        public string? Patronymic { get; set; }
        public string? ProfilePhotoUrl { get; set; }
    }
}
