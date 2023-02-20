using Domain.Helper;
using Domain.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
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

    }
    public class Case : CaseBase
    {
        [Key]
        public int Id { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }


    }

    public class CaseReport : Case
    {
        public string SourceTranslate => Source.TranslateSource();

        public string? CustomerName { get; set; }

        public string? ProductName { get; set; }
    }
}
