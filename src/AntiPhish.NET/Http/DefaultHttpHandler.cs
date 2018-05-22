using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AntiPhish.NET.Http
{
    public class DefaultHttpHandler : HttpClientHandler
    {
        public DefaultHttpHandler() : base() { }
    }
}
