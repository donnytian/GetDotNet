using Abp.Application.Services;
using Gdn.WeChat.Dto;

namespace Gdn.WeChat
{
    public interface ITokenAppService : IAsyncCrudAppService<TokenDto, int, GetAllTokensInput>
    {
    }
}
