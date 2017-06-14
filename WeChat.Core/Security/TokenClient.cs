using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace WeChat.Core
{
    /// <summary>
    /// Gets access_token for WeChat API invocations.
    /// </summary>
    public class TokenClient
    {
        // Token service endpoint.
        private const string EndPoint = "https://api.weixin.qq.com/cgi-bin/token";

        // OAuth grant type.
        private const string GrantType = "client_credential";

        /// <summary>
        /// Friendly name of client/app.
        /// </summary>
        public string ClientName { get; }

        /// <summary>
        /// Client / App ID.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Client / App secret.
        /// </summary>
        private string ClientSecret { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClient"/> class.
        /// </summary>
        /// <param name="clientName">The friendly or readable client name.</param>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret.</param>
        public TokenClient(string clientName, string clientId, string clientSecret)
        {
            ClientName = clientName;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClient"/> class.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret.</param>
        public TokenClient(string clientId, string clientSecret) : this(null, clientId, clientSecret) { }

        /// <summary>
        /// Sends a token request.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response object contains access token and expire time.</returns>
        public virtual async Task<TokenResponse> RequestAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            var client = new RestClient(EndPoint);
            var request = new RestRequest(Method.GET);
            request.AddQueryParameter(SecurityConstants.GrantType, GrantType);
            request.AddQueryParameter(SecurityConstants.AppId, ClientId);
            request.AddQueryParameter(SecurityConstants.Secret, ClientSecret);

            var response = await client.ExecuteTaskAsync<TokenResponse>(request, cancellationToken);

            return ParseResponse(response);
        }

        private TokenResponse ParseResponse(IRestResponse<TokenResponse> response)
        {
            var result = response.Data ?? new TokenResponse();
            result.ClientId = ClientId;
            result.ClientName = ClientName;

            if (response.ResponseStatus != ResponseStatus.Completed || response.StatusCode != HttpStatusCode.OK)
            {
                // Something goes wrong with network or service provider.
                result.Raw = response.Content;
                result.ErrorCode = ((int) response.StatusCode).ToString();
                result.ErrorMessage = response.ErrorMessage;
                return result;
            }

            result.Raw = response.Content;
            return result;
        }
    }
}
