using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [JsonIgnore]
    public ICollection<Feedback>? Feedbacks { get; set; }
}
