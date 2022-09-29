using System;
using Doulex.Sms.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Doulex.Sms.Aliyun
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public static class ServiceDependencyInjection
    {
        /// <summary>
        /// 添加阿里云短信发送服务
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="configureAction">配置参数节点</param>
        public static void AddAliyunSmsSender(this IServiceCollection services, Action<AliyunSmsConfig> configureAction)
        {
            var config = new AliyunSmsConfig();
            configureAction?.Invoke(config);

            services.AddSingleton(config);
            services.AddTransient<ISmsSender, AliyunSmsSender>();
        }
    }
}
