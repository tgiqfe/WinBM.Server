using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.Server.WebSocketConnect.Session
{
    internal enum SessionStatus
    {
        None = 0,       //  セッション開始前
        Begin = 1,      //  BeginReceiveProcess / BeginSendProcess
        Main = 2,       //  MainReceiveProcess / MainSendProcess
        End = 3,        //  EndReceiveProcess / EndSendProcess
        Finished = 4,   //  セッション終了済み
    }
}
