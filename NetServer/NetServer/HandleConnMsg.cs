using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    class HandleConnMsg
    {
        public void MsgHeatBeat(Conn conn, string msg)
        {
            conn.lastTickTime = System.GetTimeStamp();
            Console.WriteLine("[事件]" + conn.GetAddress());
        }
    }
}
