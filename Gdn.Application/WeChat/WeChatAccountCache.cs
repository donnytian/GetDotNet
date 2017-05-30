using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public class WeChatAccountCache : EntityCache<WeChatAccount, WeChatAccountDto, string>, IWeChatAccountCache, ITransientDependency
    {
        public const string TokensCacheName = "GdnWeChatTokens";

        /// <inheritdoc />
        public WeChatAccountCache(ICacheManager cacheManager, IRepository<WeChatAccount, string> repository)
            : base(cacheManager, repository)
        {
        }
    }
}
