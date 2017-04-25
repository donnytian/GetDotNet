using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Gdn.Authorization;

namespace Gdn.WeChat.Dto
{
    [AutoMap(typeof(Token))]
    public class TokenDto : EntityDto
    {
        public int? TenantId { get; set; }

        public string ClientName { get; set; }

        [Required]
        [StringLength(512)]
        public string ClientId { get; set; }

        [StringLength(1024)]
        public string AccessToken { get; set; }

        [StringLength(1024)]
        public string AccessTokenOld { get; set; }

        [StringLength(1024)]
        public string RefreshToken { get; set; }

        public DateTime? ExpiresAtUtc { get; set; }
    }
}
