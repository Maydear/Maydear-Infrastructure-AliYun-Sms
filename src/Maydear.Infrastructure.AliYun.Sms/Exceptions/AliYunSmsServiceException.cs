using System;

namespace Maydear.Infrastructure.AliYun.Sms.Exceptions
{
    /// <summary>
    /// (4000)阿里云短信服务异常
    /// </summary>
    [Serializable]
    public class AliYunSmsServiceException : Maydear.Exceptions.StatusCodeException
    {
        /// <summary>
        /// 错误：阿里云短信服务异常(4000).
        /// </summary>
        public AliYunSmsServiceException()
            : base(4000, "阿里云短信服务异常.") { }

        /// <summary>
        /// 错误：短信供应商服务异常(4000).
        /// </summary>
        /// <param name="excep">异常</param>
        public AliYunSmsServiceException(Exception excep)
            : base(4000, "阿里云短信服务异常.", excep) { }
    }
}
