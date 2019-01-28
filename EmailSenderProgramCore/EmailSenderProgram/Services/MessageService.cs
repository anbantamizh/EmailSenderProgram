using EmailSenderProgram.Providers;
using EmailSenderProgram.Services.Models;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailSenderProgram.Services
{
    class MessageService : IMessageService
    {
        private readonly ITemplateProvider templateProvider;
        private readonly IRenderingService renderingService;

        //[Inject]
        public MessageService(ITemplateProvider templateProvider, IRenderingService renderingService)
        {
            this.templateProvider = templateProvider;
            this.renderingService = renderingService;
        }

        public async Task<bool> SendMailAsync(MessageInput messageInput)
        {
            if (messageInput == null)
            {
                throw new ArgumentNullException(nameof(messageInput));
            }
            try
            {
                var template = await templateProvider.GetTemplateById(messageInput.EventId);
                if (template == null)
                {
                    return false;
                }
                //TODO: rename data 
                var htmlContent = renderingService.Render(template.HtmlContent, messageInput.Data.Data);
                var subjectContent = renderingService.Render(template.Subject, messageInput.Data.Data);

                using (var message = new MailMessage())
                {
                    foreach (var emailAddress in messageInput.Data.To)
                    {
                        message.To.Add(emailAddress);
                    }

                    //foreach (var emailAddress in messageInput.Data.Bcc)
                    //{
                    //    message.To.Add(emailAddress);
                    //}

                    message.Subject = subjectContent;
                    message.Body = htmlContent;
#if !DEBUG
                    using (var smtpClient = new SmtpClient())
                    {
                        await smtpClient.SendMailAsync(message);
                    }
#endif
                }
            }
            catch (Exception ex)
            {
                //TODO : Log exception 
                //TODO : Move mail message to error queue 
                return false; 
            }
            return true;
        }
    }
}
