using System.Threading.Tasks;

namespace App.Email
{
    public interface IEmailHelper
    {
        Task SendEmail(string from, string[] to, string body, string subject);
    }
}
