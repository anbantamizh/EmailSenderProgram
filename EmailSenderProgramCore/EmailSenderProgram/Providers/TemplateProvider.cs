using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Providers
{
    public interface ITemplateProvider
    {
        Task<ICollection<Template>> GetTemplates();
        Task<Template> GetTemplateById(string templateId);
    }

    public class TemplateProvider : ITemplateProvider
    {
        //[Inject]
        public TemplateProvider()
        {  

        }

        public async Task<ICollection<Template>> GetTemplates()
        {
            return GetTemplatesFromLocal();
        }

        public async Task<Template> GetTemplateById(string templateId)
        {
            var templates = GetTemplatesFromLocal();
            return templates.Where(t => t.TemplateId == templateId).FirstOrDefault();
        }

        private ICollection<Template> GetTemplatesFromLocal()
        {
            var templateList = new List<Template>();
            templateList.Add(new Template
            {
                TemplateId = "01",
                Name = "Comeback",
                HtmlContent = @"Hi {email}
                              <br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for.
                              <br>Voucher: {voucher}
                              <br><br>Best Regards,<br>EO Team",
                PlainContent = string.Empty,
                Subject = "We miss you as a customer"

            });
            templateList.Add(new Template
            {
                TemplateId = "02",
                Name = "Welcome",
                HtmlContent = @"Hi {Email} <br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>EO Team",
                PlainContent = string.Empty,
                Subject = "Welcome {name}",
            });
            return templateList;
        }
    }
}
