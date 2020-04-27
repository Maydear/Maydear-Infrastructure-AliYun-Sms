using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Infrastructure.AliYun.Sms.Internal
{
    /// <summary>
    /// 阿里云请求响应实体
    /// </summary>
    internal class ResponseBase
    {
        /// <summary>
        /// 状态码的描述。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 请求ID。
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 请求状态码。
        /// </summary>
        public string Code { get; set; }
    }
}
