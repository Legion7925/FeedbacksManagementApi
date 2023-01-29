namespace FeedbacksManagementApi.Entities
{
    public class Page
    {
        public int Id { get; set; }
        /// <summary>
        /// آدرس صفحه
        /// </summary>
        public string? PageUrl { get; set; }
        /// <summary>
        /// نام صفحه
        /// </summary>
        public string? PageName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
