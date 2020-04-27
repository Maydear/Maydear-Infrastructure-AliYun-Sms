using Microsoft.Extensions.Options;

namespace Maydear.Infrastructure.AliYun.Sms
{
    /// <summary>
    /// 阿里云手机短消息选项
    /// </summary>
    public class AliYunSmsOptions : IOptions<AliYunSmsOptions>
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 应用密钥
        /// </summary>
        public string AccessSecret { get; set; }

        /// <summary>
        /// 短信签名
        /// </summary>
        public string SignName { get; set; }


        AliYunSmsOptions IOptions<AliYunSmsOptions>.Value => this;
    }
}
