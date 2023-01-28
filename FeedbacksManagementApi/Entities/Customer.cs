namespace FeedbacksManagementApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string? NameAndFamily { get; set; } 

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }
    }
}
