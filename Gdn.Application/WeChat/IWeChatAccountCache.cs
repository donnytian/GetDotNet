using Abp.Domain.Entities.Caching;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public interface IWeChatAccountCache : IEntityCache<WeChatAccountDto, string>
    {
    }
}
