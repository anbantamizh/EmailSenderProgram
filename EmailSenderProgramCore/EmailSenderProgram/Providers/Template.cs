using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Providers
{
    public class Template
    {
        public string TemplateId { get; set; }

        public string Name { get; set; }

        public string HtmlContent { get; set; }

        public string PlainContent { get; set; }

        public string Subject { get; set; }
    }
}
