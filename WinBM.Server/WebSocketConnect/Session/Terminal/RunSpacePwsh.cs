using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WinBM.Server.WebSocketConnect.Session.Terminal
{
    internal class RunSpacePwsh : RunSpaceCmd
    {
        protected override string FileName { get; set; } = null;

        public RunSpacePwsh()
        {
            using (var proc = new Process())
            {
                proc.StartInfo.FileName = "where.exe";
                proc.StartInfo.Arguments = "pwsh";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.Start();
                proc.BeginOutputReadLine();
                proc.WaitForExit();

                string ret = proc.StandardOutput.ReadToEnd();
                using(var sr = new StringReader(ret))
                {
                    string readLine = sr.ReadLine();
                    if(readLine != null)
                    {
                        this.FileName = readLine;
                    }
                }
            }
        }
    }
}
