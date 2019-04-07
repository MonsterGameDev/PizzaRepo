using System.Threading.Tasks;

namespace Pizzaria.Services
{
    public interface IEmailService
    {
        Task SendEmail(string recipient, string subject, string mailBody);
    }
}