namespace Gdn.WeChat
{
    /// <summary>
    /// Security mode for communication with WeChat server.
    /// </summary>
    public enum WeChatMessageSecurityMode
    {
        Unencrypted,
        Encrypted,
        Compatible,     // Send message in both encrypted and unencrypted, used for development only.
    }
}
