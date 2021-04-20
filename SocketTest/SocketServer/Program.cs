using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    class Program
    {
        
        static void Main(string[] args)
        {             
                                  
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            int port = 2895;            
            TcpListener server = new TcpListener(localAddr, port);            
            server.Start();

            //після виклику методу Старт запускається безкінечний цикл,в якому об"єкт TcpListener буде очікувати 
            //підключення клієнта через метод AcceptTcpClient() і як тільки це відбудеться,створиться об"єкт 
            // TcpClient,який дозволить провести обмін даними по аналогії частині клієнта.
            while (true)
            {
                try
                {
                    // Підключення клієнта
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    // Обмін даними
                    try
                    {
                        //спочатку відбувається отримання повідомлення,а вже потім сервер відправляє повідомлення-відповідь.
                        if (stream.CanRead)
                        {
                            //спочатку відбувається отримання повідомлення,а вже потім сервер відправляє повідомлення-відповідь.
                            //Створємо масив байт,які отримує сервер.
                            byte[] myReadBuffer = new byte[1024];

                            StringBuilder myCompleteMessage = new StringBuilder();

                            //кількість считаних байтів.
                            int numbBytes = 0;
                            do
                            {
                                numbBytes = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(myReadBuffer, 0, numbBytes));
                            }
                            //поки є дані для считування(DataAvailable == true)
                            while (stream.DataAvailable);

                            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + myCompleteMessage.ToString());

                            //відправлення відповіді сервером.
                            byte[] responseData = Encoding.UTF8.GetBytes("Successfull!");
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
