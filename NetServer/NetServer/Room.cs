using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public class Room
    {
        public enum Status
        {
            Prepare = 1,
            Fight = 2,
        }
        public Status status = Status.Prepare;
        public int maxPlayers = 6;//最大玩家数量
        public Dictionary<string, Player> list = new Dictionary<string, Player>();//保存着该房间玩家信息

        public bool AddPlayer(Player player)
        {
            lock(list)
            {
                if (list.Count >= maxPlayers)
                    return false;
                player.room = this;
                player.team = SwitchTeam();
                player.status = Player.Status.Room;

                if (list.Count == 0)
                    player.isOwner = true;//他是房主
                string id = player.id;
                list.Add(id, player);
            }
            return true;
        }
        
        public int SwitchTeam()//分配队伍
        {
            int count1 = 0;
            int count2 = 0;
            foreach(Player player in list.Values)
            {
                if (player.team == 1) count1++;
                if (player.team == 2) count2++;
            }
            if (count1 <= count2)
                return 1;
            else
                return 2;
        }
        public void DelPlayer(string id)//删除玩家
        {
            lock(list)
            {
                if (!list.ContainsKey(id))
                    return;
                bool isOwner = list[id].isOwner;
                list[id].status = Player.Status.None;//出房间
                list.Remove(id);
                if (isOwner)
                    UpdataOwner();
            }
        }
        public void UpdataOwner()//更换房主
        {
            lock(list)
            {
                if(list.Count <= 0)
                {
                    return;
                }
                foreach(Player player in list.Values)
                {
                    player.isOwner = false;
                }
                Player p = list.Values.First();
                p.isOwner = true;
            }
        }
        public void Broadcast(string msg)//房间内的广播信息
        {
            foreach(Player player in list.Values)
            {
                player.Send(msg);//将信息发送出去
            }
        }
        public string GetRoomInfo()//获取房间信息
        {
            return "";
        }
    }
}
