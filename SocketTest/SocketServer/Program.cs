using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    class Program
    {
        //static int port = 2895; // порт для приема входящих запросов
        static void Main(string[] args)
        {
            //    //Console.WriteLine("Hello World!");
            //    String strHostName = string.Empty;
            //    // Getting Ip address of local machine...
            //    // First get the host name of local machine.
            //    strHostName = Dns.GetHostName();
            //    Console.WriteLine("Local Machine's Host Name: " + strHostName);
            //    // Then using host name, get the IP address list..
            //    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            //    var addr = ipEntry.AddressList.ToList();
            //    addr.Add(IPAddress.Parse("127.0.0.1"));
            //    Console.WriteLine("Оберіть IP address");
            //    for (int i = 0; i < addr.Count; i++)
            //    {
            //        Console.WriteLine("{0}. {1} ", i + 1, addr[i].ToString());
            //    }
            //    Console.Write("->_");
            //    int selectIP = int.Parse(Console.ReadLine());

            //    // получаем адреса для запуска сокета
            //    IPEndPoint ipPoint = new IPEndPoint(addr[selectIP - 1], port);
            //    // получаем адреса для запуска сокета
            //    //IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            //    // создаем сокет
            //    Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    try
            //    {
            //        // связываем сокет с локальной точкой, по которой будем принимать данные
            //        listenSocket.Bind(ipPoint);

            //        // начинаем прослушивание
            //        listenSocket.Listen(10);

            //        Console.WriteLine("Сервер запущен. Ожидание подключений...");

            //        while (true)
            //        {
            //            Socket handler = listenSocket.Accept();
            //            // получаем сообщение
            //            StringBuilder builder = new StringBuilder();
            //            int bytes = 0; // количество полученных байтов
            //            byte[] data = new byte[256]; // буфер для получаемых данных

            //            do
            //            {
            //                bytes = handler.Receive(data);
            //                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            //            }
            //            while (handler.Available > 0);

            //            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

            //            // отправляем ответ
            //            string message = "ваше сообщение доставлено";
            //            data = Encoding.Unicode.GetBytes(message);
            //            handler.Send(data);
            //            // закрываем сокет
            //            handler.Shutdown(SocketShutdown.Both);
            //            handler.Close();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }



            IPAddress localAddr = IPAddress.Parse("192.168.1.2");
            int port = 2895;
            TcpListener server = new TcpListener(localAddr, port);            
            server.Start();
            // 
            while (true)
            {
                try
                {
                    // Подключение клиента
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    // Обмен данными
                    try
                    {
                        if (stream.CanRead)
                        {
                            byte[] myReadBuffer = new byte[1024];
                            StringBuilder myCompleteMessage = new StringBuilder();
                            int numberOfBytesRead = 0;
                            do
                            {
                                numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                            }
                            while (stream.DataAvailable);
                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + myCompleteMessage.ToString());
                            byte[] responseData = Encoding.UTF8.GetBytes("УСПЕШНО!");
                            stream.Write(responseData, 0, responseData.Length);
                        }
                    }
                    finally
                    {
                       // stream.Close();
                       // client.Close();
                    }
                }
                catch
                {
                    server.Stop();
                    break;
                }
            }




        }
    
    }
}
