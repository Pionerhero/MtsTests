using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnsTests
{
    [Serializable]
    public enum Browsers
    {
        [Description("Windows Internet Explorer")]
        InternetExplorer,

        [Description("Mozilla Firefox")]
        Firefox,

        [Description("Google Chrome")]
        Chrome
    }
}
