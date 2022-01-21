using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;

namespace WinBM.Server.WebSocketConnect.Session
{
    public class CmdMessage : SessionMessage
    {
        public enum ConsoleType
        {
            StandardInput,
            StandardOutput,
            StandardError,
        }

        #region Receiver side

        public ConsoleType ReceiveType { get; set; }

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

        public ConsoleType SendType { get; set; }



        #endregion
    }
}
