using FeedbacksManagementApi.Helper.Enums;
using System.ComponentModel.DataAnnotations;

namespace FeedbacksManagementApi.Entities
{
    public class CaseBase
    {
        /// <summary>
        /// منبع
        /// </summary>
        [Required]
        public Source Source { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        [Required(ErrorMessage = "عنوان مشکل نمیتواند خالی باشد")]
        public string? Title { get; set; }
        /// <summary>
        /// شرح مشکل
        /// </summary>
        [Required(ErrorMessage = "شرح مشکل نمیتواند خالی باشد")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// آدرس منبع
        /// </summary>
        [Required(ErrorMessage = "آدرس منبع نمیتواند خالی باشد")]
        public string SourceAddress { get; set; } = string.Empty;

        /// <summary>
        /// الحاقیات
        /// </summary>
        public string? Resources { get; set; }

    }
    public class Case : CaseBase
    {
        [Key]
        public int Id { get; set; }

    }
}
