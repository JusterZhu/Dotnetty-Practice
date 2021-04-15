using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNetty.Client.Handlers;
using TestNetty.Client.Initializers;
using TTestNetty.Client.Handlers;

namespace TestNetty.Client.Clients
{
    public class TcpClient
    {
        public async Task RunClientAsync()
        {
            var group = new MultithreadEventLoopGroup();
            try
            {
                var bootstrap = new Bootstrap();
                bootstrap
                    .Group(group)
                    .Channel<TcpSocketChannel>()
                    .Option(ChannelOption.TcpNodelay, true)
                    .Handler(new TcpClientInitializer());

                IChannel clientChannel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
                Console.WriteLine("TestNetty tcp client starting...");
                do
                {
                    var result = Console.ReadLine();
                    if (result != "exit")
                    {
                        break;
                    }
                } while (true);
                await clientChannel.CloseAsync();
            }
            finally
            {
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
            }
        }
    }
}
