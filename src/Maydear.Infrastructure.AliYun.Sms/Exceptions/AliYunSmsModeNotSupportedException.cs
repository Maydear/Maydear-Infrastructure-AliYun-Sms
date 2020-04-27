using System;

namespace Maydear.Infrastructure.AliYun.Sms.Exceptions
{
    /// <summary>
    /// (4001)阿里云短信服务仅支持模板码调用
    /// </summary>
    [Serializable]
    public class AliYunSmsModeNotSupportedException : Maydear.Exceptions.StatusCodeException
    {
        /// <summary>
        /// 错误：阿里云短信服务异常(4001).
        /// </summary>
        public AliYunSmsModeNotSupportedException()
            : base(4001, "阿里云短信服务仅支持模板码调用.") { }

        /// <summary>
        /// 错误：短信供应商服务异常(4001).
        /// </summary>
        /// <param name="excep">异常</param>
        public AliYunSmsModeNotSupportedException(Exception excep)
            : base(4001, "阿里云短信服务仅支持模板码调用.", excep) { }
    }
}
