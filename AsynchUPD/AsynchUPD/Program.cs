using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsynchUPD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter user name:");
            string name = Console.ReadLine();
            Console.WriteLine("User name {0} \n-------", name);
            var ip = IPAddress.Parse("127.0.0.1");
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //sock1.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, 1);

            //Адреса і порт,звідки дані відправляються.
            IPEndPoint iep1 = new IPEndPoint(ip, 9060);

            //в циклі будуть надсилатись повідомлення.Як тільки буде натиснуто комбінацію клавіш для виходу,
            //цикл перерветься і ми вийдемо.
            var flag = true;
            do
            {

                var str = Console.ReadLine();              

                //комбінація для виходу
                if (str == "fin")
                {
                    flag = false;
                }

                //масив байт для перетворення повідомлення введеного користувачем зі строчки .
                byte[] data1 = Encoding.ASCII.GetBytes(name + ": " + str);

                //відправлення повідомлення.перший аргумент-перетворені в масив байт дані,другий-порт,звідки ми відправимо дані.
                sock.SendTo(data1, iep1);

            } while (flag);

            //закриття сокета.
            sock.Close();

            Console.ReadKey();

        }
    }
}
