using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace Gdn.Authorization
{
    /// <summary>
    /// Access token in OAuth2.
    /// </summary>
    public class Token :AuditedAggregateRoot, IMayHaveTenant
    {
        /// <inheritdoc />
        public int? TenantId { get; set; }

        /// <summary>
        /// Friendly name of client/app.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Client / App ID.
        /// </summary>
        [Required]
        [StringLength(512)]
        public string ClientId { get; set; }

        /// <summary>
        /// The access token.
        /// </summary>
        [StringLength(1024)]
        public string AccessToken { get; set; }

        /// <summary>
        /// The old or alternative access token.
        /// </summary>
        [StringLength(1024)]
        public string AccessTokenOld { get; set; }

        /// <summary>
        /// The refresh token.
        /// </summary>
        [StringLength(1024)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The time when the token expires in UTC.
        /// </summary>
        public DateTime? ExpiresAtUtc { get; set; }
    }
}
