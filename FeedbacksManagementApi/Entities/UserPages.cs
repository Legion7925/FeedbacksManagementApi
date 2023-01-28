using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbacksManagementApi.Entities
{
    public class UserPages
    {
        public int Id { get; set; }
        /// <summary>
        /// کلید خارجی کاربر
        /// </summary>
        public int FkIdUser { get; set; }
        /// <summary>
        /// کلید خارجی صفحه
        /// </summary>
        public int FkIdPage { get; set; }

        [ForeignKey(nameof(FkIdUser))]
        public User? User { get; set; }

        [ForeignKey(nameof(FkIdPage))]
        public Page? Page { get; set; }
    }
}
