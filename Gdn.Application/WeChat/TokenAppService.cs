using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Gdn.Authorization;
using Gdn.WeChat.Dto;
using WeChat.Core;

namespace Gdn.WeChat
{
    public class TokenAppService : AsyncCrudAppService<Token, TokenDto, int, GetAllTokensInput>, ITokenAppService
    {
        private static readonly object SyncRoot = new object();
        private const int RefreshLimitInSecond = 60 * 20; // Refresh required if it's going to expire in 20 minutes.
        private static readonly string AppName = ConfigurationManager.AppSettings["WeChat.MP.AppName"];
        private static readonly string AppId = ConfigurationManager.AppSettings["WeChat.MP.AppId"];
        private static readonly string AppSecret = ConfigurationManager.AppSettings["WeChat.MP.AppSecret"];

        /// <inheritdoc />
        public TokenAppService(IRepository<Token> repository) : base(repository)
        {
        }

        /// <inheritdoc />
        protected override IQueryable<Token> CreateFilteredQuery(GetAllTokensInput input)
        {
            return base.CreateFilteredQuery(input).WhereIf(input?.ClientId != null, t => t.ClientId == input.ClientId);
        }

        /// <inheritdoc />
        public async Task<TokenDto> GetAccessTokenAsync()
        {
            return await GetAccessTokenAsync(AppName, AppId, AppSecret);
        }

        /// <inheritdoc />
        public async Task<TokenDto> GetAccessTokenAsync(string clientName, string clientId, string clientSecret)
        {
            if (clientId == null) throw new ArgumentNullException(nameof(clientId));
            if (clientSecret == null) throw new ArgumentNullException(nameof(clientSecret));

            var token = await GetExistingToken(clientId);

            if (token == null)
            {
                token = await RequestToken(clientName, clientId, clientSecret);
                await Create(token);
            }
            else if (token.ExpiresAtUtc != null && token.ExpiresAtUtc.Value.ToLocalTime()
                    .AddSeconds(-RefreshLimitInSecond) < DateTime.Now)
            {
                token = await RequestToken(clientName, clientId, clientSecret);
                await Update(token);
            }

            return token;
        }

        private async Task<TokenDto> GetExistingToken(string clientId)
        {
            var tokens = await GetAll(new GetAllTokensInput { ClientId = clientId });
            if (tokens.Items.Count > 0)
            {
                return tokens.Items[0].MapTo<TokenDto>();
            }

            return null;
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
