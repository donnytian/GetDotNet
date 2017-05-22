using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Gdn.WeChat
{
    /// <summary>
    /// Represents a WeChat account.
    /// </summary>
    public class WeChatAccount : FullAuditedAggregateRoot<string>, IMustHaveTenant
    {
        /// <inheritdoc />
        [MaxLength(64)]
        public override string Id { get; set; }

        /// <inheritdoc />
        public int TenantId { get; set; }

        /// <summary>
        /// Account name.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// Account type.
        /// </summary>
        public WeChatAccountType AccountType { get; set; }

        /// <summary>
        /// App ID to request access token.
        /// </summary>
        [MaxLength(64)]
        public string AppId { get; set; }

        /// <summary>
        /// App secret to request access token.
        /// </summary>
        [MaxLength(64)]
        public string AppSecret { get; set; }

        /// <summary>
        /// Server URL to provide response for the account.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Account token to verify requests from WeChat server.
        /// </summary>
        [MaxLength(64)]
        public string Token { get; set; }

        /// <summary>
        /// Encoding AES key for message encryption.
        /// </summary>
        [MaxLength(64)]
        public string EncodingAesKey { get; set; }

        /// <summary>
        /// Message security mode.
        /// </summary>
        public WeChatMessageSecurityMode? SecurityMode { get; set; }

        /// <summary>
        /// Account description.
        /// </summary>
        public string Description { get; set; }
    }
}
