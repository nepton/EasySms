using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasySms.Aliyun
{
    /// <summary>
    /// Configure the service
    /// </summary>
    public static class AliyunSmsServiceExtensions
    {
        /// <summary>
        /// Add Ali Cloud SMS sending service
        /// </summary>
        public static IServiceCollection AddAliyunSms(this IServiceCollection services, Action<AliyunSmsOptions> configureAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configureAction == null) throw new ArgumentNullException(nameof(configureAction));

            services.Configure(configureAction);
            services.AddImplementation();
            return services;
        }

        /// <summary>
        /// Add Ali Cloud SMS sending service
        /// </summary>
        public static IServiceCollection AddAliyunSms(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<AliyunSmsOptions>(configuration);
            services.AddImplementation();
            return services;
        }

        private static IServiceCollection AddImplementation(this IServiceCollection services)
        {
            services.AddTransient<ISmsSender, AliyunSmsSender>();
            return services;
        }
    }
}
