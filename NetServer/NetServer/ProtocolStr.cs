using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//字符串协议模型
//形式 名称， 参数1， 参数2， 参数3，各个参数用逗号结束
//不是好协议，仅作联系用
namespace NetServer
{
    public class ProtocolStr : ProtocolBase
    {
        public string str;//传输的字符串
        public override ProtocolBase Decode(byte[] readbuff, int start, int length)//解码器，将字节流转换为字符串
        {
            ProtocolStr protocol = new ProtocolStr();
            protocol.str = System.Text.Encoding.UTF8.GetString(readbuff, start, length);
            return (ProtocolBase)protocol;
        }
        
        public override byte[] Encode()
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(str);
            return b;
        }

        public override string GetName()//获取协议名称
        {
            if (str.Length == 0) return "";
            return str.Split(',')[0];//究竟是用空格分隔还是用逗号分隔是在这里规定的
        }
        public override string GetDesc()//用字符串代表协议描述
        {
            return str;//是全部返回了
        }
    }
}
