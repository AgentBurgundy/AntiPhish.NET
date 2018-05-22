using System;
using NUnit.Framework;

namespace AntiPhishNET.Test
{
    [TestFixture]
    public class AntiPhishTests
    {
        [TestCase("http://google.com")]
        [TestCase("http://bing.com")]
        [TestCase("http://youtube.com")]
        public void IsNotPhishingSite_StringUrl_Success(string url)
        {
            var result = AntiPhish.IsNotPhishingSiteAsync(url).Result;
            
            Console.WriteLine(result);

            Assert.That(result);
        }

        [Test]
        public void IsNotPhishingSite_Uri_Success()
        {
            Uri url = new Uri("http://google.com");

            var result = AntiPhish.IsNotPhishingSiteAsync(url).Result;

            Console.WriteLine(result);

            Assert.That(result);
        }
    }
}
