using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AntiPhish.NET.Http
{
    internal class PhishService
    {
        #region Private Fields and Properties

        private string _scanId;
        private static string PostEndpoint { get; } = @"https://app.phish.ai/api/url/scan";
        private string GetEndpoint => _scanId == null ? null : $@"https://app.phish.ai/api/url/report?scan_id={_scanId}";

        #endregion            
        

    }
}
