using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Cle.MulticartApi.Client
{
    public class BaseApiClient
    {
        public string? BaseUrl { get; set; }

        protected virtual ValueTask PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
        {
            return PrepareRequestAsync(client, request, urlBuilder.ToString(), cancellationToken);
        }

        protected virtual ValueTask PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }
    }
}
