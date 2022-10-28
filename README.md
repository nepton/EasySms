# EasySms
[![Build status](https://ci.appveyor.com/api/projects/status/gv3je9e9u1glrj39?svg=true)](https://ci.appveyor.com/project/nepton/easysms)
[![CodeQL](https://github.com/nepton/EasySms/actions/workflows/codeql.yml/badge.svg)](https://github.com/nepton/EasySms/actions/workflows/codeql.yml)
![GitHub issues](https://img.shields.io/github/issues/nepton/EasySms.svg)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/nepton/EasySms/blob/master/LICENSE)

Template based aliyun sms sender service.

## Nuget packages

| Name                 | Version                                                                                                                   | Downloads                                                                                                                  |
|----------------------|---------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| EasySms.Abstractions | [![nuget](https://img.shields.io/nuget/v/EasySms.Abstractions.svg)](https://www.nuget.org/packages/EasySms.Abstractions/) | [![stats](https://img.shields.io/nuget/dt/EasySms.Abstractions.svg)](https://www.nuget.org/packages/EasySms.Abstractions/) |
| EasySms.Aliyun       | [![nuget](https://img.shields.io/nuget/v/EasySms.Aliyun.svg)](https://www.nuget.org/packages/EasySms.Aliyun/)             | [![stats](https://img.shields.io/nuget/dt/EasySms.Aliyun.svg)](https://www.nuget.org/packages/EasySms.Aliyun/)             |

# Usage
We are support Aliyun SMS service currently.

## Aliyun SMS

Configure follow settings in appsettins.json
```json
{
  "AliyunSms": {
    "ApiKey": "{Your api key}",
    "ApiSecret": "<Your api secret>",
    "SignName": "<Your signed>",
    "Templates": {
      "<TemplateName>": "<TemplateCode in aliyun>",
      "Login": "SMS_138011122"
    }
  }
}
```

Add service in Startup.cs
```csharp
services.AddAliyunSms();
```

Then you can use it in your code like this:
```csharp
class YourService
{
    private readonly ISmsSender _sender;

    public YourService(ISmsSender sender)
    {
        _sender = sender;
    }
    
    public async Task SendSmsAsync()
    {
        var template = new SmsTemplate("SMS_138011122", new {code = "123456"});
        var phoneNumber = "13800138000";
        await _sender.SendAsync(phoneNumber, template);
    }
}
```

This example will use template `SMS_138011122` send verification code `123456` to
phone number '13512345678' by aliyun sms service

The `templateName` must declared to Templates in appsettings.json,
service will get the code `SMS_138011122` by template name `Login` and send
code `123456` to aliyun.

The `parameter` will serialize using Json.NET to a string,
then pass to aliyun. The struct of parameter MUST equals with your template `SMS_138011122`

## Final
Leave a comment on GitHub if you have any questions or suggestions.

Turn on the star if you like this project.

## License
This project is licensed under the MIT License
