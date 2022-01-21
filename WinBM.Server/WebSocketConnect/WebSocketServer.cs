using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Reflection;

namespace WinBM.Server.WebSocketConnect
{
    internal class WebSocketServer : IDisposable
    {
        private HttpListener _listener = null;

        public string[] ServerPrefixes { get; set; }

        public WebSocketServer()
        {

        }

        public async Task Start()
        {
            _listener = new HttpListener();
            ServerPrefixes.ToList().ForEach(x => _listener.Prefixes.Add(x));
            _listener.Start();
            while (true)
            {
                HttpListenerContext context = await _listener.GetContextAsync();

                //  接続元IP
                string remoteIP = context.Request.Headers["X-Real-IP"];

                //  接続元ホスト名
                string remoteHos = context.Request.Headers["Host"];

                if (context.Request.IsWebSocketRequest)
                {
                    ProcessRequest(context);
                }
                else
                {
                    //  WebSocket通信以外だった場合
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }
        }

        private async void ProcessRequest(HttpListenerContext context)
        {
            using (WebSocket ws = (await context.AcceptWebSocketAsync(subProtocol: null)).WebSocket)
            {


                /* WebSocket Receiver */


            }
        }


        #region Disposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
