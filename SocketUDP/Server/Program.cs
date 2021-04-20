using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
           
            //у вікні клієнта вводяться імена,прописані нижче і після цього виведеться розширена інфо по них ,
            //отримана від сервера.
            UdpClient udpc = new UdpClient(2055);
            Console.WriteLine("Server started!");
            IPEndPoint ep = null;
            while (true)
            {
                byte[] rdata = udpc.Receive(ref ep);
                string name = Encoding.ASCII.GetString(rdata);
                string Info = string.Empty;
                if (name == "Ola")  Info = "Her name is Ola";
                if (name == "Valia") Info = "Her name is Valia";             
                byte[] sdata = Encoding.ASCII.GetBytes(Info);
                udpc.Send(sdata, sdata.Length, ep);
            }
        }
    }
}
