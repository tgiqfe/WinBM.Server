﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinBM.Server.WebSocketConnect.Session
{
    public enum MessageType
    {
        None,
        Logon,
        Init,
        Terminal,
        WinBM,
    }
}
