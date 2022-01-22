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

        public async Task Process(WebSocket ws)
        {
            ArraySegment<byte> buff = new ArraySegment<byte>(new byte[1024]);
            while (ws.State == WebSocketState.Open)
            {
                WebSocketReceiveResult ret = await ws.ReceiveAsync(buff, CancellationToken.None);
                if (ret.MessageType == WebSocketMessageType.Text)
                {



                }
                else if (ret.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
            }
        }

        #endregion
        #region Sender side



        #endregion
    }
}
