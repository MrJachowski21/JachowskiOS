using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Cosmos.System;

namespace JachowskiOS.System
{
    public static class ShutdownSystemCommand
    {
        public static void ShutdownSystem()
        {
            WriteMessage.WriteOK("Shutting down the system...");
            WriteMessage.WriteWarn("System shutdown in 3 seconds");
            Thread.Sleep(3000);
            Cosmos.System.Power.Shutdown();
        }
    }
}

