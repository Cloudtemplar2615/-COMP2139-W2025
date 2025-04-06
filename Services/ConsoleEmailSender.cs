using System.Threading.Tasks;
using IEmailSender = Microsoft.AspNetCore.Identity.UI.Services.IEmailSender;
namespace COMP2139_ICE.Services
{
    public class ConsoleEmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine("=== MOCK EMAIL SENT ===");
            Console.WriteLine($"To: {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {htmlMessage}");
            Console.WriteLine("========================");
            return Task.CompletedTask;
        }
    }
}