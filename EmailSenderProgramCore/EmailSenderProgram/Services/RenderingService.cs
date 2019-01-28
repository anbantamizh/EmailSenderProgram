using System.Collections.Generic;

namespace EmailSenderProgram.Services
{
    public interface IRenderingService
    {
        string Render(string template, IDictionary<string, string> data);
    }

    public class RenderingService : IRenderingService
    {
        //[Inject]
        public RenderingService()
        {

        }

        public string Render(string template, IDictionary<string, string> data)
        {
            if (string.IsNullOrEmpty(template))
            {
                return template;
            }
            foreach (var item in data)
            {
                var key = "{" + item.Key + "}";
                var replaceValue = string.IsNullOrEmpty(item.Value) ? string.Empty : item.Value;
                template = template.Replace(key, replaceValue);
            }
            return template;
        }
    }
}
