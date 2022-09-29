using Newtonsoft.Json;

namespace Doulex.Sms.Abstractions
{
    /// <summary>
    /// 短信发送请求
    /// </summary>
    public class SmsContent
    {
        /// <summary>
        /// 初始化短信内容
        /// </summary>
        /// <param name="templateNameInConfiguration">在配置文件中设置的模板的名称</param>
        /// <param name="parameterJsonObject">参数对象, 该对象将被 JsonCovert 序列化为JSON字符串</param>
        public SmsContent(string templateNameInConfiguration, object parameterJsonObject)
        {
            TemplateName      = templateNameInConfiguration;
            TemplateParameter = JsonConvert.SerializeObject(parameterJsonObject);
        }

        /// <summary>
        /// 初始化短信内容
        /// </summary>
        /// <param name="templateNameInConfiguration">在配置文件中设置的模板的名称</param>
        /// <param name="parameterString">参数字符串, 该字符串将被直接发送</param>
        public SmsContent(string templateNameInConfiguration, string parameterString)
        {
            TemplateName      = templateNameInConfiguration;
            TemplateParameter = parameterString;
        }

        /// <summary>
        /// 模板, 请在配置文件中配置模板名对应的代码
        /// </summary>
        public string TemplateName { get; }

        /// <summary>
        /// 获取模板参数
        /// </summary>
        public string TemplateParameter { get; }
    }
}
