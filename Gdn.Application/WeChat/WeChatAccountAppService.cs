using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public class WeChatAccountAppService : AsyncCrudAppService<WeChatAccount, WeChatAccountDto, string>, IWeChatAccountAppService
    {
        private readonly IWeChatAccountCache _weChatAccountCache;

        /// <inheritdoc />
        public WeChatAccountAppService(IRepository<WeChatAccount, string> repository, IWeChatAccountCache weChatAccountCache) : base(repository)
        {
            _weChatAccountCache = weChatAccountCache;
        }

        /// <inheritdoc />
        public async Task<WeChatAccountDto> GetCached(string id)
        {
            return await _weChatAccountCache.GetAsync(id);
        }
    }
}
