using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Gdn.WeChat.Dto
{
    [AutoMap(typeof(WeChatAccount))]
    public class WeChatAccountDto : FullAuditedEntityDto<string>
    {
        public int TenantId { get; set; }

        public string Name { get; set; }

        public WeChatAccountType AccountType { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string Url { get; set; }

        public string Token { get; set; }

        public string EncodingAesKey { get; set; }

        public WeChatMessageSecurityMode? SecurityMode { get; set; }

        public string Description { get; set; }

    }
}
