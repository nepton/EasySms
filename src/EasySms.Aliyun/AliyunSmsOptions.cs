using System.Collections.Generic;

namespace EasySms.Aliyun
{
    public class AliyunSmsOptions
    {
        /// <summary>
        /// APP Key
        /// </summary>
        public string AppKeyId { get; set; }

        /// <summary>
        /// App secret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// The signature in the SMS template
        /// </summary>
        public string SignName { get; set; }

        /// <summary>
        /// template of sms
        /// </summary>
        public Dictionary<string, string> Templates { get; set; } = new();
    }
}
