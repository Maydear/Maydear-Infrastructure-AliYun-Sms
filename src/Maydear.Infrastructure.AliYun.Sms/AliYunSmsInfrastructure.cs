using Maydear.Infrastructure.AliYun.Sms.Exceptions;
using Maydear.Infrastructure.AliYun.Sms.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Maydear.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AliYunSmsInfrastructure : ISmsInfrastructure
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly AliYun.Sms.Internal.AliYunAcsClient aliYunAcsClient;

        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<AliYunSmsInfrastructure> logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliYunAcsClient"></param>
        /// <param name="logger"></param>
        public AliYunSmsInfrastructure(AliYunAcsClient aliYunAcsClient, ILogger<AliYunSmsInfrastructure> logger)
        {
            this.aliYunAcsClient = aliYunAcsClient;
            this.logger = logger;
        }

        private readonly Func<string, string> NationCodeChecks = a => string.IsNullOrWhiteSpace(a) ? "" : a;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        public bool Send(Infrastructure.Sms sms)
        {
            if(sms.Mode != SmsSendMode.TemplateCode)
                throw new AliYunSmsModeNotSupportedException();

            string phoneNumbers = string.Join(",", sms.Telephones.Select(a => $"{NationCodeChecks(a.NationCode)}{a.PhoneNumber}"));
            SendSmsResponse responseMessage = aliYunAcsClient.SendSms(phoneNumbers, sms.TemplateCode, Newtonsoft.Json.JsonConvert.SerializeObject(sms.TemplateParameters));

            if (responseMessage.Code.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                logger.LogError($"AliYunSmsError:{responseMessage.Message}({responseMessage.Code})");
                throw new AliYunSmsServiceException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsFunc"></param>
        /// <returns></returns>
        public bool Send(Func<Infrastructure.Sms> smsFunc)
        {
            return Send(smsFunc?.Invoke());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sms"></param>
        /// <param name="canceltoken"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(Infrastructure.Sms sms, CancellationToken canceltoken = default(CancellationToken))
        {
            return await Task.Run(() => Send(sms));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsFunc"></param>
        /// <param name="canceltoken"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(Func<Infrastructure.Sms> smsFunc, CancellationToken canceltoken = default(CancellationToken))
        {
            return await Task.Run(() => Send(smsFunc));
        }
    }
}
