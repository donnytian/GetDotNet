using System.Threading.Tasks;
using Abp.Application.Services;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public interface IWeChatAccountAppService : IAsyncCrudAppService<WeChatAccountDto, string>
    {
        /// <summary>
        /// Gets cached entity by ID.
        /// </summary>
        /// <remarks>
        /// It automatically invalidates cached entity if this entity is updated or deleted. Thus, it will be retrieved from database in the next call.
        /// </remarks>
        /// <param name="id">The account ID.</param>
        /// <returns>The account entity.</returns>
        Task<WeChatAccountDto> GetCached(string id);
    }
}
