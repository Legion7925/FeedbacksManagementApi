using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FeedbacksManagementApi.Helper.Enums;

namespace FeedbacksManagementApi.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
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
        /// پاسخ مشکل
        /// </summary>
        public string? Respond { get; set; }
        /// <summary>
        /// منبع
        /// </summary>
        [Required]
        public Source Source { get; set; }
        /// <summary>
        /// آدرس منبع
        /// </summary>
        [Required]
        public string SourceAddress { get; set; } = string.Empty;
        /// <summary>
        /// کلید خارجی کاربر
        /// </summary>
        [ForeignKey(nameof(Customer))]
        public int FkIdCustomer { get; set; }
        /// <summary>
        /// الحاقیات
        /// </summary>
        public string? Resources { get; set; }
        /// <summary>
        /// شماره سریال
        /// </summary>
        [MaxLength(250)]
        public string? SerialNumber { get; set; }
        /// <summary>
        /// میزان شباهت
        /// </summary>
        public byte? Porbablity { get; set; }
        /// <summary>
        /// وضعیت فیدبک
        /// </summary>
        public FeedbackState State { get; set; }
        /// <summary>
        /// اولویت
        /// </summary>
        public Priority Priorty { get; set; }
        /// <summary>
        /// تاریخ ارجاع
        /// </summary>
        public DateTime? ReferralDate { get; set; }
        /// <summary>
        /// تاریخ برگشت
        /// </summary>
        public DateTime RespondDate { get; set; }

        public Customer? Customer { get; set; }

        /// <summary>
        /// لیست تگ های اتوماتیک
        /// </summary>
        public ICollection<Tag>? Tags { get; set; }

        public ICollection<Expert>? Experts { get; set; }
    }
}
