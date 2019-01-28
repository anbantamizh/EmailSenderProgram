using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderProgram.Services.Models
{
    public class MessageData
    {
        public string From { get; set; }

        public ICollection<string> To { get; set; }

        public ICollection<string> Bcc { get; set; }

        public IDictionary<string, string> Data { get; set; }
    }
}
