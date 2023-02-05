using FeedbacksManagementApi.Entities;
using FeedbacksManagementApi.Helper.Enums;

namespace FeedbacksManagementApi.Model
{
    public class FeedbackReportFilterModel
    {
        public DateTime? Created { get; set; }

        public DateTime? ReferralDate { get; set; }

        public DateTime? RespondDate { get; set; }

        public string? SerialNumber { get; set; }

        public string? Title { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int ExpertId { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? Respond { get; set; }

        public Source? Source { get; set; }

        public FeedbackState? State { get; set; }

        public Priority? Priorty { get; set; }

        public ICollection<Tag>? Tags { get; set; }

    }
}
