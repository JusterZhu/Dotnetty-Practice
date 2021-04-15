using DotNetty.Handlers.Timeout;
using DotNetty.Transport.Channels;
using System;

namespace TestNetty.Client.Handlers
{
    /// <summary>
    /// Heartbeat Handler Class.
    /// </summary>
    public class HeartBeatHandler : ChannelHandlerAdapter
    {
        /// <summary>
        /// Heart Beat Handler.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="evt"></param>
        public override void UserEventTriggered(IChannelHandlerContext context, object evt)
        {
            var eventState = evt as IdleStateEvent;
            if (eventState != null)
            {
                string type = string.Empty;
                if (eventState.State == IdleState.ReaderIdle)
                {
                    type = "read idle";
                }
                else if (eventState.State == IdleState.WriterIdle)
                {
                    type = "write idle";
                }
                else if (eventState.State == IdleState.AllIdle)
                {
                    type = "all idle";
                    context.CloseAsync();
                }
                Console.WriteLine($"[Server Message]:Heart Beat { type }.");
            }
            else
            {
                base.UserEventTriggered(context, evt);
            }
        }
    }
}
