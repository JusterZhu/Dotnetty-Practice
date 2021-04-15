using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using TestNetty.Client.Handlers;
using TTestNetty.Client.Handlers;

namespace TestNetty.Client.Initializers
{
    public class TcpClientInitializer : ChannelInitializer<ISocketChannel>
    {
        protected override void InitChannel(ISocketChannel channel)
        {
            IChannelPipeline pipeline = channel.Pipeline;
            pipeline.AddLast(new LoggingHandler("SRV-CONN"));
            pipeline.AddLast(new IdleStateHandler(30, 30, 60 * 5));
            pipeline.AddLast("encoder", new EncoderHandler());
            pipeline.AddLast("decoder", new DecoderHandler());
            pipeline.AddLast(new HeartBeatHandler());
            pipeline.AddLast(new ResponseHandler());
        }
    }
}
