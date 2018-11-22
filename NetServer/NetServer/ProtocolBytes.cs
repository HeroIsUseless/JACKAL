using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//规定字节流第一个参数必须是字符串，代表协议名称
//支持int（占用4字节），float（占用8字节）和string（有长度和内容两部分组成）三种数据类型
namespace NetServer
{
    public class ProtocolBytes : ProtocolBase
    {
        public byte[] bytes;//传输的字节流

        public override ProtocolBase Decode(byte[] readbuff, int start, int length)//解码器
        {
            ProtocolBytes protocol = new ProtocolBytes();
            protocol.bytes = new byte[length];
            Array.Copy(readbuff, start, protocol.bytes, 0, length);
            return protocol;
        }

        public override byte[] Encode()//编码器，返回字节流
        {
            return bytes;
        }

        public override string GetName()//获取协议的第一个字符串
        {
            return GetString(0);
        }

        public override string GetDesc()
        {
            string str = "";
            if (bytes == null) return str;
            for(int i=0; i<bytes.Length; i++)
            {
                int b = (int)bytes[i];
                str += b.ToString() + " ";
            }
            return str;
        }

        public void AddString(string str)//添加字符串
        {
            Int32 len = str.Length;
            byte[] lenBytes = BitConverter.GetBytes(len);
            byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(str);
            if (bytes == null)
                bytes = lenBytes.Concat(strBytes).ToArray();
            else
                bytes = bytes.Concat(lenBytes).Concat(strBytes).ToArray();
        }

        public string GetString(int start, ref int end)//从字节数组的start处开始读取字符串
        {
            if (bytes == null) return "";
            if (bytes.Length < start + sizeof(Int32)) return "";
            Int32 strLen = BitConverter.ToInt32(bytes, start);
            if (bytes.Length < start + sizeof(Int32) + strLen) return "";
            string str = System.Text.Encoding.UTF8.GetString(bytes, start + sizeof(Int32), strLen);
            end = start + sizeof(Int32) + strLen;
            return str;
        }

        public string GetString(int start)//封装一下，简化
        {
            int end = 0;
            return GetString(start, ref end);
        }

        public void AddInt(int num)
        {
            byte[] numBytes = BitConverter.GetBytes(num);
            if (bytes == null)
                bytes = numBytes;
            else
                bytes = bytes.Concat(numBytes).ToArray();
        }

        public int GetInt(int start, ref int end)
        {
            if (bytes == null) return 0;
            if (bytes.Length < start + sizeof(Int32)) return 0;
            return BitConverter.ToInt32(bytes, start);
        }

        public int GetInt(int start)
        {
            int end = 0;
            return GetInt(start, ref end);
        }

        public void AddFloat(float num)
        {
            byte[] numBytes = BitConverter.GetBytes(num);
            if (bytes == null)
                bytes = numBytes;
            else
                bytes = bytes.Concat(numBytes).ToArray();
        }

        public float GetFloat(int start, ref int end)
        {
            if (bytes == null) return 0;
            if (bytes.Length < start + sizeof(float))
                return 0;
            end = start + sizeof(float);
            return BitConverter.ToSingle(bytes, start);//实现了将byte数组start位置后的8个字节转换成float数据
        }

        public float GetFloat(int start)
        {
            int end = 0;
            return GetFloat(start, ref end);
        }

    }
}
