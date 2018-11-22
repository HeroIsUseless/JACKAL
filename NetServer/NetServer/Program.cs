using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace NetServer
{
    class Program
    {
        static string str = "";

        static void Main(string[] args)
        {
            Console.WriteLine("[开启]服务器");
            RoomMgr roomMgr = new RoomMgr();
            Serv serv = new Serv();
            serv.proto = new ProtocolBytes();
            serv.Start("127.0.0.1", 1234);


            //Timer timer = new Timer();
            //timer.AutoReset = true;
            //timer.Interval = 1000;
            //timer.Elapsed += new ElapsedEventHandler(Tick);
            //timer.Start();

            //Thread t1 = new Thread(Add1);
            //t1.Start();
            //Thread t2 = new Thread(Add2);
            //t2.Start();
            //Thread.Sleep(1000);
            //Console.WriteLine(str);


            while (true)
            {
                string str = Console.ReadLine();
                switch(str)
                {
                    case "quit":
                        return;
                }
            }
            
        }

        public static void Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("心跳信息");
        }
        public static void Add1()
        {
            lock(str)
            {
                for (int i=0; i<20; i++)
                {
                    Thread.Sleep(10);
                    str += "A";
                }

            }
        
        }
        public static void Add2()
        {
            lock (str)
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(10);
                    str += "B";
                }

            }

        }
    }
}
