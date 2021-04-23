using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        // порт для прийому повідомлень.
        static int localPort; 
        static Socket listeningSocket; 

        //список унікальних клієнтів.
        static List<IPEndPoint> clients = new List<IPEndPoint>(); 

        static void Main(string[] args)
        {
            
            Console.WriteLine("This is a chat)))");
            Console.Write("Enter port for reseive message: ");
            localPort = int.Parse(Console.ReadLine());
            Console.WriteLine();

            try
            {
                //створення сокета.
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); 

                //створення потоку.
                Task listeningTask = new Task(Listen);

                //запуск потока.
                listeningTask.Start(); 

                //поток має бути зупинений і тільки тоді іде далі.
                listeningTask.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message.ToString());
                
            }
            finally
            {
                Close(); 
            }
        }
        private static void Listen()
        {
            try
            {
                //прослуховування за адремою.
                IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), localPort);

                //приєднання до вказаних даних.
                listeningSocket.Bind(localIP);

                while (true)
                {
                    StringBuilder builder = new StringBuilder();

                    //кількість отриманитх байтів.
                    int bytes = 0;

                    //буфер для отриманих даних.
                    byte[] data = new byte[256]; 

                    //адреса,з якої прийшли дані.
                    EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0); 

                    do
                    {
                        //отримання повідомлень.
                        bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);

                        //декодування діапазону байтів з масиву байтів в строку.
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    //повертає кількість отриманих і доступних для читання даних.
                    while (listeningSocket.Available > 0);

                    // дані про підключення.
                    IPEndPoint remotedata = remoteIp as IPEndPoint; 

                    //виводимо повідомлення в консолі сервера.
                    Console.WriteLine("{0}:{1} - {2}", remotedata.Address.ToString(), remotedata.Port, builder.ToString()); 

                    //перевірка,чи клієнт новий.
                    bool addClient = true;

                    //проходимо циклом по всім клієнтам.
                    for (int i = 0; i < clients.Count; i++)
                    {
                        //перевірка,чи адреса відправника співпадає з адресою клієнта у списку що є. 
                        if (clients[i].Address.ToString() == remotedata.Address.ToString())

                            //тоді нового клієнта не додаємо.
                            addClient = false;


                        //якщо немає твкого клієнта в списку.
                        if (addClient == true)
                        {
                            //тоді додаємо.
                            clients.Add(remotedata);
                        }
                    }

                    //розсилка повідомлення всім клієнтам крім самого віправника.
                    //формування байт з тексту.
                        byte[] datas = Encoding.Unicode.GetBytes(builder.ToString()); 

                    for (int i = 0; i < clients.Count; i++)
                    {
                        // чкщо адреса отримувача не співпадає з адресою відправника.
                        if (clients[i].Address.ToString() != remotedata.Address.ToString()) 

                            //тоді відправляє повідомлення.
                            listeningSocket.SendTo(data, clients[i]);
                    }                    
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
    }
}
