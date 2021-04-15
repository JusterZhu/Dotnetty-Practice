using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MySocketClient
{
    class Program
    {
        //服务提供的端口号
        private const int PORT = 8888;
        private const string message = "Hello world,message to server!";

        static void Main(string[] args)
        {
            Socket client = null;
            try
            {
                //初始化socket client
                client = new Socket(SocketType.Stream, ProtocolType.Tcp);
                client.ReceiveBufferSize = 1024;
                client.SendBufferSize = 1024;
                client.NoDelay = true;
                client.SendTimeout = 1000 * 3;
                client.ReceiveTimeout = 1000 * 3;
                //连接服务端
                client.Connect(IPAddress.Parse("127.0.0.1"), PORT);
                //向服务端发送消息
                byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                client.Send(sendBytes);
                byte[] receiveBytes = new byte[1024];
                while (client.Receive(receiveBytes) > 0)
                {
                    string content = Encoding.UTF8.GetString(receiveBytes);
                    Console.WriteLine($"[{ DateTime.Now }] Receive message form server : { content } .");
                    client.Send(sendBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            {
                //关闭客户端
                if (client != null)
                {
                    Console.WriteLine("The client close.");
                    client.Close();
                }
            }
            Console.Read();
        }
    }
}
