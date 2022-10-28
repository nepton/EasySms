namespace EasySms
{
    /// <summary>
    /// A template message is sent in response
    /// </summary>
    public enum SmsPostResponse
    {
        Succeeded,
        Exception,
        ProviderError,
        NumberIllegal,
        Limit,
    };
}
