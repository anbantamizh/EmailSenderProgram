using System.Threading.Tasks;

namespace EmailSenderProgram.MailComposer
{
    interface IMailComposer
    {
        Task<bool> Compose();
    }
}
