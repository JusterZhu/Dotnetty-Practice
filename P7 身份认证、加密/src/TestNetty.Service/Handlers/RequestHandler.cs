using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TestNetty.Infrastructure.Common.DTO;

namespace TestNetty.Service.Handlers
{
    /// <summary>
    /// 请求处理
    /// </summary>
    public class RequestHandler : ChannelHandlerAdapter
    {
        #region Public Methods

        /// <summary>
        /// socket read byte array.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            //IByteBuffer
            var packet = message as PacketRequest<string>;
            if (packet == null) return;

            try
            {
                switch (packet.CommandId)
                {
                    case 1001:
                        Console.WriteLine($"Client request { packet.CommandId } business logic.");
                        var response = new PacketResponse<string>();
                        response.CommandId = 1001;
                        response.Body = "Recall Hello world.";
                        context.WriteAndFlushAsync(response);//send 
                        break;

                    case 10002:
                        
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
