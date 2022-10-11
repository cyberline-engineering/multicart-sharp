using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cle.MulticartApi.Client
{
    /// <summary>
    /// Multicartshop client base class
    /// </summary>
    public class BaseApiClient
    {
        /// <summary>
        /// HttpClient base uri
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Prepare request before execute http client send 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="urlBuilder"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask PrepareRequestAsync(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder, CancellationToken cancellationToken)
        {
            return PrepareRequestAsync(client, request, urlBuilder.ToString(), cancellationToken);
        }

        /// <summary>
        /// Prepare request before execute http client send
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// Process response after receive http message
        /// </summary>
        /// <param name="client"></param>
        /// <param name="response"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual ValueTask ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }
    }
}
