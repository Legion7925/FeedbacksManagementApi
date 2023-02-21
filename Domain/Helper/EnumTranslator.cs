using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    public static class EnumTranslator
    {
        public static string TranslateSource(this Source source)
        {
            switch (source)
            {
                case Source.Email:
                    return "ایمیل";
                case Source.SMS:
                    return "پیامک";
                case Source.Site:
                    return "وب سایت";
                case Source.MobileApp:
                    return "نرم افزار موبایل";
                default:
                    return "ناشناس";
            }
        }
        public static string state(this FeedbackState state)
        {
            switch (state)
            {
                case FeedbackState.ReadyToSend:
                    return "آماده ارسال";
                case FeedbackState.SentToExpert:
                    return "ارسال شده به متخصص";
                case FeedbackState.Deleted:
                    return "حذف شده";
                case FeedbackState.Archived:
                    return "بایگانی شده";
                default:
                    return "نامشخص";
            }
        }

        public static string TranslatePoriorty(this Priority priority)
        {
            switch (priority)
            {
                case Priority.Low:
                    return "پایین";
                case Priority.Medium:
                    return "معمولی";
                case Priority.High:
                    return "بالا";
                default:
                    return "نامشخص";
            }
        }
    }
}
