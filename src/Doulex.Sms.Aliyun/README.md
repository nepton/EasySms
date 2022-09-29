# What is this

Template based aliyun sms sender service.

# How to use

1. configure follow settings in appsettins.json
```json
"AliyunSmsSender": {
    "ApiKey": "{Your api key}",
    "ApiSecret": "<Your api secret>",
    "SignName": "<Your signed>",
    "Templates": {
      "<TemplateName>": "<TemplateCode in aliyun>",
      "Login": "SMS_138011122"
    }
  }
```

2. Injection interface 'ISmsSender'
```C#
class YourService
{
    private readonly ISmsSender _sender;

    public YourService(ISmsSender sender)
    {
        _sender = sender;
    }
}
```

3. Send sms
```C#
var templateName = "Login"
var parameterJsonObject = new {code = "123456"};
var content = new SmsContent(templateName, parameterJsonObject)
await _sender.SendAsync("13512345678", content);
```

This example will use template `SMS_138011122` send verification code `123456` to 
phone number '13512345678' by aliyun sms service


The `templateName` must declared to Templates in appsettings.json,
service will get the code `SMS_138011122` by template name `Login` and send
code `123456` to aliyun.

The `parameterJsonObject` will serialize using Json.NET to a string,
than pass to aliyun. The struct of parameter MUST equals with your template `SMS_138011122`
