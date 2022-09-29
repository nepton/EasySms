using System.Collections.Generic;

namespace Doulex.Sms.Aliyun
{
    public class AliyunSmsConfig
    {
        /// <summary>
        /// 应用 Key
        /// </summary>
        public string AppKeyId { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string SignName { get; set; }

        /// <summary>
        /// template of sms
        /// </summary>
        public Dictionary<string, string> Templates { get; set; }
    }
}
