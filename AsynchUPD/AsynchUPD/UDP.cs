using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsynchUPD
{
    //простий приклад асинхронної роботи сокетів клієнта.
    //за основу було взято інформацію з https://docs.microsoft.com/

    public class UDP
    {
        //public Socket _socket;
        //private const int _bufsize = 1024;

        ////Адреса,звідки прийшло повідомлення(дані).
        //private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);

        //private State state = new State();

        ////делегат AsyncCallback,надалі буде посилатись на метод,який має відпрацювати
        ////після завершення відповідної асинхронної операції.
        //private AsyncCallback recv = null;



        ////Об"єкт стану для отримання даних з сокету клієнта.Передає значення від одного асинхронного виклику до іншого.
        //public class State
        //{
        //    //буфер для даних,що приймаються.
        //    public byte[] buffer = new byte[_bufsize];

        //    //об"єкт StringBuilder для зберігання отриманих строкових даних.
        //    //public StringBuilder sb = new StringBuilder();
        //}

        //public void ServerStart(string add,int port)
        //{
        //    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //    try
        //    {
        //        _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

        //        //Локальний об"єкт EndPoint необхідно зв"язати з об"єктом Socket.
        //        _socket.Bind(new IPEndPoint(IPAddress.Parse(add), port));
        //        Console.WriteLine("OK");
        //        Recived();
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine("Warning message : " + ex.Message.ToString());
        //    }
           
        //}

        //public void Client(string add, int port)
        //{
        //    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //    try
        //    {
        //        //Підключення до локального вузла.
        //        _socket.Connect(new IPEndPoint(IPAddress.Parse(add), port));
        //        Console.WriteLine("Connected!");
        //        Recived();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Warning message : " + ex.Message.ToString());
        //    }

        //}

        ////Відправлення повідомлення.
        //public void Send(string text)
        //{
        //    try
        //    {   //перетворюємо у масив байт введене повідомлення.
        //        byte[] data = Encoding.ASCII.GetBytes(text);
        //        //асинхронна передача даних на підключений об"єкт Socket
        //        _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
        //        {
        //        //отримуємо об"єкт стану 
        //        State so = (State)ar.AsyncState;

        //        //завершення передачі даних.
        //        int bytes = _socket.EndSend(ar);
        //            Console.WriteLine("Sending: {0}, {1}",
        //                              bytes,
        //                              text);
        //        }, state);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine("Warning message : " + ex.Message.ToString());
        //    }
        //}


        ////Отримання повідомлення.
        //private void Recived()
        //{
        //    _socket.BeginReceiveFrom(state.buffer, 0, _bufsize, SocketFlags.None, ref epFrom, recv = (ar) =>
        //    {
        //        //зворотній виклик отримання реалізує делегат AsyncCallback
        //        try
        //        {
        //            State so = (State)ar.AsyncState;
        //            int bytes = _socket.EndReceiveFrom(ar, ref epFrom);
        //            _socket.BeginReceiveFrom(so.buffer, 0, _bufsize, SocketFlags.None, ref epFrom, recv, so);
        //            Console.WriteLine("RECV: {0}: {1}, {2}",
        //                              epFrom.ToString(),
        //                              bytes,
        //                              Encoding.ASCII.GetString(so.buffer, 0, bytes));
        //        }
        //        catch(Exception ex)
        //        {
        //            Console.WriteLine("Warning message : " + ex.Message.ToString());
        //        }

        //    }, state);
        //}



    }
}
