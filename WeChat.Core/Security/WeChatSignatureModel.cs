using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WeChat.Core
{
    /// <summary>
    /// Represents the model that been sent from WeChat server to verify the URL setting in MP.
    /// </summary>
    public class WeChatSignatureModel
    {
        public string Signature { get; set; }

        public string Timestamp { get; set; }

        public string Nonce { get; set; }

        public string EchoStr { get; set; }

        /// <summary>
        /// Verifies whether this request is from WeChat server.
        /// </summary>
        /// <param name="token">The token that set by account owner in WeChat MP system.</param>
        /// <returns>True if the request is verified; otherwise false.</returns>
        public bool Verify(string token)
        {
            return ComputeHash(token) == Signature;
        }

        /// <summary>
        /// Computes the SHA1 hash based on input token.
        /// </summary>
        /// <param name="token">The token that set by account owner in WeChat MP system.</param>
        /// <returns>The SHA1 hash string.</returns>
        public string ComputeHash(string token)
        {
            var array = new[] { token, Timestamp, Nonce }.OrderBy(s => s, StringComparer.Ordinal);

            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                var s = string.Join(string.Empty, array);
                var bytes = sha1.ComputeHash(Encoding.ASCII.GetBytes(s));
                var sb = new StringBuilder();
                foreach (var b in bytes)
                {
                    sb.AppendFormat("{0:x2}", b);
                }

                return sb.ToString();
            }

        }
    }
}
