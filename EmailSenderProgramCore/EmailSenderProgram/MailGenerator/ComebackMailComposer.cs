using EmailSenderProgram.Providers;
using EmailSenderProgram.Services;
using EmailSenderProgram.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailSenderProgram.MailComposer
{
    class ComebackMailComposer : IMailComposer
    {
        private readonly IMessageService messageService;
        private readonly string voucher;

        private const string Email = "email";
        private const string Voucher = "voucher";

        public ComebackMailComposer(IMessageService messageService, string voucher)
        {
            this.voucher = voucher;
            this.messageService = messageService;
        }

        /// <summary>
        /// Send Customer ComebackMail
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public async Task<bool> Compose()
        {
#if !DEBUG
            if (!DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday))
            {
                return true;
            }
#endif
            Console.WriteLine("Send Comebackmail");
            try
            {
                var customersList = DataLayer.ListCustomers();
                var ordersList = DataLayer.ListOrders();

                var customersToNotify = customersList.Where(customer =>
                                                        !(ordersList.Any(order =>
                                                        string.Equals(order.CustomerEmail, customer.Email, StringComparison.OrdinalIgnoreCase))));

                foreach (var customer in customersToNotify)
                {
                    var messageData = new MessageData
                    {
                        To = new List<string>() { customer.Email },
                        //TODO: Get from appsttings
                        From = "infor@EO.com",
                        Data = new Dictionary<string, string>()
                        {
                            { Email ,customer.Email },
                            { voucher ,voucher }
                        }

                    };
                    var messageInput = new MessageInput
                    {
                        EventId = "01",
                        Data = messageData
                    };
#if !DEBUG
                    //TODO : await can be removed
                    await messageService.SendMailAsync(messageInput);
                    //TODO : move this into messageservice
#else
                    Console.WriteLine("Send mail to:" + customer.Email);
#endif
                } 
                //All mails are sent! Success!
                return true;
            }
            catch (Exception)
            {
                //Something went wrong :(
                return false;
            }
        }
    }
}
