using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;

namespace WinBM.Server.WebSocketConnect.Session.Terminal
{
    public class TerminalMessage : SessionMessage
    {
        public override MessageType MessageType { get { return MessageType.Terminal; } }

        public ConsoleType ConsoleType { get; set; }

        public TerminalMessage() { }
        





        #region Receiver side


        #endregion
        #region Sender side



        #endregion
    }
}
