using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//处理玩家事件的脚本，都是触发事件，没有大用
namespace NetServer
{
    class HandlePlayerEvent
    {
        public void OnLogin(Player player)//上线
        {

        }
        public void OnLogout(Player player)//下线
        {
            if(player.status == Player.Status.Room)
            {
                Room room = player.room;
                RoomMgr.instance.LeaveRoom(player);
                if (room != null)
                    room.Broadcast(room.GetRoomInfo());
            }
        }
    }
}
