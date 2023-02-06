using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Specialty
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public ICollection<Expert>? Experts { get; set; }
    }
}
