using AsynchUPD;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerUDP
{
    public class Server
    {
      
        private static int portReceive = 9060;       
        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private byte[] buffer = new byte[1024];

        //всі підключення унікальних користувачів.
        public List<ClientData> clientDatas = new List<ClientData>();
        private static IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, portReceive);
        private EndPoint endPoint = ipEndPoint;

        //делегат AsyncCallback,надалі буде посилатись на метод,який має відпрацювати
        //після завершення відповідної асинхронної операції.
        private AsyncCallback recv = null;
        public void Start()
        {
            socket.Bind(endPoint);

            //socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPoint,
            //new AsyncCallback(ReceiveCallback), socket);

            socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endPoint,
              recv= (ar)=>
               {
                   ClientData data = new ClientData();
                   data.Socket = socket;
                   data.EndPoint = endPoint;
                   if (!clientDatas.Contains(data))
                           clientDatas.Add(data);

                       int n = socket.EndReceiveFrom(ar, ref endPoint);
                   Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, n));

                   if (clientDatas.Count > 1)
                       foreach (var clientData in clientDatas)
                       {
                           clientData.Socket.SendTo(buffer, data.EndPoint);
                       }

                   data.Socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref data.EndPoint,
                       recv, socket);



               }, socket);

        }

        //private void ReceiveCallback(IAsyncResult ar)
        //{
        //    ClientData data = new ClientData();
        //    data.Socket = socket;
        //    data.EndPoint = endPoint;
        //    if (!clientDatas.Contains(data))
        //        clientDatas.Add(data);

        //    int n = socket.EndReceiveFrom(ar, ref endPoint);
        //    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, n));

        //    if (clientDatas.Count > 1)
        //        foreach (var clientData in clientDatas)
        //        {
        //            clientData.Socket.SendTo(buffer, data.EndPoint);
        //        }

        //    data.Socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref data.EndPoint,
        //        new AsyncCallback(ReceiveCallback), socket);
        //}
    }

    public class ClientData
    {
        public EndPoint EndPoint;
        public Socket Socket;
    }
    class Program
    {
        static void Main(string[] args)
        {           
            Server s = new Server();
            s.Start();
            Console.WriteLine("Enter \"fin\" to quit");
            Console.ReadKey();

        }
    }
}
