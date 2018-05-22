using AntiPhish.NET.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AntiPhishNET
{
    public static class AntiPhish
    {
        private static PhishingClient client = new PhishingClient(new DefaultHttpHandler(), true);

        /// <summary>
        /// Scans the url string against the Phish.Ai Api to determine if it is NOT a phishing site.
        /// </summary>
        /// <param name="url">The url to scan.</param>
        /// <returns>True if not a phishing site, false otherwise.</returns>
        public async static Task<bool> IsNotPhishingSiteAsync(string url)
        {
            await client.VerifyUrlAsync(url);
            return await client.ParseResultAsync();
        }

        /// <summary>
        /// Scans the URI against the Phish.Ai Api to determine if it is NOT a phishing site.
        /// </summary>
        /// <param name="url">The URI object representing the url to scan.</param>
        /// <returns>True if not a phishing site, false otherwise.</returns>
        public async static Task<bool> IsNotPhishingSiteAsync(Uri url)
        {
            await client.VerifyUrlAsync(url);
            return await client.ParseResultAsync();
        }
    }
}
