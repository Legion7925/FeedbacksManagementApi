using FeedbacksManagementApi.Helper.Enums;
using System.ComponentModel.DataAnnotations;

namespace FeedbacksManagementApi.Entities
{
    public class Case
    {
        public int Id { get; set; }

        /// <summary>
        /// منبع
        /// </summary>
        [Required]
        public Source Source { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        public string? Title { get; set; }
        /// <summary>
        /// شرح مشکل
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// آدرس منبع
        /// </summary>
        [Required]
        public string SourceAddress { get; set; } = string.Empty;

        /// <summary>
        /// الحاقیات
        /// </summary>
        public string? Resources { get; set; }

    }
}
