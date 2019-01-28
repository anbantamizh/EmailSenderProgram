using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmailSenderProgram.Providers;

namespace EmailSenderProgram.Tests.ProviderTests
{
    public class TemplateProviderTests
    {
        TemplateProvider templateProvider;
        
        [SetUp]
        public void SetUp()
        {
            templateProvider = new TemplateProvider();
        }

        [Test]
        public async Task GetTemplateById_ShouldReturnTemplateForCorrectTemplateId(string templateId)
        {

        }
    }
}
