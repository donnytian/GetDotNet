using Abp.Application.Services.Dto;

namespace Gdn.WeChat.Dto
{
    public class GetAllTokensInput : PagedAndSortedResultRequestDto
    {
        public string ClientId { get; set; }
    }
}
