using Aliyun.Acs.Core;

namespace Doulex.Sms.Aliyun
{
    public class SendSmsResponse : AcsResponse
    {
        public new string RequestId { get; set; }

        public string BizId { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}
