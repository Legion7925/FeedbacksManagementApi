using FeedbacksManagementApi.Helper.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbacksManagementApi.Entities
{
    public class Expert
    {
        public int Id { get; set; }
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string? NameAndFamily { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        public string? NationalCode { get; set; }
        /// <summary>
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// شماره پاسپورت
        /// </summary>
        public string? PassportNumber { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// ملیت
        /// </summary>
        public string? Nationality { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public string? Language { get; set; }
        /// <summary>
        /// تحصیلات
        /// </summary>
        public Education Education { get; set; }
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public string? FieldOfStudy { get; set; }
        /// <summary>
        /// شغل
        /// </summary>
        public string? Occupation { get; set; }
        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string? MobileNumber { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// تاریخ عضویت
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// تعداد مورد های پاسخ داده شده
        /// </summary>
        public int AnsweredCount => Feedbacks?.Count ?? 0;
        /// <summary>
        /// کلید خارجی از جدول تخصص 
        /// </summary>
        public int FkIdExpertise { get; set; }
        /// <summary>
        /// تخصص 
        /// </summary>
        [ForeignKey(nameof(FkIdExpertise))]
        public Specialty? Expertise { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }


    }
}
