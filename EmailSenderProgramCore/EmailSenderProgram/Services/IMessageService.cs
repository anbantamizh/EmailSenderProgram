using EmailSenderProgram.Services.Models;
using System.Threading.Tasks;

namespace EmailSenderProgram.Services
{
    interface IMessageService
    {
        Task<bool> SendMailAsync(MessageInput messageInput);
    }
}
