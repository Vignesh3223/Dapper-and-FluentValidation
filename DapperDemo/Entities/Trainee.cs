using System.ComponentModel.DataAnnotations;

namespace DapperDemo.Entities
{
    public class Trainee
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
    }
}
