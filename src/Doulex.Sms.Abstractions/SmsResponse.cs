namespace Doulex.Sms.Abstractions
{
    /// <summary>
    /// 短消息发送回应
    /// </summary>
    public enum SmsResponse
    {
        Succeeded,
        Exception,
        ProviderError,
        NumberIllegal,
        Limit,
    };
}
