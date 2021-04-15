using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TestNetty.Infrastructure.Common.DB;
using TestNetty.Infrastructure.Common.DTO;
using TestNetty.Infrastructure.Common.Utils;

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
            var packet = message as PacketRequest<IMessage>;
            if (packet == null) return;

            PacketResponse<IMessage> packetResponse = null;
            try
            {
                switch (packet.CommandId)
                {
                    //获取雇员信息
                    case 1001:
                        //解析
                        var requestDTO = packet.Body as TestRequestDTO;

                        //回复
                        var response = new PacketResponse<TestResponseDTO>();
                        response.CommandId = 1001;
                        var errorResponse = new TestResponseDTO { Code = 400, Message = "Illegal message parsing failed!!!" }; 

                        if (requestDTO == null) 
                        {
                            response.Body = errorResponse;
                        } 
                        else 
                        {
                            var hashVal = requestDTO.HashValue;
                            //var uid = requestDTO.Account;
                            //var pwd = requestDTO.Passworld;
                            var nonce = requestDTO.Nonce;
                            var sign = requestDTO.Sign;
                            var time = requestDTO.ValidateTime;
                            var isVerification = EncryptionUtil.Verification(hashVal,nonce,sign,time);
                            if (isVerification)
                            {
                                response.Body = new TestResponseDTO { Code = 200, Employees = new List<string> { "zhang san", "li si", "wang wu", "zhao liu" } };
                            }
                            else 
                            {
                                response.Body = errorResponse;
                            }
                            if (!isVerification)
                            {
                                context.WriteAndFlushAsync(packetResponse);
                                context.CloseAsync();
                            }
                        }
                        context.WriteAndFlushAsync(packetResponse);
                        
                       
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
