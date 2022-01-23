using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.Server.WebSocketConnect.Session.Terminal
{
    internal class RunSpacePowerShell : RunSpaceCmd
    {
        protected override string FileName { get; set; } = "powershell.exe";
    }
}
