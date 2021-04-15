using DotNetty.Handlers.Logging;
using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Service.Handlers;

namespace TestNetty.Service.Initializers
{
    public class TcpServerInitializer : ChannelInitializer<ISocketChannel>
    {
        protected override void InitChannel(ISocketChannel channel)
        {
            IChannelPipeline pipeline = channel.Pipeline;
            pipeline.AddLast(new LoggingHandler("SRV-CONN"));
            pipeline.AddLast(new IdleStateHandler(30,30,60 * 5));//心跳
            pipeline.AddLast("encoder", new EncoderHandler());
            pipeline.AddLast("decoder", new DecoderHandler());
            pipeline.AddLast(new HeartBeatHandler());
            pipeline.AddLast(new RequestHandler());//请求消息处理类
        }
    }
}
