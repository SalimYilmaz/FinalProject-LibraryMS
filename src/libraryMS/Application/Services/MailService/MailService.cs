using Hangfire;
using Application.Services.Borrows;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Services.Members;
using Microsoft.Extensions.Configuration;
using NArchitecture.Core.Mailing;
using Application.Services.UsersService;
using Application.Features.Mail.Constants;
using Application.Services.Reservations;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using NArchitecture.Core.Application.Pipelines.Transaction;
using Nest;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Linq.Expressions;
using Application.Features.Reservations.Rules;
using Application.Features.Members.Rules;
using Application.Features.Borrows.Rules;
using YamlDotNet.Serialization;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Domain.Enums;
namespace Application.Services.MailService;


public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly IServiceProvider _serviceProvider;

    public MailService(IConfiguration configuration, IServiceProvider services)
    {
        _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>()!;
        _serviceProvider = services;
    }
    private async Task sendEmailAsync(IList<MailboxAddress> receiverEmailInformations, string mailHtmlBody,string mailSubject, SmtpClient smtp, string textBody = null)
    {
        MimeMessage email = new MimeMessage();
        BodyBuilder bodyBuilder = new BodyBuilder() { HtmlBody = mailHtmlBody,TextBody = textBody};
        email.Body = bodyBuilder.ToMessageBody();
        email.Subject = mailSubject;
        email.From.Add(new MailboxAddress(_mailSettings.SenderFullName, _mailSettings.SenderEmail));
        email.To.AddRange(receiverEmailInformations);
        await smtp.SendAsync(email);
        email.Dispose();
        smtp.Disconnect(true);
        smtp.Dispose();
    }

    public async Task SendReminderEmailTwoDaysBeforeReturnDateAsync()
    {
        DateTime today = DateTime.Today;
        DateTime twoDaysBeforeForReminder = calculateReminderDate(today, MailBusinessMessages.TwoDaysForEmailReminder);
        
        IBorrowService borrowService = _serviceProvider.GetRequiredService<IBorrowService>();

        IPaginate<Borrow>? borrows = await borrowService.GetListAsync(predicate: b =>
        b.ReturnDate == twoDaysBeforeForReminder,
        include: b => b.Include(b => b.Item).
        Include(b => b.Member).
        ThenInclude(b => b!.User)!);

        if (borrows.Items.Count is not 0)
        {
            SmtpClient smtp = connectToSmtpServer();
            checkAuthenticationRequirement(smtp);

            List<MailboxAddress> receiversEmailInformations = borrows?.Items.Select(
                b => new MailboxAddress(b.Member.Name + b.Member.Surname, b.Member.User.Email)).ToList()!;

            string emailSubject = getBorrowEmailSubject();
            string emailHtmlBody = getBorrowEmailHtmlBody(twoDaysBeforeForReminder);
            await sendEmailAsync(receiversEmailInformations, emailHtmlBody, emailSubject, smtp);
        }
    }
    public async Task DeleteExpiredReservationsThenSendEmailToNecessaryMembersAsync()
    {
        IReservationService reservationService = _serviceProvider.GetRequiredService<IReservationService>();
        IPaginate<Reservation>? reservations = await reservationService.GetListAsync(
        r => r.ReservationDate.Date < DateTime.Today.Date,
        include: r => r.Include(r => r.Member).
        ThenInclude(m => m.User).
        Include(r => r.Item)!);

        if (reservations.Items.Count is not 0)
        {
            SmtpClient smtp = connectToSmtpServer();
            checkAuthenticationRequirement(smtp);

            List<MailboxAddress> receiversEmailInformations = reservations?.Items.Select(
                b => new MailboxAddress(b.Member.Name + b.Member.Surname, b.Member.User.Email)).ToList()!;
            string emailSubject = getReservationEmailSubject();
            string emailHtmlBody = getReservationEmailHtmlBody();
            await sendEmailAsync(receiversEmailInformations, emailHtmlBody, emailSubject, smtp);
        }

    }
    private string getBorrowEmailSubject()
    {
        return $"Ninova Kütüphanesi'nden Ödünç Alınan Ürün Hakkında";
    }
    private string getBorrowEmailHtmlBody(DateTime returnDate)
    {
        return $@"<!DOCTYPE html><html><head><style>body {{ font-family: Arial, sans-serif; background-color: #f5f5f5; }} .container {{ max-width: 600px; margin: auto; padding: 20px; background-color: #ffffff; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }} .message {{ margin-bottom: 20px; }} .info {{ background-color: #e5f3ff; padding: 10px; border-radius: 5px; }}</style></head><body><div class='container'><p>Sayın Ninova Kütüphane Üyesi,</p><p>Kütüphanemizden almış olduğunuz ürün <strong>{returnDate.ToShortDateString()}</strong> tarihine kadar kütüphanemize teslim edilmelidir.</p><div class='info'><p class='message'>{MailBusinessMessages.InformationalMessage}</p><p class='message'>{MailBusinessMessages.GoodDayMessage}</p></div></div></body></html>";
    }
    public string getReservationEmailSubject()
    {
        return $"Ninova Kütüphanesi'nde rezervasyon işlemi yapılmış ürün hakkında";
    }
    public string getReservationEmailHtmlBody()
    {
        return $@"<!DOCTYPE html><html lang='en'><head><meta charset='UTF-8'><meta name='viewport' 
            content='width=device-width, initial-scale=1.0'><title>Kütüphane Rezervasyon İptali</title><style>body 
            {{ font-family: Arial, sans-serif; max-width: 600px; margin: auto; padding: 20px; background-color: #f9f9f9; }}
            </style></head><body><p>Sayın Ninova kütüphanesi Üyesi,</p><p>Rezerve ettiğiniz ürün kütüphaneden alınmamıştır bu nedenle rezervasyonunuz iptal edilmiştir.</p><p style='margin-bottom: 20px;'>{MailBusinessMessages.InformationalMessage}
            </p><p>{MailBusinessMessages.GoodDayMessage}</p></body></html>";

    }
    private DateTime calculateReminderDate(DateTime today, int daysBeforeReminder)
    {
        return today.AddDays(daysBeforeReminder);
    }
    private void checkAuthenticationRequirement(SmtpClient smtp)
    {
        if (_mailSettings.AuthenticationRequired)
            smtp.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);
    }
    private SmtpClient connectToSmtpServer()
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.StartTls);
        return smtp;
    }
    public void ScheduleReminderEmailTwoDaysBeforeReturnDateAsync()
    {
        RecurringJob.AddOrUpdate("ScheduleReminderEmailTwoDaysBeforeReturnDateAsyncJob", () => 
        SendReminderEmailTwoDaysBeforeReturnDateAsync(), Cron.Daily((int)EnumForBackgroundJob.Three,00));
    }
    public void ScheduleDeleteExpiredReservationsThenSendEmailAsync()
    {
        RecurringJob.AddOrUpdate("ScheduleDeleteExpiredReservationsThenSendEmailJob", () => 
        DeleteExpiredReservationsThenSendEmailToNecessaryMembersAsync(), Cron.Daily((int)EnumForBackgroundJob.Three,00));
    }
}