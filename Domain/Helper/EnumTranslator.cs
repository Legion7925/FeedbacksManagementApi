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
    }
}
