using System.Threading.Tasks;

namespace Doulex.Sms.Abstractions
{
    /// <summary>
    /// 短信发送服务
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// 发送通知消息短信
        /// </summary>
        /// <param name="mobileNumber">手机号</param>
        /// <param name="content">短消息内容</param>
        /// <returns></returns>
        Task<SmsResponse> SendAsync(string mobileNumber, SmsContent content);
    }
}
