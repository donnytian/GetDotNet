using System;
using RestSharp.Deserializers;

namespace WeChat.Core
{
    /// <summary>
    /// Response from an OAuth2 service provider.
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// The raw protocol response.
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// The clientID.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Friendly and readable name of client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// The access token.
        /// </summary>
        [DeserializeAs(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// The refresh token. This is not yet supported by WeChat MP.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The error code.
        /// </summary>
        [DeserializeAs(Name = "errcode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// The error message.
        /// </summary>
        [DeserializeAs(Name = "errmsg")]
        public string ErrorMessage { get; set; }

        private long _expiresIn;
        /// <summary>
        /// Seconds till the token expires returned by provider. Can be used for further calls of provider API.
        /// </summary>
        [DeserializeAs(Name = "expires_in")]
        public long ExpiresIn
        {
            get => _expiresIn;
            set
            {
                _expiresIn = value;
                ExpiresAt = _expiresIn > 0 ? DateTime.Now.AddSeconds(_expiresIn) : (DateTime?)null;
            }
        }

        /// <summary>
        /// The time when the token expires.
        /// </summary>
        public DateTime? ExpiresAt { get; private set; }
    }
}
