using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;
using TestNetty.Infrastructure.Common.DTO;
using TestNetty.Infrastructure.Common.Utils;

namespace TTestNetty.Client.Handlers
{
    /// <summary>
    /// Decoder Packet
    /// </summary>
    public class DecoderHandler : ByteToMessageDecoder
    {
        protected override void Decode(IChannelHandlerContext context, IByteBuffer input, List<object> output)
        {
            var response = new PacketResponse<string>();
            response.Checkbit = input.ReadInt();
            response.Length = input.ReadInt();
            response.CommandId = input.ReadInt();
            IByteBuffer result = Unpooled.Buffer(response.Length - 4 * 3);
            input.ReadBytes(result, response.Length - 4 * 3);
            response.Deserialize(result.Array);
            output.Add(response);
            result.Clear();
        }
    }
}
