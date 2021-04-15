using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TestNetty.Infrastructure.Common.DTO;

namespace TestNetty.Client.Handlers
{
    /// <summary>
    /// 请求处理
    /// </summary>
    public class ResponseHandler : ChannelHandlerAdapter
    {
        #region Public Methods

        /// <summary>
        /// socket read byte array.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var packet = message as PacketResponse<string>;
            if (packet == null) return;

            try
            {
                switch (packet.CommandId)
                {
                    case 1001:
                        Console.WriteLine($"Server response { packet.Body }.");
                        PacketRequest<string> request = new PacketRequest<string>();
                        request.CommandId = 1001;
                        request.Body = "Hello world.";
                        context.WriteAndFlushAsync(request);
                        break;

                    case 1002:
                        
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server request read error { ex.Message }");
            }
        }

        /// <summary>
        /// Recive Request Complete Trigger.
        /// </summary>
        /// <param name="context">频道句柄，可执行消息收、发方法等</param>
        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        /// <summary>
        /// Capture the exception and disconnect the link after outputting to the console. 
        /// Tip: The client unexpectedly disconnects the link and also triggers.
        /// </summary>
        /// <param name="context">Channel Handle.</param>
        /// <param name="exception">Exception Description.</param>
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            context.CloseAsync();
        }

        /// <summary>
        /// Channel Active(Triggered when the client connects).
        /// </summary>
        /// <param name="context">Channel Handle.</param>
        public override void ChannelActive(IChannelHandlerContext context)
        {
            Console.WriteLine(string.Format("Connection Time：{0},{1}Connected.", DateTime.Now, context.Name));
            PacketRequest<string> request = new PacketRequest<string>();
            request.CommandId = 1001;
            request.Body = "Hello world.";
            context.WriteAndFlushAsync(request);
        }

        /// <summary>
        /// Channel inactive function callback.
        /// </summary>
        /// <param name="context"></param>
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            context.CloseAsync();
            base.ChannelInactive(context);
        }

        #endregion
    }
}
