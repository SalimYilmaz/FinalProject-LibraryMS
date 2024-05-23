using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MailService;
public interface IMailService
{
    public void ScheduleReminderEmailTwoDaysBeforeReturnDateAsync();
    public  void ScheduleDeleteExpiredReservationsThenSendEmailAsync();

    public Task DeleteExpiredReservationsThenSendEmailToNecessaryMembersAsync();
    public Task SendReminderEmailTwoDaysBeforeReturnDateAsync();
}
