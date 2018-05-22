using AntiPhish.NET.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AntiPhish.NET.Http
{
    internal class PhishingClient : HttpClient
    {
        #region Private Fields and Properties

        private string _scanId;

        private string PostEndpoint => @"https://app.phish.ai/api/url/scan";
        private string GetEndpoint
        {
            get
            {
                if (_scanId == null)
                    return null;

                return $@"https://app.phish.ai/api/url/report?scan_id={_scanId}";
            }
        }

        #endregion        

        #region Constructors

        internal PhishingClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
        { }

        #endregion        

        #region Verify Url Async

        internal async Task VerifyUrlAsync(string targetUrl)
        {
            var urlToCheck = new Dictionary<string, string>
            {
                { "url", targetUrl }
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(urlToCheck);

            HttpResponseMessage response = await PostAsync(PostEndpoint, content);

            _scanId = response.Content.ReadAsStringAsync().Result.Split(':')[1].Trim(new char[] { '\"', '}' });
        }

        internal async Task VerifyUrlAsync(Uri targetUrl)
        {
            var urlToCheck = new Dictionary<string, string>
            {
                { "url", targetUrl.AbsoluteUri }
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(urlToCheck);

            HttpResponseMessage response = await PostAsync(PostEndpoint, content);

            _scanId = response.Content.ReadAsStringAsync().Result.Split(':')[1].Trim(new char[] { '\"', '}' });
        }

        #endregion

        #region Scan Result Methods

        private async Task<string> GetScanResultAsync()
        {
            if (string.IsNullOrEmpty(_scanId))
                throw new ScanNotCompletedException("Scan Id could not be found. This is probably due to not actually scanning a web page before calling this method.");

            return await GetStringAsync(GetEndpoint);
        }

        // TODO: This method has opportunity for refactoring to increase speed.
        internal async Task<bool> ParseResultAsync(string scanId = null)
        {
            if (scanId == null)
                scanId = _scanId;

            var result = await GetScanResultAsync();

            var deserializedResults = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            string status;
            string verdict;

            deserializedResults.TryGetValue("status", out status);
            deserializedResults.TryGetValue("verdict", out verdict);

            if (!string.IsNullOrEmpty(status) && status != "in progress")
                if (verdict == "clean")
                    return true;
                else
                    return false;
            else
            {
                return await ParseResultAsync();
            }
        }

        #endregion        
    }
}
