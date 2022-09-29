using System;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Doulex.Sms.Abstractions;
using Microsoft.Extensions.Logging;

namespace Doulex.Sms.Aliyun
{
    /// <summary>
    /// 阿里大鱼短消息发送接口
    /// </summary>
    public class AliyunSmsSender : ISmsSender
    {
        private readonly ILogger<AliyunSmsSender> _logger;
        private readonly AliyunSmsConfig   _configuration;

        public AliyunSmsSender(ILogger<AliyunSmsSender> logger, AliyunSmsConfig configuration)
        {
            _logger        = logger;
            _configuration = configuration;
        }


        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="smsRequest"></param>
        /// <returns></returns>
        public Task<SmsResponse> SendAsync(string mobileNumber, SmsContent smsRequest)
        {
            try
            {
                _logger.LogInformation("发送短信至 {mobileNumber}, 内容 {@content}.", mobileNumber, smsRequest);

                var appKeyId  = _configuration.AppKeyId;
                var appSecret = _configuration.AppSecret;

                if (string.IsNullOrEmpty(appKeyId))
                    throw new InvalidOperationException("请配置短信 AppKey");

                if (string.IsNullOrEmpty(appSecret))
                    throw new InvalidOperationException("请配置短信 AppSecret");

                if (!_configuration.Templates.TryGetValue(smsRequest.TemplateName, out var templateCode))
                    throw new InvalidOperationException($"请配置短信模板代码: {smsRequest.TemplateName}");

                // prepare context for send sms
                var product = "Dysmsapi";              // 产品名称:云通信短信API产品,开发者无需替换
                var domain  = "dysmsapi.aliyuncs.com"; // 产品域名,开发者无需替换
                var profile = DefaultProfile.GetProfile("cn-hangzhou", appKeyId, appSecret);
                profile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);

                var acsClient = new DefaultAcsClient(profile);
                var request = new SendSmsRequest
                {
                    PhoneNumbers  = mobileNumber,
                    SignName      = _configuration.SignName,
                    TemplateCode  = templateCode,
                    TemplateParam = smsRequest.TemplateParameter,
                    OutId         = ""
                };

                // 执行短信发送
                var response = acsClient.GetAcsResponse(request);

                // 处理回应
                if (response.Code != "OK")
                {
                    _logger.LogWarning("发送短信至 {mobileNumber} 错误: {responseCode} {responseMessage}", mobileNumber, response.Code, response.Message);

                    switch (response.Code)
                    {
                        case "isv.MOBILE_NUMBER_ILLEGAL":
                            return Task.FromResult(SmsResponse.NumberIllegal);
                        case "isv.BUSINESS_LIMIT_CONTROL":
                            return Task.FromResult(SmsResponse.Limit);
                        //case "isv.OUT_OF_SERVICE":
                        //case "isv.PRODUCT_UNSUBSCRIBE":
                        //case "isv.ACCOUNT_NOT_EXISTS":
                        //case "isv.ACCOUNT_ABNORMAL":
                        //case "isv.MOBILE_COUNT_OVER_LIMIT":
                        //case "isv.SMS_TEMPLATE_ILLEGAL":
                        //case "isv.SMS_SIGNATURE_ILLEGAL":
                        //case "isv.TEMPLATE_MISSING_PARAMETERS":
                        //case "isv.INVALID_PARAMETERS":
                        //case "isv.INVALID_JSON_PARAM":
                        default:
                            return Task.FromResult(SmsResponse.ProviderError);
                    }
                }

                return Task.FromResult(SmsResponse.Succeeded);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "无法发送短信至 {mobileNumber}", mobileNumber);
                return Task.FromResult(SmsResponse.Exception);
            }
        }
    };
}
