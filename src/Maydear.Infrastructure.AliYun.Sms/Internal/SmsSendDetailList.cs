using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Infrastructure.AliYun.Sms.Internal
{
    /// <summary>
    /// 短信发送明细。
    /// </summary>
    internal class SmsSendDetailList
    {
        /// <summary>
        /// 短信发送明细。
        /// </summary>
        IEnumerable<SmsSendDetail> SmsSendDetailDTO { get; set; }
    }
}
