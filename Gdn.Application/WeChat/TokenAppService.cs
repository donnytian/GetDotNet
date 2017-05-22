using System;
using System.Configuration;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Runtime.Caching;
using Gdn.Configuration;
using Gdn.WeChat.Dto;
using WeChat.Core;

namespace Gdn.WeChat
{
    public class TokenAppService : GdnAppServiceBase, ITokenAppService
    {
        private readonly ICacheManager _cacheManager;
        private static readonly string AppName = ConfigurationManager.AppSettings["WeChat.MP.AppName"];
        private static readonly string AppId = ConfigurationManager.AppSettings["WeChat.MP.AppId"];
        private static readonly string AppSecret = ConfigurationManager.AppSettings["WeChat.MP.AppSecret"];

        /// <inheritdoc />
        public TokenAppService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        /// <inheritdoc />
        public async Task<TokenDto> GetAccessTokenAsync(string clientId)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));

            return await _cacheManager.GetCache<string, TokenDto>(WeChatAccountCache.TokensCacheName)
                .GetAsync(clientId, () => RequestToken(AppName, AppId, AppSecret));
        }

        /// <inheritdoc />
        public async Task<TokenDto> RequestAccessTokenAsync(string clientName, string clientId, string clientSecret)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));
            if (clientSecret == null) throw new ArgumentNullException(nameof(clientSecret));

            var token = await RequestToken(clientName, clientId, clientSecret);
            _cacheManager.GetCache(WeChatAccountCache.TokensCacheName).Set(clientId, token);

            return token;
        }

        private static async Task<TokenDto> RequestToken(string clientName, string clientId, string clientSecret)
        {
            var client = new TokenClient(clientName, clientId, clientSecret);
            var response = await client.RequestAsync();

            if (response.ErrorCode != null) throw new Exception(response.ErrorMessage);

            return response.MapTo<TokenDto>();
        }
    }
}
