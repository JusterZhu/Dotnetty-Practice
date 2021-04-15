using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Infrastructure.Common.DTO;

namespace TestNetty.Client.Handlers
{
    /// <summary>
    /// Encoder Packet
    /// </summary>
    public class EncoderHandler : MessageToByteEncoder<PacketRequest<string>>
    {
        protected override void Encode(IChannelHandlerContext context, PacketRequest<string> message, IByteBuffer output)
        {
            message.Body = "hello world.";
            byte[] bodyArray = message.Serialize();
            output.WriteInt(message.Checkbit);
            output.WriteInt(message.Length);
            output.WriteInt(message.CommandId);
            output.WriteBytes(bodyArray);
        }
    }
}
