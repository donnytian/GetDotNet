using System.Threading.Tasks;
using Abp.Application.Services;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public interface ITokenAppService : IApplicationService
    {
        /// <summary>
        /// Gets access token based on client ID.
        /// </summary>
        /// <param name="clientId">The client ID.</param>
        /// <returns>Access token.</returns>
        Task<TokenDto> GetAccessTokenAsync(string clientId);

        /// <summary>
        /// Requests a new access token.
        /// </summary>
        /// <param name="clientName">Friendly and readable client name.</param>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret</param>
        /// <returns>Access token.</returns>
        Task<TokenDto> RequestAccessTokenAsync(string clientName, string clientId, string clientSecret);
    }
}
