namespace FeedbacksManagementApi.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
