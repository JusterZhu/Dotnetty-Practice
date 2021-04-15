using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Infrastructure.Common.DTO;

namespace TestNetty.Service.Handlers
{
    /// <summary>
    /// Encoder Packet
    /// </summary>
    public class EncoderHandler : MessageToByteEncoder<PacketResponse<string>>
    {
        protected override void Encode(IChannelHandlerContext context, PacketResponse<string> message, IByteBuffer output)
        {
            message.Body = "hello world.";
            byte[] bodyArray = message.Serialize();
            output.WriteInt(message.Checkbit);//0-4
            output.WriteInt(message.Length);//4-8
            output.WriteInt(message.CommandId);//4-12
            output.WriteBytes(bodyArray);
        }
    }
}
