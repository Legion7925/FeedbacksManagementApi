using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string? NameAndFamily { get; set; } 

        public string? Email { get; set; }

        public string? Phone { get; set; }

        [JsonIgnore]
        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
