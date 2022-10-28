using System;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EasySms.Aliyun
{
    /// <summary>
    /// Ali big fish SMS sending interface
    /// </summary>
    public class AliyunSmsSender : ISmsSender
    {
        private readonly ILogger<AliyunSmsSender> _logger;
        private readonly AliyunSmsOptions         _options;

        public AliyunSmsSender(ILogger<AliyunSmsSender> logger, IOptionsSnapshot<AliyunSmsOptions> options)
        {
            _logger  = logger;
            _options = options.Value;
        }

        /// <summary>
        /// Send a text message
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="smsRequest"></param>
        /// <returns></returns>
        public Task<SmsPostResponse> SendAsync(string mobileNumber, SmsTemplate smsRequest)
        {
            try
            {
                _logger.LogInformation("Post SMS to {MobileNumber}, content is {@Content}", mobileNumber, smsRequest);

                var appKeyId  = _options.AppKeyId;
                var appSecret = _options.AppSecret;

                if (string.IsNullOrEmpty(appKeyId))
                    throw new InvalidOperationException("Configure the SMS AppKey");

                if (string.IsNullOrEmpty(appSecret))
                    throw new InvalidOperationException("Configure the SMS AppSecret");

                if (!_options.Templates.TryGetValue(smsRequest.TemplateName, out var templateCode))
                    throw new InvalidOperationException($"Configure the SMS template code: {smsRequest.TemplateName}");

                // prepare context for send sms
                var product = "Dysmsapi";              // Product name: Cloud communication SMS API product, developers do not need to replace
                var domain  = "dysmsapi.aliyuncs.com"; // Product domain name, developers do not need to replace
                var profile = DefaultProfile.GetProfile("cn-hangzhou", appKeyId, appSecret);
                profile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);

                var acsClient = new DefaultAcsClient(profile);
                var parameter = JsonConvert.SerializeObject(smsRequest.Parameter);

                var request = new SendSmsRequest
                {
                    PhoneNumbers  = mobileNumber,
                    SignName      = _options.SignName,
                    TemplateCode  = templateCode,
                    TemplateParam = parameter,
                    OutId         = ""
                };

                // Sending Messages
                var response = acsClient.GetAcsResponse(request);

                // Response
                if (response.Code != "OK")
                {
                    _logger.LogWarning("Post SMS failed to {MobileNumber}: {ResponseCode} {ResponseMessage}", mobileNumber, response.Code, response.Message);

                    switch (response.Code)
                    {
                        case "isv.MOBILE_NUMBER_ILLEGAL":
                            return Task.FromResult(SmsPostResponse.NumberIllegal);
                        case "isv.BUSINESS_LIMIT_CONTROL":
                            return Task.FromResult(SmsPostResponse.Limit);
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
                            return Task.FromResult(SmsPostResponse.ProviderError);
                    }
                }

                return Task.FromResult(SmsPostResponse.Succeeded);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp, "Unable to send SMS to {MobileNumber}", mobileNumber);
                return Task.FromResult(SmsPostResponse.Exception);
            }
        }
    };
}
