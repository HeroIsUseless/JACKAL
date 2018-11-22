using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    class RoomMgr//使用单例模式
    {

        public static RoomMgr instance;
        public List<Room> list = new List<Room>();//房间列表

        public RoomMgr()
        {
            instance = this;
        }

        public void CreateRoom(Player player)
        {
            Room room = new Room();
            lock(list)
            {
                list.Add(room);
                room.AddPlayer(player);
            }
        }

        public void LeaveRoom(Player player)//之所以把移除玩家放在这里，是因为一旦玩家为0，room也要销毁
        {
            if (player.status == Player.Status.None)
                return;
            Room room = player.room;
            lock(list)
            {
                room.DelPlayer(player.id);
                if (room.list.Count == 0)
                    list.Remove(room);
            }
        }

        public string GetRoomList()//获取房间列表
        {
            return "";
        }
    }
}
