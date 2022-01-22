using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net.WebSockets;

namespace WinBM.Server.WebSocketConnect.Session.Terminal
{
    internal class RunSpace : IDisposable
    {
        protected Process _process = null;
        protected CancellationTokenSource _outputTokenSource = new CancellationTokenSource();
        protected CancellationTokenSource _errorTokenSource = new CancellationTokenSource();
        protected object _outputLock = new object();

        protected const int BUFF_SIZE = 4096;

        protected virtual string FileName { get; set; } = "cmd.exe";
        protected virtual string Arguments { get; set; } = "";

        public virtual WebSocket Ws { get; set; }

        public RunSpace() { }

        public void Start()
        {

        }

        private void CreateProcess()
        {
            _process = new Process();
            _process.StartInfo.FileName = FileName;
            _process.StartInfo.Arguments = Arguments;
            _process.StartInfo.CreateNoWindow = true;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.Start();
            _process.EnableRaisingEvents = true;
            _process.Exited += new EventHandler(CloseEvent);
        }

        protected virtual void CloseEvent(object sender, EventArgs e)
        {
            Dispose();
        }

        protected virtual void RegisterOutputThread() { }

        /*
        private void RegisterOutputThread()
        {
            Task.Run((Action)(() =>
            {
                char[] buffer = new char[BUFF_SIZE];
                try
                {
                    while (true)
                    {
                        if (_outputTokenSource.Token.IsCancellationRequested) { break; }

                        int count = _process.StandardOutput.Read(buffer, 0, buffer.Length);
                        lock (_outputLock)
                        {
                            var terminal = new TerminalMessage()
                            {
                                ConsoleType = ConsoleType.StandardError,
                                Content = new string(buffer, 0, count),
                            };
                            Output(terminal.GetPayload()).Wait();
                        }
                    }
                }
                catch { }
            }), _outputTokenSource.Token);
        }
        */

        protected virtual void RegisterErrorThread()
        {
            Task.Run(() =>
            {
                char[] buffer = new char[BUFF_SIZE];
                try
                {
                    while (true)
                    {
                        if (_errorTokenSource.Token.IsCancellationRequested) { break; }

                        int count = _process.StandardError.Read(buffer, 0, buffer.Length);
                        lock (_outputLock)
                        {
                            var terminal = new TerminalMessage()
                            {
                                ConsoleType = ConsoleType.StandardError,
                                Content = new string(buffer, 0, count),
                            };
                            Output(terminal.GetPayload()).Wait();
                        }
                    }
                }
                catch { }
            }, _errorTokenSource.Token);
        }

        protected virtual async Task Output(ArraySegment<byte> output)
        {
            if (Ws != null && Ws.State == WebSocketState.Open)
            {
                await Ws.SendAsync(output, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public virtual void Input(string command)
        {
            _process.StandardInput.WriteLine(command.Trim());
        }


        #region Disposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

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
