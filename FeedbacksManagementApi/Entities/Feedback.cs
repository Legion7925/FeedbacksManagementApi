using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using FeedbacksManagementApi.Helper.Enums;

namespace FeedbacksManagementApi.Entities
{
    public class FeedbackBase
    {
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
        [Required]
        public int FkIdCustomer { get; set; }
        /// <summary>
        /// کلید خارجی محصول
        /// </summary>
        [ForeignKey(nameof(Product))]
        [Required]
        public int FkIdProduct { get; set; }
        /// <summary>
        /// الحاقیات
        /// </summary>
        public string? Resources { get; set; }
        /// <summary>
        /// میزان شباهت
        /// </summary>
        public byte? Similarity { get; set; }
        /// <summary>
        /// اولویت
        /// </summary>
        public Priority Priorty { get; set; }
    }

    public class Feedback : FeedbackBase
    {
        public int Id { get; set; }

        /// <summary>
        /// تاریخ ارجاع
        /// </summary>
        public DateTime? ReferralDate { get; set; }
        /// <summary>
        /// تاریخ برگشت
        /// </summary>
        public DateTime RespondDate { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// وضعیت فیدبک
        /// </summary>
        public FeedbackState State { get; set; }

        /// <summary>
        /// شماره سریال
        /// </summary>
        [MaxLength(250)]
        public string? SerialNumber { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        /// <summary>
        /// لیست تگ های اتوماتیک
        /// </summary>
        public ICollection<Tag>? Tags { get; set; }

        public ICollection<Expert>? Experts { get; set; }

    }
}
