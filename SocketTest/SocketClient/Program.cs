using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        
        static int port = 2895; // порт сервера
        static string address = "192.168.1.2";//"127.0.0.1"; // адрес сервера

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //try
            //{
            //    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            //    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    // подключаемся к удаленному хосту
            //    socket.Connect(ipPoint);
            //    Console.Write("Введите сообщение:");
            //    string message = Console.ReadLine();
            //    byte[] data = Encoding.Unicode.GetBytes(message);
            //    socket.Send(data);

            //    // получаем ответ
            //    data = new byte[256]; // буфер для ответа
            //    StringBuilder builder = new StringBuilder();
            //    int bytes = 0; // количество полученных байт

            //    do
            //    {
            //        bytes = socket.Receive(data, data.Length, 0);
            //        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            //    }
            //    while (socket.Available > 0);
            //    Console.WriteLine("ответ сервера: " + builder.ToString());

            //    // закрываем сокет
            //    socket.Shutdown(SocketShutdown.Both);
            //    socket.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //Console.Read();



            TcpClient client = new TcpClient(address, port);

            //клієнт вводить повідомлення,яке буде передано далі серверу.
            Console.Write("Enter your message:");
            string message = Console.ReadLine();

            //перетворення в масив байт строкового повідомлення введеного клієнтом.
            byte[] data = Encoding.UTF8.GetBytes(message);

            //для взаємодії з сервером визначається метод GetStream(),яктй повертає об"єкт NetworkStream,через даний об"єкт
            //можна передавати повідомлення на сервер і отримувати повідомлення з серверу.
            NetworkStream stream = client.GetStream();

            try
            {
                // відправка повідомлення.Перший параметр-двійкові дані(що були перетворені вище),другий параметр-задає зсув
                // масива байт,з якого починається передача(зазвичай нуль,бо масив байт відправляється повністю),
                // третій -розмір двійкових даних(масив байт відправляється повністю).
                stream.Write(data, 0, data.Length);

                // отримання відповіді.Створємо масив байт,які отримує клієнт.
                byte[] readingData = new byte[256];

                //створення порожної строчки для запису туди вже перетворенимх даних.
                string responseData = string.Empty;

                StringBuilder completeMessage = new StringBuilder();

                //кількість считаних байтів.
                int numberbytes= 0;

                //поки в потоці є дані(властивість DataAvailable == true),відбувається поетапне формування
                //отриманої строки з сервера за допомогою класу StringBuilder,тими даними,що були прочитані в буфер на кожній ітерації.
                //Після завершення отримуємо готове строкове повідомлення.
                do
                {

                    numberbytes = stream.Read(readingData, 0, readingData.Length);
                    completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, numberbytes));
                }
                while (stream.DataAvailable);
                
                responseData = completeMessage.ToString();

                //Отримання відповіді клієнтом.
                Console.WriteLine(responseData);
                
            }
            finally
            {
                //після завершення передачі чи отримання даних,поток і серверне з"єднання повинні бути закриті.
                stream.Close();
                client.Close();
            }


        }
    }
}
