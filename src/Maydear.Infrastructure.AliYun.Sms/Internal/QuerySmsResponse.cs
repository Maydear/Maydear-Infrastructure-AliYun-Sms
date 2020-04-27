using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Infrastructure.AliYun.Sms.Internal
{
    /// <summary>
    /// 发送记录查询
    /// </summary>
    internal class QuerySmsResponse : ResponseBase
    {
        /// <summary>
        /// 短信发送明细集合。
        /// </summary>
        public SmsSendDetailList SmsSendDetailDTOs { get; set; }

        /// <summary>
        /// 短信发送总条数。
        /// </summary>
        public int TotalCount { get; set; }
    }
}
