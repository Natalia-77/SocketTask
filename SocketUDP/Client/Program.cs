using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            UdpClient udpc = new UdpClient("127.0.0.1", 2055);
            IPEndPoint ep = null;
            while (true)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                if (name == "") break;
                byte[] sdata = Encoding.ASCII.GetBytes(name);
                udpc.Send(sdata, sdata.Length);
                byte[] rdata = udpc.Receive(ref ep);
                string info = Encoding.ASCII.GetString(rdata);
                Console.WriteLine(info);
            }
        }
    }
}
