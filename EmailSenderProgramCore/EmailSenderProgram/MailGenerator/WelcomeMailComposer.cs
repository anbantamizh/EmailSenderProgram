using EmailSenderProgram.Services;
using EmailSenderProgram.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderProgram.MailComposer
{
    class WelcomeMailComposer : IMailComposer
    {
        private readonly IMessageService messageService;

        private const string Email = "Email";
        private const string CustomerName = "name";


        public WelcomeMailComposer(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task<bool> Compose()
        {
            var isSuccess = true;

            try
            {
                Console.WriteLine("Send Welcomemail");

                //List all customers
                List<Customer> customerList = DataLayer.ListCustomers();
                //TODO Cache last executed datetime instead of calculating
                var newCustomers = customerList.Where(c => c.CreatedDateTime >= DateTime.Now.AddDays(-1)).ToList();
                //loop through list of new customers
                foreach (var customer in newCustomers)
                {

                    var messageData = new MessageData
                    {
                        To = new List<string>() { customer.Email },
                        From = "infor@EO.com",
                        Data = new Dictionary<string, string>()
                        {
                            { Email,customer.Email },
                            { CustomerName, null }
                        }

                    };
                    var messageInput = new MessageInput
                    {
                        EventId = "02",
                        Data = messageData
                    };
#if !DEBUG
                    Console.WriteLine("Send mail to:" + customer.Email);
#else
                    isSuccess &= await messageService.SendMailAsync(messageInput);
#endif
                }
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
