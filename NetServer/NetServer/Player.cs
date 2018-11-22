using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public class Player
    {
        public enum Status//状态
        {
            None,//并没有进入任何房间
            Room,//正在房间里等待
            Fight,//已经战斗上了
        }
        public string id;//玩家IP
        public Conn conn;//与conn连接起来
        public Room room;//room状态
        public Status status = Status.None;//状态
        public bool isOwner = false;//玩家是否为房主
        public int team = 1;//玩家阵营

        public Player(string id, Conn conn)
        {
            this.id = id;
            this.conn = conn;
        }
        public static bool KickOff(string id, string msg)
        {
            Conn[] conns = Serv.instance.conns;
            for(int i=0; i<conns.Length; i++)
            {
                if (conns[i] == null)
                    continue;
                if (!conns[i].isUse)
                    continue;
                if (conns[i].player == null)
                    continue;
                if(conns[i].player.id == id)
                {
                    lock(conns[i].player)
                    {
                        if (msg != null)
                            conns[i].player.Send(msg);

                        return conns[i].player.Lougout();
                    }
                }
            }
            return true;
        }
        //下线
        public bool Logout()
        {
            Serv.instance.handlePlayerEvent.OnLougout(this);
            if (!DataMgr.instance.SavePlayer(this));
                return false;
            conn.player = null;
            conn.Close();
            return true;

        }
        public void Send(string msg)//专门把这条信息发送给本客户端
        {
            
        }

    }
}
