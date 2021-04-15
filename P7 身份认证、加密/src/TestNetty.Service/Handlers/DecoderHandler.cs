using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Infrastructure.Common.DTO;
using TestNetty.Infrastructure.Common.Utils;

namespace TestNetty.Service.Handlers
{
    /// <summary>
    /// Decoder Packet
    /// </summary>
    public class DecoderHandler : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var request = new PacketRequest<string>();
            request.Checkbit = input.ReadInt();
            request.Length = input.ReadInt();
            request.CommandId = input.ReadInt();
            IByteBuffer result = Unpooled.Buffer(request.Length - 4 * 3);
            input.ReadBytes(result, request.Length - 4 * 3);
            request.Deserialize(result.Array);
            output.Add(request);
            result.Clear();
        }
    }
}
