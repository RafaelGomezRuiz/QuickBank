using QuickBank.Core.Application.Dtos.Email;

namespace QuickBank.Core.Application.Interfaces.Services.Facilities
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
