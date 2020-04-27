using Maydear.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample
{
    class Program
    {
        public static void Main(string[] args) => Run().GetAwaiter().GetResult();

        public static async Task Run()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(b =>
            {
                b.AddFilter((category, level) => true);
                b.AddConsole(c => c.IncludeScopes = true);
            });

            Configure(serviceCollection);

            var services = serviceCollection.BuildServiceProvider();

            Console.WriteLine("Creating a client...");
            var smsService = services.GetRequiredService<ISmsInfrastructure>();

            Console.WriteLine("Sending a request...");
            var data = await smsService.SendAsync(new Sms()
            {
                Content = "您的验证码为：{1}，请于{2}分钟内使用。如非本人操作，请忽略本短信。",
                Mode = SmsSendMode.TemplateCode,
                TemplateCode = "",
                Telephones = new Telephone[] { new Telephone() { NationCode = "86", PhoneNumber = "" } },
                TemplateParameters = new Dictionary<string, string>()
                {
                    { "code","123456"}
                }
            });
            Console.WriteLine("Response data:");
            Console.WriteLine(data);

            Console.WriteLine("Press the ANY key to exit...");
            Console.ReadKey();
        }


        public static void Configure(IServiceCollection services)
        {
            services.AddAliYunSms(a =>
            {
                a.AccessKeyId = "";
                a.AccessSecret = "";
                a.SignName = "";
            });
        }

    }
}
