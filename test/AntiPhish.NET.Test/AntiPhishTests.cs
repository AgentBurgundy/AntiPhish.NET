using System;
using NUnit.Framework;

namespace AntiPhishNET.Test
{
    [TestFixture]
    public class AntiPhishTests
    {
        [Test]
        [TestCase("http://google.com")]
        [TestCase("http://bing.com")]
        [TestCase("http://youtube.com")]
        public void IsPhishing(string url)
        {
            var result = AntiPhish.IsPhishingSite(url);
            
            Console.WriteLine(result);

            Assert.That(result);
        }
    }
}
