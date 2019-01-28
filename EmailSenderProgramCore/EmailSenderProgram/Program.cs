using EmailSenderProgram.MailComposer;
using EmailSenderProgram.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using EmailSenderProgram.Providers;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmailSenderProgram
{
    class Program
    {
        private static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {

            //var builder = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json");

            //var config = builder.Build();
                       
            var serviceProvider = new ServiceCollection()
                .AddTransient<IRenderingService, RenderingService>()
                .AddTransient<ITemplateProvider, TemplateProvider>()
                .AddTransient<IMessageService, MessageService>()
                .BuildServiceProvider();

            var isSuccess = true;

            try
            {
                var mailComposers = new List<IMailComposer>();
                var messageService = serviceProvider.GetService<IMessageService>();
                //TODO: Retrieve voucher logic
                mailComposers.Add(new ComebackMailComposer(messageService, "EOComebackToUs"));
                mailComposers.Add(new WelcomeMailComposer(messageService));
                var tasks = new Task<bool>[mailComposers.Count];
                for (int i = 0; i < mailComposers.Count; i++)
                {
                    tasks[i] = mailComposers[i].Compose();
                }
                for (int i = 0; i < mailComposers.Count; i++)
                {
                    isSuccess &= await tasks[i];
                }
            }
            catch (Exception ex)
            {
                //TODO log exception 
                isSuccess = false;
            }
            if (isSuccess == true)
            {
                Console.WriteLine("All mails are sent, I hope...");
            }
            else 
            {
                Console.WriteLine("Oops, something went wrong when sending mail (I think...)");
            }
            Console.ReadKey();
        }

    }
}
