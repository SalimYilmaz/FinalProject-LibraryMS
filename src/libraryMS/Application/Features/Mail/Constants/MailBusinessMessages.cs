using MailKit;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Mail.Constants;
public static class MailBusinessMessages
{
    private const string _libraryName = "Ninova Kütüphanesi";
    public const int OneDayForEmailReminder = 1;
    public const int TwoDaysForEmailReminder = 2;
    public const int FiveDaysForEmailReminder = 5;
    public const int SevenDaysForEmailReminder = 7;

    public const string GoodDayMessage = $"İyi Günler {_libraryName}";
    public const string InformationalMessage = "Bu E-Posta bilgilendirme amaçlı gönderilmiştir.";
}
