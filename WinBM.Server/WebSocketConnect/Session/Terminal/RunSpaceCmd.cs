using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.Server.WebSocketConnect.Session.Terminal
{
    internal class RunSpaceCmd : RunSpace
    {
        protected  override string FileName { get; set; } = "cmd.exe";

        private int inputLength = 0;
        private int inputCount = 0;
        private bool isInit = true;
        private bool isEnter = false;

        protected override void RegisterOutputThread()
        {
            Task.Run(() =>
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
                            string output = new string(buffer, 0, count);
                            if (!isInit)
                            {
                                if (isEnter)
                                {
                                    string tempOutput = output.Trim();
                                    if (inputLength == 0 && tempOutput == "")
                                    {
                                        continue;
                                    }
                                    inputCount += tempOutput.Length;
                                    if (inputCount <= inputLength)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        isEnter = false;
                                    }
                                }
                            }

                            var terminal = new TerminalMessage()
                            {
                                ConsoleType = ConsoleType.StandardOutput,
                                Content = output,
                            };
                            Output(terminal.GetPayload()).Wait();
                        }
                    }
                }
                catch { }
            }, _outputTokenSource.Token);
        }
    }
}
