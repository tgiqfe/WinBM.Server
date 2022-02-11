using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.Server.WebSocketConnect.Session.Logon
{
    internal class LogonMessage
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DomainName { get; set; }
    }
}
