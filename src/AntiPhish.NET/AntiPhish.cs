using System;
using System.Collections.Generic;
using System.Text;

namespace AntiPhish.NET
{
    public static class AntiPhish
    {
        private static PhishingChecker _checker = new PhishingChecker();

        public static bool IsPhishingSite(string url)
        {
            var json = _checker.GetResult(
                    _checker.CheckUrlAsync(url).Result.Split(':')[1].Trim(new char[] { '\"', '}' }));

            return json;
        }
    }
}
