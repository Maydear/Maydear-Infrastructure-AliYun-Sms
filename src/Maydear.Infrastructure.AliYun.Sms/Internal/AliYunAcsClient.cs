using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Infrastructure.AliYun.Sms.Internal
{
    public class AliYunAcsClient
    {
        private readonly AliYunSmsOptions aliYunSmsOptions;
        private readonly IAcsClient acsClient;
        private readonly ILogger<AliYunAcsClient> logger;

        public AliYunAcsClient(IOptions<AliYunSmsOptions> options, ILogger<AliYunAcsClient> logger)
        {
            aliYunSmsOptions = options.Value;
            acsClient = new DefaultAcsClient(DefaultProfile.GetProfile("cn-hangzhou", aliYunSmsOptions.AccessKeyId, aliYunSmsOptions.AccessSecret));
            this.logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="templateCode"></param>
        /// <param name="templateParam"></param>
        /// <returns></returns>
        internal SendSmsResponse SendSms(string phoneNumbers, string templateCode, string templateParam)
        {
            CommonRequest request = new CommonRequest
            {
                Method = MethodType.POST,
                Domain = "dysmsapi.aliyuncs.com",
                Version = "2017-05-25",
                Action = "SendSms"
            };
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", phoneNumbers);
            request.AddQueryParameters("SignName", aliYunSmsOptions.SignName);
            request.AddQueryParameters("TemplateCode", templateCode);
            request.AddQueryParameters("TemplateParam", templateParam);
            try
            {
                CommonResponse response = acsClient.GetCommonResponse(request);
                SendSmsResponse sendSmsResponseMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<SendSmsResponse>(System.Text.Encoding.Default.GetString(response.HttpResponse.Content));
                return sendSmsResponseMessage;
            }
            catch (ServerException e)
            {
                logger.LogError(e, "Aliyuncs dysmsapi Server Exception");
                return null;
            }
            catch (ClientException e)
            {
                logger.LogError(e, "Aliyun Client Exception");
                return null;
            }
        }

        /// <summary>
        /// 查询发送记录
        /// </summary>
        /// <param name="phoneNumbers"></param>
        /// <param name="sendDate"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="bizId"></param>
        /// <returns></returns>
        internal QuerySmsResponse QuerySendRecords(string phoneNumbers, DateTime sendDate, int pageSize = 10, int currentPage = 1, string bizId = null)
        {
            CommonRequest request = new CommonRequest
            {
                Method = MethodType.POST,
                Domain = "dysmsapi.aliyuncs.com",
                Version = "2017-05-25",
                Action = "QuerySendDetails"
            };
            request.AddQueryParameters("PhoneNumber", phoneNumbers);
            request.AddQueryParameters("SendDate", sendDate.ToString("yyyyMMdd"));
            request.AddQueryParameters("PageSize", pageSize.ToString());
            request.AddQueryParameters("CurrentPage", currentPage.ToString());
            if (!string.IsNullOrWhiteSpace(bizId))
            {
                request.AddQueryParameters("BizId", bizId);
            }
            try
            {
                CommonResponse response = acsClient.GetCommonResponse(request);
                QuerySmsResponse querySmsResponseMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<QuerySmsResponse>(System.Text.Encoding.Default.GetString(response.HttpResponse.Content));
                return querySmsResponseMessage;
            }
            catch (ServerException e)
            {
                logger.LogError(e, "Aliyuncs dysmsapi Server Exception");
                return null;
            }
            catch (ClientException e)
            {
                logger.LogError(e, "Aliyun Client Exception");
                return null;
            }
        }
    }
}
