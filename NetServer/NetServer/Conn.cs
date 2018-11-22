using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

public class Conn
{

    public const int BUFFER_SIZE = 1024;
    public Socket socket;
    public bool isUse = false;
    public byte[] readBuff = new byte[BUFFER_SIZE];//字段存储池
    public int buffCount = 0;//当前读缓冲区长度,字符池里所有字符
    public Int32 msgLength = 0;//消息长度
    public byte[] lenBytes = new byte[sizeof(UInt32)];//转换成byte[]类型的消息长度
    public long lastTickTime = long.MinValue;//心跳时间
    //public GameObject player;//游戏角色对象，不应该出现在这里

    public Conn()
    {
        readBuff = new byte[BUFFER_SIZE];
    }

    public void Init(Socket socket)//这个应该是一个回调函数
    {
        this.socket = socket;
        isUse = true;
        buffCount = 0;
        //lastTickTime = Sys.GetTimeStamp();//心跳技术的实现
    }

    public int BuffRemain()//计算缓冲区剩余字节数
    {
        return BUFFER_SIZE - buffCount;
    }

    public string GetAddress()
    {
        if (!isUse)
        {
            return "无法获取地址";
        }
        return socket.RemoteEndPoint.ToString();
    }
    public void Close()
    {
        if (!isUse)
        {
            return;
        }
        Console.WriteLine("[断开]" + GetAddress());
        socket.Close();
        isUse = false;
    }

    public void Send(ProtocolType protocol)
    {

    }
}
