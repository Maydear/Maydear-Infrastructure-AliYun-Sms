using Maydear.Infrastructure;
using Maydear.Infrastructure.AliYun.Sms;
using Maydear.Infrastructure.AliYun.Sms.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 腾信云短信服务注入
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 注册阿里云短信服务
        /// </summary>
        /// <param name="services">DI容器服务集合</param>
        /// <param name="setupAction">配置对象</param>
        /// <returns></returns>
        public static IServiceCollection AddAliYunSms(this IServiceCollection services, Action<AliYunSmsOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.AddSingleton<AliYunAcsClient>();
            services.AddSingleton<ISmsInfrastructure, AliYunSmsInfrastructure>();
            return services;
        }

        /// <summary>
        /// 注册阿里云短信服务
        /// </summary>
        /// <param name="services">DI容器服务集合</param>
        /// <param name="configuration">配置对象</param>
        /// <returns></returns>
        public static IServiceCollection AddAliYunSms(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return services.AddAliYunSms(op =>
            {
                op.SignName = configuration.GetSection("AliYun:Sms:SignName").Value;
                op.AccessKeyId = configuration.GetSection("AliYun:Sms:AccessKeyId").Value?? configuration.GetSection("AliYun:AccessKeyId").Value;
                op.AccessSecret = configuration.GetSection("AliYun:Sms:AccessSecret").Value?? configuration.GetSection("AliYun:AccessSecret").Value;
            });
        }
    }
}
