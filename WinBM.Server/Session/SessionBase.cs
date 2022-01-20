using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;

namespace WinBM.Server.Session
{
    internal class SessionBase
    {
        public virtual string Name { get { return this.GetType().Name; } }

        public SessionStatus Status { get; set; } = SessionStatus.None;

        public WebSocket WS { get; set; }

        #region Receiver side

        #endregion
        #region Sender side

        #endregion
    }
}
