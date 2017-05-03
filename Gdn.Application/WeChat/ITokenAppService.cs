using System.Threading.Tasks;
using Abp.Application.Services;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public interface ITokenAppService : IAsyncCrudAppService<TokenDto, int, GetAllTokensInput>
    {
        /// <summary>
        /// Gets a valid access token based on client ID and secret from default configuration.
        /// </summary>
        /// <returns>Access token.</returns>
        Task<TokenDto> GetAccessTokenAsync();

        /// <summary>
        /// Gets a valid access token. The token might be refreshed if it's going to expire.
        /// </summary>
        /// <param name="clientName">Friendly and readable client name.</param>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret</param>
        /// <returns>Access token.</returns>
        Task<TokenDto> GetAccessTokenAsync(string clientName, string clientId, string clientSecret);
    }
}
