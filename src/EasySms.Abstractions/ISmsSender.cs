using System.Threading.Tasks;

namespace EasySms
{
    /// <summary>
    /// Short message Sending Service
    /// </summary>
    public interface ISmsSender
    {
        /// <summary>
        /// Send templated message short message
        /// </summary>
        /// <param name="mobileNumber">Mobile phone no.</param>
        /// <param name="content">Message template</param>
        /// <returns></returns>
        Task<SmsPostResponse> SendAsync(string mobileNumber, SmsTemplate content);
    }
}

