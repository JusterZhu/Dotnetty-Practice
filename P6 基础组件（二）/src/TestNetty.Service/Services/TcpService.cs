using DotNetty.Buffers;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Threading.Tasks;
using TestNetty.Infrastructure.Common.Utils;
using TestNetty.Service.Initializers;

namespace TestNetty.Service.Services
{
    public class TcpService 
    {
        private readonly IEventLoopGroup bossGroup = new MultithreadEventLoopGroup(4);
        private readonly IEventLoopGroup workerGroup = new MultithreadEventLoopGroup();

        public async Task RunServerAsync()
        {
            //SocketUtil.SetConsoleLogger();
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workerGroup);
                bootstrap.Channel<TcpServerSocketChannel>();
                bootstrap
                    .Option(ChannelOption.SoBacklog, 1024)
                    //ByteBuf的分配器(重用缓冲区)大小
                    .Option(ChannelOption.Allocator, UnpooledByteBufferAllocator.Default)
                    .Option(ChannelOption.RcvbufAllocator, new FixedRecvByteBufAllocator(1024 * 8))
                    .ChildOption(ChannelOption.SoKeepalive, true)//保持长连接
                    .ChildOption(ChannelOption.TcpNodelay, true)//端口复用
                    .ChildOption(ChannelOption.SoReuseport, true)
                    .Handler(new LoggingHandler("SRV-LSTN"))
                    //自定义初始化Tcp服务
                    .ChildHandler(new TcpServerInitializer());

                await bootstrap.BindAsync(8888);
                Console.WriteLine("TestNetty tcp server starting...");
                do
                {
                    var result = Console.ReadLine();
                    if (result != "exit")
                    {
                        break;
                    }
                } while (true);
            }
            finally
            {
                await Task.WhenAll(
                    bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                    workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }
    }
}
