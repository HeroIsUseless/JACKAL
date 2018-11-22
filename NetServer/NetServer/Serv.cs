using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using MySql;


namespace NetServer
{
    class Serv
    {
        public static Serv instance;
        public Socket listenfd;
        public Conn[] conns;//客户端连接
        public int maxConn = 50;
        public ProtocolBase proto;//用多态的形式使用协议
        public HandleConnMsg handleConnMsg = new HandleConnMsg();//这些是反射
        public HandlePlayerMsg handlePlayerMsg = new HandlePlayerMsg();
        public HandlePlayerEvent handlePlayerEvent = new HandlePlayerEvent();


        public Serv()
        {
            instance = this;
        }

        public void Start(string host, int port)//开启服务器
        {
            conns = new Conn[maxConn];//在初始化里初始了所有的conn
            for(int i=0; i<maxConn; i++)
            {
                conns[i] = new Conn();
            }
            listenfd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAdr = IPAddress.Parse(host);
            IPEndPoint ipEp = new IPEndPoint(ipAdr, port);
            listenfd.Bind(ipEp);
            listenfd.Listen(maxConn);
            listenfd.BeginAccept(AcceptCb, null);//在这里并没有定义什么object，也不知道这是从哪里发送的
            Console.WriteLine("[启动]成功");       
        }

        private void AcceptCb(IAsyncResult ar)//这个函数应该是刚连接上触发一次，用来给连接的socket分配id的
        {
            try
            {
                Socket socket = listenfd.EndAccept(ar);
                int index = NewIndex();
                if(index < 0)
                {
                    socket.Close();
                    Console.WriteLine("[警告]连接已满");
                }
                    else
                {
                    Conn conn = conns[index];
                    conn.Init(socket);
                    string adr = conn.GetAddress();
                    Console.WriteLine("[连接]" + adr + " count " + index);
                    conn.socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);
                }
                listenfd.BeginAccept(AcceptCb, null);
            }
            catch(Exception e)
            {
                Console.WriteLine("[错误]AcceptCb " + e.Message);
            }     
        }

        private void ReceiveCb(IAsyncResult ar)//这个函数是连接成功后，持续接受信息的
        {
            Conn conn = (Conn)ar.AsyncState;

            lock (conn)
            {
                try
                {
                    int count = conn.socket.EndReceive(ar);//异步读取操作，返回读取的字节数
                    if(count <= 0)
                    {
                        Console.WriteLine("[断开]ReceiveCb " + conn.GetAddress());
                        Console.WriteLine("[错误]读取字节失败 count:" + count);
                        //conn.Close();
                        return;
                    }
                    conn.buffCount += count;
                    ProcessData(conn);//处理数据
                    conn.socket.BeginReceive(conn.readBuff, conn.buffCount, conn.BuffRemain(), SocketFlags.None, ReceiveCb, conn);//继续接收
                }
                catch(Exception e)
                {
                    Console.WriteLine("[断开]" + conn.GetAddress());
                    Console.WriteLine("[断开]" + e.Message);
                    conn.Close();
                }
            }
         
        }

        private void ProcessData(Conn conn)
        {
            if (conn.buffCount < sizeof(Int32)) return;
            Array.Copy(conn.readBuff, conn.lenBytes, sizeof(Int32));//提取消息长度，消息的前4个字节
            conn.msgLength = BitConverter.ToInt32(conn.lenBytes, 0);//将字符串的长度也转换成bytes数组(消息长度)，返回一个整数，这个整数不表示自身
            if (conn.buffCount < conn.msgLength + sizeof(Int32)) return;
            string str = System.Text.Encoding.UTF8.GetString(conn.readBuff, sizeof(Int32), conn.msgLength);//现在是抽出真正的数据了
            Console.WriteLine("[信息]" + str);//一个直观的实验函数
            HandleMsg(str);//用字符串处理消息
            Broadcast(str);//全部分发，不用id

            //ProtocolBase protocol = proto.Decode(conn.readBuff, sizeof(Int32), conn.msgLength);//用协议处理消息
            //HandleMsg(conn, protocol);
            //Broadcast(protocol);

            int count = conn.buffCount - conn.msgLength - sizeof(Int32);//清除，这是剩下的字节的数量
            Array.Copy(conn.readBuff, sizeof(Int32) + conn.msgLength, conn.readBuff, 0, count);//清除抽出的数据
            conn.buffCount = count;//重新定义字符的长度，这样后面的就没用了
            if(conn.buffCount> 0)
            {
                ProcessData(conn);//又用了一个重载，我觉得不用重载，最后的消息就会清理不及时，一种的时间是接收，一种的时间是处理，处理必须小于接受
            }
            
        }

        private void HandleMsg(Conn conn, string str)
        {
            string name = ProtocolBase.GetName();
            string methodName = "Msg" + name;
        }
        if(Conn.player == null || name == "HeadBeat" || name == "Logout")
        {

        }

        private void HandleMsg(Conn conn, ProtocolBase protocolBase)
        {
            string name = protocolBase.GetName();
            if(name == "HeadBeat")//在这里进行消息的获取
            {
            }
        }

        public void Broadcast(string str)
        {
            for (int i = 0; i < conns.Length; i++)
            {
                if (!conns[i].isUse) continue;
                Send(conns[i], str);
            }
        }

        public void Broadcast(ProtocolBase protocol)
        {
            for(int i = 0; i<conns.Length; i++)
            {
                if (!conns[i].isUse) continue;
                Send(conns[i], protocol);
            }
        }

        public void Send(Conn conn, string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);//现在是分好的消息
            byte[] length = System.BitConverter.GetBytes(bytes.Length);//将指定数据转换成指定数组
            byte[] sendbuff = length.Concat(bytes).ToArray();
            try
            {
                conn.socket.BeginSend(sendbuff, 0, sendbuff.Length, SocketFlags.None, null, null);
            }
            catch(Exception e)
            {
                Console.WriteLine("[错误]Send " + e.Message);
            }
        }

        public void Send(Conn conn, ProtocolBase protocol)
        {
            byte[] bytes = protocol.Encode();
            byte[] length = BitConverter.GetBytes(bytes.Length);
            byte[] sendbuff = length.Concat(bytes).ToArray();
            try
            {
                conn.socket.BeginSend(sendbuff, 0, sendbuff.Length, SocketFlags.None, null, null);//尚未做消息分发功能，在这里进行广播
            }
            catch (Exception e)
            {
                Console.WriteLine("[错误]" + e.Message);
                Console.WriteLine("[错误]Serv.cs->Send()");
            }
            
        }

        public int NewIndex()
        {
            if(conns == null)
            {
                return -1;
            }
            for(int i=0; i<conns.Length; i++)
            {
                if(conns[i] == null)
                {
                    conns[i] = new Conn();
                    return i;
                }
                else if (conns[i].isUse == false)
                {
                    return i;
                }
            }
            return -1;
        }
        
    }
}
