using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetServer
{
    public class ProtocolBase
    {
        public virtual ProtocolBase Decode(byte[] readbuff, int start, int length)//解码器
        {
            return new ProtocolBase();
        }

        public virtual byte[] Encode()//编码器
        {
            return new byte[] { };
        }
        //不同协议名称的协议交给不同的函数来处理
        public virtual string GetName()//协议名称，用于消息分发
        {
            return "";
        }
        //描述
        public virtual string GetDesc()//用于调试时比较直观地显示出协议的内容
        {
            return "";
        }

    }
}
