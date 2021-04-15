using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MySocketServer
{
    class Program
    {
        //服务端口号
        private const int PORT = 8888;
        private const string message = "Hello world,message to client!";

        static void Main(string[] args)
        {
            Socket server = null;
            try
            {
                var ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
                //初始化socket
                server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                server.ReceiveBufferSize = 1024;
                server.SendBufferSize = 1024;
                //NoDelay:true 直接将消息发出，而不是等待发送字符累计到SendBufferSize上限时再发送
                /*
                 * NoDelay:false 将要发送的消息先缓存到发送字符池中，
                 * 等到累计到SendBufferSize（缓冲池）上限时一起发送出去.
                 * 如果一直没有到达上限，则到等待时间之后自动发出不再等待。
                */
                server.NoDelay = true;
                server.SendTimeout = 1000 * 3;
                server.ReceiveTimeout = 1000 * 3;
                //将socket绑定当前ip
                server.Bind(ip);
                //监听，监听队列最大的长度为1024
                server.Listen(1024);
                //10000 5ms
                Socket clientSocket = null;
                while (true)
                {
                    clientSocket = server.Accept();
                    clientSocket.se
                    new Thread(() =>
                    {
                        //client 交互操作处理...
                        byte[] receiveBytes = new byte[1024];
                        clientSocket.Receive(receiveBytes);
                    }).Start();
                }

                #region old

                //Socket clientSocket = null;
                //byte[] receiveBytes = new byte[1024];
                //等待客户端的连接
                //clientSocket = server.Accept();
                //接收客户端发送的消息
                //while (clientSocket.Receive(receiveBytes) > 0)
                //{
                //    //打印客户端消息，并回复
                //    string content = Encoding.UTF8.GetString(receiveBytes);
                //    Console.WriteLine($"[{ DateTime.Now }] Receive message form client : { content } .");
                //    clientSocket.Send(Encoding.UTF8.GetBytes($"[{ DateTime.Now }] { message }"));
                //}

                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //关闭服务
                if (server != null)
                {
                    Console.WriteLine("The server close.");
                    server.Close();
                }
            }
        }
    }
}
