using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        //Порт отримувача.
        static int remotePort;

        //IP-адреса сервера(отримувача).
        static  IPAddress ipAddress;

        // Сокет
        static Socket listeningSocket;



        static void Main(string[] args)
        {
            
            Console.WriteLine("Enter IP server");
            ipAddress = IPAddress.Parse(Console.ReadLine());

            Console.WriteLine("Enter port server");
            remotePort = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter user name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your message");

            try
            {
                
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); 

                //Створення потоку
                Task listeningTask = new Task(Listen); 

                //Запуск потоку.
                listeningTask.Start(); 

                //відправка повідомлень від клієнтів в безкінечному циклі.
                while(true)
                {
                    //клієнт вводить повідомлення.
                    string message = Console.ReadLine();

                    //перетворення в масив байт введеного повідомлення клієнтом.
                    byte[] data = Encoding.Unicode.GetBytes(name +": "+message);

                    //встановлення кінцевої точки для доставки повідомлення.
                    EndPoint remotePoint = new IPEndPoint(ipAddress, remotePort);

                    //відправка перетвореного масиву байт по визначеній кінцевій точці.
                    listeningSocket.SendTo(data, remotePoint);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message.ToString());
            }
            finally
            {
                Close();
                
            }
           


        }

        private static void Close()
        {
            if (listeningSocket != null)
            {
                //блокування передачі та отримання даних для об"єкта Socket.
                listeningSocket.Shutdown(SocketShutdown.Both);

                listeningSocket.Close();
                listeningSocket = null;
            }
        }


        //поток для прослуховування підключень.
        private static void Listen()
        {
            IPEndPoint localPort = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);

            //приєднання до вказаних даних.
            listeningSocket.Bind(localPort);

            //далі будемо зчитувати і отримувати дані,що будуть введені іншими клієнтами.
            //Відповідні повідомлення будуть з"являтись для перегляду іншим клієнтам.
            try
            {
                //в безкінечному циклі прийматимуться повідомлення.
                while(true)
                {
                    //отримання повідомлення.
                    StringBuilder stringBuilder = new StringBuilder();

                    //кількість отриманитх байтів.
                    int bytes = 0;

                    //буфер для отриманих даних.
                    byte[] data = new byte[256];

                    //адреса,з якої прийшли дані.
                    EndPoint endPoint = new IPEndPoint(IPAddress.Any,0);

                    do
                    {
                        //отримання повідомлень.
                        bytes = listeningSocket.ReceiveFrom(data, ref endPoint);

                        //декодування діапазону байтів з масиву байтів в строку.
                        stringBuilder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (listeningSocket.Available > 0);//повертає кількість отриманих і доступних для читання даних.
                    
                    IPEndPoint pEndPoint = endPoint as IPEndPoint;
                    Console.WriteLine("{0}:{1} --->> {2}", pEndPoint.Address.ToString(), pEndPoint.Port, stringBuilder.ToString());
                }


            }
            catch(Exception ex)
            {
                Console.WriteLine("Error message"+ ex.Message.ToString());
            }
            finally
            {
                Close();
            }


        }
    }
}
