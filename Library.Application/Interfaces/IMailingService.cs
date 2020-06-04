using Library.Application.Mailing.Models;
using System.Threading.Tasks;

namespace Library.Application.Interfaces
{
    public interface IMailingService
    {
        Task SendEmailAsync(Message message);
    }
}