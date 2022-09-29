using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Core.Utils;

namespace Doulex.Sms.Aliyun
{
    public class SendSmsRequest : RpcAcsRequest<SendSmsResponse>
    {
        public SendSmsRequest() : base("Dysmsapi", "2017-05-25", "SendSms")
        {
        }

        private string _templateCode;

        private string _phoneNumbers;

        private string _accessKeyId;

        private string _signName;

        private string _resourceOwnerAccount;

        private string _templateParam;

        private string _action;

        private long? _resourceOwnerId;

        private long? _ownerId;

        private string _smsUpExtendCode;

        private string _outId;

        public string TemplateCode
        {
            get => _templateCode;
            set
            {
                _templateCode = value;
                DictionaryUtil.Add(QueryParameters, "TemplateCode", value);
            }
        }

        public string PhoneNumbers
        {
            get => _phoneNumbers;
            set
            {
                _phoneNumbers = value;
                DictionaryUtil.Add(QueryParameters, "PhoneNumbers", value);
            }
        }

        public string AccessKeyId
        {
            get => _accessKeyId;
            set
            {
                _accessKeyId = value;
                DictionaryUtil.Add(QueryParameters, "AccessKeyId", value);
            }
        }

        public string SignName
        {
            get => _signName;
            set
            {
                _signName = value;
                DictionaryUtil.Add(QueryParameters, "SignName", value);
            }
        }

        public string ResourceOwnerAccount
        {
            get => _resourceOwnerAccount;
            set
            {
                _resourceOwnerAccount = value;
                DictionaryUtil.Add(QueryParameters, "ResourceOwnerAccount", value);
            }
        }

        public string TemplateParam
        {
            get => _templateParam;
            set
            {
                _templateParam = value;
                DictionaryUtil.Add(QueryParameters, "TemplateParam", value);
            }
        }

        public string Action
        {
            get => _action;
            set
            {
                _action = value;
                DictionaryUtil.Add(QueryParameters, "Action", value);
            }
        }

        public long? ResourceOwnerId
        {
            get => _resourceOwnerId;
            set
            {
                _resourceOwnerId = value;
                DictionaryUtil.Add(QueryParameters, "ResourceOwnerId", value.ToString());
            }
        }

        public long? OwnerId
        {
            get => _ownerId;
            set
            {
                _ownerId = value;
                DictionaryUtil.Add(QueryParameters, "OwnerId", value.ToString());
            }
        }

        public string SmsUpExtendCode
        {
            get => _smsUpExtendCode;
            set
            {
                _smsUpExtendCode = value;
                DictionaryUtil.Add(QueryParameters, "SmsUpExtendCode", value);
            }
        }

        public string OutId
        {
            get => _outId;
            set
            {
                _outId = value;
                DictionaryUtil.Add(QueryParameters, "OutId", value);
            }
        }

        public override SendSmsResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            var sendSmsResponse = new SendSmsResponse
            {
                HttpResponse = unmarshallerContext.HttpResponse,
                RequestId    = unmarshallerContext.StringValue("SendSms.RequestId"),
                BizId        = unmarshallerContext.StringValue("SendSms.BizId"),
                Code         = unmarshallerContext.StringValue("SendSms.Code"),
                Message      = unmarshallerContext.StringValue("SendSms.Message")
            };

            return sendSmsResponse;
        }
    }
}
