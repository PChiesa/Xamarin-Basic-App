using System;
using Prism.Events;

namespace BasicApp.UI.PubSubEvents
{
    public class QrCodeRefreshEvent : PubSubEvent<VoucherEventArgs>
    {
    }
}
