using Library.Application.Interfaces;
using Library.Application.Mailing.Models;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class MailingService : IMailingService
    {
        public Task SendEmailAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}