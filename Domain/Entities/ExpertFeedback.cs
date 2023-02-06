using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ExpertFeedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Feedback))]
        public int FkIdFeedback { get; set; }

        [Required]
        [ForeignKey(nameof(Expert))]
        public int FkIdExpert { get; set; }
      
        public string? Description { get; set; }

        [JsonIgnore]
        public Expert? Expert { get; set; }

        [JsonIgnore]
        public Feedback? Feedback { get; set; }
    }
}
